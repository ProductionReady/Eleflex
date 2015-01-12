#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Threading;
using Common.Logging;
using Common.Logging.Factory;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Logging
{
    /// <summary>
    /// Common Logging Adapter that utilizes the logging repository for creating log messages providing asynchronous capability. 
    /// This class instead utilizes a background thread. 
    /// </summary>
    public class CommonLoggingAsyncFactoryAdapter : ILoggerFactoryAdapter, IDisposable
    {
        /// <summary>
        /// Application name.
        /// </summary>
        protected string _application;
        /// <summary>
        /// Server name.
        /// </summary>
        protected string _server;
        /// <summary>
        /// Properties used to configure service.
        /// </summary>
        protected NameValueCollection _properties;

         /// <summary>
        /// Run interval. Provides enough delay not to spike the processor while waiting for messages.
        /// </summary>
        public const int Default_RunIntervalMilliseconds = 1000;

        /// <summary>
        /// Stop timeout. Should give ample time to underlying provider to flush queue.
        /// </summary>
        public const int Default_StopTimeoutMilliseconds = 6000;


        /// <summary>
        /// Admin thread for processing messages.
        /// </summary>
        protected static Thread _adminThread;

        /// <summary>
        /// Graceful stop.
        /// </summary>
        protected static bool _isRunning;

        /// <summary>
        /// Lock admin.
        /// </summary>
        protected static readonly object _lockAdmin = new object();

        /// <summary>
        /// Queue to store messages.
        /// </summary>
        protected static Queue<Eleflex.Logging.Log> _queue = new Queue<Eleflex.Logging.Log>();

        /// <summary>
        /// Interval which admin thread checks queue for available messages.
        /// </summary>
        protected static int _runIntervalMilliseconds;

        /// <summary>
        /// Instance.
        /// </summary>
        protected static CommonLoggingAsyncFactoryAdapter Instance = null;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="properties"></param>
        public CommonLoggingAsyncFactoryAdapter(NameValueCollection properties)
        {
            _properties = properties ?? new NameValueCollection();
            _application = _properties.Get("Application");
            if (string.IsNullOrEmpty(_application))
            {
                _application = Assembly.GetCallingAssembly().FullName;
                _properties.Add("Application", _application);
            }
            _server = _properties.Get("Server");
            if (string.IsNullOrEmpty(_server))
            {
                _server = System.Net.Dns.GetHostName();
                _properties.Add("Server", _server);
            }
            Instance = this;
            this.Start();
        }

        /// <summary>
        /// Get the logger with the given source.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILog GetLogger(string name)
        {
            return new CommonLoggingAsyncFactoryLog(_properties, name);
        }

        /// <summary>
        /// Get the loffer with the given source.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILog GetLogger(Type type)
        {
            return new CommonLoggingAsyncFactoryLog(_properties, type == null ? null : type.ToString());
        }


        /// <summary>
        /// Disposal.
        /// </summary>
        public virtual void Dispose()
        {
            Stop();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Start.
        /// </summary>
        public virtual void Start()
        {
            Start(Default_RunIntervalMilliseconds);
        }

        /// <summary>
        /// Start.
        /// </summary>
        /// <param name="runIntervalMilliseconds"></param>
        public virtual void Start(int runIntervalMilliseconds)
        {
            //Lock
            lock (_lockAdmin)
            {
                if (_isRunning)
                    return;

                //Set run properties
                if (runIntervalMilliseconds < 0)
                    _runIntervalMilliseconds = Default_RunIntervalMilliseconds;
                else
                    _runIntervalMilliseconds = runIntervalMilliseconds;

                //Start admin thread
                _isRunning = true;
                _adminThread = new Thread(new ThreadStart(AdminThreadProcess));
                _adminThread.IsBackground = true;
                _adminThread.Start();
            }
        }

        /// <summary>
        /// Stop.
        /// </summary>
        public void Stop()
        {
            Stop(Default_StopTimeoutMilliseconds);
        }

        /// <summary>
        /// Stop.
        /// </summary>
        /// <param name="timeoutMilliseconds"></param>
        public void Stop(int timeoutMilliseconds)
        {
            //Lock resource
            lock (_lockAdmin)
            {
                if (!_isRunning)
                    return;

                //Try graceful stop
                _isRunning = false;
            }

            //Timout to kill process
            DateTime timeout = DateTime.Now.AddMilliseconds(timeoutMilliseconds);
            while (DateTime.Now < timeout)
            {
                if (_adminThread == null || !_adminThread.IsAlive)
                    break;

                Thread.Sleep(100);
            }

            //Abort for timeout
            if (_adminThread != null && _adminThread.IsAlive)
                _adminThread.Abort();
            _adminThread = null;
        }

        /// <summary>
        /// Admin thread entry point.
        /// </summary>
        protected void AdminThreadProcess()
        {
            try
            {
                bool running = true;
                while (running)
                {
                    //Check running flag, allow process one more time if stop requested
                    lock (_lockAdmin)
                    {
                        if (!_isRunning)
                            running = false;
                    }

                    //Get queue to process
                    Queue<Eleflex.Logging.Log> tempQueue = null;
                    lock (_lockAdmin)
                    {
                        if (_queue.Count > 0)
                        {
                            tempQueue = _queue;
                            _queue = new Queue<Eleflex.Logging.Log>();
                        }
                    }

                    //Process pending queued messages
                    while (tempQueue != null && tempQueue.Count > 0)
                    {
                        Eleflex.Logging.Log tempMessage = tempQueue.Peek();
                        bool success = ProcessMessage(tempMessage);
                        if (success)
                        {
                            tempQueue.Dequeue();
                        }
                        else
                        {
                            //If failure, add new messages to end of any non-processed messages and exit
                            lock (_lockAdmin)
                            {
                                while (_queue.Count > 0)
                                    tempQueue.Enqueue(_queue.Dequeue());

                                _queue = tempQueue;
                                tempQueue = null;
                            }
                        }
                    }

                    //Pause for defined run interval
                    if (running)
                        Thread.Sleep(_runIntervalMilliseconds);
                }
            }
            catch { }
        }

        /// <summary>
        /// Add message to the queue.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual void AddMessageToQueue(Eleflex.Logging.Log message)
        {
            lock (_lockAdmin)
            {
                _queue.Enqueue(message);
            }
        }

        /// <summary>
        /// Log a message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void Log(Eleflex.Logging.Log message)
        {
            Instance.AddMessageToQueue(message);
        }


        /// <summary>
        /// Process the log message. (Store it somewhere)
        /// </summary>
        /// <param name="message"></param>
        public virtual bool ProcessMessage(Eleflex.Logging.Log message)
        {
            //We want to process logging on a different thread from the current request thread as to not slow down processing for persisting log messages.
            //Additionally messages created on the context of the request thread would be rolledback should the unit of work be rolledback/disposed.
            try
            {
                ILogRepository logRepository = ServiceLocator.Current.GetInstance<ILogRepository>();
                logRepository.Insert(message);
                ServiceLocator.Current.GetInstance<Eleflex.IUnitOfWork>().Commit();
                return true;
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<Eleflex.IUnitOfWork>().Rollback();
                Common.Logging.LogManager.GetLogger("CommonLoggingAsyncFactoryAdapter:ProcessMessage").Error(ex);
                return false;
            }
            //Don't call dispose on the unit of work as this is a long running thread. Calling Commit or Rollback on the unit of work 
            //will cause it to clear storageproviders and reset internally, so next request will create new sessions if needed.
        }

    }
}

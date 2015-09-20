﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using Common.Logging;

namespace Eleflex.Logging.CommonLogging
{
    /// <summary>
    /// Common Logging Adapter that utilizes the logging repository for creating log messages providing asynchronous capability. 
    /// This class instead utilizes a background thread. 
    /// </summary>
    public abstract partial class CommonLoggingFactoryAdapter : ILoggerFactoryAdapter, IDisposable
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
        /// Stop timeout. Should give ample time to underlying storage service to flush queue.
        /// </summary>
        public const int Default_StopTimeoutMilliseconds = 5000;


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
        protected static Queue<Eleflex.LogMessage> _queue = new Queue<Eleflex.LogMessage>();

        /// <summary>
        /// Interval which admin thread checks queue for available messages.
        /// </summary>
        protected static int _runIntervalMilliseconds;

        /// <summary>
        /// Instance.
        /// </summary>
        protected static CommonLoggingFactoryAdapter Instance = null;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="properties"></param>
        public CommonLoggingFactoryAdapter(NameValueCollection properties)
        {
            _properties = properties ?? new NameValueCollection();
            _application = _properties.Get("Application");
            if (string.IsNullOrEmpty(_application))
            {
                _application = "Eleflex Application";
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
        public virtual ILog GetLogger(string name)
        {
            return new CommonLoggingFactoryLog(_properties, name);
        }

        /// <summary>
        /// Get the loffer with the given source.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual ILog GetLogger(Type type)
        {
            return new CommonLoggingFactoryLog(_properties, type == null ? null : type.ToString());
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
        public virtual void Stop()
        {
            Stop(Default_StopTimeoutMilliseconds);
        }

        /// <summary>
        /// Stop.
        /// </summary>
        /// <param name="timeoutMilliseconds"></param>
        public virtual void Stop(int timeoutMilliseconds)
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
                    Queue<Eleflex.LogMessage> tempQueue = null;
                    lock (_lockAdmin)
                    {
                        if (_queue.Count > 0)
                        {
                            tempQueue = _queue;
                            _queue = new Queue<Eleflex.LogMessage>();
                        }
                    }

                    //Process pending queued messages
                    while (tempQueue != null && tempQueue.Count > 0)
                    {
                        Eleflex.LogMessage tempMessage = tempQueue.Peek();
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
        protected virtual void AddMessageToQueue(Eleflex.LogMessage message)
        {
            lock (_lockAdmin)
            {
                _queue.Enqueue(message);
            }
        }


        /// <summary>
        /// Process the log message. (Store it somewhere)
        /// </summary>
        /// <param name="message"></param>
        public abstract bool ProcessMessage(Eleflex.LogMessage message);


        /// <summary>
        /// Log a message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static void Log(Eleflex.LogMessage message)
        {
            Instance.AddMessageToQueue(message);
        }

    }
}

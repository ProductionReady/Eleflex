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
using Common.Logging;
using Common.Logging.Factory;
using Microsoft.Practices.ServiceLocation;
using Eleflex.Logging;

namespace Eleflex.Logging
{
    /// <summary>
    /// Common Logging logger utilizing the logging repository to save log message.
    /// </summary>
    public class CommonLoggingAsyncFactoryLog : AbstractLogger
    {
        /// <summary>
        /// Application name.
        /// </summary>
        private string _application;
        /// <summary>
        /// Server name.
        /// </summary>
        private string _server;
        /// <summary>
        /// Source name.
        /// </summary>
        private string _source;
        /// <summary>
        /// Debug enabled.
        /// </summary>
        private bool _debug = false;
        /// <summary>
        /// Error enabled.
        /// </summary>
        private bool _error = true;
        /// <summary>
        /// Fatal enabled.
        /// </summary>
        private bool _fatal = true;
        /// <summary>
        /// Info enabled.
        /// </summary>
        private bool _info = true;
        /// <summary>
        /// Trace enabled.
        /// </summary>
        private bool _trace = false;
        /// <summary>
        /// Warn enabled.
        /// </summary>
        private bool _warn = true;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="properties"></param>
        public CommonLoggingAsyncFactoryLog(NameValueCollection properties)
            : this(properties, null)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="source"></param>
        public CommonLoggingAsyncFactoryLog(NameValueCollection properties, string source)
            : base()
        {
            _application = properties.Get("Application");
            if (string.IsNullOrEmpty(_application))
                _application = Assembly.GetCallingAssembly().FullName;
            _server = properties.Get("Server");
            if (string.IsNullOrEmpty(_server))
                _server = System.Net.Dns.GetHostName();
            _source = source;
            
            string debug = properties.Get("Debug");
            if (!string.IsNullOrEmpty(debug))
                _debug = bool.Parse(debug);
            string error = properties.Get("Error");
            if (!string.IsNullOrEmpty(error))
                _error = bool.Parse(error);
            string fatal = properties.Get("Fatal");
            if (!string.IsNullOrEmpty(fatal))
                _fatal = bool.Parse(fatal);
            string info = properties.Get("Info");
            if (!string.IsNullOrEmpty(info))
                _info = bool.Parse(info);
            string trace = properties.Get("Trace");
            if (!string.IsNullOrEmpty(trace))
                _trace = bool.Parse(trace);
            string warn = properties.Get("Warn");
            if (!string.IsNullOrEmpty(warn))
                _warn = bool.Parse(warn);
        }

        /// <summary>
        /// Debug enabled.
        /// </summary>
        public override bool IsDebugEnabled
        {
            get { return _debug; }
        }

        /// <summary>
        /// Error enabled.
        /// </summary>
        public override bool IsErrorEnabled
        {
            get { return _error; }
        }

        /// <summary>
        /// Fatal enabled.
        /// </summary>
        public override bool IsFatalEnabled
        {
            get { return _fatal; }
        }

        /// <summary>
        /// Info enabled.
        /// </summary>
        public override bool IsInfoEnabled
        {
            get { return _info; }
        }

        /// <summary>
        /// Trace enabled.
        /// </summary>
        public override bool IsTraceEnabled
        {
            get { return _trace; }
        }

        /// <summary>
        /// Warn enabled.
        /// </summary>
        public override bool IsWarnEnabled
        {
            get { return _warn; }
        }

        /// <summary>
        /// Write the log message.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            Eleflex.Logging.Log item = new Eleflex.Logging.Log();
            item.Application = _application;
            item.CreateDate = DateTimeOffset.UtcNow;
            if (level == LogLevel.Error || level == LogLevel.Fatal)
                item.IsError = true;
            else
                item.IsError = false;
            item.Source = _source;
            if (message is System.Runtime.InteropServices._Exception) //sometimes compiler uses wrong resolution method
            {
                item.Message = ((System.Runtime.InteropServices._Exception)message).Message;
                if (exception == null)
                    item.Exception = ((System.Runtime.InteropServices._Exception)message).ToString();
                else
                    item.Exception = exception.ToString();
            }
            else
            {
                item.Message = message == null ? null : message.ToString();
                item.Exception = exception == null ? null : exception.ToString();
            }
            item.Server = _server;
            item.Severity = level.ToString();            

            //Store the message on the queue to be processed on a background thread later
            CommonLoggingAsyncFactoryAdapter.Log(item);           
        }
    }
}

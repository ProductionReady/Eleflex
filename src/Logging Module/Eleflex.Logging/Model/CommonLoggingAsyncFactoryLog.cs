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
        }

        /// <summary>
        /// Debug enabled.
        /// </summary>
        public override bool IsDebugEnabled
        {
            get { return false; }
        }

        /// <summary>
        /// Error enabled.
        /// </summary>
        public override bool IsErrorEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// Fatal enabled.
        /// </summary>
        public override bool IsFatalEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// Info enabled.
        /// </summary>
        public override bool IsInfoEnabled
        {
            get { return true; }
        }

        /// <summary>
        /// Trace enabled.
        /// </summary>
        public override bool IsTraceEnabled
        {
            get { return false; }
        }

        /// <summary>
        /// Warn enabled.
        /// </summary>
        public override bool IsWarnEnabled
        {
            get { return true; }
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
            item.Message = message == null ? null : message.ToString();
            item.Exception = exception == null ? null : exception.ToString();
            item.Server = _server;
            item.Severity = level.ToString();            

            //Store the message on the queue to be processed on a background thread later
            CommonLoggingAsyncFactoryAdapter.Log(item);           
        }
    }
}

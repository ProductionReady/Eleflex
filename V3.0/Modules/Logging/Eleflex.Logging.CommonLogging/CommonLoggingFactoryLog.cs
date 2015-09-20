using System;
using System.Collections.Specialized;
using System.Reflection;
using Common.Logging;
using Common.Logging.Factory;

namespace Eleflex.Logging.CommonLogging
{
    /// <summary>
    /// Common Logging logger utilizing the logging repository to save log message.
    /// </summary>
    public partial class CommonLoggingFactoryLog : AbstractLogger
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
        /// Source name.
        /// </summary>
        protected string _source;
        /// <summary>
        /// Debug enabled.
        /// </summary>
        protected bool _debug = false;
        /// <summary>
        /// Error enabled.
        /// </summary>
        protected bool _error = true;
        /// <summary>
        /// Fatal enabled.
        /// </summary>
        protected bool _fatal = true;
        /// <summary>
        /// Info enabled.
        /// </summary>
        protected bool _info = true;
        /// <summary>
        /// Trace enabled.
        /// </summary>
        protected bool _trace = false;
        /// <summary>
        /// Warn enabled.
        /// </summary>
        protected bool _warn = true;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="properties"></param>
        public CommonLoggingFactoryLog(NameValueCollection properties)
            : this(properties, null)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="source"></param>
        public CommonLoggingFactoryLog(NameValueCollection properties, string source)
            : base()
        {
            _application = properties.Get(LoggingConstants.CONFIGPARAM_APPLICATION);
            if (string.IsNullOrEmpty(_application))
                _application = Assembly.GetCallingAssembly().FullName;
            _server = properties.Get(LoggingConstants.CONFIGPARAM_SERVER);
            if (string.IsNullOrEmpty(_server))
                _server = System.Net.Dns.GetHostName();
            _source = source;
            
            string debug = properties.Get(LoggingConstants.CONFIGPARAM_DEBUG);
            if (!string.IsNullOrEmpty(debug))
                _debug = bool.Parse(debug);
            string error = properties.Get(LoggingConstants.CONFIGPARAM_ERROR);
            if (!string.IsNullOrEmpty(error))
                _error = bool.Parse(error);
            string fatal = properties.Get(LoggingConstants.CONFIGPARAM_FATAL);
            if (!string.IsNullOrEmpty(fatal))
                _fatal = bool.Parse(fatal);
            string info = properties.Get(LoggingConstants.CONFIGPARAM_INFO);
            if (!string.IsNullOrEmpty(info))
                _info = bool.Parse(info);
            string trace = properties.Get(LoggingConstants.CONFIGPARAM_TRACE);
            if (!string.IsNullOrEmpty(trace))
                _trace = bool.Parse(trace);
            string warn = properties.Get(LoggingConstants.CONFIGPARAM_WARN);
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
            Eleflex.LogMessage item = new Eleflex.LogMessage();
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
            CommonLoggingFactoryAdapter.Log(item);           
        }
    }
}

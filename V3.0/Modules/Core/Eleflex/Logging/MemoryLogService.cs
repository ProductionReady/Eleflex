using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a LogService object that writes messages to an internal list.
    /// </summary>
    public partial class MemoryLogService : ILogService
    {

        /// <summary>
        /// The list of log messages stored.
        /// </summary>
        public List<LogMessage> List = new List<LogMessage>();

        /// <summary>
        /// Log a message.
        /// </summary>
        /// <param name="message"></param>
        public virtual void Log(LogMessage message)
        {
            List.Add(message);            
        }

        /// <summary>
        /// Debug.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Debug(string source, string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_DEBUG, Source = source, Message = message });
        }
        /// <summary>
        /// Debug.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Debug(string source, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_DEBUG, Source = source, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Debug.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Debug(string source, string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_DEBUG, Source = source, Message = message, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Debug.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Debug<T>(Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_DEBUG, Source = typeof(T).FullName, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Debug.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Debug<T>(string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_DEBUG, Source = typeof(T).FullName, Message = message });
        }
        /// <summary>
        /// Debug.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Debug<T>(string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_DEBUG, Source = typeof(T).FullName, Message = message, Exception = ex != null ? ex.ToString() : null });
        }


        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Error(string source, string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_ERROR, Source = source, Message = message });
        }
        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Error(string source, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_ERROR, Source = source, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Error(string source, string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_ERROR, Source = source, Message = message, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Error<T>(Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_ERROR, Source = typeof(T).FullName, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Error<T>(string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_ERROR, Source = typeof(T).FullName, Message = message });
        }
        /// <summary>
        /// Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Error<T>(string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_ERROR, Source = typeof(T).FullName, Message = message, Exception = ex != null ? ex.ToString() : null });
        }


        /// <summary>
        /// Fatal.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Fatal(string source, string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_FATAL, Source = source, Message = message });
        }
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Fatal(string source, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_FATAL, Source = source, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Fatal(string source, string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_FATAL, Source = source, Message = message, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Fatal<T>(Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_FATAL, Source = typeof(T).FullName, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Fatal<T>(string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_FATAL, Source = typeof(T).FullName, Message = message });
        }
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Fatal<T>(string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_FATAL, Source = typeof(T).FullName, Message = message, Exception = ex != null ? ex.ToString() : null });
        }


        /// <summary>
        /// Info.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Info(string source, string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_INFO, Source = source, Message = message });
        }
        /// <summary>
        /// Info.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Info(string source, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_INFO, Source = source, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Info.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Info(string source, string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_INFO, Source = source, Message = message, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Info<T>(Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_INFO, Source = typeof(T).FullName, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Info<T>(string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_INFO, Source = typeof(T).FullName, Message = message });
        }
        /// <summary>
        /// Info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Info<T>(string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_INFO, Source = typeof(T).FullName, Message = message, Exception = ex != null ? ex.ToString() : null });
        }

        /// <summary>
        /// Warning.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Warn(string source, string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_WARN, Source = source, Message = message });
        }
        /// <summary>
        /// Warning.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Warn(string source, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_WARN, Source = source, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Warning.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Warn(string source, string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_WARN, Source = source, Message = message, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Warning.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Warn<T>(Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_WARN, Source = typeof(T).FullName, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Warning.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Warn<T>(string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_WARN, Source = typeof(T).FullName, Message = message });
        }
        /// <summary>
        /// Warning.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Warn<T>(string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_WARN, Source = typeof(T).FullName, Message = message, Exception = ex != null ? ex.ToString() : null });
        }

        /// <summary>
        /// Trace.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Trace(string source, string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_TRACE, Source = source, Message = message });
        }
        /// <summary>
        /// Trace.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Trace(string source, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_TRACE, Source = source, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Trace.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Trace(string source, string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_TRACE, Source = source, Message = message, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Trace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Trace<T>(Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_TRACE, Source = typeof(T).FullName, Exception = ex != null ? ex.ToString() : null });
        }
        /// <summary>
        /// Trace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Trace<T>(string message)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_TRACE, Source = typeof(T).FullName, Message = message });
        }
        /// <summary>
        /// Trace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Trace<T>(string message, Exception ex)
        {
            List.Add(new LogMessage() { Severity = LoggingConstants.SEVERITY_TRACE, Source = typeof(T).FullName, Message = message, Exception = ex != null ? ex.ToString() : null });
        }

    }
}

using System;

namespace Eleflex.Logging.CommonLogging
{
    /// <summary>
    /// Represents a LogProvider object that writes messages to system diagnostics debug.
    /// </summary>
    public partial class CommonLoggingService : ILogService
    {

        /// <summary>
        /// Log a message.
        /// </summary>
        /// <param name="message"></param>
        public virtual void Log(LogMessage message)
        {
            switch(message.Severity)
            {
                default:
                case LoggingConstants.SEVERITY_INFO:
                    Common.Logging.LogManager.GetLogger(message.Source).Info(message.Message, message.Exception != null ? new Exception(message.Exception) : null);
                    break;
                case LoggingConstants.SEVERITY_DEBUG:
                    Common.Logging.LogManager.GetLogger(message.Source).Debug(message.Message, message.Exception != null ? new Exception(message.Exception) : null);
                    break;
                case LoggingConstants.SEVERITY_ERROR:
                    Common.Logging.LogManager.GetLogger(message.Source).Error(message.Message, message.Exception != null ? new Exception(message.Exception) : null);
                    break;
                case LoggingConstants.SEVERITY_FATAL:
                    Common.Logging.LogManager.GetLogger(message.Source).Fatal(message.Message, message.Exception != null ? new Exception(message.Exception) : null);
                    break;
                case LoggingConstants.SEVERITY_WARN:
                    Common.Logging.LogManager.GetLogger(message.Source).Warn(message.Message, message.Exception != null ? new Exception(message.Exception) : null);
                    break;
                case LoggingConstants.SEVERITY_TRACE:
                    Common.Logging.LogManager.GetLogger(message.Source).Trace(message.Message, message.Exception != null ? new Exception(message.Exception) : null);
                    break;
            }            
        }

        /// <summary>
        /// Debug.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Debug(string source, string message)
        {
            Common.Logging.LogManager.GetLogger(source).Debug(message);
        }
        /// <summary>
        /// Debug.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Debug(string source, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Debug(ex);
        }
        /// <summary>
        /// Debug.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Debug(string source, string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Debug(message, ex);
        }
        /// <summary>
        /// Debug.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Debug<T>(Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Debug(ex);
        }
        /// <summary>
        /// Debug.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Debug<T>(string message)
        {
            Common.Logging.LogManager.GetLogger<T>().Debug(message);
        }
        /// <summary>
        /// Debug.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Debug<T>(string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Debug(message, ex);
        }

        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Error(string source, string message)
        {
            Common.Logging.LogManager.GetLogger(source).Error(message);
        }
        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Error(string source, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Error(ex);
        }
        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Error(string source, string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Error(message, ex);
        }
        /// <summary>
        /// Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Error<T>(string message)
        {
            Common.Logging.LogManager.GetLogger<T>().Error(message);
        }
        /// <summary>
        /// Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Error<T>(Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Error(ex);
        }
        /// <summary>
        /// Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Error<T>(string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Error(message, ex);
        }

        /// <summary>
        /// Fatal.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Fatal(string source, string message)
        {
            Common.Logging.LogManager.GetLogger(source).Fatal(message);
        }
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Fatal(string source, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Fatal(ex);
        }
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Fatal(string source, string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Fatal(message, ex);
        }
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Fatal<T>(string message)
        {
            Common.Logging.LogManager.GetLogger<T>().Fatal(message);
        }
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Fatal<T>(Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Fatal(ex);
        }
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Fatal<T>(string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Fatal(message, ex);
        }

        /// <summary>
        /// Info.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Info(string source, string message)
        {
            Common.Logging.LogManager.GetLogger(source).Info(message);
        }
        /// <summary>
        /// Info.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Info(string source, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Info(ex);
        }
        /// <summary>
        /// Info.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Info(string source, string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Info(message, ex);
        }
        /// <summary>
        /// Info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Info<T>(string message)
        {
            Common.Logging.LogManager.GetLogger<T>().Info(message);
        }
        /// <summary>
        /// Info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Info<T>(Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Info(ex);
        }
        /// <summary>
        /// Info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Info<T>(string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Info(message, ex);
        }

        /// <summary>
        /// Warn.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Warn(string source, string message)
        {
            Common.Logging.LogManager.GetLogger(source).Warn(message);
        }
        /// <summary>
        /// Warn.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Warn(string source, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Warn(ex);
        }
        /// <summary>
        /// Warn.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Warn(string source, string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Warn(message, ex);
        }
        /// <summary>
        /// Warn.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Warn<T>(string message)
        {
            Common.Logging.LogManager.GetLogger<T>().Warn(message);
        }
        /// <summary>
        /// Warn.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Warn<T>(Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Warn(ex);
        }
        /// <summary>
        /// Warn.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Warn<T>(string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Warn(message, ex);
        }

        /// <summary>
        /// Trace.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        public virtual void Trace(string source, string message)
        {
            Common.Logging.LogManager.GetLogger(source).Trace(message);
        }
        /// <summary>
        /// Trace.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public virtual void Trace(string source, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Trace(ex);
        }
        /// <summary>
        /// Trace.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Trace(string source, string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger(source).Trace(message, ex);
        }
        /// <summary>
        /// Trace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        public virtual void Trace<T>(string message)
        {
            Common.Logging.LogManager.GetLogger<T>().Trace(message);
        }
        /// <summary>
        /// Trace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        public virtual void Trace<T>(Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Trace(ex);
        }
        /// <summary>
        /// Trace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public virtual void Trace<T>(string message, Exception ex)
        {
            Common.Logging.LogManager.GetLogger<T>().Trace(message, ex);
        }
    }
}

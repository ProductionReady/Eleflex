using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a service used to log messages in the application.
    /// </summary>
    public partial interface ILogService
    {

        /// <summary>
        /// Log a message.
        /// </summary>
        /// <param name="message"></param>
        void Log(LogMessage message);

        /// <summary>
        /// Debug.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        void Debug(string source, Exception ex);
        /// <summary>
        /// Debug.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        void Debug(string source, string message);
        /// <summary>
        /// Debug.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Debug(string source, string message, Exception ex);
        /// <summary>
        /// Debug.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        void Debug<T>(Exception ex);
        /// <summary>
        /// Debug.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void Debug<T>(string message);
        /// <summary>
        /// Debug.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Debug<T>(string message, Exception ex);


        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        void Error(string source, Exception ex);
        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        void Error(string source, string message);
        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Error(string source, string message, Exception ex);
        /// <summary>
        /// Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        void Error<T>(Exception ex);
        /// <summary>
        /// Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void Error<T>(string message);
        /// <summary>
        /// Error.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Error<T>(string message, Exception ex);

        /// <summary>
        /// Fatal.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        void Fatal(string source, Exception ex);
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        void Fatal(string source, string message);
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Fatal(string source, string message, Exception ex);
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        void Fatal<T>(Exception ex);
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void Fatal<T>(string message);
        /// <summary>
        /// Fatal.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Fatal<T>(string message, Exception ex);

        /// <summary>
        /// Info.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        void Info(string source, Exception ex);
        /// <summary>
        /// Info.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        void Info(string source, string message);
        /// <summary>
        /// Info.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Info(string source, string message, Exception ex);
        /// <summary>
        /// Info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        void Info<T>(Exception ex);
        /// <summary>
        /// Info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void Info<T>(string message);
        /// <summary>
        /// Info.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Info<T>(string message, Exception ex);

        /// <summary>
        /// Warning.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        void Warn(string source, Exception ex);
        /// <summary>
        /// Warning.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        void Warn(string source, string message);
        /// <summary>
        /// Warning.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Warn(string source, string message, Exception ex);
        /// <summary>
        /// Warning.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        void Warn<T>(Exception ex);
        /// <summary>
        /// Warning.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void Warn<T>(string message);
        /// <summary>
        /// Warning.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Warn<T>(string message, Exception ex);


        /// <summary>
        /// Trace.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        void Trace(string source, Exception ex);
        /// <summary>
        /// Trace.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        void Trace(string source, string message);
        /// <summary>
        /// Trace.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Trace(string source, string message, Exception ex);
        /// <summary>
        /// Trace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ex"></param>
        void Trace<T>(Exception ex);
        /// <summary>
        /// Trace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void Trace<T>(string message);
        /// <summary>
        /// Trace.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        void Trace<T>(string message, Exception ex);


    }
}

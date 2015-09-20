using Eleflex.Services.WCF;

namespace Eleflex.Logging.Services.WCF.Message
{
    /// <summary>
    /// Represents an object for sending request for all service command for the Logging module.
    /// </summary>
    public class LoggingRequestDispatcher : WCFCommandRequestDispatcher, ILoggingRequestDispatcher
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LoggingRequestDispatcher()
            : base(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpoint"></param>
        public LoggingRequestDispatcher(string endpoint)
            : base(endpoint)
        { }

    }
}


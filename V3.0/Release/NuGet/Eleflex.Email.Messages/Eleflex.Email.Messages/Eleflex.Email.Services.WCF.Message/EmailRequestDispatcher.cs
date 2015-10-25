using Eleflex.Services.WCF;

namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Represents an object for sending  request for all service command for the Email module.
    /// </summary>
    public class EmailRequestDispatcher : WCFCommandRequestDispatcher, IEmailRequestDispatcher
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EmailRequestDispatcher()
            : base(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpoint"></param>
        public EmailRequestDispatcher(string endpoint)
            : base(endpoint)
        { }

    }
}


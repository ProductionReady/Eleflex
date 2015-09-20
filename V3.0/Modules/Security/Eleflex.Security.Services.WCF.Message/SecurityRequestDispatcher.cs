using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object for sending service request for all service command for the Security module.
    /// </summary>
    public class SecurityRequestDispatcher : WCFCommandRequestDispatcher, ISecurityRequestDispatcher
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityRequestDispatcher()
            : base(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpoint"></param>
        public SecurityRequestDispatcher(string endpoint)
            : base(endpoint)
        { }

    }
}


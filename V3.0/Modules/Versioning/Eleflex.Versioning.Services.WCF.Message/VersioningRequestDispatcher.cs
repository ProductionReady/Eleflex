using Eleflex.Services.WCF;

namespace Eleflex.Versioning.Services.WCF.Message
{
    /// <summary>
    /// Represents an object for sending  request for all service command for the Versioning module.
    /// </summary>
    public class VersioningRequestDispatcher : WCFCommandRequestDispatcher, IVersioningRequestDispatcher
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public VersioningRequestDispatcher()
            : base(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpoint"></param>
        public VersioningRequestDispatcher(string endpoint)
            : base(endpoint)
        { }

    }
}


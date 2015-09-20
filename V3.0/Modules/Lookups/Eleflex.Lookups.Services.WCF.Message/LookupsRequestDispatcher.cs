using Eleflex.Services.WCF;

namespace Eleflex.Lookups.Services.WCF.Message
{
    /// <summary>
    /// Represents an object for sending  request for all service command for the Lookups module.
    /// </summary>
    public class LookupsRequestDispatcher : WCFCommandRequestDispatcher, ILookupsRequestDispatcher
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public LookupsRequestDispatcher()
            : base(WCFConstants.SERVICE_ENDPOINT_NAME_DEFAULT)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="endpoint"></param>
        public LookupsRequestDispatcher(string endpoint)
            : base(endpoint)
        { }

    }
}


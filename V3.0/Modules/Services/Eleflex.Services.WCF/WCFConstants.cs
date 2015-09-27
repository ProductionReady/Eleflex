namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Static class containing constants for the WCFCommand module.
    /// </summary>
    public static partial class WCFConstants
    {
        /// <summary>
        /// THe name of the service enpoint (in app/web.config) for the default Eleflex command service.
        /// </summary>
        public const string SERVICE_ENDPOINT_NAME_DEFAULT = "EleflexDefault";


        /// <summary>
        /// Constructor parameter name used for WCFCommandRequestDispatcher setting the endpoint of the WCF service.
        /// </summary>
        public const string IWCFCOMMANDREQUESTDISPATCHER_CONSTRUCTORPARAM_ENDPOINT = "endpoint";
    }
}

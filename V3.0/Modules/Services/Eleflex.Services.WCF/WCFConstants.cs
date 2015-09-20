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


        //public const string ERROR_SYSTEM_GENERAL = "A general system error has occured.";
        //public const string ERROR_SYSTEM_GENERAL_CODE = "ERROR_SYSTEM_GENERAL";

        //public const string ERROR_SERVICE = "The service may be busy or is offline.";
        //public const string ERROR_SERVICE_CODE = "ERROR_SERVICE";

        //public const string ERROR_SERVICE_REQUEST_INVALD = "The request is invalid.";
        //public const string ERROR_SERVICE_REQUEST_INVALD_CODE = "ERROR_SERVICE_REQUEST_INVALD";

        //public const string ERROR_SERVICE_CALL_ERROR = "An error occured calling the service.";
        //public const string ERROR_SERVICE_CALL_ERROR_CODE = "ERROR_SERVICE_CALL_ERROR";

        //public const string ERROR_SYSTEM_SECURITY = "Security authorization error.";
        //public const string ERROR_SYSTEM_SECURITY_CODE = "ERROR_SYSTEM_SECURITY";

        /// <summary>
        /// Constructor parameter name used for WCFCommandRequestDispatcher setting the endpoint of the WCF service.
        /// </summary>
        public const string IWCFCOMMANDREQUESTDISPATCHER_CONSTRUCTORPARAM_ENDPOINT = "endpoint";
    }
}

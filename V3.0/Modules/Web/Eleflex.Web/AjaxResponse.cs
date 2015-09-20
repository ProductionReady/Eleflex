namespace Eleflex.Web
{
    /// <summary>
    /// Represents and object used for common json responses for web requests.
    /// </summary>
    public partial class AjaxResponse
    {

        /// <summary>
        /// Status success.
        /// </summary>
        public const string STATUS_SUCCESS = "success";
        /// <summary>
        /// Status info.
        /// </summary>
        public const string STATUS_INFO = "info";
        /// <summary>
        /// Status warning.
        /// </summary>
        public const string STATUS_WARNING = "warning";
        /// <summary>
        /// Status error.
        /// </summary>
        public const string STATUS_ERROR = "error";

        /// <summary>
        /// Reload the page.
        /// </summary>
        public const string REDIRECT_RELOAD = "reload";

        /// <summary>
        /// The data with the response
        /// </summary>
        public virtual object Data { get; set; }

        /// <summary>
        /// The status.
        /// </summary>
        public virtual string Status { get; set; }

        /// <summary>
        /// The message.
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// The redirect option.
        /// </summary>
        public virtual string Redirect { get; set; }

        /// <summary>
        /// Get a success response.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static AjaxResponse Success(string message)
        {
            AjaxResponse result = new AjaxResponse();
            result.Status = STATUS_SUCCESS;
            result.Message = message;
            return result;
        }

        /// <summary>
        /// Get a sucess response.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static AjaxResponse Success(string message, object data)
        {
            AjaxResponse result = new AjaxResponse();
            result.Status = STATUS_SUCCESS;
            result.Message = message;
            result.Data = data;
            return result;
        }

        /// <summary>
        /// Get an info response.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static AjaxResponse Info(string message)
        {
            AjaxResponse result = new AjaxResponse();
            result.Status = STATUS_INFO;
            result.Message = message;
            return result;
        }

        /// <summary>
        /// Get a warning response.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static AjaxResponse Warning(string message)
        {
            AjaxResponse result = new AjaxResponse();
            result.Status = STATUS_WARNING;
            result.Message = message;
            return result;
        }

        /// <summary>
        /// Get an error response.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static AjaxResponse Error(string message)
        {
            AjaxResponse result = new AjaxResponse();
            result.Status = STATUS_ERROR;
            result.Message = message;
            return result;
        }

        /// <summary>
        /// Get a reload response.
        /// </summary>
        /// <returns></returns>
        public static AjaxResponse Reload()
        {
            AjaxResponse result = new AjaxResponse();
            result.Redirect = REDIRECT_RELOAD;
            return result;
        }

        /// <summary>
        /// Get a redirect to response.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static AjaxResponse RedirectTo(string url)
        {
            AjaxResponse result = new AjaxResponse();
            result.Redirect = url;
            return result;
        }

        /// <summary>
        /// Get a success reload response.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static AjaxResponse SuccessReload(string message)
        {
            AjaxResponse result = new AjaxResponse();
            result.Status = STATUS_SUCCESS;
            result.Message = message;
            result.Redirect = REDIRECT_RELOAD;
            return result;
        }

        /// <summary>
        /// Get a success redirect response.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static AjaxResponse SuccessRedirectTo(string message, string url)
        {
            AjaxResponse result = new AjaxResponse();
            result.Status = STATUS_SUCCESS;
            result.Message = message;
            result.Redirect = url;
            return result;
        }
    }
}

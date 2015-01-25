using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Web
{

    public class AjaxResult
    {

        public const string STATUS_SUCCESS = "success";
        public const string STATUS_INFO = "info";
        public const string STATUS_WARNING = "warning";
        public const string STATUS_ERROR = "error";

        public const string REDIRECT_RELOAD = "reload";

        public object Data { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }

        public string Redirect { get; set; }

        public static AjaxResult Success(string message)
        {
            AjaxResult result = new AjaxResult();
            result.Status = STATUS_SUCCESS;
            result.Message = message;
            return result;
        }

        public static AjaxResult Success(string message, object data)
        {
            AjaxResult result = new AjaxResult();
            result.Status = STATUS_SUCCESS;
            result.Message = message;
            result.Data = data;
            return result;
        }

        public static AjaxResult Info(string message)
        {
            AjaxResult result = new AjaxResult();
            result.Status = STATUS_INFO;
            result.Message = message;
            return result;
        }

        public static AjaxResult Warning(string message)
        {
            AjaxResult result = new AjaxResult();
            result.Status = STATUS_WARNING;
            result.Message = message;
            return result;
        }

        public static AjaxResult Error(string message)
        {
            AjaxResult result = new AjaxResult();
            result.Status = STATUS_ERROR;
            result.Message = message;
            return result;
        }

        public static AjaxResult Reload()
        {
            AjaxResult result = new AjaxResult();
            result.Redirect = REDIRECT_RELOAD;
            return result;
        }

        public static AjaxResult RedirectTo(string url)
        {
            AjaxResult result = new AjaxResult();
            result.Redirect = url;
            return result;
        }

        public static AjaxResult SuccessReload(string message)
        {
            AjaxResult result = new AjaxResult();
            result.Status = STATUS_SUCCESS;
            result.Message = message;
            result.Redirect = REDIRECT_RELOAD;
            return result;
        }

        public static AjaxResult SuccessRedirectTo(string message, string url)
        {
            AjaxResult result = new AjaxResult();
            result.Status = STATUS_SUCCESS;
            result.Message = message;
            result.Redirect = url;
            return result;
        }
    }
}

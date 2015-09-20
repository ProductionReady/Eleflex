using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// This message inspector is used to pass the user's OWIN cookie to the WCF service in the header. Additionally, we can override the request
    /// to impersonate the ELEFLEX system account (Admin).
    /// </summary>
    public partial class CookieSecurityClientMessageInspector : IClientMessageInspector
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public CookieSecurityClientMessageInspector()
        {
        }

        /// <summary>
        /// After recieve reply.
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public virtual void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        /// <summary>
        /// Before send request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <returns></returns>
        public virtual object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            HttpRequestMessageProperty requestMessageProperty = new HttpRequestMessageProperty();

            //Get user's current OWIN cookie
            if (HttpContext.Current != null && HttpContext.Current.Request.Headers.AllKeys.Contains("Cookie"))
            {
                //Set OWIN cookie to service call (passthrough)
                var cookie = HttpContext.Current.Request.Headers["Cookie"];
                if (!string.IsNullOrEmpty(cookie))
                {
                    if (requestMessageProperty.Headers.AllKeys.Contains("Cookie"))
                        requestMessageProperty.Headers["Cookie"] = cookie;
                    else
                        requestMessageProperty.Headers.Add("Cookie", cookie);
                }
            }

            //Check for user account impersonation
            if (ImpersonateUser.IsImpersonateUser)
            {
                if (requestMessageProperty.Headers.AllKeys.Contains(ImpersonateUser.KEY_IMPERSONATE_USER_USERNAME))
                    requestMessageProperty.Headers[ImpersonateUser.KEY_IMPERSONATE_USER_USERNAME] = ImpersonateUser.GetUsername;
                else
                    requestMessageProperty.Headers.Add(ImpersonateUser.KEY_IMPERSONATE_USER_USERNAME, ImpersonateUser.GetUsername);

                if (requestMessageProperty.Headers.AllKeys.Contains(ImpersonateUser.KEY_IMPERSONATE_USER_PASSWORD))
                    requestMessageProperty.Headers[ImpersonateUser.KEY_IMPERSONATE_USER_PASSWORD] = ImpersonateUser.GetPassword;
                else
                    requestMessageProperty.Headers.Add(ImpersonateUser.KEY_IMPERSONATE_USER_PASSWORD, ImpersonateUser.GetPassword);
            }

            //Check for system admin account impersonation (sometimes required to be done to invoke system processes during context of a user request)
            if (ImpersonateSystem.IsImpersonateSystem)
            {

                if (requestMessageProperty.Headers.AllKeys.Contains(ImpersonateSystem.KEY_IMPERSONATE_SYSTEM_TOKEN))
                    requestMessageProperty.Headers[ImpersonateSystem.KEY_IMPERSONATE_SYSTEM_TOKEN] = ImpersonateSystem.GetToken;
                else
                    requestMessageProperty.Headers.Add(ImpersonateSystem.KEY_IMPERSONATE_SYSTEM_TOKEN, ImpersonateSystem.GetToken);
            }

            //Add property to request
            request.Properties[HttpRequestMessageProperty.Name] = requestMessageProperty;
            return null;
        }
    }

}

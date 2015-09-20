using System.Linq;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Eleflex.Security.ASPNetIdentity;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Message inspector used to perform additional security authentication/authorization for all requests.
    /// </summary>
    public partial class CookieSecurityServerMessageInspector : IDispatchMessageInspector
    {

        /// <summary>
        /// After receive request.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="channel"></param>
        /// <param name="instanceContext"></param>
        /// <returns></returns>
        public virtual object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {            
            var messageProperty = (HttpRequestMessageProperty)OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name];

            //If the client caller passed a "Cookie" header with their OWIN cookie, then the OWIN pipeline has picked it up and processed the user at this point and is authenticated

            //Check for user credentials with request
            if (messageProperty.Headers.AllKeys.Contains(ImpersonateUser.KEY_IMPERSONATE_USER_USERNAME))
            {
                //Validate system secret key token with request
                string username = messageProperty.Headers[ImpersonateUser.KEY_IMPERSONATE_USER_USERNAME];
                string password = messageProperty.Headers[ImpersonateUser.KEY_IMPERSONATE_USER_PASSWORD];
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    //Valid user impersonation, try to signin with user account
                    var signinmgr = HttpContext.Current.GetOwinContext().Get<IdentitySignInManager>();
                    var signinStatus = signinmgr.PasswordSignIn(username, password, false, true);
                    if (signinStatus == SignInStatus.Success)
                    {

                        //Build up roles and claims for user and then impersonate
                        var usermgr = HttpContext.Current.GetOwinContext().Get<IdentityUserManager>();
                        var user = usermgr.FindByNameAsync(username).Result;
                        if (user != null)
                        {
                            var claims = usermgr.GetClaimsAsync(user.SecurityUserKey.ToString()).Result;
                            var roles = usermgr.GetRolesAsync(user.SecurityUserKey.ToString()).Result;
                            GenericIdentity identity = new GenericIdentity(user.UserName);
                            if (claims != null)
                                identity.AddClaims(claims);
                            IPrincipal principal = null;
                            if (roles != null)
                            {
                                principal = new GenericPrincipal(identity, roles.ToArray());

                                //Set context on both current thread and context (used both places)
                                System.Threading.Thread.CurrentPrincipal = principal;
                                HttpContext.Current.User = principal;
                            }
                        }
                    }                    
                }
            }

            //Check for system admin account impersonation
            if (messageProperty.Headers.AllKeys.Contains(ImpersonateSystem.KEY_IMPERSONATE_SYSTEM_TOKEN))
            {
                //Validate system secret key token with request
                string token = messageProperty.Headers[ImpersonateSystem.KEY_IMPERSONATE_SYSTEM_TOKEN];
                if(!string.IsNullOrEmpty(token))
                {
                    //Valid impersonation, signin with system admin account
                    if(token == System.Configuration.ConfigurationManager.AppSettings[ImpersonateSystem.KEY_IMPERSONATE_SYSTEM_TOKEN])
                    {
                        //Build up roles and claims for user and then impersonate
                        var usermgr = HttpContext.Current.GetOwinContext().Get<IdentityUserManager>();
                        if (usermgr != null)
                        {
                            var systemUser = usermgr.FindByIdAsync(SecurityConstants.USERKEY_SYSTEM_ADMIN.ToString()).Result;
                            if (systemUser != null)
                            {
                                var claims = usermgr.GetClaimsAsync(systemUser.SecurityUserKey.ToString()).Result;
                                var roles = usermgr.GetRolesAsync(systemUser.SecurityUserKey.ToString()).Result;                                                                
                                GenericIdentity identity = new GenericIdentity(systemUser.Username);
                                identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, SecurityConstants.USERKEY_SYSTEM_ADMIN.ToString()));
                                identity.AddClaim(new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, systemUser.Username));
                                if (claims != null)
                                    identity.AddClaims(claims);
                                IPrincipal principal = null;
                                if (roles != null)
                                {
                                    //Set context on both current thread and context (used both places)
                                    principal = new GenericPrincipal(identity, roles.ToArray());
                                    System.Threading.Thread.CurrentPrincipal = principal;
                                    HttpContext.Current.User = principal;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Before send reply.
        /// </summary>
        /// <param name="reply"></param>
        /// <param name="correlationState"></param>
        public virtual void BeforeSendReply(ref Message reply, object correlationState)
        {

        }
    }
}

#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Net;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using Eleflex.Web;

namespace Eleflex.WebServer
{
    /// <summary>
    /// Message inspector used to perform additional security authentication/authorization for all requests.
    /// </summary>
    public class CookieSecurityServerMessageInspector : IDispatchMessageInspector
    {

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {            
            var messageProperty = (HttpRequestMessageProperty)OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name];

            //If the client caller passed a "Cookie" header with their OWIN cookie, then the OWIN pipeline has picked it up and processed the user at this point and is authenticated

            //Check for user credentials with request
            if (messageProperty.Headers.AllKeys.Contains(Eleflex.Web.ImpersonateUser.KEY_IMPERSONATE_USER_USERNAME))
            {
                //Validate system secret key token with request
                string username = messageProperty.Headers[Eleflex.Web.ImpersonateUser.KEY_IMPERSONATE_USER_USERNAME];
                string password = messageProperty.Headers[Eleflex.Web.ImpersonateUser.KEY_IMPERSONATE_USER_PASSWORD];
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    //Valid user impersonation, try to signin with user account
                    var signinmgr = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
                    var signinStatus = signinmgr.PasswordSignIn(username, password, false, true);
                    if (signinStatus == SignInStatus.Success)
                    {

                        //Build up roles and claims for user and then impersonate
                        var usermgr = HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();
                        var user = usermgr.FindByNameAsync(username).Result;
                        var claims = usermgr.GetClaimsAsync(user.UserKey.ToString()).Result;
                        var roles = usermgr.GetRolesAsync(user.UserKey.ToString()).Result;
                        GenericIdentity identity = new GenericIdentity(user.Username);
                        identity.AddClaims(claims);
                        IPrincipal principal = new GenericPrincipal(identity, roles.ToArray());

                        //Set context on both current thread and context (used both places)
                        System.Threading.Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;
                    }                    
                }
            }

            //Check for system admin account impersonation
            if (messageProperty.Headers.AllKeys.Contains(Eleflex.Web.ImpersonateSystem.KEY_IMPERSONATE_SYSTEM_TOKEN))
            {
                //Validate system secret key token with request
                string token = messageProperty.Headers[Eleflex.Web.ImpersonateSystem.KEY_IMPERSONATE_SYSTEM_TOKEN];
                if(!string.IsNullOrEmpty(token))
                {
                    //Valid impersonation, signin with system admin account
                    if(token == System.Configuration.ConfigurationManager.AppSettings[Eleflex.Web.ImpersonateSystem.KEY_IMPERSONATE_SYSTEM_TOKEN])
                    {                                        
                        //Build up roles and claims for user and then impersonate
                        var usermgr = HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>();
                        var systemUser = usermgr.FindByIdAsync(Eleflex.Security.SecurityConstants.USERKEY_SYSTEM_ADMIN.ToString()).Result;
                        var claims = usermgr.GetClaimsAsync(systemUser.UserKey.ToString()).Result;
                        var roles = usermgr.GetRolesAsync(systemUser.UserKey.ToString()).Result;                                                
                        GenericIdentity identity = new GenericIdentity(systemUser.Username);
                        identity.AddClaims(claims);
                        IPrincipal principal = new GenericPrincipal(identity, roles.ToArray());

                        //Set context on both current thread and context (used both places)
                        System.Threading.Thread.CurrentPrincipal = principal;
                        HttpContext.Current.User = principal;                        
                    }
                }
            }
            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {

        }
    }
}

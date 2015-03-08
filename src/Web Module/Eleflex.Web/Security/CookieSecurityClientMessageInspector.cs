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
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Net;
using System.Web;

namespace Eleflex.Web
{

    /// <summary>
    /// This message inspector is used to pass the user's OWIN cookie to the WCF service in the header. Additionally, we can override the request
    /// to impersonate the ELEFLEX system account (Admin).
    /// </summary>
    public class CookieSecurityClientMessageInspector : IClientMessageInspector
    {

        public CookieSecurityClientMessageInspector()
        {
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            HttpRequestMessageProperty requestMessageProperty = new HttpRequestMessageProperty();

            //Get user's current OWIN cookie
            if(HttpContext.Current != null && HttpContext.Current.Request.Headers.AllKeys.Contains("Cookie"))
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
            if(ImpersonateSystem.IsImpersonateSystem)
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

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
using System.Web;
using Eleflex.Services.Server;
using Eleflex.Security;
using Eleflex.Security.Message.ServiceLoginCookieCommand;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using DomainModel = Eleflex.Security;
using ServiceModel = Eleflex.Security.Message;
using Eleflex.Web;

namespace Eleflex.WebServer
{
    /// <summary>
    /// Service command to login a user and return a cookie response.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(ServiceLoginCookieRequest))]
    public class ServiceLoginCookieLogin : ServiceCommandHandler<ServiceLoginCookieRequest, ServiceLoginCookieResponse>
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userRepository"></param>
        public ServiceLoginCookieLogin(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public override void Execute(ServiceLoginCookieRequest request, ServiceLoginCookieResponse response)
        {
            ApplicationSignInManager signinManager = HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            SignInStatus status = signinManager.PasswordSignIn(request.Username, request.Password, false, true);
            switch (status)
            {
                case SignInStatus.Failure:
                    response.ResponseStatus.AddError(Services.ServicesConstants.ERROR_SYSTEM_SECURITY,Services.ServicesConstants.ERROR_SYSTEM_SECURITY_CODE);
                    break;
                case SignInStatus.LockedOut:
                    response.ResponseStatus.AddError(Services.ServicesConstants.ERROR_SYSTEM_SECURITY,Services.ServicesConstants.ERROR_SYSTEM_SECURITY_CODE);
                    break;
                case SignInStatus.RequiresVerification:
                    response.ResponseStatus.AddError(Services.ServicesConstants.ERROR_SYSTEM_SECURITY,Services.ServicesConstants.ERROR_SYSTEM_SECURITY_CODE);
                    break;
                case SignInStatus.Success:
                    response.Item = HttpContext.Current.Response.Headers["Set-Cookie"];
                    break;
            }
                
        }
    }
}

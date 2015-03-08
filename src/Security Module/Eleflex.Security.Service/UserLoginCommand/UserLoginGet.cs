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
using System.Security.Permissions;
using Eleflex.Services.Server;
using Eleflex.Security;
using Eleflex.Security.Message.UserLoginCommand;
using DomainModel = Eleflex.Security;
using ServiceModel = Eleflex.Security.Message;

namespace Eleflex.Security.Service.UserLoginCommand
{
    /// <summary>
    /// Service command to get a UserLogin.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(UserLoginGetRequest))]
    public class UserLoginGet : ServiceCommandHandler<UserLoginGetRequest, UserLoginGetResponse>
    {
        private readonly IUserLoginRepository _userLoginRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="UserLoginRepository"></param>
        public UserLoginGet(IUserLoginRepository userLoginRepository)
        {
            _userLoginRepository = userLoginRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(UserLoginGetRequest request, UserLoginGetResponse response)
        {            
            DomainModel.UserLogin item = _userLoginRepository.Get(request.Item);
            response.Item = AutoMapper.Mapper.Map<DomainModel.UserLogin,ServiceModel.UserLogin>(item);
        }
    }
}

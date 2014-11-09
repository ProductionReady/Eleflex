#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//Copyright © 2014 Production Ready, LLC. All Rights Reserved.
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
using Eleflex.Services.Server;
using Eleflex.Security;
using Eleflex.Security.Message.UserCommand;
using DomainModel = Eleflex.Security;
using ServiceModel = Eleflex.Security.Message;

namespace Eleflex.Security.Service.UserCommand
{
    /// <summary>
    /// Service command to update a User.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(UserUpdateRequest))]
    public class UserUpdate : ServiceCommandHandler<UserUpdateRequest, UserUpdateResponse>
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userRepository"></param>
        public UserUpdate(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public override void Execute(UserUpdateRequest request, UserUpdateResponse response)
        {
            DomainModel.User item = _userRepository.Get(request.Item.UserKey);            
            item.ChangeFirstName(request.Item.FirstName);
            item.ChangeLastName(request.Item.LastName);
            item.ChangeUsername(request.Item.Username);
            item.ChangeEmail(request.Item.Email);
            item.ChangePassword(request.Item.Password);
            item.ChangePasswordSalt(request.Item.PasswordSalt);
            item.ChangePasswordLastChangeDate(request.Item.PasswordLastChangeDate);
            item.ChangeLoginFailedAttempts(request.Item.LoginFailedAttempts);
            item.ChangeIsLockedOut(request.Item.IsLockedOut);
            item.ChangeLastLoginDate(request.Item.LastLoginDate);
            item.ChangeLockoutReinstateDate(request.Item.LockoutReinstateDate);
            item.ChangeComment(request.Item.Comment);
            item.ChangeExtraData(request.Item.ExtraData);
            item = _userRepository.Update(item);
            response.Item = AutoMapper.Mapper.Map<ServiceModel.User>(item);
        }
    }
}

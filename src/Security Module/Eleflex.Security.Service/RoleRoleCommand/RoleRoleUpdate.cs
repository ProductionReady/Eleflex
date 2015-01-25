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
using Eleflex.Services.Server;
using Eleflex.Security;
using Eleflex.Security.Message.RoleRoleCommand;
using DomainModel = Eleflex.Security;
using ServiceModel = Eleflex.Security.Message;

namespace Eleflex.Security.Service.RoleRoleCommand
{
    /// <summary>
    /// Service command to update a RoleRole.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(RoleRoleUpdateRequest))]
    public class RoleRoleUpdate : ServiceCommandHandler<RoleRoleUpdateRequest, RoleRoleUpdateResponse>
    {
        private readonly IRoleRoleRepository _roleRoleRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="RoleRoleRepository"></param>
        public RoleRoleUpdate(IRoleRoleRepository roleRoleRepository)
        {
            _roleRoleRepository = roleRoleRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public override void Execute(RoleRoleUpdateRequest request, RoleRoleUpdateResponse response)
        {
            DomainModel.RoleRole item = _roleRoleRepository.Get(request.Item.RoleRoleKey);
            item.ChangeChildRoleKey(request.Item.ChildRoleKey);
            item.ChangeComment(request.Item.Comment);
            item.ChangeEndDate(request.Item.EndDate);
            item.ChangeExtraData(request.Item.ExtraData);
            item.ChangeInactive(request.Item.Inactive);
            item.ChangeModuleKey(request.Item.ModuleKey);
            item.ChangeParentRoleKey(request.Item.ParentRoleKey);
            item.ChangeStartDate(request.Item.StartDate);            
            item = _roleRoleRepository.Update(item);
            response.Item = AutoMapper.Mapper.Map<ServiceModel.RoleRole>(item);
        }
    }
}

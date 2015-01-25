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
using Eleflex.Security.Message.RolePermissionCommand;
using DomainModel = Eleflex.Security;
using ServiceModel = Eleflex.Security.Message;

namespace Eleflex.Security.Service.RolePermissionCommand
{
    /// <summary>
    /// Service command to create a RolePermission.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(RolePermissionCreateRequest))]
    public class RolePermissionCreate : ServiceCommandHandler<RolePermissionCreateRequest, RolePermissionCreateResponse>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="RolePermissionRepository"></param>
        public RolePermissionCreate(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public override void Execute(RolePermissionCreateRequest request, RolePermissionCreateResponse response)
        {
            DomainModel.RolePermission item = new DomainModel.RolePermission();
            item.ChangeComment(request.Item.Comment);
            item.ChangeEndDate(request.Item.EndDate);
            item.ChangeExtraData(request.Item.ExtraData);
            item.ChangeInactive(request.Item.Inactive);
            item.ChangeModuleKey(request.Item.ModuleKey);
            item.ChangePermissionKey(request.Item.PermissionKey);
            item.ChangeRoleKey(request.Item.RoleKey);
            item.ChangeStartDate(request.Item.StartDate);
            item = _rolePermissionRepository.Insert(item);
            response.Item = AutoMapper.Mapper.Map<ServiceModel.RolePermission>(item);
        }
    }
}

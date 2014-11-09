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
using Eleflex.Security.Message.PermissionCommand;
using DomainModel = Eleflex.Security;
using ServiceModel = Eleflex.Security.Message;

namespace Eleflex.Security.Service.PermissionCommand
{
    /// <summary>
    /// Service command to update a Permission.
    /// </summary>
    public class PermissionUpdate : ServiceCommandHandler<PermissionUpdateRequest, PermissionUpdateResponse>
    {
        private readonly IPermissionRepository _permissionRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="PermissionRepository"></param>
        public PermissionUpdate(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public override void Execute(PermissionUpdateRequest request, PermissionUpdateResponse response)
        {
            DomainModel.Permission item = _permissionRepository.Get(request.Item.PermissionKey);
            item.ChangeModuleKey(request.Item.ModuleKey);
            item.ChangeInactive(request.Item.Inactive);
            item.ChangeName(request.Item.Name);
            item.ChangeDescription(request.Item.Description);
            item.ChangeExtraData(request.Item.ExtraData);
            item = _permissionRepository.Update(item);
            response.Item = AutoMapper.Mapper.Map<ServiceModel.Permission>(item);
        }
    }
}

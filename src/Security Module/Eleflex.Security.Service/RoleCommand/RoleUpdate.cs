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
using Eleflex.Security.Message.RoleCommand;
using DomainModel = Eleflex.Security;
using ServiceModel = Eleflex.Security.Message;

namespace Eleflex.Security.Service.RoleCommand
{
    /// <summary>
    /// Service command to update a Role.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(RoleUpdateRequest))]
    public class RoleUpdate : ServiceCommandHandler<RoleUpdateRequest, RoleUpdateResponse>
    {
        private readonly IRoleRepository _roleRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="RoleRepository"></param>
        public RoleUpdate(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public override void Execute(RoleUpdateRequest request, RoleUpdateResponse response)
        {
            DomainModel.Role item = _roleRepository.Get(request.Item.RoleKey);
            item.ChangeInactive(request.Item.Inactive);
            item.ChangeName(request.Item.Name);
            item.ChangeDescription(request.Item.Description);
            item.ChangeExtraData(request.Item.ExtraData);
            item.ChangeStartDate(request.Item.StartDate);
            item.ChangeEndDate(request.Item.EndDate);
            item.ChangeModuleKey(request.Item.ModuleKey);
            item = _roleRepository.Update(item);
            response.Item = AutoMapper.Mapper.Map<ServiceModel.Role>(item);
        }
    }
}

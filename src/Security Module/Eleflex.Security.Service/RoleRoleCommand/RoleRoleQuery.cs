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
using System.Linq;
using System.Collections.Generic;
using Eleflex.Services.Server;
using Eleflex.Security;
using Eleflex.Security.Message.RoleRoleCommand;
using DomainModel = Eleflex.Security;
using ServiceModel = Eleflex.Security.Message;

namespace Eleflex.Security.Service.RoleRoleCommand
{
    /// <summary>
    /// Service command to query RoleRoles.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(RoleRoleQueryRequest))]
    public class RoleRoleQuery : ServiceCommandHandler<RoleRoleQueryRequest, RoleRoleQueryResponse>
    {
        private readonly IRoleRoleRepository _roleRoleRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="RoleRoleRepository"></param>
        public RoleRoleQuery(IRoleRoleRepository roleRoleRepository)
        {
            _roleRoleRepository = roleRoleRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public override void Execute(RoleRoleQueryRequest request, RoleRoleQueryResponse response)
        {            
            var items = _roleRoleRepository.Query(request).ToList();
            response.Items = AutoMapper.Mapper.Map<List<DomainModel.RoleRole>, List<ServiceModel.RoleRole>>(items);
        }
    }
}

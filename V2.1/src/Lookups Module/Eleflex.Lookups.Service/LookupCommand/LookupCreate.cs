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
using Eleflex.Services;
using Eleflex.Services.Server;
using Eleflex.Lookups;
using Eleflex.Lookups.Message.LookupCommand;
using DomainModel = Eleflex.Lookups;
using ServiceModel = Eleflex.Lookups.Message;

namespace Eleflex.Lookups.Service.LookupCommand
{
    /// <summary>
    /// Service command to create a lookup.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(LookupCreateRequest))]
    public class LookupCreate : ServiceCommandHandler<LookupCreateRequest, LookupCreateResponse>
    {
        private readonly ILookupsRepository _lookupRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="lookupRepository"></param>
        public LookupCreate(ILookupsRepository lookupRepository)
        {
            _lookupRepository = lookupRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(LookupCreateRequest request, LookupCreateResponse response)
        {
            DomainModel.Lookup item = new DomainModel.Lookup();            
            item.ChangeInactive(request.Item.Inactive);
            item.ChangeCategory(AutoMapper.Mapper.Map<DomainModel.Lookup>(request.Item.Category));
            item.ChangeSort (request.Item.SortOrder);
            item.ChangeAbbreviation (request.Item.Abbreviation);
            item.ChangeName (request.Item.Name);
            item.ChangeDescription(request.Item.Description);
            item = _lookupRepository.Insert(item);
            response.Item = AutoMapper.Mapper.Map<ServiceModel.Lookup>(item);
        }
    }
}

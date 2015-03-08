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
using System.Security.Permissions;
using Eleflex.Services.Server;
using Eleflex.Lookups;
using Eleflex.Lookups.Message.LookupCommand;
using DomainModel = Eleflex.Lookups;
using ServiceModel = Eleflex.Lookups.Message;

namespace Eleflex.Lookups.Service.LookupCommand
{
    /// <summary>
    /// Service command to query logs.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(LookupQueryRequest))]
    public class LookupQuery : ServiceCommandHandler<LookupQueryRequest, LookupQueryResponse>
    {
        private readonly ILookupsRepository _lookupRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="lookupRepository"></param>
        public LookupQuery(ILookupsRepository lookupRepository)
        {
            _lookupRepository = lookupRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "User")]
        public override void Execute(LookupQueryRequest request, LookupQueryResponse response)
        {
            var items = _lookupRepository.Query(request).ToList();
            response.Items = AutoMapper.Mapper.Map<List<DomainModel.Lookup>, List<ServiceModel.Lookup>>(items);
        }
    }
}

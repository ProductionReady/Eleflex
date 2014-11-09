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
using Eleflex.Versioning;
using Eleflex.Versioning.Message.ModuleVersionCommand;
using DomainModel = Eleflex.Versioning;
using ServiceModel = Eleflex.Versioning.Message;

namespace Eleflex.Versioning.Service.ModuleVersionCommand
{
    /// <summary>
    /// Service command to update a log.
    /// </summary>
    [ServiceCommandHandlerAttribute(typeof(ModuleVersionUpdateRequest))]
    public class VersionUpdate : ServiceCommandHandler<ModuleVersionUpdateRequest, ModuleVersionUpdateResponse>
    {
        private readonly IModuleVersionRepository _moduleVersionRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="moduleVersionRepository"></param>
        public VersionUpdate(IModuleVersionRepository moduleVersionRepository)
        {
            _moduleVersionRepository = moduleVersionRepository;
        }

        /// <summary>
        /// Execute.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        public override void Execute(ModuleVersionUpdateRequest request, ModuleVersionUpdateResponse response)
        {
            DomainModel.ModuleVersion item = _moduleVersionRepository.Get(request.Item.ModuleKey);
            item.ChangeModuleName(request.Item.ModuleName);
            item.ChangeUpdateDate(request.Item.UpdateDate);
            item.Version.ChangeMajor(request.Item.Version.Major);
            item.Version.ChangeMinor(request.Item.Version.Minor);
            item.Version.ChangeBuild(request.Item.Version.Build);
            item.Version.ChangeRevision(request.Item.Version.Revision);
            item.ChangeExtraData(request.Item.CustomData);
            item = _moduleVersionRepository.Update(item);
            response.Item = AutoMapper.Mapper.Map<ServiceModel.ModuleVersion>(item);
        }
    }
}

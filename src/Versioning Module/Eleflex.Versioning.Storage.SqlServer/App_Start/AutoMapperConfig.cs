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
using Bootstrap.AutoMapper;
using AutoMapper;
using AutoMapper.Mappers;
using DomainModel = Eleflex.Versioning;
using StorageModel = Eleflex.Versioning.Storage.SqlServer.Model;

namespace Eleflex.Versioning.Storage.SqlServer
{
    /// <summary>
    /// Registers AutoMapper configurations used in this assembly.
    /// </summary>
    public class AutoMapperConfig : IMapCreator
    {
        /// <summary>
        /// Create mappings.
        /// </summary>
        /// <param name="mapper"></param>
        public void CreateMap(IProfileExpression mapper)
        {
            //ModuleVersion
            mapper.CreateMap<DomainModel.ModuleVersion, StorageModel.ModuleVersion>();
            mapper.CreateMap<StorageModel.ModuleVersion, DomainModel.ModuleVersion>();
        }
    }
}

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
using DomainModel = Eleflex.Security;
using StorageModel = Eleflex.Security.Storage.SqlServer.Model;

namespace Eleflex.Security.Storage.SqlServer
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
                        

            mapper.CreateMap<DomainModel.Permission, StorageModel.Permission>();
            mapper.CreateMap<StorageModel.Permission, DomainModel.Permission>();

            mapper.CreateMap<DomainModel.Role, StorageModel.Role>();
            mapper.CreateMap<StorageModel.Role, DomainModel.Role>();

            mapper.CreateMap<DomainModel.RoleRole, StorageModel.RoleRole>();
            mapper.CreateMap<StorageModel.RoleRole, DomainModel.RoleRole>();

            mapper.CreateMap<DomainModel.RolePermission, StorageModel.RolePermission>();
            mapper.CreateMap<StorageModel.RolePermission, DomainModel.RolePermission>();

            mapper.CreateMap<DomainModel.User, StorageModel.User>();
            mapper.CreateMap<StorageModel.User, DomainModel.User>();

            mapper.CreateMap<DomainModel.UserClaim, StorageModel.UserClaim>();
            mapper.CreateMap<StorageModel.UserClaim, DomainModel.UserClaim>();

            mapper.CreateMap<DomainModel.UserLogin, StorageModel.UserLogin>();
            mapper.CreateMap<StorageModel.UserLogin, DomainModel.UserLogin>();

            mapper.CreateMap<DomainModel.UserRole, StorageModel.UserRole>();
            mapper.CreateMap<StorageModel.UserRole, DomainModel.UserRole>();

            mapper.CreateMap<DomainModel.UserPermission, StorageModel.UserPermission>();
            mapper.CreateMap<StorageModel.UserPermission, DomainModel.UserPermission>();
        }
    }
}

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
using ServiceModel = Eleflex.Security.Message;

namespace Eleflex.Security.Service
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

            mapper.CreateMap<DomainModel.Permission, ServiceModel.Permission>();
            mapper.CreateMap<ServiceModel.Permission, DomainModel.Permission>();

            mapper.CreateMap<DomainModel.Role, ServiceModel.Role>();
            mapper.CreateMap<ServiceModel.Role, DomainModel.Role>();

            mapper.CreateMap<DomainModel.RoleRole, ServiceModel.RoleRole>();
            mapper.CreateMap<ServiceModel.RoleRole, DomainModel.RoleRole>();

            mapper.CreateMap<DomainModel.RolePermission, ServiceModel.RolePermission>();
            mapper.CreateMap<ServiceModel.RolePermission, DomainModel.RolePermission>();

            mapper.CreateMap<DomainModel.User, ServiceModel.User>();
            mapper.CreateMap<ServiceModel.User, DomainModel.User>();

            mapper.CreateMap<DomainModel.UserClaim, ServiceModel.UserClaim>();
            mapper.CreateMap<ServiceModel.UserClaim, DomainModel.UserClaim>();

            mapper.CreateMap<DomainModel.UserLogin, ServiceModel.UserLogin>();
            mapper.CreateMap<ServiceModel.UserLogin, DomainModel.UserLogin>();

            mapper.CreateMap<DomainModel.UserRole, ServiceModel.UserRole>();
            mapper.CreateMap<ServiceModel.UserRole, DomainModel.UserRole>();

            mapper.CreateMap<DomainModel.UserDetail, ServiceModel.UserDetail>();
            mapper.CreateMap<ServiceModel.UserDetail, DomainModel.UserDetail>();

            mapper.CreateMap<DomainModel.UserPermission, ServiceModel.UserPermission>();
            mapper.CreateMap<ServiceModel.UserPermission, DomainModel.UserPermission>();
        }
    }
}

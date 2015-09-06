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
using ServiceModel = Eleflex.Security.Message;

namespace Eleflex.Security.Web
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

            //Users
            mapper.CreateMap<Eleflex.Security.Web.Security.Users.EditViewModel, ServiceModel.User>();
            mapper.CreateMap<ServiceModel.User, Eleflex.Security.Web.Security.Users.EditViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Users.EditViewModel, Eleflex.Security.User>();
            mapper.CreateMap<Eleflex.Security.User, Eleflex.Security.Web.Security.Users.EditViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Users.UserRoleViewModel, Eleflex.Security.Message.UserRole>();
            mapper.CreateMap<Eleflex.Security.Message.UserRole, Eleflex.Security.Web.Security.Users.UserRoleViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Users.EditRoleViewModel, Eleflex.Security.Message.UserRole>();
            mapper.CreateMap<Eleflex.Security.Message.UserRole, Eleflex.Security.Web.Security.Users.EditRoleViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Roles.EditViewModel, ServiceModel.Role>();
            mapper.CreateMap<ServiceModel.Role, Eleflex.Security.Web.Security.Roles.EditViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Roles.EditViewModel, Eleflex.Security.Role>();
            mapper.CreateMap<Eleflex.Security.Role, Eleflex.Security.Web.Security.Roles.EditViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Users.UserPermissionViewModel, Eleflex.Security.Message.UserPermission>();
            mapper.CreateMap<Eleflex.Security.Message.UserPermission, Eleflex.Security.Web.Security.Users.UserPermissionViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Users.EditPermissionViewModel, Eleflex.Security.Message.UserPermission>();
            mapper.CreateMap<Eleflex.Security.Message.UserPermission, Eleflex.Security.Web.Security.Users.EditPermissionViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Users.UserClaimViewModel, Eleflex.Security.Message.UserClaim>();
            mapper.CreateMap<Eleflex.Security.Message.UserClaim, Eleflex.Security.Web.Security.Users.UserClaimViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Users.EditClaimViewModel, Eleflex.Security.Message.UserClaim>();
            mapper.CreateMap<Eleflex.Security.Message.UserClaim, Eleflex.Security.Web.Security.Users.EditClaimViewModel>();


            //Roles
            mapper.CreateMap<Eleflex.Security.Web.Security.Roles.RoleRoleViewModel, Eleflex.Security.Message.RoleRole>();
            mapper.CreateMap<Eleflex.Security.Message.RoleRole, Eleflex.Security.Web.Security.Roles.RoleRoleViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Roles.EditRoleViewModel, Eleflex.Security.Message.RoleRole>();
            mapper.CreateMap<Eleflex.Security.Message.RoleRole, Eleflex.Security.Web.Security.Roles.EditRoleViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Roles.EditViewModel, ServiceModel.Role>();
            mapper.CreateMap<ServiceModel.Role, Eleflex.Security.Web.Security.Roles.EditViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Roles.EditViewModel, Eleflex.Security.Role>();
            mapper.CreateMap<Eleflex.Security.Role, Eleflex.Security.Web.Security.Roles.EditViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Roles.RolePermissionViewModel, Eleflex.Security.Message.RolePermission>();
            mapper.CreateMap<Eleflex.Security.Message.RolePermission, Eleflex.Security.Web.Security.Roles.RolePermissionViewModel>();

            mapper.CreateMap<Eleflex.Security.Web.Security.Roles.EditPermissionViewModel, Eleflex.Security.Message.RolePermission>();
            mapper.CreateMap<Eleflex.Security.Message.RolePermission, Eleflex.Security.Web.Security.Roles.EditPermissionViewModel>();
        }
    }
}

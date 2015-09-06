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
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
﻿using Microsoft.AspNet.Identity;
using Microsoft.Practices.ServiceLocation;
using Eleflex.Security.Message;

namespace Eleflex.Web
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity role store iterfaces
    /// </summary>
    public class IdentityRoleStoreServiceClient<TRole> : IRoleStore<TRole>, IQueryableRoleStore<TRole>
        where TRole : Eleflex.Security.Role
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public IdentityRoleStoreServiceClient()
        {
        }

        /// <summary>
        /// Disposal.
        /// </summary>
        public void Dispose()
        {
        }


        public IQueryable<TRole> Roles
        {
            get
            {
                try
                {
                    using (ImpersonateSystem impersonate = new ImpersonateSystem())
                    {
                        IRoleServiceClient roleServiceClient = ServiceLocator.Current.GetInstance<IRoleServiceClient>();
                        Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
                        var respRoles = roleServiceClient.Query(builder.GetStorageQuery());
                        if (respRoles.Items != null)
                            return AutoMapper.Mapper.Map<List<TRole>>(respRoles.Items).AsQueryable();
                    }
                }
                catch (Exception ex)
                {
                    Common.Logging.LogManager.GetLogger<IdentityRoleStoreServiceClient<TRole>>().Error(ex);
                }
                return new List<TRole>().AsQueryable();
            }
        }

        public Task CreateAsync(TRole role)
        {
            try
            {
                using (ImpersonateSystem impersonate = new ImpersonateSystem())
                {
                    IRoleServiceClient roleServiceClient = ServiceLocator.Current.GetInstance<IRoleServiceClient>();
                    roleServiceClient.Insert(AutoMapper.Mapper.Map<Eleflex.Security.Message.Role>(role));
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityRoleStoreServiceClient<TRole>>().Error(ex);
                return Task.FromResult(IdentityResult.Failed("Internal error"));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task DeleteAsync(TRole role)
        {
            try
            {
                using (ImpersonateSystem impersonate = new ImpersonateSystem())
                {
                    IRoleServiceClient roleServiceClient = ServiceLocator.Current.GetInstance<IRoleServiceClient>();

                    var resp = roleServiceClient.Get(role.RoleKey);
                    if (resp.Item != null)
                    {
                        resp.Item.Inactive = true;
                        roleServiceClient.Update(resp.Item);
                    }

                    return Task.FromResult<Object>(null);
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityRoleStoreServiceClient<TRole>>().Error(ex);
            }
            return Task.FromResult<Object>(null);
        }

        public Task<TRole> FindByIdAsync(string roleId)
        {
            try
            {
                using (ImpersonateSystem impersonate = new ImpersonateSystem())
                {
                    Guid key;
                    if (!Guid.TryParse(roleId, out key))
                        throw new FormatException("Argument not a Guid: " + roleId);

                    IRoleServiceClient roleServiceClient = ServiceLocator.Current.GetInstance<IRoleServiceClient>();
                    var resp = roleServiceClient.Get(key);

                    return Task.FromResult<TRole>(AutoMapper.Mapper.Map<Eleflex.Security.Role>(resp.Item) as TRole);
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityRoleStoreServiceClient<TRole>>().Error(ex);
            }
            return Task.FromResult<TRole>(null);
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            try
            {
                using (ImpersonateSystem impersonate = new ImpersonateSystem())
                {
                    DateTimeOffset now = DateTimeOffset.UtcNow;
                    IRoleServiceClient roleServiceClient = ServiceLocator.Current.GetInstance<IRoleServiceClient>();
                    Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
                    builder.BeginExpression()
                        .IsEqual("Name", roleName)
                        .And()
                        .IsEqual("Inactive", false.ToString())
                        .EndExpression()
                        .And()
                        .BeginExpression()
                        .IsNull("EndDate")
                        .Or()
                        .IsGreaterThanOrEqual("EndDate", now.ToString())
                        .EndExpression()
                        .And()
                        .BeginExpression()
                        .IsNull("StartDate")
                        .Or()
                        .IsLessThanOrEqual("StartDate", now.ToString())
                        .EndExpression();
                    var resp = roleServiceClient.Query(builder.GetStorageQuery());

                    if (resp.Items != null && resp.Items.Count > 0)
                    {
                        return Task.FromResult<TRole>(AutoMapper.Mapper.Map<Eleflex.Security.Role>(resp.Items[0]) as TRole);
                    }
                }

            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityRoleStoreServiceClient<TRole>>().Error(ex);
            }
            return Task.FromResult<TRole>(null);
        }

        public Task UpdateAsync(TRole role)
        {
            try
            {
                using (ImpersonateSystem impersonate = new ImpersonateSystem())
                {
                    IRoleServiceClient roleServiceClient = ServiceLocator.Current.GetInstance<IRoleServiceClient>();
                    roleServiceClient.Update(AutoMapper.Mapper.Map<Eleflex.Security.Message.Role>(role));
                }
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityRoleStoreServiceClient<TRole>>().Error(ex);
                return Task.FromResult(IdentityResult.Failed("Internal error"));
            }
            return Task.FromResult<object>(null);
        }

    }
}

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

namespace Eleflex.Security
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity role store iterfaces
    /// </summary>
    public class IdentityRoleStore<TRole> : IRoleStore<TRole>, IQueryableRoleStore<TRole>
        where TRole : Eleflex.Security.Role
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public IdentityRoleStore()
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
                Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
                List<TRole> output = new List<TRole>();
                try
                {
                    IRoleRepository roleRepository = ServiceLocator.Current.GetInstance<IRoleRepository>();
                    Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
                    var roles = roleRepository.Query(builder.GetStorageQuery());

                    uow.Commit();
                    
                    if (roles != null && roles.Count > 0)
                    {
                        foreach(var role in roles)
                            output.Add(role as TRole);
                    }
                    return output.AsQueryable();
                }
                catch (Exception ex)
                {
                    Common.Logging.LogManager.GetLogger<IdentityRoleStore<TRole>>().Error(ex);
                }
                return output.AsQueryable();
            }
        }

        public Task CreateAsync(TRole role)
        {
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IRoleRepository roleRepository = ServiceLocator.Current.GetInstance<IRoleRepository>();
                roleRepository.Insert(role);
                uow.Commit();
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityRoleStore<TRole>>().Error(ex);
                return Task.FromResult(IdentityResult.Failed("Internal error"));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        public Task DeleteAsync(TRole role)
        {
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IRoleRepository roleRepository = ServiceLocator.Current.GetInstance<IRoleRepository>();

                TRole item = roleRepository.Get(role.RoleKey) as TRole;
                if (item != null)
                {
                    item.ChangeInactive(true);
                    roleRepository.Update(item);
                }

                uow.Commit();

                return Task.FromResult<Object>(null);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityRoleStore<TRole>>().Error(ex);
            }
            return Task.FromResult<Object>(null);
        }

        public Task<TRole> FindByIdAsync(string roleId)
        {
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                Guid key;
                if (!Guid.TryParse(roleId, out key))
                    throw new FormatException("Argument not a Guid: " + roleId);

                IRoleRepository roleRepository = ServiceLocator.Current.GetInstance<IRoleRepository>();
                TRole item = roleRepository.Get(key) as TRole;
                uow.Commit();
                return Task.FromResult<TRole>(item);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityRoleStore<TRole>>().Error(ex);
            }
            return Task.FromResult<TRole>(null); 
        }

        public Task<TRole> FindByNameAsync(string roleName)
        {
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                IRoleRepository roleRepository = ServiceLocator.Current.GetInstance<IRoleRepository>();
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
                IList<Role> roles = roleRepository.Query(builder.GetStorageQuery());

                uow.Commit();

                if (roles != null && roles.Count > 0)
                    return Task.FromResult<TRole>(roles[0] as TRole);

            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityRoleStore<TRole>>().Error(ex);
            }
            return Task.FromResult<TRole>(null);
        }

        public Task UpdateAsync(TRole role)
        {
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IRoleRepository roleRepository = ServiceLocator.Current.GetInstance<IRoleRepository>();
                roleRepository.Update(role);
                uow.Commit();
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityRoleStore<TRole>>().Error(ex);
                return Task.FromResult(IdentityResult.Failed("Internal error"));
            }
            return Task.FromResult<object>(null);
        }

    }
}

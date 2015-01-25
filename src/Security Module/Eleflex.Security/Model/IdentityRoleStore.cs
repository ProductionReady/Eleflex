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
                return new List<TRole>().AsQueryable();
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

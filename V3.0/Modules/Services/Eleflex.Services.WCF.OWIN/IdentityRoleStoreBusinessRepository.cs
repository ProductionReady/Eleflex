using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Eleflex.Security.ASPNetIdentity;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents an object that implements the key ASP.NET Identity role store iterfaces.
    /// </summary>
    public partial class IdentityRoleStoreBusinessRepository : IdentityRoleStore, IQueryableRoleStore<IdentityRole>
    {

        /// <summary>
        /// The underlying repository.
        /// </summary>
        protected IRepository<IdentityRole, Guid> _roleRepository = null;
        protected IMappingService _mappingService = null;

        public IdentityRoleStoreBusinessRepository(
            Eleflex.ISecurityRoleBusinessRepository roleRepository,
            IMappingService mappingService)
        {
            _roleRepository = new MappingRepository<IdentityRole, Guid, SecurityRole, ISecurityRoleBusinessRepository>(roleRepository, mappingService);
        }

        
        /// <summary>
        /// Get a list of queryable roles.
        /// </summary>
        public override IQueryable<IdentityRole> Roles
        {
            get
            {
                IStorageContextUnitOfWork uow = ObjectLocator.Current.GetInstance<IStorageContextUnitOfWork>();
                List<IdentityRole> output = new List<IdentityRole>();
                try
                {
                    //Get all roles     
                    StorageQueryBuilder builder = new StorageQueryBuilder();
                    var resp = _roleRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                    uow.Commit();

                    if (resp.ResponseSuccess)
                        return resp.Items.AsQueryable();
                    return null;
                }
                catch (Exception ex)
                {
                    Logger.Current.Error<IdentityRoleStoreBusinessRepository>(ex);
                    uow.Rollback();
                }
                return output.AsQueryable();
            }
        }

        /// <summary>
        /// Create a role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public override Task CreateAsync(IdentityRole role)
        {
            IStorageContextUnitOfWork uow = ObjectLocator.Current.GetInstance<IStorageContextUnitOfWork>();
            try
            {
                var resp = _roleRepository.Insert(new RequestItem<IdentityRole>() { Item = role });
                uow.Commit();
                if (!resp.ResponseSuccess)
                    return Task.FromResult(IdentityResult.Failed("Internal error"));
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityRoleStoreBusinessRepository>(ex);
                uow.Rollback();
                return Task.FromResult(IdentityResult.Failed("Internal error"));
            }

            return Task.FromResult(IdentityResult.Success);
        }

        /// <summary>
        /// Delete a role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public override Task DeleteAsync(IdentityRole role)
        {
            IStorageContextUnitOfWork uow = ObjectLocator.Current.GetInstance<IStorageContextUnitOfWork>();
            try
            {
                var resp = _roleRepository.Get(new RequestItem<Guid>() { Item = role.SecurityRoleKey });
                if (resp.ResponseSuccess && resp.Item != null)
                {
                    resp.Item.Active = false;
                    _roleRepository.Update(new RequestItem<IdentityRole>() { Item = resp.Item });
                }

                uow.Commit();

                return Task.FromResult<Object>(null);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityRoleStoreBusinessRepository>(ex);
                uow.Rollback();
            }
            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Find Role by ID.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public override Task<IdentityRole> FindByIdAsync(string roleId)
        {
            IStorageContextUnitOfWork uow = ObjectLocator.Current.GetInstance<IStorageContextUnitOfWork>();
            try
            {
                Guid key;
                if (!Guid.TryParse(roleId, out key))
                    throw new FormatException("Argument not a Guid: " + roleId);

                var resp = _roleRepository.Get(new RequestItem<Guid>() { Item = key });
                uow.Commit();

                return Task.FromResult<IdentityRole>(resp.Item);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityRoleStoreBusinessRepository>(ex);
                uow.Rollback();
            }
            return Task.FromResult<IdentityRole>(null);
        }

        /// <summary>
        /// Find a role by name.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public override Task<IdentityRole> FindByNameAsync(string roleName)
        {
            IStorageContextUnitOfWork uow = ObjectLocator.Current.GetInstance<IStorageContextUnitOfWork>();
            try
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                StorageQueryBuilder builder = new StorageQueryBuilder();
                builder.BeginExpression()
                    .IsEqual("Name", roleName)
                    .And()
                    .IsEqual("Active", true.ToString())
                    .EndExpression()
                    .And()
                    .BeginExpression()
                    .IsNull("EffectiveEndDate")
                    .Or()
                    .IsGreaterThanOrEqual("EffectiveEndDate", now.ToString())
                    .EndExpression()
                    .And()
                    .BeginExpression()
                    .IsNull("EffectiveStartDate")
                    .Or()
                    .IsLessThanOrEqual("EffectiveStartDate", now.ToString())
                    .EndExpression();
                var resp = _roleRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                uow.Commit();

                if (resp.ResponseSuccess && resp.Items.Count > 0)
                    return Task.FromResult<IdentityRole>(resp.Items[0] as IdentityRole);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityRoleStoreBusinessRepository>(ex);
                uow.Rollback();
            }
            return Task.FromResult<IdentityRole>(null);
        }

        /// <summary>
        /// Update a role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public override Task UpdateAsync(IdentityRole role)
        {
            IStorageContextUnitOfWork uow = ObjectLocator.Current.GetInstance<IStorageContextUnitOfWork>();
            try
            {
                var resp = _roleRepository.Update(new RequestItem<IdentityRole>() { Item = role });
                uow.Commit();

                if (!resp.ResponseSuccess)
                    return Task.FromResult(IdentityResult.Failed("Internal error"));
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityRoleStoreBusinessRepository>(ex);
                uow.Rollback();
                return Task.FromResult(IdentityResult.Failed("Internal error"));
            }
            return Task.FromResult<object>(null);
        }

    }
}

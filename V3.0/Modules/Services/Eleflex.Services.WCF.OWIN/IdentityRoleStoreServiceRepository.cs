using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Eleflex.Security.ASPNetIdentity;
using Eleflex.Security.Services.WCF.Message;
using ServiceModel = Eleflex;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents an object that implements the key ASP.NET Identity role store iterfaces.
    /// </summary>
    public partial class IdentityRoleStoreServiceRepository : IdentityRoleStore
    {

        public IdentityRoleStoreServiceRepository()
        {
        }


        /// <summary>
        /// Get a list of queryable roles.
        /// </summary>
        public override IQueryable<IdentityRole> Roles
        {
            get
            {
                List<IdentityRole> output = new List<IdentityRole>();
                try
                {
                    //Get all roles     
                    IStorageQueryResponseItems<IdentityRole> resp = null;
                    var _roleRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository>>();
                    using (var adminAccess = new ImpersonateSystem())
                        resp = _roleRepository.Query(new RequestItem<IStorageQuery>() { Item = new StorageQuery() });

                    if (resp.ResponseSuccess)
                        return resp.Items.AsQueryable();
                    return null;
                }
                catch (Exception ex)
                {
                    Logger.Current.Error<IdentityRoleStore>(ex);
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
            try
            {
                var _roleRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository>>();
                IResponseItem<IdentityRole> resp = null;
                using (var adminAccess = new ImpersonateSystem())
                    resp = _roleRepository.Insert(new RequestItem<IdentityRole>() { Item = role });
                if (!resp.ResponseSuccess)
                    return Task.FromResult(IdentityResult.Failed("Internal error"));
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityRoleStore>(ex);
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
            try
            {
                var _roleRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository>>();
                IResponseItem<IdentityRole> resp = null;
                using (var adminAccess = new ImpersonateSystem())
                    resp = _roleRepository.Get(new RequestItem<Guid>() { Item = role.SecurityRoleKey });
                if (resp.ResponseSuccess && resp.Item != null)
                {
                    resp.Item.Active = false;
                    _roleRepository.Update(new RequestItem<IdentityRole>() { Item = resp.Item });
                }

                return Task.FromResult<Object>(null);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityRoleStore>(ex);
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
            try
            {
                Guid key;
                if (!Guid.TryParse(roleId, out key))
                    throw new FormatException("Argument not a Guid: " + roleId);

                var _roleRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository>>();
                IResponseItem<IdentityRole> resp = null;
                using (var adminAccess = new ImpersonateSystem())
                    resp = _roleRepository.Get(new RequestItem<Guid>() { Item = key });

                return Task.FromResult<IdentityRole>(resp.Item);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityRoleStore>(ex);
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

                var _roleRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository>>();
                IStorageQueryResponseItems<IdentityRole> resp = null;
                using (var adminAccess = new ImpersonateSystem())
                    resp = _roleRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });

                if (resp.ResponseSuccess && resp.Items.Count > 0)
                    return Task.FromResult<IdentityRole>(resp.Items[0] as IdentityRole);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityRoleStore>(ex);
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
            try
            {
                var _roleRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository>>();
                IResponseItem<IdentityRole> resp = null;
                using (var adminAccess = new ImpersonateSystem())
                    resp = _roleRepository.Update(new RequestItem<IdentityRole>() { Item = role });

                if (!resp.ResponseSuccess)
                    return Task.FromResult(IdentityResult.Failed("Internal error"));
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityRoleStore>(ex);
                return Task.FromResult(IdentityResult.Failed("Internal error"));
            }
            return Task.FromResult<object>(null);
        }

    }
}

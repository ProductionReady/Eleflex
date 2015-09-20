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
    public abstract partial class IdentityRoleStore : 
        IRoleStore<IdentityRole>, 
        IQueryableRoleStore<IdentityRole>
    {


        /// <summary>
        /// Disposal.
        /// </summary>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Get a list of queryable roles.
        /// </summary>
        public abstract IQueryable<IdentityRole> Roles { get; }

        /// <summary>
        /// Create a role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public abstract Task CreateAsync(IdentityRole role);

        /// <summary>
        /// Delete a role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public abstract Task DeleteAsync(IdentityRole role);

        /// <summary>
        /// Find Role by ID.
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public abstract Task<IdentityRole> FindByIdAsync(string roleId);

        /// <summary>
        /// Find a role by name.
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public abstract Task<IdentityRole> FindByNameAsync(string roleName);

        /// <summary>
        /// Update a role.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public abstract Task UpdateAsync(IdentityRole role);

    }
}

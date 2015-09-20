using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Eleflex.Security.ASPNetIdentity;
using ServiceModel = Eleflex;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents an object that implements the key ASP.NET Identity user store iterfaces and communicates via data storage repositories.
    /// </summary>
    public abstract partial class IdentityUserStore : 
        IUserLoginStore<IdentityUser>,
        IUserClaimStore<IdentityUser>,
        IUserRoleStore<IdentityUser>,
        IUserPasswordStore<IdentityUser>,
        IUserSecurityStampStore<IdentityUser>,
        IQueryableUserStore<IdentityUser>,
        IUserEmailStore<IdentityUser>,
        IUserPhoneNumberStore<IdentityUser>,
        IUserTwoFactorStore<IdentityUser, string>,
        IUserLockoutStore<IdentityUser, string>,
        IUserStore<IdentityUser>        
    {

        /// <summary>
        /// Dispose.
        /// </summary>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        public abstract IQueryable<IdentityUser> Users { get; }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public abstract Task CreateAsync(IdentityUser user);

        /// <summary>
        /// Find a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public abstract Task<IdentityUser> FindByIdAsync(string userId);

        /// <summary>
        /// Find by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public abstract Task<IdentityUser> FindByNameAsync(string userName);

        /// <summary>
        /// Updates the UsersTable with the IdentityUser instance values
        /// </summary>
        /// <param name="user">IdentityUser to be updated</param>
        /// <returns></returns>
        public abstract Task UpdateAsync(IdentityUser user);

        /// <summary>
        /// Inserts a claim to the UserClaimsTable for the given user
        /// </summary>
        /// <param name="user">User to have claim added</param>
        /// <param name="claim">Claim to be added</param>
        /// <returns></returns>
        public abstract Task AddClaimAsync(IdentityUser user, Claim claim);

        /// <summary>
        /// Get the effective date storage query
        /// </summary>
        /// <param name="userKey"></param>
        /// <returns></returns>
        protected virtual StorageQueryBuilder GeIdentityUserEffectiveDateBuilder(string userKey)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.BeginExpression()
                .IsEqual("SecurityUserKey", userKey)
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
            return builder;
        }


        /// <summary>
        /// Returns all claims for a given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public abstract Task<IList<Claim>> GetClaimsAsync(IdentityUser user);

        /// <summary>
        /// Removes a claim from a user
        /// </summary>
        /// <param name="user">User to have claim removed</param>
        /// <param name="claim">Claim to be removed</param>
        /// <returns></returns>
        public abstract Task RemoveClaimAsync(IdentityUser user, Claim claim);

        /// <summary>
        /// Inserts a Login in the UserLoginsTable for a given User
        /// </summary>
        /// <param name="user">User to have login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        public abstract Task AddLoginAsync(IdentityUser user, UserLoginInfo login);

        /// <summary>
        /// Returns an IdentityUser based on the Login info
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public abstract Task<IdentityUser> FindAsync(UserLoginInfo login);

        /// <summary>
        /// Returns list of UserLoginInfo for a given IdentityUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public abstract Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user);

        /// <summary>
        /// Deletes a login from UserLoginsTable for a given IdentityUser
        /// </summary>
        /// <param name="user">User to have login removed</param>
        /// <param name="login">Login to be removed</param>
        /// <returns></returns>
        public abstract Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login);

        /// <summary>
        /// Inserts a entry in the UserRoles table
        /// </summary>
        /// <param name="user">User to have role added</param>
        /// <param name="roleName">Name of the role to be added to user</param>
        /// <returns></returns>
        public abstract Task AddToRoleAsync(IdentityUser user, string roleName);

        /// <summary>
        /// Returns the roles for a given IdentityUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<IList<string>> GetRolesAsync(IdentityUser user)
        {
            IList<string> assignedRoles = GetInheritedRoles(user);
            return Task.FromResult<IList<string>>(assignedRoles);
        }


        protected abstract IList<string> GetInheritedRoles(IdentityUser user);


        protected virtual IStorageQuery GetRoleRoleEffectiveDateQuery(string[] inheritedRoles)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.BeginExpression()
                .IsInSet("ParentSecurityRoleKey", inheritedRoles)
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
            return builder.GetStorageQuery();
        }

        protected virtual IStorageQuery GetRoleEffectiveDateQuery(string[] roles)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.BeginExpression()
                .IsInSet("SecurityRoleKey", roles)
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
            return builder.GetStorageQuery();
        }

        /// <summary>
        /// Get storage quer for permission effective dates
        /// </summary>
        /// <param name="permissionKeys"></param>
        /// <returns></returns>
        protected virtual IStorageQuery GetPermissionEffectiveDateQuery(string[] permissionKeys)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.IsInSet("SecurityPermissionKey", permissionKeys);
            builder.And();
            builder.IsEqual("Active", true.ToString());
            return builder.GetStorageQuery();
        }

        /// <summary>
        /// Verifies if a user is in a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public virtual Task<bool> IsInRoleAsync(IdentityUser user, string role)
        {
            IList<string> list = GetInheritedRoles(user);
            return Task.FromResult<bool>(list.Contains(role));
        }

        /// <summary>
        /// Removes a user from a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public abstract Task RemoveFromRoleAsync(IdentityUser user, string role);

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public abstract Task DeleteAsync(IdentityUser user);

        /// <summary>
        /// Returns the PasswordHash for a given IdentityUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            return Task.FromResult<string>(user.Password);
        }

        /// <summary>
        /// Verifies if user has password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> HasPasswordAsync(IdentityUser user)
        {
            return Task.FromResult<bool>(!string.IsNullOrEmpty(user.Password));
        }

        /// <summary>
        /// Sets the password hash for a given IdentityUser
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public virtual Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            user.Password = passwordHash;
            return Task.FromResult<Object>(null);
        }

        /// <summary>
        ///  Set security stamp
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <returns></returns>
        public virtual Task SetSecurityStampAsync(IdentityUser user, string stamp)
        {
            user.SecurityStamp = stamp;
            return Task.FromResult(0);

        }

        /// <summary>
        /// Get security stamp
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetSecurityStampAsync(IdentityUser user)
        {
            return Task.FromResult(user.SecurityStamp);            
        }

        /// <summary>
        /// Set email on user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public virtual Task SetEmailAsync(IdentityUser user, string email)
        {
            user.Email = email;
            return Task.FromResult(0);

        }

        /// <summary>
        /// Get email from user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetEmailAsync(IdentityUser user)
        {
            return Task.FromResult(user.Email);
        }

        /// <summary>
        /// Get if user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            return Task.FromResult(user.EmailValid);            
        }

        /// <summary>
        /// Set when user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public virtual Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            user.EmailValid = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public abstract Task<IdentityUser> FindByEmailAsync(string email);

        /// <summary>
        /// Set user phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public virtual Task SetPhoneNumberAsync(IdentityUser user, string phoneNumber)
        {
            user.Phone = phoneNumber;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Get user phone number
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<string> GetPhoneNumberAsync(IdentityUser user)
        {
            return Task.FromResult(user.Phone);
        }

        /// <summary>
        /// Get if user phone number is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> GetPhoneNumberConfirmedAsync(IdentityUser user)
        {
            return Task.FromResult(user.PhoneValid);
        }

        /// <summary>
        /// Set phone number if confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public virtual Task SetPhoneNumberConfirmedAsync(IdentityUser user, bool confirmed)
        {
            user.PhoneValid = confirmed;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Set two factor authentication is enabled on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public virtual Task SetTwoFactorEnabledAsync(IdentityUser user, bool enabled)
        {
            user.TwoFactorAuth = enabled;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Get if two factor authentication is enabled on the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> GetTwoFactorEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(user.TwoFactorAuth);
        }

        /// <summary>
        /// Get user lock out end date
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<DateTimeOffset> GetLockoutEndDateAsync(IdentityUser user)
        {
            if (!user.Active)
                return Task.FromResult(DateTimeOffset.MaxValue);
            return
                Task.FromResult(user.LockoutReinstateDate.HasValue
                    ? user.LockoutReinstateDate.Value
                    : new DateTimeOffset());
        }


        /// <summary>
        /// Set user lockout end date
        /// </summary>
        /// <param name="user"></param>
        /// <param name="lockoutEnd"></param>
        /// <returns></returns>
        public virtual Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset lockoutEnd)
        {
            user.LockoutReinstateDate = lockoutEnd;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Increment failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<int> IncrementAccessFailedCountAsync(IdentityUser user)
        {
            if (user.EnableLockout)
            {
                user.LoginFailedAttempts = user.LoginFailedAttempts + 1;
            }
            return Task.FromResult(user.LoginFailedAttempts);
        }

        /// <summary>
        /// Reset failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task ResetAccessFailedCountAsync(IdentityUser user)
        {
            user.LoginFailedAttempts = 0;
            return Task.FromResult(0);
        }

        /// <summary>
        /// Get failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<int> GetAccessFailedCountAsync(IdentityUser user)
        {
            return Task.FromResult(user.LoginFailedAttempts);
        }

        /// <summary>
        /// Asynchronously returns whether the user can be locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual Task<bool> GetLockoutEnabledAsync(IdentityUser user)
        {
            return Task.FromResult(user.EnableLockout);
        }

        /// <summary>
        /// Set lockout enabled for user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public virtual Task SetLockoutEnabledAsync(IdentityUser user, bool enabled)
        {
            user.EnableLockout = enabled;
            return Task.FromResult(0);
        }
    }
}

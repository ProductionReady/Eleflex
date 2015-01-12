﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Eleflex.Security.Message;
using Eleflex.Services;
using DomainModel = Eleflex.Security;
using ServiceModel = Eleflex.Security.Message;

namespace Eleflex.Web
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity user store iterfaces
    /// </summary>
    public class IdentityUserStoreServiceClient<TUser> : IUserLoginStore<TUser>,
        IUserClaimStore<TUser>,
        IUserRoleStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserSecurityStampStore<TUser>,
        IQueryableUserStore<TUser>,
        IUserEmailStore<TUser>,
        IUserPhoneNumberStore<TUser>,
        IUserTwoFactorStore<TUser, string>,
        IUserLockoutStore<TUser, string>,
        IUserStore<TUser>
        where TUser : Eleflex.Security.User
    {

        IRoleServiceClient _roleRepository = null;
        IUserServiceClient _userRepository = null;
        IUserClaimServiceClient _userClaimRepository = null;
        IUserLoginServiceClient _userLoginRepository = null;
        IUserRoleServiceClient _userRoleRepository = null;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="userRepository"></param>
        public IdentityUserStoreServiceClient(
            IRoleServiceClient roleRepository,
            IUserServiceClient userRepository,
            IUserClaimServiceClient userClaimRepository,
            IUserLoginServiceClient userLoginRepository,
            IUserRoleServiceClient userRoleRepository
        )
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _userClaimRepository = userClaimRepository;
            _userLoginRepository = userLoginRepository;
            _userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        public IQueryable<TUser> Users
        {
            get
            {
                return new List<TUser>().AsQueryable();
            }
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task CreateAsync(TUser user)
        {
            ServiceModel.User item = new ServiceModel.User();
            AutoMapper.Mapper.Map(user, item);
            _userRepository.Insert(item);            

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Find a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<TUser> FindByIdAsync(string userId)
        {
            Guid key;
            if (!Guid.TryParse(userId, out key))
                throw new FormatException("Argument not a Guid: userId");

            IServiceCommandResponseItem<ServiceModel.User> resp = _userRepository.Get(key);
            if(resp.Item != null)
            {
                DomainModel.User item = new DomainModel.User();
                AutoMapper.Mapper.Map(resp.Item, item);
                return Task.FromResult<TUser>(item as TUser);
            }
            return Task.FromResult<TUser>(null);            
        }

        /// <summary>
        /// Find by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Task<TUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentException("Null or empty argument: userName");

            Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
            builder.IsEqual("Username", userName);
            IServiceCommandResponseItems<ServiceModel.User> resp = _userRepository.Query(builder.GetStorageQuery());

            if (resp.Items != null && resp.Items.Count > 0)
            {
                DomainModel.User item = new DomainModel.User();
                AutoMapper.Mapper.Map(resp.Items[0], item);
                return Task.FromResult<TUser>(item as TUser);
            }
            return Task.FromResult<TUser>(null);
        }

        /// <summary>
        /// Updates the UsersTable with the TUser instance values
        /// </summary>
        /// <param name="user">TUser to be updated</param>
        /// <returns></returns>
        public Task UpdateAsync(TUser user)
        {
            ServiceModel.User item = new User();
            AutoMapper.Mapper.Map(user, item);
            _userRepository.Update(item);            

            return Task.FromResult<object>(null);
        }



        /// <summary>
        /// Inserts a claim to the UserClaimsTable for the given user
        /// </summary>
        /// <param name="user">User to have claim added</param>
        /// <param name="claim">Claim to be added</param>
        /// <returns></returns>
        public Task AddClaimAsync(TUser user, Claim claim)
        {

            ServiceModel.UserClaim newClaim = new UserClaim();
            newClaim.ClaimType = claim.Type;
            newClaim.ClaimValue = claim.Value;
            newClaim.UserKey = user.UserKey;
            newClaim.StartDate = DateTimeOffset.UtcNow;
            _userClaimRepository.Insert(newClaim);

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns all claims for a given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.BeginExpression()
                .IsEqual("UserKey", user.UserKey.ToString())
                .And()
                .IsEqual("Inactive", false.ToString())
                .EndExpression()
                .And()
                .BeginExpression()
                .IsNull("EndDate")
                .Or()
                .IsLessThan("EndDate", DateTimeOffset.UtcNow.ToString())
                .EndExpression();

            var resp = _userClaimRepository.Query(builder.GetStorageQuery());

            List<Claim> claims = new List<Claim>();
            foreach (var item in resp.Items)
                claims.Add(new Claim(item.ClaimType, item.ClaimValue));

            return Task.FromResult<IList<Claim>>(claims);
        }

        /// <summary>
        /// Removes a claim froma user
        /// </summary>
        /// <param name="user">User to have claim removed</param>
        /// <param name="claim">Claim to be removed</param>
        /// <returns></returns>
        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("UserKey", user.UserKey.ToString());
            builder.IsEqual("ClaimType", claim.Type);
            builder.IsEqual("ClaimValue", claim.Value);
            var resp = _userClaimRepository.Query(builder.GetStorageQuery());
            if (resp.Items != null && resp.Items.Count > 0)
            {
                foreach (var item in resp.Items)
                    _userClaimRepository.Delete(item.UserClaimKey);
            }            

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Inserts a Login in the UserLoginsTable for a given User
        /// </summary>
        /// <param name="user">User to have login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        public Task AddLoginAsync(TUser user, UserLoginInfo login)
        {
            ServiceModel.UserLogin newItem = new ServiceModel.UserLogin();
            newItem.LoginProvider = login.LoginProvider;
            newItem.ProviderKey = login.ProviderKey;
            newItem.UserKey = user.UserKey;
            newItem.StartDate = DateTimeOffset.UtcNow;
            _userLoginRepository.Insert(newItem);            

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns an TUser based on the Login info
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("LoginProvider", login.LoginProvider);
            builder.IsEqual("ProviderKey", login.ProviderKey);
            var respLogin = _userLoginRepository.Query(builder.GetStorageQuery());

            if (respLogin.Items != null && respLogin.Items.Count > 0)
            {
                var respUser = _userRepository.Get(respLogin.Items[0].UserKey);
                DomainModel.User item = new DomainModel.User();
                AutoMapper.Mapper.Map(respUser.Item, item);
                return Task.FromResult<TUser>(item as TUser);
            }

            return Task.FromResult<TUser>(null);
        }

        /// <summary>
        /// Returns list of UserLoginInfo for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user)
        {
            List<UserLoginInfo> userLogins = new List<UserLoginInfo>();

            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("UserKey", user.UserKey.ToString());
            var respLogins = _userLoginRepository.Query(builder.GetStorageQuery());
            

            List<UserLoginInfo> logins = new List<UserLoginInfo>();
            if (respLogins.Items != null && respLogins.Items.Count > 0)
            {
                foreach (var item in respLogins.Items)
                    logins.Add(new UserLoginInfo(item.LoginProvider, item.ProviderKey));
                return Task.FromResult<IList<UserLoginInfo>>(logins);
            }
            return Task.FromResult<IList<UserLoginInfo>>(logins);
        }

        /// <summary>
        /// Deletes a login from UserLoginsTable for a given TUser
        /// </summary>
        /// <param name="user">User to have login removed</param>
        /// <param name="login">Login to be removed</param>
        /// <returns></returns>
        public Task RemoveLoginAsync(TUser user, UserLoginInfo login)
        {
            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("UserKey", user.UserKey.ToString());
            builder.IsEqual("LoginProvider", login.LoginProvider);
            builder.IsEqual("ProviderKey", login.ProviderKey);
            var respLogins = _userLoginRepository.Query(builder.GetStorageQuery());


            if (respLogins.Items != null && respLogins.Items.Count > 0)
            {
                foreach (var item in respLogins.Items)
                    _userLoginRepository.Delete(item.UserLoginKey);
            }
            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Inserts a entry in the UserRoles table
        /// </summary>
        /// <param name="user">User to have role added</param>
        /// <param name="roleName">Name of the role to be added to user</param>
        /// <returns></returns>
        public Task AddToRoleAsync(TUser user, string roleName)
        {
            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("Name", roleName);
            var respRole = _roleRepository.Query(builder.GetStorageQuery());

            if (respRole.Items != null && respRole.Items.Count > 0)
            {
                ServiceModel.UserRole newItem = new ServiceModel.UserRole();
                newItem.RoleKey = respRole.Items[0].RoleKey;
                newItem.UserKey = user.UserKey;
                newItem.StartDate = DateTimeOffset.UtcNow;
                _userRoleRepository.Insert(newItem);
            }            

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns the roles for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<string>> GetRolesAsync(TUser user)
        {
            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("UserKey", user.UserKey.ToString());
            var respUserRoles = _userRoleRepository.Query(builder.GetStorageQuery());
            string[] roleKeys = respUserRoles.Items.Select(x => x.RoleKey.ToString()).ToArray();

            List<string> list = new List<string>();
            if (roleKeys != null && roleKeys.Length > 0)
            {
                builder = new Storage.StorageQueryBuilder();
                builder.IsInSet("RoleKey", roleKeys);
                var respRoles = _roleRepository.Query(builder.GetStorageQuery());
                if (respRoles.Items != null && respRoles.Items.Count > 0)
                {
                    foreach (var item in respRoles.Items)
                        list.Add(item.Name);
                }
            }

            return Task.FromResult<IList<string>>(list);
        }

        /// <summary>
        /// Verifies if a user is in a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<bool> IsInRoleAsync(TUser user, string role)
        {
            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("Name", role);
            var respRoles = _roleRepository.Query(builder.GetStorageQuery());            

            bool found = false;
            if (respRoles.Items != null && respRoles.Items.Count > 0)
            {
                builder = new Storage.StorageQueryBuilder();
                builder.IsEqual("UserKey", user.UserKey.ToString());
                builder.IsEqual("RoleKey", respRoles.Items[0].RoleKey.ToString());
                var respUserRole = _userRoleRepository.Query(builder.GetStorageQuery());

                if (respUserRole.Items != null && respUserRole.Items.Count > 0)
                    found = true;
            }

            return Task.FromResult<bool>(found);
        }

        /// <summary>
        /// Removes a user from a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task RemoveFromRoleAsync(TUser user, string role)
        {
            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("Name", role);
            var respRoles = _roleRepository.Query(builder.GetStorageQuery());

            if (respRoles.Items != null && respRoles.Items.Count > 0)
            {
                builder = new Storage.StorageQueryBuilder();
                builder.IsEqual("UserKey", user.UserKey.ToString());
                builder.IsEqual("RoleKey", respRoles.Items[0].RoleKey.ToString());
                var respUserRoles = _userRoleRepository.Query(builder.GetStorageQuery());

                if (respUserRoles.Items != null && respUserRoles.Items.Count > 0)
                {
                    foreach (var item in respUserRoles.Items)
                        _userRoleRepository.Delete(item.UserRoleKey);
                }
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task DeleteAsync(TUser user)
        {
            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("UserKey", user.UserKey.ToString());

            var respUserRoles = _userRoleRepository.Query(builder.GetStorageQuery());
            foreach (var userRoleItem in respUserRoles.Items)
                _userRoleRepository.Delete(userRoleItem.UserRoleKey);            

            var respUserClaims = _userClaimRepository.Query(builder.GetStorageQuery());
            foreach (var userClaimItem in respUserClaims.Items)
                _userClaimRepository.Delete(userClaimItem.UserClaimKey);            

            var respUserLogins = _userLoginRepository.Query(builder.GetStorageQuery());
            foreach (var userLoginItem in respUserLogins.Items)
                _userLoginRepository.Delete(userLoginItem.UserLoginKey);            

            //var userPermissions = _userPermissionRepository.Query(builder.GetStorageQuery());
            //foreach (var userPermissionItem in userPermissions)
            //    _userPermissionRepository.Delete(userPermissionItem.UserPermissionKey);
            //_userPermissionRepository.Commit();

            _userRepository.Delete(user.UserKey);            

            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Returns the PasswordHash for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPasswordHashAsync(TUser user)
        {
            return Task.FromResult<string>(user.Password);
        }

        /// <summary>
        /// Verifies if user has password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult<bool>(!string.IsNullOrEmpty(user.Password));
        }

        /// <summary>
        /// Sets the password hash for a given TUser
        /// </summary>
        /// <param name="user"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            user.ChangePassword(passwordHash);
            return Task.FromResult<Object>(null);
        }

        /// <summary>
        ///  Set security stamp
        /// </summary>
        /// <param name="user"></param>
        /// <param name="stamp"></param>
        /// <returns></returns>
        public Task SetSecurityStampAsync(TUser user, string stamp)
        {
            user.ChangePasswordSalt(stamp);
            return Task.FromResult(0);

        }

        /// <summary>
        /// Get security stamp
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetSecurityStampAsync(TUser user)
        {
            return Task.FromResult(user.PasswordSalt);
        }

        /// <summary>
        /// Set email on user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task SetEmailAsync(TUser user, string email)
        {
            user.ChangeEmail(email);
            return Task.FromResult(0);

        }

        /// <summary>
        /// Get email from user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetEmailAsync(TUser user)
        {
            return Task.FromResult(user.Email);
        }

        /// <summary>
        /// Get if user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.EmailValid);
        }

        /// <summary>
        /// Set when user email is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            user.ChangeEmailValid(confirmed);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<TUser> FindByEmailAsync(string email)
        {
            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("Email", email);
            var respUsers = _userRepository.Query(builder.GetStorageQuery());

            if (respUsers.Items != null && respUsers.Items.Count > 0)
            {
                DomainModel.User item = new DomainModel.User();
                AutoMapper.Mapper.Map(respUsers.Items[0],item);                
                return Task.FromResult<TUser>(item as TUser);
            }

            return Task.FromResult<TUser>(null);
        }

        /// <summary>
        /// Set user phone number
        /// </summary>
        /// <param name="user"></param>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        public Task SetPhoneNumberAsync(TUser user, string phoneNumber)
        {
            user.ChangePhone(phoneNumber);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Get user phone number
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<string> GetPhoneNumberAsync(TUser user)
        {
            return Task.FromResult(user.Phone);
        }

        /// <summary>
        /// Get if user phone number is confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetPhoneNumberConfirmedAsync(TUser user)
        {
            return Task.FromResult(user.PhoneValid);
        }

        /// <summary>
        /// Set phone number if confirmed
        /// </summary>
        /// <param name="user"></param>
        /// <param name="confirmed"></param>
        /// <returns></returns>
        public Task SetPhoneNumberConfirmedAsync(TUser user, bool confirmed)
        {
            user.ChangePhoneValid(confirmed);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Set two factor authentication is enabled on the user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            user.ChangeTwoFactorAuth(enabled);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Get if two factor authentication is enabled on the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return Task.FromResult(user.TwoFactorAuth);
        }

        /// <summary>
        /// Get user lock out end date
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
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
        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            user.ChangeLockoutReinstateDate(lockoutEnd);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Increment failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            user.ChangeLoginFailedAttempts(user.LoginFailedAttempts + 1);
            return Task.FromResult(user.LoginFailedAttempts);
        }

        /// <summary>
        /// Reset failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task ResetAccessFailedCountAsync(TUser user)
        {
            user.ChangeLoginFailedAttempts(0);
            return Task.FromResult(0);
        }

        /// <summary>
        /// Get failed access count
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(user.LoginFailedAttempts);
        }

        /// <summary>
        /// Asynchronously returns whether the user can be locked out.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(user.EnableLockout);
        }

        /// <summary>
        /// Set lockout enabled for user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            user.ChangeEnableLockout(enabled);
            return Task.FromResult(0);
        }
    }
}

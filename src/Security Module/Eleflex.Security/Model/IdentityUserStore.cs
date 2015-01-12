﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eleflex.Security
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity user store iterfaces
    /// </summary>
    public class IdentityUserStore<TUser> : IUserLoginStore<TUser>,
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

        IRoleRepository _roleRepository = null;
        IUserRepository _userRepository = null;
        IUserClaimRepository _userClaimRepository = null;
        IUserLoginRepository _userLoginRepository = null;
        IUserRoleRepository _userRoleRepository = null;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="roleRepository"></param>
        /// <param name="userRepository"></param>
        public IdentityUserStore(
            IRoleRepository roleRepository,
            IUserRepository userRepository,
            IUserClaimRepository userClaimRepository,
            IUserLoginRepository userLoginRepository,
            IUserRoleRepository userRoleRepository
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
            _roleRepository.Commit();
            _userRepository.Commit();
            _userClaimRepository.Commit();
            _userLoginRepository.Commit();
            _userRoleRepository.Commit();
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
            _userRepository.Insert(user);
            _userRepository.Commit();

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Find a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<TUser> FindByIdAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentException("Null or empty argument: userId");
            Guid key;
            if (!Guid.TryParse(userId, out key))
                throw new FormatException("Argument not a Guid: userId");

            TUser item = _userRepository.Get(key) as TUser;
            _userRepository.Commit();

            return Task.FromResult<TUser>(item);
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
            IList<User> users = _userRepository.Query(builder.GetStorageQuery());
            _userRepository.Commit();

            if (users != null && users.Count > 0)
                return Task.FromResult<TUser>(users[0] as TUser);
            return Task.FromResult<TUser>(null);
        }

        /// <summary>
        /// Updates the UsersTable with the TUser instance values
        /// </summary>
        /// <param name="user">TUser to be updated</param>
        /// <returns></returns>
        public Task UpdateAsync(TUser user)
        {
            _userRepository.Update(user);
            _userRepository.Commit();

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
            UserClaim newClaim = new UserClaim();
            newClaim.ClaimType = claim.Type;
            newClaim.ClaimValue = claim.Value;
            newClaim.UserKey = user.UserKey;
            newClaim.StartDate = DateTimeOffset.UtcNow;
            _userClaimRepository.Insert(newClaim);
            _userClaimRepository.Commit();

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

            var list = _userClaimRepository.Query(builder.GetStorageQuery());

            List<Claim> claims = new List<Claim>();
            foreach (var item in list)
                claims.Add(new Claim(item.ClaimType, item.ClaimValue));

            _userClaimRepository.Commit();

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
            var list = _userClaimRepository.Query(builder.GetStorageQuery());
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                    _userClaimRepository.Delete(item.UserClaimKey);
            }
            _userClaimRepository.Commit();

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
            UserLogin newItem = new UserLogin();
            newItem.LoginProvider = login.LoginProvider;
            newItem.ProviderKey = login.ProviderKey;
            newItem.UserKey = user.UserKey;
            newItem.StartDate = DateTimeOffset.UtcNow;
            _userLoginRepository.Insert(newItem);
            _userLoginRepository.Commit();

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
            var list = _userLoginRepository.Query(builder.GetStorageQuery());

            if (list != null && list.Count > 0)
            {
                var user = _userRepository.Get(list[0].UserKey);
                return Task.FromResult<TUser>(user as TUser);
            }
            _userLoginRepository.Commit();

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
            if (user == null)
                throw new ArgumentNullException("user");

            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("UserKey", user.UserKey.ToString());
            var list = _userLoginRepository.Query(builder.GetStorageQuery());
            _userLoginRepository.Commit();

            List<UserLoginInfo> logins = new List<UserLoginInfo>();
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
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
            var list = _userLoginRepository.Query(builder.GetStorageQuery());
            _userLoginRepository.Commit();

            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
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
            var list = _roleRepository.Query(builder.GetStorageQuery());

            if (list != null && list.Count > 0)
            {
                UserRole newItem = new UserRole();
                newItem.RoleKey = list[0].RoleKey;
                newItem.UserKey = user.UserKey;
                newItem.StartDate = DateTimeOffset.UtcNow;
                _userRoleRepository.Insert(newItem);
            }
            _roleRepository.Commit();

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
            var userRoles = _userRoleRepository.Query(builder.GetStorageQuery());
            _userRoleRepository.Commit();

            string[] roleKeys = userRoles.Select(x => x.RoleKey.ToString()).ToArray();

            List<string> list = new List<string>();

            if (roleKeys != null && roleKeys.Length > 0)
            {
                builder = new Storage.StorageQueryBuilder();
                builder.IsInSet("RoleKey", roleKeys);
                var roles = _roleRepository.Query(builder.GetStorageQuery());
                _roleRepository.Commit();

                if (roles != null && roles.Count > 0)
                {
                    foreach (var item in roles)
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
            if (user == null)
                throw new ArgumentNullException("user");

            if (string.IsNullOrEmpty(role))
                throw new ArgumentNullException("role");

            Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
            builder.IsEqual("Name", role);
            var roleList = _roleRepository.Query(builder.GetStorageQuery());
            _roleRepository.Commit();

            bool found = false;
            if (roleList != null && roleList.Count > 0)
            {
                builder = new Storage.StorageQueryBuilder();
                builder.IsEqual("UserKey", user.UserKey.ToString());
                builder.IsEqual("RoleKey", roleList[0].RoleKey.ToString());
                var userRole = _userRoleRepository.Query(builder.GetStorageQuery());
                _userRoleRepository.Commit();
                if (userRole != null && userRole.Count > 0)
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
            var roleList = _roleRepository.Query(builder.GetStorageQuery());
            _roleRepository.Commit();
            
            if (roleList != null && roleList.Count > 0)
            {
                builder = new Storage.StorageQueryBuilder();
                builder.IsEqual("UserKey", user.UserKey.ToString());
                builder.IsEqual("RoleKey", roleList[0].RoleKey.ToString());
                var userRole = _userRoleRepository.Query(builder.GetStorageQuery());                
                if (userRole != null && userRole.Count > 0)
                {
                    foreach (var item in userRole)
                        _userRoleRepository.Delete(item.UserRoleKey);
                }
                _userRoleRepository.Commit();
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

            var userRoles = _userRoleRepository.Query(builder.GetStorageQuery());
            foreach(var userRoleItem in userRoles)
                _userRoleRepository.Delete(userRoleItem.UserRoleKey);
            _userRoleRepository.Commit();

            var userClaims = _userClaimRepository.Query(builder.GetStorageQuery());
            foreach (var userClaimItem in userClaims)
                _userClaimRepository.Delete(userClaimItem.UserClaimKey);
            _userClaimRepository.Commit();

            var userLogins = _userLoginRepository.Query(builder.GetStorageQuery());
            foreach (var userLoginItem in userLogins)
                _userLoginRepository.Delete(userLoginItem.UserLoginKey);
            _userLoginRepository.Commit();

            //var userPermissions = _userPermissionRepository.Query(builder.GetStorageQuery());
            //foreach (var userPermissionItem in userPermissions)
            //    _userPermissionRepository.Delete(userPermissionItem.UserPermissionKey);
            //_userPermissionRepository.Commit();

            
            _userRepository.Delete(user.UserKey);
            _userRepository.Commit();

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
            var users = _userRepository.Query(builder.GetStorageQuery());
            _userRepository.Commit();

            if (users != null && users.Count > 0)
                return Task.FromResult<TUser>(users[0] as TUser);

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

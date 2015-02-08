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
    /// Class that implements the key ASP.NET Identity user store iterfaces and communicates via service clients.
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

        /// <summary>
        /// Constructor.
        /// </summary>
        public IdentityUserStoreServiceClient()
        {
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
            try
            {
                IUserServiceClient userServiceClient = ServiceLocator.Current.GetInstance<IUserServiceClient>();
                userServiceClient.Insert(AutoMapper.Mapper.Map<Eleflex.Security.Message.User>(user));                
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Find a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Task<TUser> FindByIdAsync(string userId)
        {
            try
            {
                Guid key;
                if (!Guid.TryParse(userId, out key))
                    throw new FormatException("Argument not a Guid:" + userId);

                IUserServiceClient userServiceClient = ServiceLocator.Current.GetInstance<IUserServiceClient>();
                var respGet = userServiceClient.Get(key);
                TUser item = AutoMapper.Mapper.Map<Eleflex.Security.User>(respGet.Item) as TUser;
                return Task.FromResult<TUser>(item);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
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
            try
            {
                IUserServiceClient userServiceClient = ServiceLocator.Current.GetInstance<IUserServiceClient>();
                Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
                builder.IsEqual("Username", userName);
                builder.IsEqual("Inactive", false.ToString());
                var respUsers = userServiceClient.Query(builder.GetStorageQuery());

                if (respUsers.Items != null && respUsers.Items.Count > 0)
                    return Task.FromResult<TUser>(AutoMapper.Mapper.Map<Eleflex.Security.User>(respUsers.Items[0]) as TUser);

            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
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
            try
            {
                IUserServiceClient userServiceClient = ServiceLocator.Current.GetInstance<IUserServiceClient>();
                userServiceClient.Update(AutoMapper.Mapper.Map<Eleflex.Security.Message.User>(user));
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
            }
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
            try
            {
                IUserClaimServiceClient userClaimServiceClient = ServiceLocator.Current.GetInstance<IUserClaimServiceClient>();
                Eleflex.Security.Message.UserClaim newClaim = new Eleflex.Security.Message.UserClaim();
                newClaim.ClaimType = claim.Type;
                newClaim.ClaimValue = claim.Value;
                newClaim.UserKey = user.UserKey;
                newClaim.StartDate = DateTimeOffset.UtcNow;
                userClaimServiceClient.Insert(newClaim);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
            }

            return Task.FromResult<object>(null);
        }

        protected virtual Eleflex.Storage.StorageQueryBuilder GetUserEffectiveDateBuilder(string userKey)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
            builder.BeginExpression()
                .IsEqual("UserKey", userKey)
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
            return builder;
        }


        /// <summary>
        /// Returns all claims for a given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task<IList<Claim>> GetClaimsAsync(TUser user)
        {
            try
            {
                IUserClaimServiceClient userClaimServiceClient = ServiceLocator.Current.GetInstance<IUserClaimServiceClient>();
                Eleflex.Storage.StorageQueryBuilder builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                var list = userClaimServiceClient.Query(builder.GetStorageQuery());
                List<Claim> claims = new List<Claim>();
                foreach (var item in list.Items)
                    claims.Add(new Claim(item.ClaimType, item.ClaimValue));

                return Task.FromResult<IList<Claim>>(claims);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
            }
            return Task.FromResult<IList<Claim>>(null);
        }

        /// <summary>
        /// Removes a claim froma user
        /// </summary>
        /// <param name="user">User to have claim removed</param>
        /// <param name="claim">Claim to be removed</param>
        /// <returns></returns>
        public Task RemoveClaimAsync(TUser user, Claim claim)
        {
            try
            {
                IUserClaimServiceClient userClaimServiceClient = ServiceLocator.Current.GetInstance<IUserClaimServiceClient>();
                Eleflex.Storage.StorageQueryBuilder builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                builder.And()
                    .BeginExpression()
                    .IsEqual("ClaimType", claim.Type)
                    .And()
                    .IsEqual("ClaimValue", claim.Value)
                    .EndExpression();

                var list = userClaimServiceClient.Query(builder.GetStorageQuery());
                if (list.Items != null && list.Items.Count > 0)
                {
                    foreach (var item in list.Items)
                        userClaimServiceClient.Delete(item.UserClaimKey);
                }

                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
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
            try
            {
                IUserLoginServiceClient userLoginServiceClient = ServiceLocator.Current.GetInstance<IUserLoginServiceClient>();

                Eleflex.Security.Message.UserLogin newItem = new Eleflex.Security.Message.UserLogin();
                newItem.LoginProvider = login.LoginProvider;
                newItem.ProviderKey = login.ProviderKey;
                newItem.UserKey = user.UserKey;
                newItem.StartDate = DateTimeOffset.UtcNow;
                userLoginServiceClient.Insert(newItem);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns an TUser based on the Login info
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Task<TUser> FindAsync(UserLoginInfo login)
        {
            try
            {
                IUserLoginServiceClient userLoginServiceClient = ServiceLocator.Current.GetInstance<IUserLoginServiceClient>();

                DateTimeOffset now = DateTimeOffset.UtcNow;
                Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
                builder.BeginExpression()
                    .IsEqual("LoginProvider", login.LoginProvider)
                    .And()
                    .IsEqual("ProviderKey", login.ProviderKey)
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

                var list = userLoginServiceClient.Query(builder.GetStorageQuery());

                Eleflex.Security.Message.User user = null;
                if (list.Items != null && list.Items.Count > 0)
                {
                    IUserServiceClient userServiceClient = ServiceLocator.Current.GetInstance<IUserServiceClient>();
                    var respUser = userServiceClient.Get(list.Items[0].UserKey);
                    user = respUser.Item;
                }

                return Task.FromResult<TUser>(AutoMapper.Mapper.Map<Eleflex.Security.User>(user) as TUser);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
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
            List<UserLoginInfo> logins = new List<UserLoginInfo>();
            try
            {
                IUserLoginServiceClient userLoginServiceClient = ServiceLocator.Current.GetInstance<IUserLoginServiceClient>();

                List<UserLoginInfo> userLogins = new List<UserLoginInfo>();
                Eleflex.Storage.StorageQueryBuilder builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                var list = userLoginServiceClient.Query(builder.GetStorageQuery());
                if (list.Items != null && list.Items.Count > 0)
                {
                    foreach (var item in list.Items)
                        logins.Add(new UserLoginInfo(item.LoginProvider, item.ProviderKey));
                }

                return Task.FromResult<IList<UserLoginInfo>>(logins);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
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
            try
            {
                IUserLoginServiceClient userLoginServiceClient = ServiceLocator.Current.GetInstance<IUserLoginServiceClient>();

                Eleflex.Storage.StorageQueryBuilder builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                builder.And()
                    .BeginExpression()
                    .IsEqual("LoginProvider", login.LoginProvider)
                    .And()
                    .IsEqual("ProviderKey", login.ProviderKey)
                    .EndExpression();
                var list = userLoginServiceClient.Query(builder.GetStorageQuery());
                if (list.Items != null && list.Items.Count > 0)
                {
                    foreach (var item in list.Items)
                        userLoginServiceClient.Delete(item.UserLoginKey);
                }

                return Task.FromResult<Object>(null);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
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
            try
            {
                IRoleServiceClient roleServiceClient = ServiceLocator.Current.GetInstance<IRoleServiceClient>();

                DateTimeOffset now = DateTimeOffset.UtcNow;
                Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
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

                var list = roleServiceClient.Query(builder.GetStorageQuery());
                if (list.Items != null && list.Items.Count > 0)
                {
                    IUserRoleServiceClient userRoleServiceClient = ServiceLocator.Current.GetInstance<IUserRoleServiceClient>();

                    Eleflex.Security.Message.UserRole newItem = new Eleflex.Security.Message.UserRole();
                    newItem.RoleKey = list.Items[0].RoleKey;
                    newItem.UserKey = user.UserKey;
                    newItem.StartDate = DateTimeOffset.UtcNow;
                    userRoleServiceClient.Insert(newItem);
                }

                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
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
            IList<string> assignedRoles = GetInheritedRoles(user);
            return Task.FromResult<IList<string>>(assignedRoles);
        }


        protected virtual IList<string> GetInheritedRoles(TUser user)
        {
            List<string> assignedRoleNames = new List<string>();
            try
            {
                IUserRoleServiceClient userRoleServiceClient = ServiceLocator.Current.GetInstance<IUserRoleServiceClient>();
                IRoleRoleServiceClient roleRoleServiceClient = ServiceLocator.Current.GetInstance<IRoleRoleServiceClient>();
                IRoleServiceClient roleServiceClient = ServiceLocator.Current.GetInstance<IRoleServiceClient>();
                IRolePermissionServiceClient rolePermissionServiceClient = ServiceLocator.Current.GetInstance<IRolePermissionServiceClient>();
                IPermissionServiceClient permissionServiceClient = ServiceLocator.Current.GetInstance<IPermissionServiceClient>();
                IUserPermissionServiceClient userPermissionServiceClient = ServiceLocator.Current.GetInstance<IUserPermissionServiceClient>();

                //Get list of assigned user roles                
                Eleflex.Storage.StorageQueryBuilder builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                var assignedUserRoles = userRoleServiceClient.Query(builder.GetStorageQuery());

                //Find list of inherited roles   
                List<string> permissionKeys = new List<string>();
                List<string> assignedRoleKeys = new List<string>(assignedUserRoles.Items.Select(x => x.RoleKey.ToString()).ToArray());
                if (assignedRoleKeys.Count > 0)
                {
                    //Recursively query to find all linked roles (this gets more expensive the more complex the structure)
                    List<string> nextSet = new List<string>(assignedRoleKeys);
                    while (nextSet.Count > 0)
                    {
                        Eleflex.Storage.IStorageQuery roleRoleQuery = GetRoleRoleEffectiveDateQuery(nextSet.ToArray());
                        var inheritedRoles = roleRoleServiceClient.Query(roleRoleQuery);
                        nextSet = new List<string>();
                        foreach (var irole in inheritedRoles.Items)
                        {
                            string newRoleKey = irole.ChildRoleKey.ToString();
                            if (!assignedRoleKeys.Contains(newRoleKey))
                            {
                                //New inherited role found
                                assignedRoleKeys.Add(newRoleKey);
                                nextSet.Add(newRoleKey);
                            }
                        }
                    }

                    //Get the role names
                    var roles = roleServiceClient.Query(GetRoleEffectiveDateQuery(assignedRoleKeys.ToArray()));
                    assignedRoleNames.AddRange(roles.Items.Select(x => x.Name).ToArray());

                    //Find all permissions for all assigned roles
                    var rolePermissions = rolePermissionServiceClient.Query(GetRoleEffectiveDateQuery(assignedRoleKeys.ToArray()));
                    permissionKeys.AddRange(rolePermissions.Items.Select(x => x.PermissionKey.ToString()).ToList());
                }

                //Get user permissions
                builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                var respUserPerm = userPermissionServiceClient.Query(builder.GetStorageQuery());
                permissionKeys.AddRange(respUserPerm.Items.Select(x => x.PermissionKey.ToString()).ToList());

                if (permissionKeys.Count > 0)
                {
                    //Get permission names
                    var permissions =
                        permissionServiceClient.Query(GetPermissionEffectiveDateQuery(permissionKeys.Distinct().ToArray()));

                    //Add permissions names
                    assignedRoleNames.AddRange(permissions.Items.Select(x => x.Name).ToArray());
                }

                return assignedRoleNames.Distinct().ToList();
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
            }
            return assignedRoleNames;
        }


        protected virtual Eleflex.Storage.IStorageQuery GetRoleRoleEffectiveDateQuery(string[] inheritedRoles)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
            builder.BeginExpression()
                .IsInSet("ParentRoleKey", inheritedRoles)
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
            return builder.GetStorageQuery();
        }

        protected virtual Eleflex.Storage.IStorageQuery GetRoleEffectiveDateQuery(string[] roles)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
            builder.BeginExpression()
                .IsInSet("RoleKey", roles)
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
            return builder.GetStorageQuery();
        }

        protected virtual Eleflex.Storage.IStorageQuery GetPermissionEffectiveDateQuery(string[] permissionKeys)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;
            Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
            builder.IsInSet("PermissionKey", permissionKeys);
            builder.And();
            builder.IsEqual("Inactive", false.ToString());
            return builder.GetStorageQuery();
        }

        /// <summary>
        /// Verifies if a user is in a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<bool> IsInRoleAsync(TUser user, string role)
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
        public Task RemoveFromRoleAsync(TUser user, string role)
        {
            try
            {
                IRoleServiceClient roleServiceClient = ServiceLocator.Current.GetInstance<IRoleServiceClient>();
                IUserRoleServiceClient userRoleServiceClient = ServiceLocator.Current.GetInstance<IUserRoleServiceClient>();

                DateTimeOffset now = DateTimeOffset.UtcNow;
                Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
                builder.BeginExpression()
                    .IsEqual("Name", role)
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

                var list = roleServiceClient.Query(builder.GetStorageQuery());
                if (list.Items != null && list.Items.Count > 0)
                {
                    builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                    builder.And()
                        .BeginExpression()
                        .IsEqual("RoleKey", list.Items[0].RoleKey.ToString())
                        .EndExpression();

                    var assignedUserRoles = userRoleServiceClient.Query(builder.GetStorageQuery());
                    if (assignedUserRoles.Items != null && assignedUserRoles.Items.Count > 0)
                    {
                        foreach (var userRole in assignedUserRoles.Items)
                            userRoleServiceClient.Delete(userRole.UserRoleKey);
                    }
                }

                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
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
            try
            {
                IUserServiceClient userServiceClient = ServiceLocator.Current.GetInstance<IUserServiceClient>();

                var respItem = userServiceClient.Get(user.UserKey);
                if (respItem.Item != null)
                {
                    respItem.Item.Inactive = true;
                    userServiceClient.Update(respItem.Item);
                }

                return Task.FromResult<Object>(null);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
            }
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
            try
            {
                IUserServiceClient userServiceClient = ServiceLocator.Current.GetInstance<IUserServiceClient>();

                Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
                builder.IsEqual("Email", email);
                builder.IsEqual("Inactive", false.ToString());
                var users = userServiceClient.Query(builder.GetStorageQuery());

                Eleflex.Security.User user = null;
                if (users.Items != null && users.Items.Count > 0)
                    user = AutoMapper.Mapper.Map<Eleflex.Security.User>(users.Items[0]);

                return Task.FromResult<TUser>(user as TUser);
            }
            catch (Exception ex)
            {
                Common.Logging.LogManager.GetLogger<IdentityUserStoreServiceClient<TUser>>().Error(ex);
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
            if (user.Inactive)
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

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
    /// Class that implements the key ASP.NET Identity user store iterfaces and communicates via data storage repositories.
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

        /// <summary>
        /// Constructor.
        /// </summary>
        public IdentityUserStore()
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
                Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
                List<TUser> output = new List<TUser>();
                try
                {
                    IUserRepository userRepository = ServiceLocator.Current.GetInstance<IUserRepository>();
                    Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
                    IList<User> users = userRepository.Query(builder.GetStorageQuery());

                    uow.Commit();

                    if (users != null && users.Count > 0)
                    {
                        foreach (var user in users)
                            output.Add(user as TUser);
                    }
                    return output.AsQueryable();

                }
                catch (Exception ex)
                {
                    uow.Rollback();
                    Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
                }
                return output.AsQueryable();
            }
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task CreateAsync(TUser user)
        {
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IUserRepository userRepository = ServiceLocator.Current.GetInstance<IUserRepository>();
                userRepository.Insert(user);                
                uow.Commit();
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {                
                Guid key;
                if (!Guid.TryParse(userId, out key))
                    throw new FormatException("Argument not a Guid:" + userId);

                IUserRepository userRepository = ServiceLocator.Current.GetInstance<IUserRepository>();
                TUser item = userRepository.Get(key) as TUser;                
                uow.Commit();
                return Task.FromResult<TUser>(item);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IUserRepository userRepository = ServiceLocator.Current.GetInstance<IUserRepository>();       
                Eleflex.Storage.StorageQueryBuilder builder = new Eleflex.Storage.StorageQueryBuilder();
                builder.IsEqual("Username", userName);
                builder.IsEqual("Inactive", false.ToString());
                IList<User> users = userRepository.Query(builder.GetStorageQuery());

                uow.Commit();

                if (users != null && users.Count > 0)
                    return Task.FromResult<TUser>(users[0] as TUser);

            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IUserRepository userRepository = ServiceLocator.Current.GetInstance<IUserRepository>();
                userRepository.Update(user);
                uow.Commit();
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IUserClaimRepository userClaimRepository = ServiceLocator.Current.GetInstance<IUserClaimRepository>();
                UserClaim newClaim = new UserClaim();
                newClaim.ClaimType = claim.Type;
                newClaim.ClaimValue = claim.Value;
                newClaim.UserKey = user.UserKey;
                newClaim.StartDate = DateTimeOffset.UtcNow;
                userClaimRepository.Insert(newClaim);
                uow.Commit();
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IUserClaimRepository userClaimRepository = ServiceLocator.Current.GetInstance<IUserClaimRepository>();
                Eleflex.Storage.StorageQueryBuilder builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                var list = userClaimRepository.Query(builder.GetStorageQuery());
                List<Claim> claims = new List<Claim>();
                foreach (var item in list)
                    claims.Add(new Claim(item.ClaimType, item.ClaimValue));
                
                uow.Commit();

                return Task.FromResult<IList<Claim>>(claims);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IUserClaimRepository userClaimRepository = ServiceLocator.Current.GetInstance<IUserClaimRepository>();
                Eleflex.Storage.StorageQueryBuilder builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                builder.And()
                    .BeginExpression()
                    .IsEqual("ClaimType", claim.Type)
                    .And()
                    .IsEqual("ClaimValue", claim.Value)
                    .EndExpression();

                var list = userClaimRepository.Query(builder.GetStorageQuery());
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                        userClaimRepository.Delete(item.UserClaimKey);
                }

                uow.Commit();

                return Task.FromResult<object>(null);                
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IUserLoginRepository userLoginRepository = ServiceLocator.Current.GetInstance<IUserLoginRepository>();

                UserLogin newItem = new UserLogin();
                newItem.LoginProvider = login.LoginProvider;
                newItem.ProviderKey = login.ProviderKey;
                newItem.UserKey = user.UserKey;
                newItem.StartDate = DateTimeOffset.UtcNow;
                userLoginRepository.Insert(newItem);

                uow.Commit();
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IUserLoginRepository userLoginRepository = ServiceLocator.Current.GetInstance<IUserLoginRepository>();

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

                var list = userLoginRepository.Query(builder.GetStorageQuery());

                User user = null;
                if (list != null && list.Count > 0)
                {
                    IUserRepository userRepository = ServiceLocator.Current.GetInstance<IUserRepository>();
                    user = userRepository.Get(list[0].UserKey);                    
                }

                uow.Commit();

                return Task.FromResult<TUser>(user as TUser);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            List<UserLoginInfo> logins = new List<UserLoginInfo>();
            try
            {
                IUserLoginRepository userLoginRepository = ServiceLocator.Current.GetInstance<IUserLoginRepository>();

                List<UserLoginInfo> userLogins = new List<UserLoginInfo>();
                Eleflex.Storage.StorageQueryBuilder builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                var list = userLoginRepository.Query(builder.GetStorageQuery());
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                        logins.Add(new UserLoginInfo(item.LoginProvider, item.ProviderKey));                    
                }
                
                uow.Commit();

                return Task.FromResult<IList<UserLoginInfo>>(logins);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();            
            try
            {
                IUserLoginRepository userLoginRepository = ServiceLocator.Current.GetInstance<IUserLoginRepository>();

                Eleflex.Storage.StorageQueryBuilder builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                builder.And()
                    .BeginExpression()
                    .IsEqual("LoginProvider", login.LoginProvider)
                    .And()
                    .IsEqual("ProviderKey", login.ProviderKey)
                    .EndExpression();
                var list = userLoginRepository.Query(builder.GetStorageQuery());
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                        userLoginRepository.Delete(item.UserLoginKey);                    
                }

                uow.Commit();

                return Task.FromResult<Object>(null);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IRoleRepository roleRepository = ServiceLocator.Current.GetInstance<IRoleRepository>();

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

                var list = roleRepository.Query(builder.GetStorageQuery());
                if (list != null && list.Count > 0)
                {
                    IUserRoleRepository userRoleRepository = ServiceLocator.Current.GetInstance<IUserRoleRepository>();

                    UserRole newItem = new UserRole();
                    newItem.RoleKey = list[0].RoleKey;
                    newItem.UserKey = user.UserKey;
                    newItem.StartDate = DateTimeOffset.UtcNow;
                    userRoleRepository.Insert(newItem);
                }

                uow.Commit();

                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            List<string> assignedRoleNames = new List<string>();
            try
            {
                IUserRoleRepository userRoleRepository = ServiceLocator.Current.GetInstance<IUserRoleRepository>();
                IRoleRoleRepository roleRoleRepository = ServiceLocator.Current.GetInstance<IRoleRoleRepository>();
                IRoleRepository roleRepository = ServiceLocator.Current.GetInstance<IRoleRepository>();
                IRolePermissionRepository rolePermissionRepository = ServiceLocator.Current.GetInstance<IRolePermissionRepository>();
                IPermissionRepository permissionRepository = ServiceLocator.Current.GetInstance<IPermissionRepository>();
                IUserPermissionRepository userPermissionRepository = ServiceLocator.Current.GetInstance<IUserPermissionRepository>();

                //Get list of assigned user roles                
                Eleflex.Storage.StorageQueryBuilder builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                var assignedUserRoles = userRoleRepository.Query(builder.GetStorageQuery());

                //Find list of inherited roles   
                List<string> permissionKeys = new List<string>();
                List<string> assignedRoleKeys = new List<string>(assignedUserRoles.Select(x => x.RoleKey.ToString()).ToArray());
                if (assignedRoleKeys.Count > 0)
                {
                    //Recursively query to find all linked roles (this gets more expensive the more complex the structure)
                    List<string> nextSet = new List<string>(assignedRoleKeys);
                    while (nextSet.Count > 0)
                    {
                        Eleflex.Storage.IStorageQuery roleRoleQuery = GetRoleRoleEffectiveDateQuery(nextSet.ToArray());
                        var inheritedRoles = roleRoleRepository.Query(roleRoleQuery);
                        nextSet = new List<string>();
                        foreach (var irole in inheritedRoles)
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
                    var roles = roleRepository.Query(GetRoleEffectiveDateQuery(assignedRoleKeys.ToArray()));
                    assignedRoleNames.AddRange(roles.Select(x => x.Name).ToArray());

                    //Find all permissions for all assigned roles
                    var rolePermissions = rolePermissionRepository.Query(GetRoleEffectiveDateQuery(assignedRoleKeys.ToArray()));
                    permissionKeys.AddRange(rolePermissions.Select(x => x.PermissionKey.ToString()).ToList());
                }       
         
                //Get user permissions
                builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                var respUserPerm = userPermissionRepository.Query(builder.GetStorageQuery());
                permissionKeys.AddRange(respUserPerm.Select(x => x.PermissionKey.ToString()).ToList());

                if(permissionKeys.Count > 0)
                {
                    //Get permission names
                    var permissions =
                        permissionRepository.Query(GetPermissionEffectiveDateQuery(permissionKeys.Distinct().ToArray()));

                    //Add permissions names
                    assignedRoleNames.AddRange(permissions.Select(x => x.Name).ToArray());
                }


                uow.Commit();

                return assignedRoleNames.Distinct().ToList();
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IRoleRepository roleRepository = ServiceLocator.Current.GetInstance<IRoleRepository>();
                IUserRoleRepository userRoleRepository = ServiceLocator.Current.GetInstance<IUserRoleRepository>();

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

                var list = roleRepository.Query(builder.GetStorageQuery());
                if (list != null && list.Count > 0)
                {
                    builder = GetUserEffectiveDateBuilder(user.UserKey.ToString());
                    builder.And()
                        .BeginExpression()
                        .IsEqual("RoleKey", list[0].RoleKey.ToString())
                        .EndExpression();

                    var assignedUserRoles = userRoleRepository.Query(builder.GetStorageQuery());
                    if (assignedUserRoles != null && assignedUserRoles.Count > 0)
                    {
                        foreach (var userRole in assignedUserRoles)
                            userRoleRepository.Delete(userRole.UserRoleKey);
                    }
                }                

                uow.Commit();

                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IUserRepository userRepository = ServiceLocator.Current.GetInstance<IUserRepository>();

                TUser item = userRepository.Get(user.UserKey) as TUser;
                if (item != null)
                {
                    item.ChangeInactive(true);
                    userRepository.Update(item);
                }                

                uow.Commit();

                return Task.FromResult<Object>(null);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            Eleflex.Storage.IStorageProviderUnitOfWork uow = ServiceLocator.Current.GetInstance<Eleflex.Storage.IStorageProviderUnitOfWork>();
            try
            {
                IUserRepository userRepository = ServiceLocator.Current.GetInstance<IUserRepository>();

                Eleflex.Storage.StorageQueryBuilder builder = new Storage.StorageQueryBuilder();
                builder.IsEqual("Email", email);
                builder.IsEqual("Inactive", false.ToString());
                var users = userRepository.Query(builder.GetStorageQuery());

                User user = null;
                if (users != null && users.Count > 0)
                    user = users[0];                

                uow.Commit();

                return Task.FromResult<TUser>(user as TUser);
            }
            catch (Exception ex)
            {
                uow.Rollback();
                Common.Logging.LogManager.GetLogger<IdentityUserStore<TUser>>().Error(ex);
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
            if (user.EnableLockout)
            {
                user.ChangeLoginFailedAttempts(user.LoginFailedAttempts + 1);
            }
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

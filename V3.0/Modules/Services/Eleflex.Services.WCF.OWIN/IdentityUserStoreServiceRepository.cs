using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Eleflex.Security.ASPNetIdentity;
using Eleflex.Security.Services.WCF.Message;
using ServiceModel = Eleflex;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents an object that implements the key ASP.NET Identity user store iterfaces and communicates via data storage repositories.
    /// </summary>
    public partial class IdentityUserStoreServiceRepository : IdentityUserStore
    {

        public IdentityUserStoreServiceRepository()
            :base()

        {
        }


        protected IdentityUser _cachedUser = null;


        /// <summary>
        /// Get all users.
        /// </summary>
        public override IQueryable<IdentityUser> Users
        {
            get
            {                
                List<IdentityUser> output = new List<IdentityUser>();
                try
                {                    
                    //Get all users
                    IStorageQueryResponseItems<IdentityUser> resp = null;
                    var _userRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository>>();
                    using (var adminAccess = new ImpersonateSystem())
                        resp = _userRepository.Query(new RequestItem<IStorageQuery>() { Item = new StorageQuery() });

                    if (resp.ResponseSuccess)
                        return resp.Items.AsQueryable();
                    return null;
                }
                catch (Exception ex)
                {
                    Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
                }
                return output.AsQueryable();
            }
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override Task CreateAsync(IdentityUser user)
        {
            try
            {
                var _userRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository>>();
                using (var adminAccess = new ImpersonateSystem())
                    _userRepository.Insert(new RequestItem<IdentityUser>() { Item = user });
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Find a user.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public override Task<IdentityUser> FindByIdAsync(string userId)
        {
            if (_cachedUser != null && _cachedUser.Id == userId)
                return Task.FromResult<IdentityUser>(_cachedUser);
            try
            {
                Guid key;
                if (!Guid.TryParse(userId, out key))
                    throw new FormatException("Argument not a Guid: " + userId);

                IResponseItem<IdentityUser> resp = null;
                //var _userRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository>>();
                var repo = ObjectLocator.Current.GetInstance<ISecurityUserServiceRepository>();
                var map = ObjectLocator.Current.GetInstance<IMappingService>();
                var _userRepository = new MappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository>(repo, map);
                using (var adminAccess = new ImpersonateSystem())
                    resp = _userRepository.Get(new RequestItem<Guid>() { Item = key });                

                _cachedUser = resp.Item;

                return Task.FromResult<IdentityUser>(resp.Item);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<IdentityUser>(null);
        }

        /// <summary>
        /// Find by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public override Task<IdentityUser> FindByNameAsync(string userName)
        {
            try
            {
                StorageQueryBuilder builder = new StorageQueryBuilder();
                builder.IsEqual("Username", userName);
                builder.IsEqual("Active", true.ToString());
                IStorageQueryResponseItems<IdentityUser> resp = null;

                var _userRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository>>();
                using (var adminAccess = new ImpersonateSystem())
                    resp = _userRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });

                if (resp.ResponseSuccess && resp.Items != null && resp.Items.Count > 0)
                    return Task.FromResult<IdentityUser>(resp.Items[0] as IdentityUser);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<IdentityUser>(null);
        }

        /// <summary>
        /// Updates the UsersTable with the IdentityUser instance values
        /// </summary>
        /// <param name="user">IdentityUser to be updated</param>
        /// <returns></returns>
        public override Task UpdateAsync(IdentityUser user)
        {
            try
            {
                var _userRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository>>();
                using (var adminAccess = new ImpersonateSystem())
                    _userRepository.Update(new RequestItem<IdentityUser>() { Item = user });
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<object>(null);
        }



        /// <summary>
        /// Inserts a claim to the UserClaimsTable for the given user
        /// </summary>
        /// <param name="user">User to have claim added</param>
        /// <param name="claim">Claim to be added</param>
        /// <returns></returns>
        public override Task AddClaimAsync(IdentityUser user, Claim claim)
        {
            try
            {
                ServiceModel.SecurityUserClaim newClaim = new ServiceModel.SecurityUserClaim();
                newClaim.ClaimType = claim.Type;
                newClaim.ClaimValue = claim.Value;
                newClaim.SecurityUserKey = user.SecurityUserKey;
                newClaim.EffectiveStartDate = DateTimeOffset.UtcNow;
                var _userClaimRepository = ObjectLocator.Current.GetInstance<ISecurityUserClaimServiceRepository>();
                using (var adminAccess = new ImpersonateSystem())
                    _userClaimRepository.Insert(new RequestItem<ServiceModel.SecurityUserClaim>() { Item = newClaim });
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<object>(null);
        }


        /// <summary>
        /// Returns all claims for a given user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override Task<IList<Claim>> GetClaimsAsync(IdentityUser user)
        {
            try
            {
                StorageQueryBuilder builder = GeIdentityUserEffectiveDateBuilder(user.Id);
                IStorageQueryResponseItems<ServiceModel.SecurityUserClaim> resp = null;
                var _userClaimRepository = ObjectLocator.Current.GetInstance<ISecurityUserClaimServiceRepository>();
                using (var adminAccess = new ImpersonateSystem())
                    resp = _userClaimRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                List<Claim> claims = new List<Claim>();
                if (resp.ResponseSuccess)
                {
                    foreach (var item in resp.Items)
                        claims.Add(new Claim(item.ClaimType, item.ClaimValue));
                }
                return Task.FromResult<IList<Claim>>(claims);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<IList<Claim>>(null);
        }

        /// <summary>
        /// Removes a claim from a user
        /// </summary>
        /// <param name="user">User to have claim removed</param>
        /// <param name="claim">Claim to be removed</param>
        /// <returns></returns>
        public override Task RemoveClaimAsync(IdentityUser user, Claim claim)
        {
            try
            {
                StorageQueryBuilder builder = GeIdentityUserEffectiveDateBuilder(user.Id);
                builder.And()
                    .BeginExpression()
                    .IsEqual("ClaimType", claim.Type)
                    .And()
                    .IsEqual("ClaimValue", claim.Value)
                    .EndExpression();

                IStorageQueryResponseItems<ServiceModel.SecurityUserClaim> resp = null;
                var _userClaimRepository = ObjectLocator.Current.GetInstance<ISecurityUserClaimServiceRepository>();
                using (var adminAccess = new ImpersonateSystem())
                    resp = _userClaimRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });

                if (resp.ResponseSuccess)
                {
                    foreach (var item in resp.Items)
                    {
                        item.Active = false;
                        _userClaimRepository.Update(new RequestItem<ServiceModel.SecurityUserClaim>() { Item = item });
                    }
                }
                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Inserts a Login in the UserLoginsTable for a given User
        /// </summary>
        /// <param name="user">User to have login added</param>
        /// <param name="login">Login to be added</param>
        /// <returns></returns>
        public override Task AddLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            try
            {
                ServiceModel.SecurityUserLogin newItem = new ServiceModel.SecurityUserLogin();
                newItem.Active = true;
                newItem.LoginProvider = login.LoginProvider;
                newItem.ProviderKey = login.ProviderKey;
                newItem.SecurityUserKey = user.SecurityUserKey;
                newItem.EffectiveStartDate = DateTimeOffset.UtcNow;
                var _userLoginRepository = ObjectLocator.Current.GetInstance<ISecurityUserLoginServiceRepository>();
                using (var adminAccess = new ImpersonateSystem())
                    _userLoginRepository.Insert(new RequestItem<ServiceModel.SecurityUserLogin>() { Item = newItem });
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Returns an IdentityUser based on the Login info
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public override Task<IdentityUser> FindAsync(UserLoginInfo login)
        {           
            try
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                StorageQueryBuilder builder = new StorageQueryBuilder();
                builder.BeginExpression()
                    .IsEqual("LoginProvider", login.LoginProvider)
                    .And()
                    .IsEqual("ProviderKey", login.ProviderKey)
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

                IStorageQueryResponseItems<ServiceModel.SecurityUserLogin> resp = null;
                var _userLoginRepository = ObjectLocator.Current.GetInstance<ISecurityUserLoginServiceRepository>();
                using (var adminAccess = new ImpersonateSystem())
                    resp = _userLoginRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });

                ServiceModel.SecurityUser user = null;
                if (resp.ResponseSuccess)
                {
                    if (resp.Items != null && resp.Items.Count > 0)
                    {
                        IResponseItem<IdentityUser> respU;
                        var _userRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository>>();
                        using (var adminAccess = new ImpersonateSystem())
                            respU = _userRepository.Get(new RequestItem<Guid>() { Item = resp.Items[0].SecurityUserKey });
                        if (respU.ResponseSuccess && respU.Item != null)
                            return Task.FromResult<IdentityUser>(user as IdentityUser);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<IdentityUser>(null);
        }

        /// <summary>
        /// Returns list of UserLoginInfo for a given IdentityUser
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user)
        {
            List<UserLoginInfo> logins = new List<UserLoginInfo>();
            try
            {
                List<UserLoginInfo> userLogins = new List<UserLoginInfo>();
                StorageQueryBuilder builder = GeIdentityUserEffectiveDateBuilder(user.SecurityUserKey.ToString());
                IStorageQueryResponseItems<ServiceModel.SecurityUserLogin> resp = null;
                var _userLoginRepository = ObjectLocator.Current.GetInstance<ISecurityUserLoginServiceRepository>();
                using (var adminAccess = new ImpersonateSystem())
                    resp = _userLoginRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });

                if (resp.ResponseSuccess)
                {
                    if (resp.Items != null && resp.Items.Count > 0)
                    {
                        foreach (var item in resp.Items)
                            logins.Add(new UserLoginInfo(item.LoginProvider, item.ProviderKey));
                    }
                }
                return Task.FromResult<IList<UserLoginInfo>>(logins);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<IList<UserLoginInfo>>(logins);
        }

        /// <summary>
        /// Deletes a login from UserLoginsTable for a given IdentityUser
        /// </summary>
        /// <param name="user">User to have login removed</param>
        /// <param name="login">Login to be removed</param>
        /// <returns></returns>
        public override Task RemoveLoginAsync(IdentityUser user, UserLoginInfo login)
        {
            try
            {
                StorageQueryBuilder builder = GeIdentityUserEffectiveDateBuilder(user.SecurityUserKey.ToString());
                builder.And()
                    .BeginExpression()
                    .IsEqual("LoginProvider", login.LoginProvider)
                    .And()
                    .IsEqual("ProviderKey", login.ProviderKey)
                    .EndExpression();

                IStorageQueryResponseItems<ServiceModel.SecurityUserLogin> resp = null;
                var _userLoginRepository = ObjectLocator.Current.GetInstance<ISecurityUserLoginServiceRepository>();
                using (var adminAccess = new ImpersonateSystem())
                    resp = _userLoginRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });                

                if (resp.ResponseSuccess)
                {
                    foreach (var item in resp.Items)
                    {
                        item.Active = false;
                        using (var adminAccess = new ImpersonateSystem())
                            _userLoginRepository.Update(new RequestItem<ServiceModel.SecurityUserLogin>() { Item = item });
                    }
                }
                return Task.FromResult<Object>(null);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<Object>(null);
        }

        /// <summary>
        /// Inserts a entry in the UserRoles table
        /// </summary>
        /// <param name="user">User to have role added</param>
        /// <param name="roleName">Name of the role to be added to user</param>
        /// <returns></returns>
        public override Task AddToRoleAsync(IdentityUser user, string roleName)
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


                IStorageQueryResponseItems<IdentityRole> resp = null;
                var _roleRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository>>();
                using (var adminAccess = new ImpersonateSystem())
                    resp = _roleRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                if (resp.ResponseSuccess && resp.Items != null && resp.Items.Count > 0)
                {
                    ServiceModel.SecurityUserRole newItem = new ServiceModel.SecurityUserRole();
                    newItem.Active = true;
                    newItem.SecurityRoleKey = resp.Items[0].SecurityRoleKey;
                    newItem.SecurityUserKey = user.SecurityUserKey;
                    newItem.EffectiveStartDate = DateTimeOffset.UtcNow;

                    var _userRoleRepository = ObjectLocator.Current.GetInstance<ISecurityUserRoleServiceRepository>();
                    using (var adminAccess = new ImpersonateSystem())
                        _userRoleRepository.Insert(new RequestItem<ServiceModel.SecurityUserRole>() { Item = newItem });
                }

                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<object>(null);
        }

  
        protected override IList<string> GetInheritedRoles(IdentityUser user)
        {
            List<string> assignedRoleNames = new List<string>();
            try
            {
                //Get list of assigned user roles                
                StorageQueryBuilder builder = GeIdentityUserEffectiveDateBuilder(user.SecurityUserKey.ToString());
                IStorageQueryResponseItems<ServiceModel.SecurityUserRole> respUserRoles = null;
                var _userRoleRepository = ObjectLocator.Current.GetInstance<ISecurityUserRoleServiceRepository>();
                using (var adminAccess = new ImpersonateSystem())
                    respUserRoles = _userRoleRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                if (respUserRoles.ResponseSuccess && respUserRoles.Items != null)
                {
                    //Find list of inherited roles
                    List<string> permissionKeys = new List<string>();
                    List<string> assignedRoleKeys = new List<string>(respUserRoles.Items.Select(x => x.SecurityRoleKey.ToString()).ToArray());
                    if (assignedRoleKeys.Count > 0)
                    {
                        //Recursively query to find all linked roles (this gets more expensive the more complex the structure)
                        List<string> nextSet = new List<string>(assignedRoleKeys);
                        while (nextSet.Count > 0)
                        {
                            IStorageQuery roleRoleQuery = GetRoleRoleEffectiveDateQuery(nextSet.ToArray());
                            IStorageQueryResponseItems<ServiceModel.SecurityRoleRole> respInheritedRoles = null;
                            var _roleRoleRepository = ObjectLocator.Current.GetInstance<ISecurityRoleRoleServiceRepository>();
                            using (var adminAccess = new ImpersonateSystem())
                                respInheritedRoles = _roleRoleRepository.Query(new RequestItem<IStorageQuery>() { Item = roleRoleQuery });
                            if (respInheritedRoles.ResponseSuccess && respInheritedRoles.Items != null)
                            {
                                nextSet = new List<string>();
                                foreach (var irole in respInheritedRoles.Items)
                                {
                                    string newRoleKey = irole.ChildSecurityRoleKey.ToString();
                                    if (!assignedRoleKeys.Contains(newRoleKey))
                                    {
                                        //New inherited role found
                                        assignedRoleKeys.Add(newRoleKey);
                                        nextSet.Add(newRoleKey);
                                    }
                                }
                            }
                        }

                        //Get the role names
                        IStorageQueryResponseItems<IdentityRole> respRoles = null;
                        var _roleRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository>>();
                        using (var adminAccess = new ImpersonateSystem())
                            respRoles = _roleRepository.Query(new RequestItem<IStorageQuery>() { Item = GetRoleEffectiveDateQuery(assignedRoleKeys.ToArray()) });
                        if (respRoles.ResponseSuccess && respRoles.Items != null)
                        {
                            assignedRoleNames.AddRange(respRoles.Items.Select(x => x.Name).ToArray());

                            //Find all permissions for all assigned roles
                            IStorageQueryResponseItems<ServiceModel.SecurityRolePermission> respRolePermissions = null;
                            var _rolePermissionRepository = ObjectLocator.Current.GetInstance<ISecurityRolePermissionServiceRepository>();
                            using (var adminAccess = new ImpersonateSystem())
                                respRolePermissions = _rolePermissionRepository.Query(new RequestItem<IStorageQuery>() { Item = GetRoleEffectiveDateQuery(assignedRoleKeys.ToArray()) });
                            if (respRolePermissions.ResponseSuccess && respRolePermissions.Items != null)
                                permissionKeys.AddRange(respRolePermissions.Items.Select(x => x.SecurityPermissionKey.ToString()).ToList());
                        }
                    }

                    //Get user permissions
                    builder = GeIdentityUserEffectiveDateBuilder(user.SecurityUserKey.ToString());
                    IStorageQueryResponseItems<ServiceModel.SecurityUserPermission> respUserPerm = null;
                    var _userPermissionRepository = ObjectLocator.Current.GetInstance<ISecurityUserPermissionServiceRepository>();
                    using (var adminAccess = new ImpersonateSystem())
                        respUserPerm = _userPermissionRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                    if (respUserPerm.ResponseSuccess && respUserPerm.Items != null)
                    {
                        permissionKeys.AddRange(respUserPerm.Items.Select(x => x.SecurityPermissionKey.ToString()).ToList());

                        if (permissionKeys.Count > 0)
                        {
                            //Get permission names
                            IStorageQueryResponseItems<ServiceModel.SecurityPermission> respPermissions = null;
                            var _permissionRepository = ObjectLocator.Current.GetInstance<ISecurityPermissionServiceRepository>();
                            using (var adminAccess = new ImpersonateSystem())
                                respPermissions = _permissionRepository.Query(new RequestItem<IStorageQuery>() { Item = GetPermissionEffectiveDateQuery(permissionKeys.Distinct().ToArray()) });
                            if (respPermissions.ResponseSuccess && respPermissions.Items != null)
                            {
                                //Add permissions names
                                assignedRoleNames.AddRange(respPermissions.Items.Select(x => x.Name).ToArray());
                            }
                        }
                    }
                }

                return assignedRoleNames.Distinct().ToList();
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return assignedRoleNames;
        }

        
        /// <summary>
        /// Removes a user from a role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public override Task RemoveFromRoleAsync(IdentityUser user, string role)
        {
            try
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                StorageQueryBuilder builder = new StorageQueryBuilder();
                builder.BeginExpression()
                    .IsEqual("Name", role)
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

                IStorageQueryResponseItems<IdentityRole> respRole = null;
                var _roleRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository>>();
                using (var adminAccess = new ImpersonateSystem())
                    respRole = _roleRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                if (respRole.ResponseSuccess && respRole.Items != null && respRole.Items.Count > 0)
                {
                    builder = GeIdentityUserEffectiveDateBuilder(user.SecurityUserKey.ToString());
                    builder.And()
                        .BeginExpression()
                        .IsEqual("SecurityRoleKey", respRole.Items[0].SecurityRoleKey.ToString())
                        .EndExpression();

                    IStorageQueryResponseItems<ServiceModel.SecurityUserRole> respAssignedUserRoles = null;
                    var _userRoleRepository = ObjectLocator.Current.GetInstance<ISecurityUserRoleServiceRepository>();
                    using (var adminAccess = new ImpersonateSystem())
                        respAssignedUserRoles = _userRoleRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                    if (respAssignedUserRoles.ResponseSuccess && respRole.Items != null && respRole.Items.Count > 0)
                    {
                        foreach (var item in respAssignedUserRoles.Items)
                        {
                            item.Active = false;
                            using (var adminAccess = new ImpersonateSystem())
                                _userRoleRepository.Update(new RequestItem<ServiceModel.SecurityUserRole>() { Item = item });
                        }
                    }
                }

                return Task.FromResult<object>(null);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override Task DeleteAsync(IdentityUser user)
        {
            try
            {
                IResponseItem<IdentityUser> resp = null;
                var _userRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository>>();
                using (var adminAccess = new ImpersonateSystem())
                    resp = _userRepository.Get(new RequestItem<Guid>() { Item = user.SecurityUserKey });
                if (resp.ResponseSuccess && resp.Item != null)
                {
                    IdentityUser item = resp.Item as IdentityUser;
                    if (item != null)
                    {
                        item.Active = false;
                        using (var adminAccess = new ImpersonateSystem())
                            _userRepository.Update(new RequestItem<IdentityUser>() { Item = item });
                    }
                }

                return Task.FromResult<Object>(null);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<Object>(null);
        }
        
        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public override Task<IdentityUser> FindByEmailAsync(string email)
        {
            try
            {
                StorageQueryBuilder builder = new StorageQueryBuilder();
                builder.IsEqual("Email", email);
                builder.IsEqual("Active", true.ToString());

                IStorageQueryResponseItems<IdentityUser> respUsers = null;
                var _userRepository = ObjectLocator.Current.GetInstance<IMappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository>>();
                using (var adminAccess = new ImpersonateSystem())
                    respUsers = _userRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                if (respUsers.ResponseSuccess && respUsers.Items != null && respUsers.Items.Count > 0)
                    return Task.FromResult<IdentityUser>(respUsers.Items[0] as IdentityUser);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<IdentityUserStoreServiceRepository>(ex);
            }
            return Task.FromResult<IdentityUser>(null);
        }
        
    }
}

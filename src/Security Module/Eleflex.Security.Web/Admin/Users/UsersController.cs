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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Eleflex.Storage;
using Eleflex.Security.Message;
using Eleflex.Security.Message.UserCommand;
using Eleflex.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Eleflex.Security.Web.Security.Users
{
    /// <summary>
    /// Security Users.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        /// <summary>
        /// user service client.
        /// </summary>
        protected IUserServiceClient _userServiceClient;

        protected IUserRoleServiceClient _userRoleServiceClient;
        protected IUserPermissionServiceClient _userPermissionServiceClient;
        protected IUserClaimServiceClient _userClaimServiceClient;
        protected IRoleServiceClient _roleServiceClient;
        protected IPermissionServiceClient _permissionServiceClient;

        /// <summary>
        /// Instance for UserManager property. Use property instead of this variable.
        /// </summary>
        protected ApplicationUserManager _userManager;

        

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userServiceClient"></param>
        public UsersController(
            IUserServiceClient userServiceClient,
            IUserRoleServiceClient userRoleServiceClient,
            IUserPermissionServiceClient userPermissionServiceClient,
            IUserClaimServiceClient userClaimServiceClient,
            IRoleServiceClient roleServiceClient,
            IPermissionServiceClient permissionServiceClient)
        {
            _userServiceClient = userServiceClient;
            _userRoleServiceClient = userRoleServiceClient;
            _userPermissionServiceClient = userPermissionServiceClient;
            _userClaimServiceClient = userClaimServiceClient;
            _roleServiceClient = roleServiceClient;
            _permissionServiceClient = permissionServiceClient;
        }

        /// <summary>
        /// Get user manager
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Create()
        {
            EditViewModel model = new EditViewModel();
            model.EnableLockout = true;
            return View("Edit", model);
        }
        

        /// <summary>
        /// List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult List()
        {
            ListViewModel model = new ListViewModel();
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.Paging(1, StorageConstants.MAX_RETURNED_RECORDS_DEFAULT);
            builder.Sort("Email", true);
            var response = _userServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(response))
                return View(model);

            model.Items = response.Items;
            return View(model);
        }

        /// <summary>
        /// List.
        /// </summary>
        /// <returns></returns>        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult List(ListViewModel model)
        {
            StorageQueryBuilder builder = new StorageQueryBuilder();
            if (model != null)
            {
                if (!string.IsNullOrWhiteSpace(model.Inactive))
                    builder.IsEqual("Inactive", model.Inactive);
                if (!string.IsNullOrWhiteSpace(model.Email))
                    builder.Contains("Email", model.Email);
                if (!string.IsNullOrWhiteSpace(model.Username))
                    builder.Contains("Username", model.Username);
                if (!string.IsNullOrWhiteSpace(model.FirstName))
                    builder.Contains("FirstName", model.FirstName);
                if (!string.IsNullOrWhiteSpace(model.LastName))
                    builder.Contains("LastName", model.LastName);
            }

            if (model != null && model.MaxRecords.HasValue)
                builder.Paging(1, model.MaxRecords.Value);
            else
                builder.Paging(1, StorageConstants.MAX_RETURNED_RECORDS_DEFAULT);

            builder.Sort("Email", true);
            var response = _userServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(response))
                return View(model);

            model.Items = response.Items;
            return View(model);
        }

        /// <summary>
        /// Edit.
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        public ActionResult Edit(Guid userKey)
        {
            var resp = _userServiceClient.Get(userKey);
            if (resp.Item == null)
                throw new Exception("User not found for key:" + userKey.ToString());

            EditViewModel viewModel = AutoMapper.Mapper.Map<EditViewModel>(resp.Item);
            return View(viewModel);
        }

        /// <summary>
        /// Details.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            EditViewModel newModel = null;
            if (model.UserKey.HasValue)
            {
                Eleflex.Security.User user = await UserManager.FindByIdAsync(model.UserKey.Value.ToString());
                if (user == null)
                {
                    ModelState.AddModelError("", "User does not exist.");
                    return View(model);
                }

                if (user.Inactive != model.Inactive)
                {
                    //Updating inactive to be active, check if another record already in use
                    if (user.Inactive)
                    {
                        var findUsernameResult = await UserManager.FindByNameAsync(model.Username);
                        if (findUsernameResult != null)
                        {
                            ModelState.AddModelError("", "Username is already in use.");
                            return View(model);
                        }
                        var findEmailResult = await UserManager.FindByEmailAsync(model.Email);
                        if (findEmailResult != null)
                        {
                            ModelState.AddModelError("", "Email is already in use.");
                            return View(model);
                        }
                    }
                    user.ChangeInactive(model.Inactive);
                }
                if (string.Compare(user.Username, model.Username, true) != 0)
                {
                    if (!user.Inactive)
                    {
                        //Updating username, check if already in use
                        var findUsernameResult = await UserManager.FindByNameAsync(model.Username);
                        if (findUsernameResult != null)
                        {
                            ModelState.AddModelError("", "Username is already in use.");
                            return View(model);
                        }
                    }
                    user.ChangeUsername(model.Username);
                }
                if (string.Compare(user.Email, model.Email, true) != 0)
                {
                    if (!user.Inactive)
                    {
                        //Updating email, check if already in use
                        var findEmailResult = await UserManager.FindByEmailAsync(model.Email);
                        if (findEmailResult != null)
                        {
                            ModelState.AddModelError("", "Email is already in use.");
                            return View(model);
                        }
                    }
                    user.ChangeEmail(model.Email);
                }

                user.ChangeFirstName(model.FirstName);
                user.ChangeLastName(model.LastName);
                user.ChangeComment(model.Comment);
                user.ChangePhone(model.Phone);

                //Call service instead of UserManager (we checked requirements above)
                var respUpdate = _userServiceClient.Update(AutoMapper.Mapper.Map<Eleflex.Security.Message.User>(user));
                if (ModelState.IsServiceError(respUpdate))
                    if (respUpdate.ResponseStatus.IsError)
                        return View(model);

                newModel = AutoMapper.Mapper.Map<EditViewModel>(user);
                newModel.SuccessMessage = "User updated.";
            }
            else
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                string tempPass = Guid.NewGuid().ToString() + "aaAA11!!";
                Eleflex.Security.User item = new Eleflex.Security.User();
                item.ChangeUserKey(Guid.NewGuid());
                item.ChangeCreateDate(now);
                item.ChangeEmail(model.Email);
                item.ChangeUsername(model.Username);
                item.ChangeFirstName(model.FirstName);
                item.ChangeLastName(model.LastName);
                item.ChangeComment(model.Comment);
                item.ChangeInactive(model.Inactive);
                item.ChangePhone(model.Phone);
                item.ChangePasswordLastChangeDate(now);
                item.ChangeEnableLockout(true);

                if (model.Inactive)
                {
                    //No validation checks if inactive, call service instead of UserManager
                    item.ChangePassword(UserManager.PasswordHasher.HashPassword(tempPass));
                    item.ChangePasswordSalt(Guid.NewGuid().ToString());
                    var respInsert = _userServiceClient.Insert(AutoMapper.Mapper.Map<Eleflex.Security.Message.User>(item));
                    if (ModelState.IsServiceError(respInsert))
                        return View(model);
                    newModel = AutoMapper.Mapper.Map<EditViewModel>(respInsert.Item);
                }
                else
                {
                    //UserManager will check if username or email already exists
                    var result = await UserManager.CreateAsync(item, tempPass);
                    if (result.Succeeded)
                    {
                        newModel = AutoMapper.Mapper.Map<EditViewModel>(item);
                    }
                    else
                    {
                        foreach (string err in result.Errors)
                            ModelState.AddModelError("", err);
                        return View(model); //Return previous values
                    }
                }
                //Add default user role
                await UserManager.AddToRoleAsync(newModel.UserKey.ToString(), Eleflex.Security.SecurityConstants.ROLE_USER);
                newModel.SuccessMessage = "User created.";
                ModelState.Clear(); //update the hiddenfield
            }
            return View(newModel);
        }

        /// <summary>
        /// Reset.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LockoutUser(Guid userKey, DateTimeOffset? lockoutDate)
        {
            var resp = _userServiceClient.Get(userKey);
            if (resp.Item == null)
                return Json(AjaxResult.Error("Service error."));

            var item = resp.Item;
            item.LockoutReinstateDate = lockoutDate;
            item.LoginFailedAttempts = 0;
            var respUpdate = _userServiceClient.Update(item);
            if (ModelState.IsServiceError(respUpdate))
                return Json(AjaxResult.Error("Service error."));

            return Json(AjaxResult.Success("Lockout date changed.", respUpdate.Item));
        }


        [HttpGet]
        public ActionResult Roles(Guid userKey)
        {            
            //Get user and set needed props
            RolesViewModel model = new RolesViewModel();
            var respUser =_userServiceClient.Get(userKey);
            if (ModelState.IsServiceError(respUser))
                return View(model);
            model.UserKey = respUser.Item.UserKey;
            model.Username = respUser.Item.Username;
            model.Email = respUser.Item.Email;

            //Get user roles
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.IsEqual("UserKey", respUser.Item.UserKey.ToString());
            var respUserRoles = _userRoleServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(respUserRoles))
                return View(model);

            //Get Role Names
            string[] roleKeys = respUserRoles.Items.Select(x=>x.RoleKey.ToString()).Distinct().ToArray();
            builder = new StorageQueryBuilder();
            builder.IsInSet("RoleKey",roleKeys);
            var respRoles = _roleServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(respUserRoles))
                return View(model);

            //Build new userrole model
            foreach (var userRole in respUserRoles.Items)
            {
                UserRoleViewModel role = AutoMapper.Mapper.Map<UserRoleViewModel>(userRole);
                role.RoleName = respRoles.Items.Where(x => x.RoleKey == userRole.RoleKey).Select(x => x.Name).FirstOrDefault();
                model.UserRoles.Add(role);
            }

            return View(model);
        }

        /// <summary>
        /// EditRole assignment.
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        public ActionResult EditRole(Guid userKey, long? userRoleKey = null)
        {
            EditRoleViewModel model = new EditRoleViewModel();            
            if(userRoleKey.HasValue)
            {
                var respUserRole = _userRoleServiceClient.Get(userRoleKey.Value);
                if (ModelState.IsServiceError(respUserRole))
                    return View(model);
                model = AutoMapper.Mapper.Map<EditRoleViewModel>(respUserRole.Item);
                var respRole = _roleServiceClient.Get(model.RoleKey.Value);
                if (ModelState.IsServiceError(respRole))
                    return View(model);
                model.SelectedRole = respRole.Item.Name;
            }
            ModelState.Clear();
            model.UserKey = userKey;
            model.UserRoleKey = userRoleKey;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(EditRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.RoleKey.HasValue)
                {
                    var respRole = _roleServiceClient.Get(model.RoleKey.Value);
                    if (ModelState.IsServiceError(respRole))
                        return View(model);
                    model.SelectedRole = respRole.Item.Name;
                }
                return View(model);
            }
            
            if(model.UserRoleKey.HasValue)
            {
                var respUserRole = _userRoleServiceClient.Get(model.UserRoleKey.Value);
                if (ModelState.IsServiceError(respUserRole))
                    return View(model);

                var item = AutoMapper.Mapper.Map<Eleflex.Security.Message.UserRole>(model);
                var respUserRoleUpdate = _userRoleServiceClient.Update(item);
                EditRoleViewModel newModel = AutoMapper.Mapper.Map<EditRoleViewModel>(respUserRoleUpdate.Item);

                var respRole = _roleServiceClient.Get(model.RoleKey.Value);
                if (ModelState.IsServiceError(respRole))
                    return View(model);
                newModel.SelectedRole = respRole.Item.Name;
                newModel.SuccessMessage = "Assignment updated.";

                return View(newModel);
            }
            else
            {
                var item = AutoMapper.Mapper.Map<Eleflex.Security.Message.UserRole>(model);
                var respInsert = _userRoleServiceClient.Insert(item);
                EditRoleViewModel newModel = AutoMapper.Mapper.Map<EditRoleViewModel>(respInsert.Item);

                var respRole = _roleServiceClient.Get(model.RoleKey.Value);
                if (ModelState.IsServiceError(respRole))
                    return View(model);
                newModel.SelectedRole = respRole.Item.Name;
                newModel.SuccessMessage = "Assignment created.";
                ModelState.Clear();
                return View(newModel);
            }
        }

        /// <summary>
        /// EditRole search.
        /// </summary>
        /// <returns></returns>        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoleSearch(string roleName)
        {
            SelectRoleViewModel newModel = new SelectRoleViewModel();
            StorageQueryBuilder builder = new StorageQueryBuilder();
            if (!string.IsNullOrEmpty(roleName))
                builder.Contains("Name", roleName);
            builder.Paging(1, 50);
            builder.Sort("Name", true);
            var response = _roleServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(response))
                return View(newModel);
            
            newModel.SearchName = roleName;
            newModel.SearchRoles = response.Items;
            return PartialView("SelectRole", newModel);
        }


        [HttpGet]
        public ActionResult Permissions(Guid userKey)
        {
            //Get user and set needed props
            PermissionsViewModel model = new PermissionsViewModel();
            var respUser = _userServiceClient.Get(userKey);
            if (ModelState.IsServiceError(respUser))
                return View(model);
            model.UserKey = respUser.Item.UserKey;
            model.Username = respUser.Item.Username;
            model.Email = respUser.Item.Email;

            //Get user permissions
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.IsEqual("UserKey", respUser.Item.UserKey.ToString());
            var respUserPermissions = _userPermissionServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(respUserPermissions))
                return View(model);

            //Get Permission Names
            string[] permissionKeys = respUserPermissions.Items.Select(x => x.PermissionKey.ToString()).Distinct().ToArray();
            builder = new StorageQueryBuilder();
            builder.IsInSet("PermissionKey", permissionKeys);
            var respPermissions = _permissionServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(respUserPermissions))
                return View(model);

            //Build new userpermission model
            foreach (var userPermission in respUserPermissions.Items)
            {
                UserPermissionViewModel permission = AutoMapper.Mapper.Map<UserPermissionViewModel>(userPermission);
                permission.PermissionName = respPermissions.Items.Where(x => x.PermissionKey == userPermission.PermissionKey).Select(x => x.Name).FirstOrDefault();
                model.UserPermissions.Add(permission);
            }

            return View(model);
        }

        /// <summary>
        /// EditPermission assignment.
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        public ActionResult EditPermission(Guid userKey, long? userPermissionKey = null)
        {
            EditPermissionViewModel model = new EditPermissionViewModel();
            if (userPermissionKey.HasValue)
            {
                var respUserPermission = _userPermissionServiceClient.Get(userPermissionKey.Value);
                if (ModelState.IsServiceError(respUserPermission))
                    return View(model);
                model = AutoMapper.Mapper.Map<EditPermissionViewModel>(respUserPermission.Item);
                var respPermission = _permissionServiceClient.Get(model.PermissionKey.Value);
                if (ModelState.IsServiceError(respPermission))
                    return View(model);
                model.SelectedPermission = respPermission.Item.Name;
            }
            ModelState.Clear();
            model.UserKey = userKey;
            model.UserPermissionKey = userPermissionKey;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPermission(EditPermissionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.PermissionKey.HasValue)
                {
                    var respPermission = _permissionServiceClient.Get(model.PermissionKey.Value);
                    if (ModelState.IsServiceError(respPermission))
                        return View(model);
                    model.SelectedPermission = respPermission.Item.Name;
                }
                return View(model);
            }

            if (model.UserPermissionKey.HasValue)
            {
                var respUserPermission = _userPermissionServiceClient.Get(model.UserPermissionKey.Value);
                if (ModelState.IsServiceError(respUserPermission))
                    return View(model);

                var item = AutoMapper.Mapper.Map<Eleflex.Security.Message.UserPermission>(model);
                var respUserPermissionUpdate = _userPermissionServiceClient.Update(item);
                EditPermissionViewModel newModel = AutoMapper.Mapper.Map<EditPermissionViewModel>(respUserPermissionUpdate.Item);

                var respPermission = _permissionServiceClient.Get(model.PermissionKey.Value);
                if (ModelState.IsServiceError(respPermission))
                    return View(model);
                newModel.SelectedPermission = respPermission.Item.Name;
                newModel.SuccessMessage = "Assignment updated.";

                return View(newModel);
            }
            else
            {
                var item = AutoMapper.Mapper.Map<Eleflex.Security.Message.UserPermission>(model);
                var respInsert = _userPermissionServiceClient.Insert(item);
                EditPermissionViewModel newModel = AutoMapper.Mapper.Map<EditPermissionViewModel>(respInsert.Item);

                var respPermission = _permissionServiceClient.Get(model.PermissionKey.Value);
                if (ModelState.IsServiceError(respPermission))
                    return View(model);
                newModel.SelectedPermission = respPermission.Item.Name;
                newModel.SuccessMessage = "Assignment created.";
                ModelState.Clear();
                return View(newModel);
            }
        }

        /// <summary>
        /// EditPermission search.
        /// </summary>
        /// <returns></returns>        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPermissionSearch(string permissionName)
        {
            SelectPermissionViewModel newModel = new SelectPermissionViewModel();
            StorageQueryBuilder builder = new StorageQueryBuilder();
            if (!string.IsNullOrEmpty(permissionName))
                builder.Contains("Name", permissionName);
            builder.Paging(1, 50);
            builder.Sort("Name", true);
            var response = _permissionServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(response))
                return View(newModel);

            newModel.SearchName = permissionName;
            newModel.SearchPermissions = response.Items;
            return PartialView("SelectPermission", newModel);
        }

        [HttpGet]
        public ActionResult Claims(Guid userKey)
        {
            //Get user and set needed props
            ClaimsViewModel model = new ClaimsViewModel();
            var respUser = _userServiceClient.Get(userKey);
            if (ModelState.IsServiceError(respUser))
                return View(model);
            model.UserKey = respUser.Item.UserKey;
            model.Username = respUser.Item.Username;
            model.Email = respUser.Item.Email;

            //Get user claims
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.IsEqual("UserKey", respUser.Item.UserKey.ToString());
            var respUserClaims = _userClaimServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(respUserClaims))
                return View(model);

            //Build new userclaim model
            foreach (var userClaim in respUserClaims.Items)
            {
                UserClaimViewModel claim = AutoMapper.Mapper.Map<UserClaimViewModel>(userClaim);
                model.UserClaims.Add(claim);
            }

            return View(model);
        }

        /// <summary>
        /// EditClaim assignment.
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        public ActionResult EditClaim(Guid userKey, long? userClaimKey = null)
        {
            EditClaimViewModel model = new EditClaimViewModel();
            if (userClaimKey.HasValue)
            {
                var respUserClaim = _userClaimServiceClient.Get(userClaimKey.Value);
                if (ModelState.IsServiceError(respUserClaim))
                    return View(model);
                model = AutoMapper.Mapper.Map<EditClaimViewModel>(respUserClaim.Item);
            }
            ModelState.Clear();
            model.UserKey = userKey;
            model.UserClaimKey = userClaimKey;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClaim(EditClaimViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.UserClaimKey.HasValue)
            {
                var respUserClaim = _userClaimServiceClient.Get(model.UserClaimKey.Value);
                if (ModelState.IsServiceError(respUserClaim))
                    return View(model);

                var item = AutoMapper.Mapper.Map<Eleflex.Security.Message.UserClaim>(model);
                var respUserClaimUpdate = _userClaimServiceClient.Update(item);
                EditClaimViewModel newModel = AutoMapper.Mapper.Map<EditClaimViewModel>(respUserClaimUpdate.Item);

                newModel.SuccessMessage = "Claim updated.";
                return View(newModel);
            }
            else
            {
                var item = AutoMapper.Mapper.Map<Eleflex.Security.Message.UserClaim>(model);
                var respInsert = _userClaimServiceClient.Insert(item);
                EditClaimViewModel newModel = AutoMapper.Mapper.Map<EditClaimViewModel>(respInsert.Item);

                newModel.SuccessMessage = "Claim created.";
                ModelState.Clear();
                return View(newModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetEffectiveSecurity(Guid userKey)
        {
            EffectiveSecurityViewModel model = new EffectiveSecurityViewModel();
            model.Roles = UserManager.GetRoles(userKey.ToString()).ToList();
            model.Roles.Sort();
            return PartialView("EffectiveSecurity", model);
        }
        
    }
}

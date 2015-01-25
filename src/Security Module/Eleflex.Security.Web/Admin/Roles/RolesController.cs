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
using Eleflex.Security.Message.RoleCommand;
using Eleflex.Web;
using Microsoft.AspNet.Identity.Owin;

namespace Eleflex.Security.Web.Security.Roles
{
    /// <summary>
    /// Security Rolese.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        /// <summary>
        /// Role service client.
        /// </summary>
        protected IRoleServiceClient _roleServiceClient;
        /// <summary>
        /// Role Role service client.
        /// </summary>
        protected IRoleRoleServiceClient _roleRoleServiceClient;
        /// <summary>
        /// Role Permission service client.
        /// </summary>
        protected IRolePermissionServiceClient _rolePermissionServiceClient;
        /// <summary>
        /// Permission service client.
        /// </summary>
        protected IPermissionServiceClient _permissionServiceClient;
        /// <summary>
        /// Instance for UserManager property. Use property instead of this variable.
        /// </summary>
        protected ApplicationUserManager _userManager;
        /// <summary>
        /// Instance for RoleManager property. Use property instead of this variable.
        /// </summary>
        protected ApplicationRoleManager _roleManager;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="roleServiceClient"></param>
        public RolesController(
            IRoleServiceClient roleServiceClient,
            IRoleRoleServiceClient roleRoleServiceClient,
            IRolePermissionServiceClient rolePermissionServiceClient,
            IPermissionServiceClient permissionServiceClient)
        {
            _roleServiceClient = roleServiceClient;
            _roleRoleServiceClient = roleRoleServiceClient;
            _rolePermissionServiceClient = rolePermissionServiceClient;
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
        /// Get role manager
        /// </summary>
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
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
            builder.Sort("Name", true);
            var response = _roleServiceClient.Query(builder.GetStorageQuery());
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
                if (!string.IsNullOrWhiteSpace(model.Name))
                    builder.Contains("Name", model.Name);
                if (!string.IsNullOrWhiteSpace(model.Description))
                    builder.Contains("Description", model.Description);
            }

            if (model != null && model.MaxRecords.HasValue)
                builder.Paging(1, model.MaxRecords.Value);
            else
                builder.Paging(1, StorageConstants.MAX_RETURNED_RECORDS_DEFAULT);

            builder.Sort("Name", true);
            var response = _roleServiceClient.Query(builder.GetStorageQuery());
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
        public ActionResult Edit(Guid roleKey)
        {
            var resp = _roleServiceClient.Get(roleKey);
            if (resp.Item == null)
                throw new Exception("Role not found for key:" + roleKey.ToString());

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
            if (model.RoleKey.HasValue)
            {
                Eleflex.Security.Role role = await RoleManager.FindByIdAsync(model.RoleKey.Value.ToString());

                if (role == null)
                {
                    ModelState.AddModelError("", "Role does not exist.");
                    return View(model);
                }

                if (role.Inactive != model.Inactive)
                {
                    //Updating inactive to be active, check if another record already in use
                    if (role.Inactive)
                    {
                        var findNameResult = await RoleManager.FindByNameAsync(model.Name);
                        if (findNameResult != null)
                        {
                            ModelState.AddModelError("", "Name is already in use.");
                            return View(model);
                        }
                    }
                    role.ChangeInactive(model.Inactive);
                }
                if (string.Compare(role.Name, model.Name, true) != 0)
                {
                    if (!role.Inactive)
                    {
                        //Updating username, check if already in use
                        var findNameResult = await RoleManager.FindByNameAsync(model.Name);
                        if (findNameResult != null)
                        {
                            ModelState.AddModelError("", "Name is already in use.");
                            return View(model);
                        }
                    }
                    role.ChangeName(model.Name);
                }

                role.ChangeDescription(model.Description);
                role.ChangeEndDate(model.EndDate);
                role.ChangeStartDate(model.StartDate);

                //Call service instead of RoleManager (we checked requirements above)
                var respUpdate = _roleServiceClient.Update(AutoMapper.Mapper.Map<Eleflex.Security.Message.Role>(role));
                if (ModelState.IsServiceError(respUpdate))
                    if (respUpdate.ResponseStatus.IsError)
                        return View(model);

                newModel = AutoMapper.Mapper.Map<EditViewModel>(role);
                newModel.SuccessMessage = "Role updated.";
            }
            else
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                Eleflex.Security.Role item = new Eleflex.Security.Role();
                item.ChangeRoleKey(Guid.NewGuid());
                item.ChangeDescription(model.Description);
                item.ChangeInactive(model.Inactive);
                item.ChangeName(model.Name);
                item.ChangeDescription(model.Description);
                item.ChangeEndDate(model.EndDate);
                item.ChangeStartDate(model.StartDate);

                if (model.Inactive)
                {
                    //No validation checks if inactive, call service instead of RoleManager
                    var respInsert = _roleServiceClient.Insert(AutoMapper.Mapper.Map<Eleflex.Security.Message.Role>(item));
                    if (ModelState.IsServiceError(respInsert))
                        return View(model);
                    newModel = AutoMapper.Mapper.Map<EditViewModel>(respInsert.Item);
                }
                else
                {
                    //RoleManager will check if username or email already exists
                    var result = await RoleManager.CreateAsync(item);
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
                newModel.SuccessMessage = "Role created.";
                ModelState.Clear(); //update the hiddenfield
            }
            return View(newModel);
        }




        [HttpGet]
        public ActionResult Roles(Guid roleKey)
        {
            //Get role and set needed props
            RolesViewModel model = new RolesViewModel();
            var respRole = _roleServiceClient.Get(roleKey);
            if (ModelState.IsServiceError(respRole))
                return View(model);
            model.RoleKey = respRole.Item.RoleKey;
            model.RoleName = respRole.Item.Name;

            //Get role roles
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.IsEqual("ParentRoleKey", respRole.Item.RoleKey.ToString());
            var respRoleRoles = _roleRoleServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(respRoleRoles))
                return View(model);

            //Get Role Names
            string[] roleKeys = respRoleRoles.Items.Select(x => x.ChildRoleKey.ToString()).Distinct().ToArray();
            builder = new StorageQueryBuilder();
            builder.IsInSet("RoleKey", roleKeys);
            var respRoles = _roleServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(respRoleRoles))
                return View(model);

            //Build new rolerole model
            foreach (var roleRole in respRoleRoles.Items)
            {
                RoleRoleViewModel role = AutoMapper.Mapper.Map<RoleRoleViewModel>(roleRole);
                role.RoleName = respRoles.Items.Where(x => x.RoleKey == roleRole.ChildRoleKey).Select(x => x.Name).FirstOrDefault();
                model.RoleRoles.Add(role);
            }

            return View(model);
        }

        /// <summary>
        /// EditRole assignment.
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        public ActionResult EditRole(Guid roleKey, long? roleRoleKey = null)
        {
            EditRoleViewModel model = new EditRoleViewModel();
            if (roleRoleKey.HasValue)
            {
                var respRoleRole = _roleRoleServiceClient.Get(roleRoleKey.Value);
                if (ModelState.IsServiceError(respRoleRole))
                    return View(model);
                model = AutoMapper.Mapper.Map<EditRoleViewModel>(respRoleRole.Item);
                var respRole = _roleServiceClient.Get(model.ChildRoleKey.Value);
                if (ModelState.IsServiceError(respRole))
                    return View(model);
                model.SelectedRole = respRole.Item.Name;
            }
            ModelState.Clear();
            model.ParentRoleKey = roleKey;
            model.RoleRoleKey = roleRoleKey;            

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(EditRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.ChildRoleKey.HasValue)
                {
                    var respRole = _roleServiceClient.Get(model.ChildRoleKey.Value);
                    if (ModelState.IsServiceError(respRole))
                        return View(model);
                    model.SelectedRole = respRole.Item.Name;
                }
                return View(model);
            }

            if (model.RoleRoleKey.HasValue)
            {
                var respRoleRole = _roleRoleServiceClient.Get(model.RoleRoleKey.Value);
                if (ModelState.IsServiceError(respRoleRole))
                    return View(model);

                var item = AutoMapper.Mapper.Map<Eleflex.Security.Message.RoleRole>(model);
                var respRoleRoleUpdate = _roleRoleServiceClient.Update(item);
                EditRoleViewModel newModel = AutoMapper.Mapper.Map<EditRoleViewModel>(respRoleRoleUpdate.Item);

                var respRole = _roleServiceClient.Get(model.ChildRoleKey.Value);
                if (ModelState.IsServiceError(respRole))
                    return View(model);
                newModel.SelectedRole = respRole.Item.Name;
                newModel.SuccessMessage = "Assignment updated.";

                return View(newModel);
            }
            else
            {
                var item = AutoMapper.Mapper.Map<Eleflex.Security.Message.RoleRole>(model);
                var respInsert = _roleRoleServiceClient.Insert(item);
                EditRoleViewModel newModel = AutoMapper.Mapper.Map<EditRoleViewModel>(respInsert.Item);

                var respRole = _roleServiceClient.Get(model.ChildRoleKey.Value);
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
        public ActionResult Permissions(Guid roleKey)
        {
            //Get role and set needed props
            PermissionsViewModel model = new PermissionsViewModel();
            var respRole = _roleServiceClient.Get(roleKey);
            if (ModelState.IsServiceError(respRole))
                return View(model);
            model.RoleKey = respRole.Item.RoleKey;
            model.Name = respRole.Item.Name;

            //Get role permissions
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.IsEqual("RoleKey", respRole.Item.RoleKey.ToString());
            var respRolePermissions = _rolePermissionServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(respRolePermissions))
                return View(model);

            //Get Permission Names
            string[] permissionKeys = respRolePermissions.Items.Select(x => x.PermissionKey.ToString()).Distinct().ToArray();
            builder = new StorageQueryBuilder();
            builder.IsInSet("PermissionKey", permissionKeys);
            var respPermissions = _permissionServiceClient.Query(builder.GetStorageQuery());
            if (ModelState.IsServiceError(respRolePermissions))
                return View(model);

            //Build new rolepermission model
            foreach (var rolePermission in respRolePermissions.Items)
            {
                RolePermissionViewModel permission = AutoMapper.Mapper.Map<RolePermissionViewModel>(rolePermission);
                permission.PermissionName = respPermissions.Items.Where(x => x.PermissionKey == rolePermission.PermissionKey).Select(x => x.Name).FirstOrDefault();
                model.RolePermissions.Add(permission);
            }

            return View(model);
        }

        /// <summary>
        /// EditPermission assignment.
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        public ActionResult EditPermission(Guid roleKey, long? rolePermissionKey = null)
        {
            EditPermissionViewModel model = new EditPermissionViewModel();
            if (rolePermissionKey.HasValue)
            {
                var respRolePermission = _rolePermissionServiceClient.Get(rolePermissionKey.Value);
                if (ModelState.IsServiceError(respRolePermission))
                    return View(model);
                model = AutoMapper.Mapper.Map<EditPermissionViewModel>(respRolePermission.Item);
                var respPermission = _permissionServiceClient.Get(model.PermissionKey.Value);
                if (ModelState.IsServiceError(respPermission))
                    return View(model);
                model.SelectedPermission = respPermission.Item.Name;
            }
            ModelState.Clear();
            model.RoleKey = roleKey;
            model.RolePermissionKey = rolePermissionKey;

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

            if (model.RolePermissionKey.HasValue)
            {
                var respRolePermission = _rolePermissionServiceClient.Get(model.RolePermissionKey.Value);
                if (ModelState.IsServiceError(respRolePermission))
                    return View(model);

                var item = AutoMapper.Mapper.Map<Eleflex.Security.Message.RolePermission>(model);
                var respRolePermissionUpdate = _rolePermissionServiceClient.Update(item);
                EditPermissionViewModel newModel = AutoMapper.Mapper.Map<EditPermissionViewModel>(respRolePermissionUpdate.Item);

                var respPermission = _permissionServiceClient.Get(model.PermissionKey.Value);
                if (ModelState.IsServiceError(respPermission))
                    return View(model);
                newModel.SelectedPermission = respPermission.Item.Name;
                newModel.SuccessMessage = "Assignment updated.";

                return View(newModel);
            }
            else
            {
                var item = AutoMapper.Mapper.Map<Eleflex.Security.Message.RolePermission>(model);
                var respInsert = _rolePermissionServiceClient.Insert(item);
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


    }
}

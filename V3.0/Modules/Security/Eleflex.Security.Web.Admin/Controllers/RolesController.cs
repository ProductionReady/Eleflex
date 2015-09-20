using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Eleflex.Security.Services.WCF.Message;
using Eleflex.Security.ASPNetIdentity;
using Microsoft.AspNet.Identity.Owin;
using Eleflex.Security.Web.Admin.Models.Roles;
using Eleflex.Web;
using ServiceModel = Eleflex;

namespace Eleflex.Security.Web.Admin.Controllers
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
        protected IRepository<IdentityRole, Guid> _roleServiceRepository;
        /// <summary>
        /// Role Role service client.
        /// </summary>
        protected ISecurityRoleRoleServiceRepository _roleRoleServiceRepository;
        /// <summary>
        /// Role Permission service client.
        /// </summary>
        protected ISecurityRolePermissionServiceRepository _rolePermissionServiceRepository;
        /// <summary>
        /// Permission service client.
        /// </summary>
        protected ISecurityPermissionServiceRepository _permissionServiceRepository;
        /// <summary>
        /// Instance for UserManager property. Use property instead of this variable.
        /// </summary>
        protected IdentityUserManager _userManager;
        /// <summary>
        /// Instance for RoleManager property. Use property instead of this variable.
        /// </summary>
        protected IdentityRoleManager _roleManager;
        /// <summary>
        /// The mapping service.
        /// </summary>
        protected IMappingService _mappingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="roleServiceRepository"></param>
        public RolesController(
            IMappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository> roleServiceRepository,
            ISecurityRoleRoleServiceRepository roleRoleServiceRepository,
            ISecurityRolePermissionServiceRepository rolePermissionServiceRepository,
            ISecurityPermissionServiceRepository permissionServiceRepository,
            IMappingService mappingService)
        {            
            _roleServiceRepository = roleServiceRepository;
            _roleRoleServiceRepository = roleRoleServiceRepository;
            _rolePermissionServiceRepository = rolePermissionServiceRepository;
            _permissionServiceRepository = permissionServiceRepository;
            _mappingService = mappingService;
        }

        /// <summary>
        /// Get user manager
        /// </summary>
        public IdentityUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<IdentityUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        /// <summary>
        /// Get role manager
        /// </summary>
        public IdentityRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<IdentityRoleManager>();
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
            var response = _roleServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
            if (ModelState.IsResponseError(response))
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
                if (!string.IsNullOrWhiteSpace(model.Active))
                    builder.IsEqual("Active", model.Active);
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
            var response = _roleServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
            if (ModelState.IsResponseError(response))
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
            var resp = _roleServiceRepository.Get(new RequestItem<Guid>() { Item = roleKey });
            if (resp.Item == null)
                throw new Exception("Role not found for key:" + roleKey.ToString());

            EditViewModel viewModel = _mappingService.Map<ServiceModel.SecurityRole, EditViewModel>(resp.Item);
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
            if (model.SecurityRoleKey.HasValue)
            {
                IdentityRole role = await RoleManager.FindByIdAsync(model.SecurityRoleKey.Value.ToString());

                if (role == null)
                {
                    ModelState.AddModelError("", "Role does not exist.");
                    return View(model);
                }

                if (role.Active != model.Active)
                {
                    //Updating inactive to be active, check if another record already in use
                    if (!role.Active)
                    {
                        var findNameResult = await RoleManager.FindByNameAsync(model.Name);
                        if (findNameResult != null)
                        {
                            ModelState.AddModelError("", "Name is already in use.");
                            return View(model);
                        }
                    }
                    role.Active = model.Active;
                }
                if (string.Compare(role.Name, model.Name, true) != 0)
                {
                    if (role.Active)
                    {
                        //Updating username, check if already in use
                        var findNameResult = await RoleManager.FindByNameAsync(model.Name);
                        if (findNameResult != null)
                        {
                            ModelState.AddModelError("", "Name is already in use.");
                            return View(model);
                        }
                    }
                    role.Name = model.Name;
                }

                role.Description = model.Description;
                role.EffectiveEndDate = model.EffectiveEndDate;
                role.EffectiveStartDate = model.EffectiveStartDate;

                //Call service instead of RoleManager (we checked requirements above)
                var respUpdate = _roleServiceRepository.Update(new RequestItem<IdentityRole>() { Item = role });
                if (ModelState.IsResponseError(respUpdate))
                    if (!respUpdate.ResponseSuccess)
                        return View(model);

                newModel = _mappingService.Map<IdentityRole, EditViewModel>(role);
                newModel.SuccessMessage = "Role updated.";
            }
            else
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                IdentityRole item = new IdentityRole();
                item.SecurityRoleKey =Guid.NewGuid();
                item.Description = model.Description;
                item.Active = model.Active;
                item.Name = model.Name;
                item.Description = model.Description;
                item.EffectiveEndDate = model.EffectiveEndDate;
                item.EffectiveStartDate = model.EffectiveStartDate;

                if (!model.Active)
                {
                    //No validation checks if inactive, call service instead of RoleManager
                    var respInsert = _roleServiceRepository.Insert(new RequestItem<IdentityRole>() { Item = item });
                    if (ModelState.IsResponseError(respInsert))
                        return View(model);
                    newModel = _mappingService.Map<ServiceModel.SecurityRole, EditViewModel>(respInsert.Item);
                }
                else
                {
                    //RoleManager will check if username or email already exists
                    var result = await RoleManager.CreateAsync(item);
                    if (result.Succeeded)
                    {
                        newModel = _mappingService.Map<EditViewModel, IdentityRole>(item);
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
            var respRole = _roleServiceRepository.Get(new RequestItem<Guid>() { Item = roleKey });
            if (ModelState.IsResponseError(respRole))
                return View(model);
            model.SecurityRoleKey = respRole.Item.SecurityRoleKey;
            model.RoleName = respRole.Item.Name;

            //Get role roles
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.IsEqual("ParentSecurityRoleKey", respRole.Item.SecurityRoleKey.ToString());
            var respRoleRoles = _roleRoleServiceRepository.Query(new RequestItem<IStorageQuery>(){ Item = builder.GetStorageQuery() });
            if (ModelState.IsResponseError(respRoleRoles))
                return View(model);

            //Get Role Names
            string[] roleKeys = respRoleRoles.Items.Select(x => x.ChildSecurityRoleKey.ToString()).Distinct().ToArray();
            if (roleKeys.Length > 0)
            {
                builder = new StorageQueryBuilder();
                builder.IsInSet("SecurityRoleKey", roleKeys);
                var respRoles = _roleServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                if (ModelState.IsResponseError(respRoleRoles))
                    return View(model);

                //Build new rolerole model
                foreach (var roleRole in respRoleRoles.Items)
                {
                    RoleRoleViewModel role = _mappingService.Map<RoleRoleViewModel, ServiceModel.SecurityRoleRole>(roleRole);
                    role.RoleName = respRoles.Items.Where(x => x.SecurityRoleKey == roleRole.ChildSecurityRoleKey).Select(x => x.Name).FirstOrDefault();
                    model.RoleRoles.Add(role);
                }
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
                var respRoleRole = _roleRoleServiceRepository.Get(new RequestItem<long>() { Item = roleRoleKey.Value });
                if (ModelState.IsResponseError(respRoleRole))
                    return View(model);
                model = _mappingService.Map<EditRoleViewModel, ServiceModel.SecurityRoleRole>(respRoleRole.Item);
                var respRole = _roleServiceRepository.Get(new RequestItem<Guid>() { Item = model.ChildSecurityRoleKey.Value });
                if (ModelState.IsResponseError(respRole))
                    return View(model);
                model.SelectedRole = respRole.Item.Name;
            }
            ModelState.Clear();
            model.ParentSecurityRoleKey = roleKey;
            model.SecurityRoleRoleKey = roleRoleKey;            

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(EditRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.ChildSecurityRoleKey.HasValue)
                {
                    var respRole = _roleServiceRepository.Get(new RequestItem<Guid>() { Item = model.ChildSecurityRoleKey.Value });
                    if (ModelState.IsResponseError(respRole))
                        return View(model);
                    model.SelectedRole = respRole.Item.Name;
                }
                return View(model);
            }

            if (model.SecurityRoleRoleKey.HasValue)
            {
                var respRoleRole = _roleRoleServiceRepository.Get(new RequestItem<long>() { Item = model.SecurityRoleRoleKey.Value });
                if (ModelState.IsResponseError(respRoleRole))
                    return View(model);

                var item = _mappingService.Map<ServiceModel.SecurityRoleRole, EditRoleViewModel>(model);
                var respRoleRoleUpdate = _roleRoleServiceRepository.Update(new RequestItem<ServiceModel.SecurityRoleRole>() { Item = item });
                EditRoleViewModel newModel = _mappingService.Map<EditRoleViewModel, ServiceModel.SecurityRoleRole>(respRoleRoleUpdate.Item);

                var respRole = _roleServiceRepository.Get(new RequestItem<Guid>() { Item = model.ChildSecurityRoleKey.Value });
                if (ModelState.IsResponseError(respRole))
                    return View(model);
                newModel.SelectedRole = respRole.Item.Name;
                newModel.SuccessMessage = "Assignment updated.";

                return View(newModel);
            }
            else
            {
                var item = _mappingService.Map<ServiceModel.SecurityRoleRole, EditRoleViewModel>(model);
                var respInsert = _roleRoleServiceRepository.Insert(new RequestItem<ServiceModel.SecurityRoleRole>() { Item = item });
                EditRoleViewModel newModel = _mappingService.Map<EditRoleViewModel, ServiceModel.SecurityRoleRole>(respInsert.Item);

                var respRole = _roleServiceRepository.Get(new RequestItem<Guid>() { Item = model.ChildSecurityRoleKey.Value });
                if (ModelState.IsResponseError(respRole))
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
            var response = _roleServiceRepository.Query(new RequestItem<IStorageQuery>(){ Item = builder.GetStorageQuery() });
            if (ModelState.IsResponseError(response))
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
            var respRole = _roleServiceRepository.Get(new RequestItem<Guid>() { Item = roleKey });
            if (ModelState.IsResponseError(respRole))
                return View(model);
            model.SecurityRoleKey = respRole.Item.SecurityRoleKey;
            model.Name = respRole.Item.Name;

            //Get role permissions
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.IsEqual("SecurityRoleKey", respRole.Item.SecurityRoleKey.ToString());
            var respRolePermissions = _rolePermissionServiceRepository.Query(new RequestItem<IStorageQuery>(){ Item = builder.GetStorageQuery() });
            if (ModelState.IsResponseError(respRolePermissions))
                return View(model);

            //Get Permission Names
            string[] permissionKeys = respRolePermissions.Items.Select(x => x.SecurityPermissionKey.ToString()).Distinct().ToArray();
            if (permissionKeys != null && permissionKeys.Length > 0)
            {
                builder = new StorageQueryBuilder();
                builder.IsInSet("SecurityPermissionKey", permissionKeys);
                var respPermissions = _permissionServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                if (ModelState.IsResponseError(respRolePermissions))
                    return View(model);

                //Build new rolepermission model
                foreach (var rolePermission in respRolePermissions.Items)
                {
                    RolePermissionViewModel permission = _mappingService.Map<RolePermissionViewModel, ServiceModel.SecurityRolePermission>(rolePermission);
                    permission.PermissionName = respPermissions.Items.Where(x => x.SecurityPermissionKey == rolePermission.SecurityPermissionKey).Select(x => x.Name).FirstOrDefault();
                    model.SecurityRolePermissions.Add(permission);
                }
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
                var respRolePermission = _rolePermissionServiceRepository.Get(new RequestItem<long>() { Item = rolePermissionKey.Value });
                if (ModelState.IsResponseError(respRolePermission))
                    return View(model);
                model = _mappingService.Map<EditPermissionViewModel, ServiceModel.ISecurityRolePermission>(respRolePermission.Item);
                var respPermission = _permissionServiceRepository.Get(new RequestItem<Guid>() { Item = model.SecurityPermissionKey.Value });
                if (ModelState.IsResponseError(respPermission))
                    return View(model);
                model.SelectedPermission = respPermission.Item.Name;
            }
            ModelState.Clear();
            model.SecurityRoleKey = roleKey;
            model.SecurityRolePermissionKey = rolePermissionKey;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPermission(EditPermissionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.SecurityPermissionKey.HasValue)
                {
                    var respPermission = _permissionServiceRepository.Get(new RequestItem<Guid>() { Item = model.SecurityPermissionKey.Value });
                    if (ModelState.IsResponseError(respPermission))
                        return View(model);
                    model.SelectedPermission = respPermission.Item.Name;
                }
                return View(model);
            }

            if (model.SecurityRolePermissionKey.HasValue)
            {
                var respRolePermission = _rolePermissionServiceRepository.Get(new RequestItem<long>() { Item = model.SecurityRolePermissionKey.Value });
                if (ModelState.IsResponseError(respRolePermission))
                    return View(model);

                var item = _mappingService.Map<ServiceModel.SecurityRolePermission, EditPermissionViewModel>(model);
                var respRolePermissionUpdate = _rolePermissionServiceRepository.Update(new RequestItem<ServiceModel.SecurityRolePermission>() { Item = item });
                EditPermissionViewModel newModel = _mappingService.Map<EditPermissionViewModel, ServiceModel.SecurityRolePermission>(respRolePermissionUpdate.Item);

                var respPermission = _permissionServiceRepository.Get(new RequestItem<Guid>() { Item = model.SecurityPermissionKey.Value });
                if (ModelState.IsResponseError(respPermission))
                    return View(model);
                newModel.SelectedPermission = respPermission.Item.Name;
                newModel.SuccessMessage = "Assignment updated.";

                return View(newModel);
            }
            else
            {
                var item = _mappingService.Map<ServiceModel.SecurityRolePermission, EditPermissionViewModel>(model);
                var respInsert = _rolePermissionServiceRepository.Insert(new RequestItem<ServiceModel.SecurityRolePermission>() { Item = item });
                EditPermissionViewModel newModel = _mappingService.Map<EditPermissionViewModel, ServiceModel.SecurityRolePermission>(respInsert.Item);

                var respPermission = _permissionServiceRepository.Get(new RequestItem<Guid>() { Item = model.SecurityPermissionKey.Value });
                if (ModelState.IsResponseError(respPermission))
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
            var response = _permissionServiceRepository.Query(new RequestItem<IStorageQuery>(){ Item = builder.GetStorageQuery() });
            if (ModelState.IsResponseError(response))
                return View(newModel);

            newModel.SearchName = permissionName;
            newModel.SearchPermissions = response.Items;
            return PartialView("SelectPermission", newModel);
        }


    }
}

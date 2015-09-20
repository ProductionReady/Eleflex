using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Eleflex.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Eleflex.Security.ASPNetIdentity;
using Eleflex.Security.Services.WCF.Message;
using ServiceModel = Eleflex;
using Eleflex.Security.Web.Admin.Models.Users;

namespace Eleflex.Security.Web.Admin.Controllers
{
    /// <summary>
    /// Security Users.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {

        protected IRepository<IdentityUser, Guid> _userServiceRepository;
        protected ISecurityUserRoleServiceRepository _userRoleServiceRepository;
        protected ISecurityUserPermissionServiceRepository _userPermissionServiceRepository;
        protected ISecurityUserClaimServiceRepository _userClaimServiceRepository;
        protected ISecurityRoleServiceRepository _roleServiceRepository;
        protected ISecurityPermissionServiceRepository _permissionServiceRepository;
        protected IMappingService _mappingService;

        /// <summary>
        /// Instance for UserManager property. Use property instead of this variable.
        /// </summary>
        protected IdentityUserManager _userManager;

        
        public UsersController(
            IMappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository> userServiceRepository,
            ISecurityUserRoleServiceRepository userRoleServiceRepository,
            ISecurityUserPermissionServiceRepository userPermissionServiceRepository,
            ISecurityUserClaimServiceRepository userClaimServiceRepository,
            ISecurityRoleServiceRepository roleServiceRepository,
            ISecurityPermissionServiceRepository permissionServiceRepository,
            IMappingService mappingService)
        {
            _userServiceRepository = userServiceRepository;
            _userRoleServiceRepository = userRoleServiceRepository;
            _userPermissionServiceRepository = userPermissionServiceRepository;
            _userClaimServiceRepository = userClaimServiceRepository;
            _roleServiceRepository = roleServiceRepository;
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
            var response = _userServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
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
            var response = _userServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
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
        public ActionResult Edit(Guid userKey)
        {
            var resp = _userServiceRepository.Get(new RequestItem<Guid>() { Item = userKey });
            if (resp.Item == null)
                throw new Exception("User not found for key:" + userKey.ToString());

            EditViewModel viewModel = _mappingService.Map<IdentityUser, EditViewModel>(resp.Item);
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
            if (model.SecurityUserKey.HasValue)
            {
                IdentityUser user = await UserManager.FindByIdAsync(model.SecurityUserKey.Value.ToString());
                if (user == null)
                {
                    ModelState.AddModelError("", "User does not exist.");
                    return View(model);
                }

                if (user.Active != model.Active)
                {
                    //Updating inactive to be active, check if another record already in use
                    if (!user.Active)
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
                    user.Active = model.Active;
                }
                if (string.Compare(user.Username, model.Username, true) != 0)
                {
                    if (user.Active)
                    {
                        //Updating username, check if already in use
                        var findUsernameResult = await UserManager.FindByNameAsync(model.Username);
                        if (findUsernameResult != null)
                        {
                            ModelState.AddModelError("", "Username is already in use.");
                            return View(model);
                        }
                    }
                    user.Username = model.Username;
                }
                if (string.Compare(user.Email, model.Email, true) != 0)
                {
                    if (user.Active)
                    {
                        //Updating email, check if already in use
                        var findEmailResult = await UserManager.FindByEmailAsync(model.Email);
                        if (findEmailResult != null)
                        {
                            ModelState.AddModelError("", "Email is already in use.");
                            return View(model);
                        }
                    }
                    user.Email = model.Email;
                }

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Comment = model.Comment;
                user.Phone = model.Phone;

                //Call service instead of UserManager (we checked requirements above)
                var respUpdate = _userServiceRepository.Update(new RequestItem<IdentityUser>() { Item = user });
                if (ModelState.IsResponseError(respUpdate))
                    if (!respUpdate.ResponseSuccess)
                        return View(model);

                newModel = _mappingService.Map<IdentityUser, EditViewModel>(user);
                newModel.SuccessMessage = "User updated.";
            }
            else
            {
                DateTimeOffset now = DateTimeOffset.UtcNow;
                string tempPass = Guid.NewGuid().ToString() + "aA!1"; //So meets default security requirements
                IdentityUser item = new IdentityUser();
                item.SecurityUserKey = Guid.NewGuid();
                item.CreateDate = now;
                item.Email = model.Email;
                item.Username = model.Username;
                item.FirstName = model.FirstName;
                item.LastName = model.LastName;
                item.Comment = model.Comment;
                item.Active = model.Active;
                item.Phone = model.Phone;
                item.PasswordLastChangeDate = now;
                item.EnableLockout = true;

                if (!model.Active)
                {
                    //No validation checks if inactive, call service instead of UserManager
                    item.Password = UserManager.PasswordHasher.HashPassword(tempPass);
                    item.SecurityStamp = Guid.NewGuid().ToString();
                    var respInsert = _userServiceRepository.Insert(new RequestItem<IdentityUser>() { Item = item });
                    if (ModelState.IsResponseError(respInsert))
                        return View(model);
                    newModel = _mappingService.Map<IdentityUser, EditViewModel>(respInsert.Item);
                }
                else
                {
                    //UserManager will check if username or email already exists
                    var result = await UserManager.CreateAsync(item, tempPass);
                    if (result.Succeeded)
                    {
                        newModel = _mappingService.Map<IdentityUser, EditViewModel>(item);
                    }
                    else
                    {
                        foreach (string err in result.Errors)
                            ModelState.AddModelError("", err);
                        return View(model); //Return previous values
                    }
                }
                //Add default user role
                await UserManager.AddToRoleAsync(newModel.SecurityUserKey.ToString(), SecurityConstants.ROLE_USER);
                newModel.SuccessMessage = "User created.";
                ModelState.Clear(); //update the hiddenfield

                //Send an email to the user with the confirmation link and temporary password
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(newModel.SecurityUserKey.ToString());
                var callbackUrl = Url.Action("ConfirmEmail", "~~Account.Account", new { userId = newModel.SecurityUserKey.ToString(), code = code}, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(newModel.SecurityUserKey.ToString(), "Confirm your account", "Please confirm your account by clicking " + callbackUrl + " This account was created by an admin of the system and your temporary password is " + tempPass);
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
            var resp = _userServiceRepository.Get(new RequestItem<Guid>() { Item = userKey });
            if (resp.Item == null)
                return Json(AjaxResponse.Error("Service error."));

            var item = resp.Item;
            item.LockoutReinstateDate = lockoutDate;
            item.LoginFailedAttempts = 0;
            var respUpdate = _userServiceRepository.Update(new RequestItem<IdentityUser>() { Item = item });
            if (ModelState.IsResponseError(respUpdate))
                return Json(AjaxResponse.Error("Service error."));

            return Json(AjaxResponse.Success("Lockout date changed.", respUpdate.Item));
        }


        [HttpGet]
        public ActionResult Roles(Guid userKey)
        {            
            //Get user and set needed props
            RolesViewModel model = new RolesViewModel();
            var respUser =_userServiceRepository.Get(new RequestItem<Guid>() { Item = userKey });
            if (ModelState.IsResponseError(respUser))
                return View(model);
            model.SecurityUserKey = respUser.Item.SecurityUserKey;
            model.Username = respUser.Item.Username;
            model.Email = respUser.Item.Email;

            //Get user roles
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.IsEqual("SecurityUserKey", respUser.Item.SecurityUserKey.ToString());
            var respUserRoles = _userRoleServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
            if (ModelState.IsResponseError(respUserRoles))
                return View(model);

            //Get Role Names
            string[] roleKeys = respUserRoles.Items.Select(x=>x.SecurityRoleKey.ToString()).Distinct().ToArray();
            if (roleKeys != null && roleKeys.Length > 0)
            {
                builder = new StorageQueryBuilder();
                builder.IsInSet("SecurityRoleKey", roleKeys);
                var respRoles = _roleServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                if (ModelState.IsResponseError(respUserRoles))
                    return View(model);

                //Build new userrole model
                foreach (var userRole in respUserRoles.Items)
                {
                    UserRoleViewModel role = _mappingService.Map<ServiceModel.SecurityUserRole, UserRoleViewModel>(userRole);
                    role.RoleName = respRoles.Items.Where(x => x.SecurityRoleKey == userRole.SecurityRoleKey).Select(x => x.Name).FirstOrDefault();
                    model.UserRoles.Add(role);
                }
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
                var respUserRole = _userRoleServiceRepository.Get(new RequestItem<long>() { Item = userRoleKey.Value });
                if (ModelState.IsResponseError(respUserRole))
                    return View(model);
                model = _mappingService.Map<ServiceModel.SecurityUserRole, EditRoleViewModel>(respUserRole.Item);
                var respRole = _roleServiceRepository.Get(new RequestItem<Guid>() { Item = model.SecurityRoleKey.Value });
                if (ModelState.IsResponseError(respRole))
                    return View(model);
                model.SelectedRole = respRole.Item.Name;
            }
            ModelState.Clear();
            model.SecurityUserKey = userKey;
            model.SecurityUserRoleKey = userRoleKey;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(EditRoleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.SecurityRoleKey.HasValue)
                {
                    var respRole = _roleServiceRepository.Get(new RequestItem<Guid>() { Item = model.SecurityRoleKey.Value });
                    if (ModelState.IsResponseError(respRole))
                        return View(model);
                    model.SelectedRole = respRole.Item.Name;
                }
                return View(model);
            }
            
            if(model.SecurityUserRoleKey.HasValue)
            {
                var respUserRole = _userRoleServiceRepository.Get(new RequestItem<long>() { Item = model.SecurityUserRoleKey.Value });
                if (ModelState.IsResponseError(respUserRole))
                    return View(model);

                var item = _mappingService.Map<ServiceModel.SecurityUserRole, EditRoleViewModel>(model);
                var respUserRoleUpdate = _userRoleServiceRepository.Update(new RequestItem<ServiceModel.SecurityUserRole>() { Item = item });
                EditRoleViewModel newModel = _mappingService.Map<EditRoleViewModel, ServiceModel.SecurityUserRole>(respUserRoleUpdate.Item);

                var respRole = _roleServiceRepository.Get(new RequestItem<Guid>() { Item = model.SecurityRoleKey.Value });
                if (ModelState.IsResponseError(respRole))
                    return View(model);
                newModel.SelectedRole = respRole.Item.Name;
                newModel.SuccessMessage = "Assignment updated.";

                return View(newModel);
            }
            else
            {
                var item = _mappingService.Map<ServiceModel.SecurityUserRole, EditRoleViewModel>(model);
                var respInsert = _userRoleServiceRepository.Insert(new RequestItem<ServiceModel.SecurityUserRole>() { Item = item });
                EditRoleViewModel newModel = _mappingService.Map<EditRoleViewModel, ServiceModel.SecurityUserRole>(respInsert.Item);

                var respRole = _roleServiceRepository.Get(new RequestItem<Guid>() { Item = model.SecurityRoleKey.Value });
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
            var response = _roleServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
            if (ModelState.IsResponseError(response))
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
            var respUser = _userServiceRepository.Get(new RequestItem<Guid>() { Item = userKey });
            if (ModelState.IsResponseError(respUser))
                return View(model);
            model.SecurityUserKey = respUser.Item.SecurityUserKey;
            model.Username = respUser.Item.Username;
            model.Email = respUser.Item.Email;

            //Get user permissions
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.IsEqual("SecurityUserKey", respUser.Item.SecurityUserKey.ToString());
            var respUserPermissions = _userPermissionServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
            if (ModelState.IsResponseError(respUserPermissions))
                return View(model);

            //Get Permission Names
            string[] permissionKeys = respUserPermissions.Items.Select(x => x.SecurityPermissionKey.ToString()).Distinct().ToArray();
            if (permissionKeys != null && permissionKeys.Length > 0)
            {
                builder = new StorageQueryBuilder();
                builder.IsInSet("SecurityPermissionKey", permissionKeys);
                var respPermissions = _permissionServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
                if (ModelState.IsResponseError(respUserPermissions))
                    return View(model);

                //Build new userpermission model
                foreach (var userPermission in respUserPermissions.Items)
                {
                    UserPermissionViewModel permission = _mappingService.Map<ServiceModel.SecurityUserPermission, UserPermissionViewModel>(userPermission);
                    permission.PermissionName = respPermissions.Items.Where(x => x.SecurityPermissionKey == userPermission.SecurityPermissionKey).Select(x => x.Name).FirstOrDefault();
                    model.UserPermissions.Add(permission);
                }
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
                var respUserPermission = _userPermissionServiceRepository.Get(new RequestItem<long>() { Item = userPermissionKey.Value });
                if (ModelState.IsResponseError(respUserPermission))
                    return View(model);
                model = _mappingService.Map<EditPermissionViewModel, ServiceModel.SecurityUserPermission>(respUserPermission.Item);
                var respPermission = _permissionServiceRepository.Get(new RequestItem<Guid>() { Item = model.SecurityPermissionKey.Value });
                if (ModelState.IsResponseError(respPermission))
                    return View(model);
                model.SelectedPermission = respPermission.Item.Name;
            }
            ModelState.Clear();
            model.SecurityUserKey = userKey;
            model.SecurityUserPermissionKey = userPermissionKey;

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

            if (model.SecurityUserPermissionKey.HasValue)
            {
                var respUserPermission = _userPermissionServiceRepository.Get(new RequestItem<long>() { Item = model.SecurityUserPermissionKey.Value });
                if (ModelState.IsResponseError(respUserPermission))
                    return View(model);

                var item = _mappingService.Map<ServiceModel.SecurityUserPermission, EditPermissionViewModel>(model);
                var respUserPermissionUpdate = _userPermissionServiceRepository.Update(new RequestItem<ServiceModel.SecurityUserPermission>() { Item = item });
                EditPermissionViewModel newModel = _mappingService.Map<EditPermissionViewModel, ServiceModel.SecurityUserPermission>(respUserPermissionUpdate.Item);

                var respPermission = _permissionServiceRepository.Get(new RequestItem<Guid>() { Item = model.SecurityPermissionKey.Value });
                if (ModelState.IsResponseError(respPermission))
                    return View(model);
                newModel.SelectedPermission = respPermission.Item.Name;
                newModel.SuccessMessage = "Assignment updated.";

                return View(newModel);
            }
            else
            {
                var item = _mappingService.Map<ServiceModel.SecurityUserPermission, EditPermissionViewModel>(model);
                var respInsert = _userPermissionServiceRepository.Insert(new RequestItem<ServiceModel.SecurityUserPermission>() { Item = item });
                EditPermissionViewModel newModel = _mappingService.Map<EditPermissionViewModel, ServiceModel.SecurityUserPermission>(respInsert.Item);

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
            var response = _permissionServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
            if (ModelState.IsResponseError(response))
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
            var respUser = _userServiceRepository.Get(new RequestItem<Guid>() { Item = userKey });
            if (ModelState.IsResponseError(respUser))
                return View(model);
            model.SecurityUserKey = respUser.Item.SecurityUserKey;
            model.Username = respUser.Item.Username;
            model.Email = respUser.Item.Email;

            //Get user claims
            StorageQueryBuilder builder = new StorageQueryBuilder();
            builder.IsEqual("SecurityUserKey", respUser.Item.SecurityUserKey.ToString());
            var respUserClaims = _userClaimServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
            if (ModelState.IsResponseError(respUserClaims))
                return View(model);

            //Build new userclaim model
            foreach (var userClaim in respUserClaims.Items)
            {
                UserClaimViewModel claim = _mappingService.Map<UserClaimViewModel, ServiceModel.SecurityUserClaim>(userClaim);
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
                var respUserClaim = _userClaimServiceRepository.Get(new RequestItem<long>() { Item = userClaimKey.Value });
                if (ModelState.IsResponseError(respUserClaim))
                    return View(model);
                model = _mappingService.Map<EditClaimViewModel, ServiceModel.SecurityUserClaim>(respUserClaim.Item);
            }
            ModelState.Clear();
            model.SecurityUserKey = userKey;
            model.SecurityUserClaimKey = userClaimKey;

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClaim(EditClaimViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.SecurityUserClaimKey.HasValue)
            {
                var respUserClaim = _userClaimServiceRepository.Get(new RequestItem<long>() { Item = model.SecurityUserClaimKey.Value });
                if (ModelState.IsResponseError(respUserClaim))
                    return View(model);

                var item = _mappingService.Map<ServiceModel.SecurityUserClaim, EditClaimViewModel>(model);
                var respUserClaimUpdate = _userClaimServiceRepository.Update(new RequestItem<ServiceModel.SecurityUserClaim>() { Item = item });
                EditClaimViewModel newModel = _mappingService.Map<ServiceModel.SecurityUserClaim, EditClaimViewModel>(respUserClaimUpdate.Item);

                newModel.SuccessMessage = "Claim updated.";
                return View(newModel);
            }
            else
            {
                var item = _mappingService.Map<ServiceModel.SecurityUserClaim, EditClaimViewModel>(model);
                var respInsert = _userClaimServiceRepository.Insert(new RequestItem<ServiceModel.SecurityUserClaim>() { Item = item });
                EditClaimViewModel newModel = _mappingService.Map<EditClaimViewModel, ServiceModel.SecurityUserClaim>(respInsert.Item);

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

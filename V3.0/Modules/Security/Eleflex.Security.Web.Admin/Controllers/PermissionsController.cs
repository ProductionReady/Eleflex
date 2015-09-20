using System.Web.Mvc;
using Eleflex.Security.Services.WCF.Message;
using Eleflex.Web;
using Eleflex.Security.Web.Admin.Models.Permissions;

namespace Eleflex.Security.Web.Admin.Controllers
{
    /// <summary>
    /// Security Permissions.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class PermissionsController : Controller
    {
        /// <summary>
        /// Permission service client.
        /// </summary>
        ISecurityPermissionServiceRepository _permissionServiceRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="permissionServiceRepository"></param>
        public PermissionsController(ISecurityPermissionServiceRepository permissionServiceRepository)
        {
            _permissionServiceRepository = permissionServiceRepository;
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return RedirectToAction("List");
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
            var response = _permissionServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
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
            var response = _permissionServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
            model.Items = response.Items;
            return View(model);
        }
    }
}

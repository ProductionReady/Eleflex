using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using ServiceModel = Eleflex.Lookups.Services.WCF.Message;
using Eleflex.Lookups.Web.Admin.Models;
using Eleflex.Web;

namespace Eleflex.Lookups.Web.Admin.Controllers
{
    /// <summary>
    /// Lookups admin controller.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// Service client for lookups
        /// </summary>
        ServiceModel.ILookupServiceRepository _lookupServiceRepository = null;
        /// <summary>
        /// Mapping service
        /// </summary>
        protected IMappingService _mappingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="lookupServiceRepository"></param>
        /// <param name="mappingService"></param>
        public AdminController(
            ServiceModel.ILookupServiceRepository lookupServiceRepository,
            IMappingService mappingService)
        {
            _lookupServiceRepository = lookupServiceRepository;
            _mappingService = mappingService;
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }


        [HttpGet]
        public ActionResult Create()
        {
            EditViewModel model = new EditViewModel();
            model.LookupKey = Guid.NewGuid();
            return View("Edit", model);
        }


        /// <summary>
        /// List.
        /// </summary>
        /// <returns></returns>        
        public ActionResult List(ListModel model = null)
        {
            StorageQueryBuilder builder = new StorageQueryBuilder();
            if (model != null)
            {
                if (!ModelState.IsValid)
                    return View(model);

                if (model.Active.HasValue)
                    builder.IsEqual("Active", model.Active.Value.ToString());

                if (!string.IsNullOrWhiteSpace(model.Name))
                    builder.Contains("Name", model.Name);

                if (!string.IsNullOrWhiteSpace(model.Description))
                    builder.Contains("Description", model.Description);

                if (model.ParentLookupKey.HasValue)
                    builder.IsEqual("ParentLookupKey", model.ParentLookupKey.Value.ToString());
            }

            if (model != null && model.MaxRecords.HasValue)
                builder.Paging(1, model.MaxRecords.Value);
            else
                builder.Paging(1, StorageConstants.MAX_RETURNED_RECORDS_DEFAULT);

            var response = _lookupServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
            model.Items = response.Items;
            return View(model);
        }


        /// <summary>
        /// Edit.
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        public ActionResult Edit(Guid lookupKey)
        {
            var resp = _lookupServiceRepository.Get(new RequestItem<Guid>() { Item = lookupKey });
            if (resp.Item == null)
                throw new Exception("Lookup not found for key:" + lookupKey.ToString());

            EditViewModel viewModel = _mappingService.Map<ServiceModel.Lookup, EditViewModel>(resp.Item);
            return View(viewModel);
        }

        /// <summary>
        /// Details.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var respLookup = _lookupServiceRepository.Get(new RequestItem<Guid>() { Item = model.LookupKey.Value });
            if (ModelState.IsResponseError(respLookup))
                return View(model);

            EditViewModel newModel = null;
            if (respLookup.Item != null)
            {                 
                ServiceModel.Lookup lookup = respLookup.Item;                                
                if(lookup != null)
                {
                    lookup.Active = model.Active;
                    lookup.Description = model.Description;
                    lookup.Name = model.Name;
                    lookup.ParentLookupKey = model.ParentLookupKey;
                    lookup.SortOrder = model.SortOrder;

                    var respUpdate = _lookupServiceRepository.Update(new RequestItem<ServiceModel.Lookup>() { Item = lookup });
                    if (ModelState.IsResponseError(respUpdate))
                        return View(model);                    

                    newModel = _mappingService.Map<ServiceModel.Lookup, EditViewModel>(respUpdate.Item);
                    newModel.SuccessMessage = "Lookup updated.";
                }                
            }
            else
            {
                ServiceModel.Lookup item = new ServiceModel.Lookup();
                if (model.LookupKey.HasValue)
                    item.LookupKey = model.LookupKey.Value;
                else
                    item.LookupKey = Guid.NewGuid();
                item.Active = model.Active;
                item.Description = model.Description;
                item.LookupKey = Guid.NewGuid();
                item.Name = model.Name;
                item.ParentLookupKey = model.ParentLookupKey;
                item.SortOrder = model.SortOrder;

                var respInsert = _lookupServiceRepository.Insert(new RequestItem<ServiceModel.Lookup>() { Item = item });
                if (ModelState.IsResponseError(respInsert))
                    return View(model);

                newModel = _mappingService.Map<ServiceModel.Lookup, EditViewModel>(respInsert.Item);
                newModel.SuccessMessage = "Lookup created.";                          
            }
            return View(newModel);
        }

    }
}

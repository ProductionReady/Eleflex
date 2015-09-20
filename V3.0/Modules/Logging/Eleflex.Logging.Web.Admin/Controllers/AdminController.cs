using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eleflex.Logging.Services.WCF.Message;
using Eleflex.Web;
using Eleflex.Logging.Web.Admin.Models;

namespace Eleflex.Logging.Web.Admin.Controllers
{
    /// <summary>
    /// Logging admin controller.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        protected readonly ILogMessageServiceRepository _logServiceRepository;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="logServiceRepository"></param>
        public AdminController(ILogMessageServiceRepository logServiceRepository)
        {
            _logServiceRepository = logServiceRepository;
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
        /// List.
        /// </summary>
        /// <returns></returns>        
        public ActionResult List(ListModel model = null)
        {            
            StorageQueryBuilder builder = new StorageQueryBuilder();
            if (model != null)
            {                
                if (model.Severity != null && model.Severity.Length > 0)
                    builder.IsInSet("Severity", model.Severity);

                if (model.IsError.HasValue)
                    builder.IsEqual("IsError", model.IsError.Value.ToString());

                if (!string.IsNullOrWhiteSpace(model.Message))
                    builder.Contains("Message", model.Message);

                if (!string.IsNullOrWhiteSpace(model.Source))
                    builder.Contains("Source", model.Source);

                if (!string.IsNullOrWhiteSpace(model.Exception))
                    builder.Contains("Exception", model.Exception);

                if (model.DateFrom.HasValue || model.DateTo.HasValue)
                {
                    if (model.DateFrom.HasValue && model.DateTo.HasValue)
                    {
                        if (model.DateFrom.Value > model.DateTo.Value)
                        {
                            ModelState.AddModelError("DateFrom", "DateFrom cannot be after DateTo");
                            return View(model);
                        }
                        builder.Between("CreateDate", model.DateFrom.Value.ToString(), model.DateTo.Value.ToString());
                    }
                    else
                    {
                        if (model.DateFrom.HasValue)
                            builder.IsGreaterThanOrEqual("CreateDate", model.DateFrom.Value.ToString());
                        if (model.DateTo.HasValue)
                            builder.IsLessThanOrEqual("CreateDate", model.DateTo.Value.ToString());

                    }
                }
            }

            if (model != null && model.MaxRecords.HasValue)
                builder.Paging(1, model.MaxRecords.Value);
            else
                builder.Paging(1, StorageConstants.MAX_RETURNED_RECORDS_DEFAULT);
            
            builder.Sort("CreateDate", false);
            var response = _logServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
            if(ModelState.IsResponseError(response))
                return View(model);

            model.Items = response.Items;
            return View(model);
        }

    }
}

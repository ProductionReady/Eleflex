using System;
using System.Linq;
using System.Web.Mvc;
using System.Threading.Tasks;
using ServiceModel = Eleflex.Email.Services.WCF.Message;
using Eleflex.Email.Web.Admin.Models;
using Eleflex.Web;

namespace Eleflex.Email.Web.Admin.Controllers
{
    /// <summary>
    /// Email admin controller.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// Service client for Email
        /// </summary>
        protected ServiceModel.IEmailProcessServiceRepository _emailProcessServiceRepository = null;
        /// <summary>
        /// Mapping service
        /// </summary>
        protected IMappingService _mappingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="EmailerviceRepository"></param>
        /// <param name="mappingService"></param>
        public AdminController(
            ServiceModel.IEmailProcessServiceRepository emailProcessServiceRepository,
            IMappingService mappingService)
        {
            _emailProcessServiceRepository = emailProcessServiceRepository;
            _mappingService = mappingService;
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


        [HttpGet]
        public ActionResult Create()
        {
            EditViewModel model = new EditViewModel();
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

                if (!string.IsNullOrWhiteSpace(model.Subject))
                    builder.Contains("Subject", model.Subject);

                if (!string.IsNullOrWhiteSpace(model.Body))
                    builder.Contains("Body", model.Body);

                if (!string.IsNullOrWhiteSpace(model.FromAddress))
                    builder.Contains("FromAddress", model.FromAddress);

                if (!string.IsNullOrWhiteSpace(model.ToAddress))
                    builder.Contains("ToAddress", model.ToAddress);

                if (!string.IsNullOrWhiteSpace(model.CcAddress))
                    builder.Contains("CcAddress", model.CcAddress);

                if (!string.IsNullOrWhiteSpace(model.BccAddress))
                    builder.Contains("BccAddress", model.BccAddress);

                if (!string.IsNullOrWhiteSpace(model.CreateDate))
                    builder.IsGreaterThanOrEqual("CreateDate", model.CreateDate);

                if (!string.IsNullOrWhiteSpace(model.SendDate))
                    builder.IsGreaterThanOrEqual("SendDate", model.CreateDate);

                if (model.IsError.HasValue)
                    builder.IsEqual("IsError", model.IsError.Value.ToString());
            }

            if (model != null && model.MaxRecords.HasValue)
                builder.Paging(1, model.MaxRecords.Value);
            else
                builder.Paging(1, StorageConstants.MAX_RETURNED_RECORDS_DEFAULT);

            var response = _emailProcessServiceRepository.Query(new RequestItem<IStorageQuery>() { Item = builder.GetStorageQuery() });
            model.Items = response.Items;
            return View(model);
        }


        /// <summary>
        /// Details.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(long emailProcessKey)
        {
            var respGet = _emailProcessServiceRepository.Get(new RequestItem<long>(emailProcessKey));
            if (ModelState.IsResponseError(respGet) || respGet.Item == null)
                return View("List");

            EditViewModel model = new EditViewModel();
            model.BccAddress = respGet.Item.BccAddress;
            model.Body = respGet.Item.Body;
            model.CcAddress = respGet.Item.CcAddress;
            model.EmailProcessKey = respGet.Item.EmailProcessKey;
            model.FromAddress = respGet.Item.FromAddress;
            model.FutureSendDate = respGet.Item.FutureSendDate;
            model.IsHtml = respGet.Item.IsHtml;
            model.Subject = respGet.Item.Subject;
            model.ToAddress = respGet.Item.ToAddress;
            return View(model);
        }

        /// <summary>
        /// Details.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditViewModel model)
        {
            //Validation
            if (!ModelState.IsValid)
                return View(model);

            if (string.IsNullOrEmpty(model.ToAddress) && string.IsNullOrEmpty(model.CcAddress) && string.IsNullOrEmpty(model.BccAddress))
            {
                ModelState.AddModelError("ToAddress", "At least one recipient is required.");
                ModelState.AddModelError("CcAddress", "At least one recipient is required.");
                ModelState.AddModelError("BccAddress", "At least one recipient is required.");
                return View(model);
            }

            //Get existing item if updating
            ServiceModel.EmailProcess item = new ServiceModel.EmailProcess();
            if (model.EmailProcessKey.HasValue)
            {
                var respGet = _emailProcessServiceRepository.Get(new RequestItem<long>(model.EmailProcessKey.Value));
                if (ModelState.IsResponseError(respGet))
                    return View(model);
                if (respGet.Item != null)
                    item = respGet.Item;
            }
            //Only change values displayed (not processing, error, sent date, etc)
            item.BccAddress = model.BccAddress;
            item.Body = model.Body;
            item.CcAddress = model.CcAddress;
            item.CreateDate = DateTimeOffset.UtcNow;
            item.FromAddress = model.FromAddress;
            item.FutureSendDate = model.FutureSendDate;
            item.IsHtml = model.IsHtml;
            item.Subject = model.Subject;
            item.ToAddress = model.ToAddress;

            //Update or insert
            var req = new RequestItem<ServiceModel.EmailProcess>(item);
            if (model.EmailProcessKey.HasValue)
            {
                var respUpdate = _emailProcessServiceRepository.Update(req);
                if (ModelState.IsResponseError(respUpdate))
                    return View(model);
                model.SuccessMessage = "Email updated.";
            }
            else
            {
                var respInsert = _emailProcessServiceRepository.Insert(req);
                if (ModelState.IsResponseError(respInsert))
                    return View(model);
                model.EmailProcessKey = respInsert.Item.EmailProcessKey;
                ModelState.Clear(); //so key gets updated
                model.SuccessMessage = "Email created.";
            }

            //Display form again
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResendEmail(long emailProcessKey)
        {
            //Find existing email
            var resp = _emailProcessServiceRepository.Get(new RequestItem<long>(emailProcessKey));
            if (ModelState.IsResponseError(resp))
                return Json(AjaxResponse.Error("Error getting email."));
            if (resp.Item == null)
                return Json(AjaxResponse.Error("Email not found."));

            //Update the item to be resent
            ServiceModel.EmailProcess item = resp.Item;
            item.IsError = false;
            item.ErrorDate = null;
            item.ErrorMessage = null;
            item.IsProcessing = false;
            item.ProcessingDate = null;
            item.SentDate = null;
            var respUpdate = _emailProcessServiceRepository.Update(new RequestItem<ServiceModel.EmailProcess>(item));
            if (ModelState.IsResponseError(resp))
                return Json(AjaxResponse.Error("Error updating email."));

            return Json(AjaxResponse.SuccessReload("Email marked to be resent."));

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long emailProcessKey)
        {
            //Find existing email
            var resp = _emailProcessServiceRepository.Get(new RequestItem<long>(emailProcessKey));
            if (ModelState.IsResponseError(resp))
                return Json(AjaxResponse.Error("Error getting email."));
            if (resp.Item == null)
                return Json(AjaxResponse.Error("Email not found."));

            var respD = _emailProcessServiceRepository.Delete(new RequestItem<long>(emailProcessKey));
            if (ModelState.IsResponseError(resp))
                return Json(AjaxResponse.Error("Error deleting email."));

            return Json(AjaxResponse.SuccessReload("Email deleted."));            
        }
    }
}

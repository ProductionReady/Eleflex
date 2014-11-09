using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Eleflex.Storage;
using Eleflex.Security.Message;
using Eleflex.Security.Message.UserCommand;

namespace Eleflex.Security.Web.Security.Users
{
    public class UsersController : Controller
    {

        IUserServiceClient _userServiceClient;

        public UsersController(IUserServiceClient userServiceClient)
        {
            _userServiceClient = userServiceClient;
        }


        //
        // GET: /Admin/

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
                if (!string.IsNullOrWhiteSpace(model.Inactive))
                    builder.IsEqual("Inactive", model.Inactive);
                if (!string.IsNullOrWhiteSpace(model.Email))
                    builder.Contains("Email", model.Email);
                if (!string.IsNullOrWhiteSpace(model.Username))
                    builder.Contains("Username", model.Username);
            }

            if (model != null && model.MaxRecords.HasValue)
                builder.Paging(1, model.MaxRecords.Value);
            else
                builder.Paging(1, Constants.MAX_RETURNED_RECORDS_DEFAULT);

            builder.Sort("Username", true);
            var response = _userServiceClient.Query(builder.GetStorageQuery());
            model.Items = response.Items;
            return View(model);
        }

    }
}

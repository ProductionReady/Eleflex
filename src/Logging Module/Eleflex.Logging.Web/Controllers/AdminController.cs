#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//Copyright © 2014 Production Ready, LLC. All Rights Reserved.
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
using System.Web;
using System.Web.Mvc;
using Eleflex.Logging.Message;
using Eleflex.Storage;

namespace Eleflex.Logging.Web.Controllers
{
    /// <summary>
    /// Logging admin controller.
    /// </summary>
    public class AdminController : Controller
    {
        protected readonly ILogServiceClient _logServiceClient;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="logServiceClient"></param>
        public AdminController(ILogServiceClient logServiceClient)
        {
            _logServiceClient = logServiceClient;
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
        public ActionResult List(Eleflex.Logging.Web.Controllers.ListModel model = null)
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
                builder.Paging(1, Constants.MAX_RETURNED_RECORDS_DEFAULT);
            
            builder.Sort("CreateDate", false);
            var response = _logServiceClient.Query(builder.GetStorageQuery());            
            model.Items = response.Items;
            return View(model);
        }

    }
}

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
using System.Web;
using System.Web.Mvc;
using Eleflex.Storage;
using Eleflex.Lookups.Message;

namespace Eleflex.Lookups.Web
{
    /// <summary>
    /// Lookups admin controller.
    /// </summary>
    public class AdminController : Controller
    {
        /// <summary>
        /// Service client for loojups
        /// </summary>
        ILookupsServiceClient _lookupServiceClient = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="lookupServiceClient"></param>
        public AdminController(ILookupsServiceClient lookupServiceClient)
        {
            _lookupServiceClient = lookupServiceClient;
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
        /// Index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            DetailsModel model = new DetailsModel();
            return View("Details", model);
        }

        /// <summary>
        /// Details.
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(DetailsModel model)
        {
            if(ModelState.IsValid)
            {

            }
            return View(model);
        }

        /// <summary>
        /// List.
        /// </summary>
        /// <returns></returns>        
        public ActionResult List(Eleflex.Lookups.Web.ListModel model = null)
        {
            StorageQueryBuilder builder = new StorageQueryBuilder();
            if (model != null)
            {
                if (model.Inactive.HasValue)
                    builder.IsEqual("Inactive", model.Inactive.Value.ToString());

                if (!string.IsNullOrWhiteSpace(model.Abbreviation))
                    builder.Contains("Abbreviation", model.Abbreviation);

                if (!string.IsNullOrWhiteSpace(model.Name))
                    builder.Contains("Name", model.Name);

                if (!string.IsNullOrWhiteSpace(model.Description))
                    builder.Contains("Description", model.Description);

            }

            if (model != null && model.MaxRecords.HasValue)
                builder.Paging(1, model.MaxRecords.Value);
            else
                builder.Paging(1, Constants.MAX_RETURNED_RECORDS_DEFAULT);

            var response = _lookupServiceClient.Query(builder.GetStorageQuery());
            model.Items = response.Items;
            return View(model);
        }

    }
}

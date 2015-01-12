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
using Eleflex.Security.Message;
using Eleflex.Security.Message.RoleCommand;

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
        IRoleServiceClient _roleServiceClient;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="roleServiceClient"></param>
        public RolesController(IRoleServiceClient roleServiceClient)
        {
            _roleServiceClient = roleServiceClient;
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
            DetailsViewModel model = new DetailsViewModel();
            return View("Details", model);
        }

        /// <summary>
        /// Edit.
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string key)
        {
            DetailsViewModel viewModel = new DetailsViewModel();
            return View("Details", viewModel);
        }

        /// <summary>
        /// Details.
        /// </summary>
        /// <returns></returns>
        public ActionResult Details(DetailsViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            return View(model);
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
                if (!string.IsNullOrWhiteSpace(model.Name))
                    builder.Contains("Name", model.Name);
                if (!string.IsNullOrWhiteSpace(model.Description))
                    builder.Contains("Description", model.Description);
            }

            if (model != null && model.MaxRecords.HasValue)
                builder.Paging(1, model.MaxRecords.Value);
            else
                builder.Paging(1, Constants.MAX_RETURNED_RECORDS_DEFAULT);

            builder.Sort("Name", true);
            var response = _roleServiceClient.Query(builder.GetStorageQuery());
            model.Items = response.Items;
            return View(model);
        }
    }
}

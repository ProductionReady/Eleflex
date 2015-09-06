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
using Eleflex.Versioning.Message;
using Eleflex.Storage;

namespace Eleflex.Versioning.Web.Controllers
{
    /// <summary>
    /// Logging admin controller.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        protected readonly IModuleVersionServiceClient _moduleVersionServiceClient;

        /// <summary>
        /// Contructor.
        /// </summary>
        /// <param name="moduleVersionServiceClient"></param>
        public AdminController(IModuleVersionServiceClient moduleVersionServiceClient)
        {
            _moduleVersionServiceClient = moduleVersionServiceClient;
        }

        /// <summary>
        /// Index.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Eleflex.Services.IServiceCommandResponseItems<ModuleVersion> resp = _moduleVersionServiceClient.Query(new StorageQuery());
            if (resp != null && resp.Items != null)
                resp.Items = resp.Items.OrderBy(x => x.ModuleName).ToList();
            return View(resp);
        }

    }
}

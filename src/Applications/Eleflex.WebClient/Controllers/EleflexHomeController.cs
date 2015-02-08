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
using Eleflex.WebClient.Models;

namespace Eleflex.WebClient.Controllers
{
    /// <summary>
    /// Home controller.
    /// </summary>
    public class EleflexHomeController : Controller
    {
        /// <summary>
        /// Default.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// About page.
        /// </summary>
        /// <returns></returns>        
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// AGPL page.
        /// </summary>
        /// <returns></returns>
        public ActionResult AGPL()
        {
            return View();
        }


        /// <summary>
        /// AGPL page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Error(string error)
        {
            ErrorViewModel viewModel = new ErrorViewModel();
            viewModel.Error = error;
            return View(viewModel);
        }
    }
}

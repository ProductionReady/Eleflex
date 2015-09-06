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

namespace Eleflex.Admin.Web.DeveloperTools
{

    [Authorize(Roles = "Admin")]
    public class DeveloperToolsController : Controller
    {
        //
        // GET: /Developer/        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InstalledComponents()
        {
            return View();
        }

        public ActionResult FontAwesomeCheatSheet()
        {
            return View();
        }

        public ActionResult FontAwesomeExamples()
        {
            return View();
        }

        public ActionResult BootstrapCss()
        {
            return View();
        }

        public ActionResult BootstrapComponents()
        {
            return View();
        }

        public ActionResult BootstrapJavascript()
        {
            return View();
        }

    }
}

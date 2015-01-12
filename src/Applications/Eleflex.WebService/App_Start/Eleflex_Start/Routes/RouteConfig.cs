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
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Bootstrap.Extensions.StartupTasks;
using Eleflex.Services;
using Eleflex.Web;
using StructureMap;
using MvcCodeRouting;

namespace Eleflex.WebService.App_Start.Eleflex_Start
{
    /// <summary>
    /// Startup task to register routing configurations for the application.
    /// </summary>
    [Task]
    public class RouteConfig : IStartupTask
    {
        /// <summary>
        /// Run.
        /// </summary>
        public void Run()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Set the default controller
            RouteTable.Routes.MapCodeRoutes(rootController: typeof(Eleflex.WebService.Controllers.EleflexHomeController));
            
            ViewEngines.Engines.EnableCodeRouting();
            ControllerBuilder.Current.EnableCodeRouting();

            //TO VIEW ALL ROUTES AVAILABLE IN APPLICATION, BROWSE TO http://localhost:16185/routes.axd This will only work for local requests, no public access
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public void Reset()
        {
        }
    }
}
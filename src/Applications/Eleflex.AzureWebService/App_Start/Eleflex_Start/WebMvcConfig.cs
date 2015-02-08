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

namespace Eleflex.AzureWebService.App_Start.Eleflex_Start
{
    /// <summary>
    /// Startup task to register MVC configurations for the application.
    /// </summary>
    [Task]
    public class WebMvcConfig : IStartupTask
    {
        /// <summary>
        /// Run.
        /// </summary>
        public void Run()
        {
            //Set default IOC resolver for web controllers
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(Bootstrap.Bootstrapper.Container as IContainer));            
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public void Reset()
        {
        }
    }
}
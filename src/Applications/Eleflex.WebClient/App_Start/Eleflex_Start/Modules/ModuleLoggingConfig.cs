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
using System.Collections.Specialized;
using Bootstrap.Extensions.StartupTasks;
using Eleflex.Services;

namespace Eleflex.WebClient.App_Start.Eleflex_Start
{
    /// <summary>
    /// Startup task to configure logging for the application.
    /// </summary>
    [Task]
    public class ModuleLoggingConfig : IStartupTask
    {
        /// <summary>
        /// Run.
        /// </summary>
        public void Run()
        {           
            NameValueCollection properties = new NameValueCollection();
            properties.Add("Application", "ELEFLEX Client Application");

            //Setup adapter for async storage repository logging
            Common.Logging.LogManager.Adapter = new Eleflex.Logging.Message.CommonLoggingAsyncServiceAdapter(properties);
            
            //Setup logging service endpoint (storage provider not needed)
            StructureMap.IContainer container = Bootstrap.Bootstrapper.Container as StructureMap.IContainer;
            container.Configure(x =>
            {                
                x.For<Eleflex.Logging.Message.ILoggingRequestDispatcher>().Use<Eleflex.Logging.Message.LoggingRequestDispatcher>()
                    .Ctor<string>("endpoint").Is(Eleflex.Services.ServicesConstants.SERVICE_ENDPOINT_NAME_DEFAULT);
            });            
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public void Reset()
        {
        }

    }
}
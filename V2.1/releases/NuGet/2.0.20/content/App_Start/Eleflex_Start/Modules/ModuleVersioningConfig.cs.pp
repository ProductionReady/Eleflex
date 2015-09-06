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
using System.Collections.Specialized;
using Bootstrap.Extensions.StartupTasks;
using Eleflex.Services;

namespace $rootnamespace$.App_Start.Eleflex_Start
{
    /// <summary>
    /// Startup task to configure versioning for the application.
    /// </summary>
    [Task]
    public class ModuleVersioningConfig : IStartupTask
    {
        /// <summary>
        /// Run.
        /// </summary>
        public void Run()
        {
            //Setup storage endpoints
            StructureMap.IContainer container = Bootstrap.Bootstrapper.Container as StructureMap.IContainer;
            container.Configure(x =>
            {
                x.For<Eleflex.Versioning.IVersioningStorageProvider>().Use<Eleflex.Versioning.Storage.SqlServer.VersioningStorageProvider>()
                    .Ctor<string>("connectionStringKey").Is(Eleflex.Storage.Constants.CONNECTION_STRING_KEY_DEFAULT);

                x.For<Eleflex.Versioning.Message.IVersioningRequestDispatcher>().Use<Eleflex.Versioning.Message.VersioningRequestDispatcher>()
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
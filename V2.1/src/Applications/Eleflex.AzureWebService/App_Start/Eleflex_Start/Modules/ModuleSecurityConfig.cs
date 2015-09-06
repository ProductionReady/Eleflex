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
using Microsoft.AspNet.Identity;
using StructureMap.Pipeline;
using StructureMap.Web;

namespace Eleflex.AzureWebService.App_Start.Eleflex_Start
{
    /// <summary>
    /// Startup task to configure security for the application.
    /// </summary>
    [Task]
    public class ModuleSecurityConfig : IStartupTask
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
                x.For<Eleflex.Security.ISecurityStorageProvider>().Use<Eleflex.Security.Storage.Azure.SecurityStorageProvider>()
                    .Ctor<string>("connectionStringKey").Is(Eleflex.Storage.StorageConstants.CONNECTION_STRING_KEY_DEFAULT);

                x.For<Eleflex.Security.Message.ISecurityRequestDispatcher>().Use<Eleflex.Security.Message.SecurityRequestDispatcher>()
                    .Ctor<string>("endpoint").Is(Eleflex.Services.ServicesConstants.SERVICE_ENDPOINT_NAME_DEFAULT);

                //Setup user store for Identity and claims. Use (Eleflex.Web.IdentityUserStoreServiceClient and Eleflex.Web.IdentityRoleStoreServiceClient) for client apps
                x.For<Eleflex.Security.IdentityUserStore<Eleflex.Security.User>>().HybridHttpOrThreadLocalScoped();
                x.For<IUserStore<Eleflex.Security.User>>().LifecycleIs<StructureMap.Pipeline.ThreadLocalStorageLifecycle>().Use<Eleflex.Security.IdentityUserStore<Eleflex.Security.User>>();

                x.For<Eleflex.Security.IdentityRoleStore<Eleflex.Security.Role>>().HybridHttpOrThreadLocalScoped();
                x.For<IRoleStore<Eleflex.Security.Role>>().LifecycleIs<StructureMap.Pipeline.ThreadLocalStorageLifecycle>().Use<Eleflex.Security.IdentityRoleStore<Eleflex.Security.Role>>();
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
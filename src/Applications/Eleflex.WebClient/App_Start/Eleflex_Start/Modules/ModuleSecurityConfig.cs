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

namespace Eleflex.WebClient.App_Start.Eleflex_Start
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
            //Setup service endpoints (storage provider not needed)
            StructureMap.IContainer container = Bootstrap.Bootstrapper.Container as StructureMap.IContainer;
            container.Configure(x =>
            {
                x.For<Eleflex.Security.Message.ISecurityRequestDispatcher>().Use<Eleflex.Security.Message.SecurityRequestDispatcher>()
                    .Ctor<string>("endpoint").Is(Eleflex.Services.ServicesConstants.SERVICE_ENDPOINT_NAME_DEFAULT);

                //Setup user store for Identity and roles (use client configs)
                x.For<Eleflex.Web.IdentityUserStoreServiceClient<Eleflex.Security.User>>().HybridHttpOrThreadLocalScoped();
                x.For<IUserStore<Eleflex.Security.User>>().LifecycleIs<StructureMap.Pipeline.ThreadLocalStorageLifecycle>().Use<Eleflex.Web.IdentityUserStoreServiceClient<Eleflex.Security.User>>();

                x.For<Eleflex.Web.IdentityRoleStoreServiceClient<Eleflex.Security.Role>>().HybridHttpOrThreadLocalScoped();
                x.For<IRoleStore<Eleflex.Security.Role>>().LifecycleIs<StructureMap.Pipeline.ThreadLocalStorageLifecycle>().Use<Eleflex.Web.IdentityRoleStoreServiceClient<Eleflex.Security.Role>>();
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
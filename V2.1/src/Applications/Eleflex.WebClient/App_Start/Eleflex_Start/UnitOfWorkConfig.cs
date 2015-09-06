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
using StructureMap;
using StructureMap.Pipeline;
using StructureMap.Pipeline.Lazy;
using StructureMap.Web;
using StructureMap.Web.Pipeline;
using Bootstrap.Extensions.StartupTasks;
using Eleflex.Storage;

namespace Eleflex.WebClient.App_Start.Eleflex_Start
{
    /// <summary>
    /// Startup task to configure unit of work for the application.
    /// </summary>
    [Task]
    public class UnitOfWorkConfig : IStartupTask
    {
        /// <summary>
        /// Run.
        /// </summary>
        public void Run()
        {
            
            //Set the default unit of work provider to account for storage
            StructureMap.IContainer container = Bootstrap.Bootstrapper.Container as StructureMap.IContainer;
            container.Configure(x =>
            {

                x.For<Eleflex.Storage.StorageProviderUnitOfWork>().HybridHttpOrThreadLocalScoped();

                x.For<Eleflex.Storage.IUnitOfWork>().LifecycleIs<HttpContextLifecycle>().Use(() => container.GetInstance<Eleflex.Storage.StorageProviderUnitOfWork>());
                x.For<Eleflex.Storage.IStorageProviderUnitOfWork>().LifecycleIs<HttpContextLifecycle>().Use(() => container.GetInstance<Eleflex.Storage.StorageProviderUnitOfWork>());

                x.For<Eleflex.Storage.IUnitOfWork>().LifecycleIs<ThreadLocalStorageLifecycle>().Use(() => container.GetInstance<Eleflex.Storage.StorageProviderUnitOfWork>());
                x.For<Eleflex.Storage.IStorageProviderUnitOfWork>().LifecycleIs<ThreadLocalStorageLifecycle>().Use(() => container.GetInstance<Eleflex.Storage.StorageProviderUnitOfWork>());
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
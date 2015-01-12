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
using System.IO;
using System.Reflection;
using Bootstrap.StructureMap;
using CommonServiceLocator.StructureMapAdapter.Unofficial;
using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Graph;

namespace Eleflex
{
    /// <summary>
    /// Registers StructureMap for use with the application by loading/configuring all associated assemblies 
    /// in the hosting project and registering the IOC container with the ServiceLocator for use in object creation later.
    /// </summary>
    public class StructureMapRegistration : Bootstrap.StructureMap.IStructureMapRegistration
    {
        /// <summary>
        /// Implements the registration logic for StructureMap.
        /// </summary>
        /// <param name="container"></param>
        public void Register(IContainer container)
        {
            //Ensure all assemblies are loaded into appdomain for startup components later
            LoadAllAssemblies();

            //Configure the container
            container.Configure(x =>
            {
                x.Scan(scan =>
                    {
                        scan.AssembliesFromApplicationBaseDirectory();
                        scan.WithDefaultConventions();
                    });

                x.Scan(scan =>
                    {
                        scan.AssembliesFromApplicationBaseDirectory();
                        scan.LookForRegistries();
                    });
            });

            //Use ServiceLocator in the rest of the application for IOC instance creation
            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator(Bootstrap.Bootstrapper.Container as IContainer));
        }

        /// <summary>
        /// Load all assemblies.
        /// </summary>
        protected virtual void LoadAllAssemblies()
        {
            List<Assembly> list = new List<Assembly>();
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            foreach (string dll in Directory.GetFiles(basePath, "*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    Assembly loadedAssembly = Assembly.LoadFile(dll);
                }
                catch { }
            }
        }
    }
}
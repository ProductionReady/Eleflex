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
using System.Reflection;
using Bootstrap.Extensions.StartupTasks;
using Common.Logging;
using Eleflex.Services;
using StructureMap;

namespace Eleflex.Services.Server
{
    /// <summary>
    /// Bootstrapper startup task to create the server configuration.
    /// </summary>
    [Task]
    public class ServerServiceConfig : IStartupTask
    {
        /// <summary>
        /// Execute the logic.
        /// </summary>
        public void Run()
        {
            //Register all service command handlers
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct().ToList();
            Type handlerType = typeof(IServiceCommandHandler);
            Type attributeType = typeof(ServiceCommandHandlerAttribute);
            foreach (Assembly assembly in assemblies)
            {                
                List<Type> handlers = assembly.GetTypes().Where(x => handlerType.IsAssignableFrom(x) && x.IsClass && !x.IsAbstract).ToList();
                foreach (Type t in handlers)
                {
                    //The attribute tells us which request object it services. Otherwise, we would have to add a prop/method to the interface and construct an instance to figure it out
                    CustomAttributeData cad = t.CustomAttributes.Where(x => x.AttributeType == attributeType).FirstOrDefault();
                    if (cad != null)
                        ServiceCommandHandlerProvider.Register(cad.ConstructorArguments[0].Value as Type, t);
                }
            }
        }

        /// <summary>
        /// Reset.
        /// </summary>
        public void Reset()
        {
        }

    }
}
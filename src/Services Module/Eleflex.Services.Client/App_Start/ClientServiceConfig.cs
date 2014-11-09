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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bootstrap.Extensions.StartupTasks;
using Eleflex.Services.Client;
using StructureMap;

namespace Eleflex.Services.Client
{
    /// <summary>
    /// Bootstrapper startup task to create the client configuration.
    /// </summary>
    [Task]
    public class ClientServiceConfig : IStartupTask
    {
        /// <summary>
        /// Execute the logic.
        /// </summary>
        public void Run()
        {            
            //Load all types used with a service command handler
            List<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies().Distinct().ToList();
            foreach (Assembly assembly in assemblies)
            {
                //Find types derived from request and reponse interfaces
                ServiceCommandKnownTypeProvider.RegisterAllTypesFromInterface<IServiceCommandRequest>(assembly);
                ServiceCommandKnownTypeProvider.RegisterAllTypesFromInterface<IServiceCommandResponse>(assembly);
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

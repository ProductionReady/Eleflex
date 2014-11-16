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
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace Eleflex.Services.Server
{
    /// <summary>
    /// Allows registering of all known types of service command handlers in the platform.
    /// </summary>
    public static class ServiceCommandHandlerProvider
    {

        /// <summary>
        /// All handlers. The first type is the commandrequest object, the second the commandhandler.
        /// </summary>
        private static Dictionary<string, Type> Handlers = new Dictionary<string, Type>();

        /// <summary>
        /// Clear list of known types.
        /// </summary>
        public static void Clear()
        {
            Handlers.Clear();
        }

        /// <summary>
        /// Get an instance of a service command handler.
        /// </summary>
        /// <returns></returns>
        public static IServiceCommandHandler GetHandler(Type type)
        {
            if (Handlers.ContainsKey(type.FullName))
            {
                IContainer container = Bootstrap.Bootstrapper.Container as IContainer;
                return container.GetInstance(Handlers[type.FullName]) as IServiceCommandHandler;
            }
            return null;
        }

        /// <summary>
        /// Register a type.
        /// </summary>
        /// <param name="type"></param>
        public static void Register(Type requestType, Type handlerType)
        {
            if (!Handlers.ContainsKey(requestType.FullName))
                Handlers.Add(requestType.FullName, handlerType);
        }

    }
}

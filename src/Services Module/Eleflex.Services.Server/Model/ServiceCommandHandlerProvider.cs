using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.ServiceLocation;
using StructureMap;


namespace Eleflex.Services.Server
{
    /// <summary>
    /// Allows registering of all known types of service command handlers in the solution.
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

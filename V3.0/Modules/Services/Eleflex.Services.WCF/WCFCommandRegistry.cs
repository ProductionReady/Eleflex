using System;
using System.Collections.Generic;
using System.Reflection;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Represents a registry of all business rules by type in the application.
    /// </summary>
    public partial class WCFCommandRegistry : Registry<Type, Type>, IWCFCommandRegistryService
    {

        /// <summary>
        /// Private Constructor.
        /// </summary>
        private WCFCommandRegistry() { }

        /// <summary>
        /// Current static instance of the WCFCommandRegistryService.
        /// </summary>
        public static IWCFCommandRegistryService Current = new WCFCommandRegistry();
        

        /// <summary>
        /// Get all known types.
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider) //This property is required on the method according to documentation
        {
            return Current.RegistryCache.Keys;
            
        }

        /// <summary>
        /// Get a WCF command.
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        public virtual IWCFCommand GetCommand(Type requestType)
        {
            if (RegistryCache.ContainsKey(requestType))
            {
                Type commandHandlerType = RegistryCache[requestType];
                IWCFCommand handler = ObjectLocator.Current.GetInstance(commandHandlerType) as IWCFCommand;
                return handler;                
            }
            return null;
        }

    }
}

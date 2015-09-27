using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Represents a registry of all business rules by type in the application.
    /// </summary>
    public static partial class WCFCommandRegistry
    {

        /// <summary>
        /// Current static instance of the WCFCommandRegistryService.
        /// </summary>
        public static IWCFCommandRegistryService Current = new WCFCommandRegistryService();
        

        /// <summary>
        /// Get all known types.
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider) //This "provider" property is required on the method according to documentation
        {
            return Current.RegistryCache.Keys;            
        }

    }
}

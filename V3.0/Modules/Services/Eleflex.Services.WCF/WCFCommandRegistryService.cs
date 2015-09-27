using System;
using System.Collections.Generic;
using System.Linq;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Represents a registry service that passthrough method calls to the WCFComanndRegistry.Current instance.
    /// </summary>
    public partial class WCFCommandRegistryService : Registry<Type, Type>, IWCFCommandRegistryService
    {

        /// <summary>
        /// Get a WCF command.
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        public virtual IWCFCommand GetCommand(Type requestType)
        {
            if (requestType == null)
                return null;

            Type key = RegistryCache.Keys.Where(x => x.FullName == requestType.FullName).FirstOrDefault();
            if (key == null)
                return null;

            Type commandHandlerType = RegistryCache[key];
            if (commandHandlerType == null)
                return null;

            var handler = ObjectLocator.Current.GetInstance(commandHandlerType);
            if (handler != null)
                return handler as IWCFCommand;
            return null;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Represents a registry service that passthrough method calls to the WCFComanndRegistry.Current instance.
    /// </summary>
    public partial class WCFCommandRegistryService : IWCFCommandRegistryService
    {

        /// <summary>
        /// Get the registry list.
        /// </summary>
        public virtual Dictionary<Type, Type> RegistryCache
        {
            get { return WCFCommandRegistry.Current.RegistryCache; }
            set { WCFCommandRegistry.Current.RegistryCache = value; }
        }

        /// <summary>
        /// Get a WCF command.
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        public virtual IWCFCommand GetCommand(Type requestType)
        {
            return WCFCommandRegistry.Current.GetCommand(requestType);
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual Type GetRegistryItem(Type key)
        {
            return WCFCommandRegistry.Current.GetRegistryItem(key);
        }

        /// <summary>
        /// Register an item.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void RegisterItem(Type key, Type value)
        {
            WCFCommandRegistry.Current.RegisterItem(key, value);
        }

        /// <summary>
        /// Unregister an item.
        /// </summary>
        /// <param name="key"></param>
        public virtual void UnRegisterItem(Type key)
        {
            WCFCommandRegistry.Current.UnRegisterItem(key);
        }
    }
}

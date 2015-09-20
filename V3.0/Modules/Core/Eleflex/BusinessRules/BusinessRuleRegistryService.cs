using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a registry of all business rules by type in the application.
    /// </summary>
    public partial class BusinessRuleRegistryService : IBusinessRuleRegistryService
    {        

        /// <summary>
        /// The collection of registry entries.
        /// </summary>
        public virtual Dictionary<Type, IList<Type>> RegistryCache
        {
            get
            {
                return BusinessRuleRegistry.Current.RegistryCache;
            }

            set
            {
                BusinessRuleRegistry.Current.RegistryCache = value;
            }
        }

        /// <summary>
        /// Get the list of types for the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual IList<Type> GetRegistryList(Type key)
        {
            return BusinessRuleRegistry.Current.GetRegistryList(key);
        }

        /// <summary>
        /// Register an item.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void RegisterItem(Type key, Type value)
        {
            BusinessRuleRegistry.Current.RegisterItem(key, value);
        }

        /// <summary>
        /// Unregister all items.
        /// </summary>
        /// <param name="key"></param>
        public virtual void UnRegisterItem(Type key)
        {
            BusinessRuleRegistry.Current.UnRegisterItem(key);
        }

        /// <summary>
        /// Unregister a single item from the list.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void UnRegisterItem(Type key, Type value)
        {
            BusinessRuleRegistry.Current.UnRegisterItem(key, value);
        }
    }
}

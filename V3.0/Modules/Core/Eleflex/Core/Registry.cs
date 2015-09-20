using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a Registry of items stored in the application.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public partial class Registry<TKey, TValue> : IRegistry<TKey, TValue>
    {        

        /// <summary>
        /// Constructor.
        /// </summary>
        public Registry()
        {
            RegistryCache = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Static instance for the registry.
        /// </summary>
        public static Registry<TKey, TValue> RegistryInstance = new Registry<TKey, TValue>();

        /// <summary>
        /// The collection of registry entries.
        /// </summary>
        public virtual Dictionary<TKey,TValue> RegistryCache { get; set; }
        
        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TValue GetRegistryItem(TKey key)
        {
            if (RegistryCache.ContainsKey(key))
                return RegistryCache[key];
            return default(TValue);
        }

        /// <summary>
        /// Register an item.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public virtual void RegisterItem(TKey key, TValue data)
        {
            if (RegistryCache.ContainsKey(key))
                RegistryCache[key] = data;
            else
                RegistryCache.Add(key, data);            
        }

        /// <summary>
        /// Unregister an item.
        /// </summary>
        /// <param name="key"></param>
        public virtual void UnRegisterItem(TKey key)
        {
            if (RegistryCache.ContainsKey(key))
                RegistryCache.Remove(key);
        }
    }
}

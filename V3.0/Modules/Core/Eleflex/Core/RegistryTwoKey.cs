using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a Registry of items stored in the application using two keys.
    /// </summary>
    /// <typeparam name="TKey1"></typeparam>
    /// <typeparam name="TKey2"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public partial class RegistryTwoKey<TKey1, TKey2, TValue> : IRegistryTwoKey<TKey1, TKey2, TValue>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public RegistryTwoKey()
        {
            RegistryCache = new Dictionary<TKey1, IDictionary<TKey2, TValue>>();
        }

        /// <summary>
        /// The collection of registry entries.
        /// </summary>
        public virtual IDictionary<TKey1, IDictionary<TKey2, TValue>> RegistryCache { get; set; }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        public virtual TValue GetRegistryItem(TKey1 key1, TKey2 key2)
        {
            if (RegistryCache.ContainsKey(key1))
            {
                if (RegistryCache[key1].ContainsKey(key2))
                    return RegistryCache[key1][key2];
            }                
            return default(TValue);
        }

        /// <summary>
        /// Register an item.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <param name="value"></param>
        public virtual void RegisterItem(TKey1 key1, TKey2 key2, TValue value)
        {
            if (RegistryCache.ContainsKey(key1))
            {
                if (RegistryCache[key1].ContainsKey(key2))
                    RegistryCache[key1][key2] = value;
                else
                    RegistryCache[key1].Add(key2, value);
            }
            else
            {
                IDictionary<TKey2, TValue> subset = new Dictionary<TKey2, TValue>();
                subset.Add(key2, value);
                RegistryCache.Add(key1, subset);
            }
        }

        /// <summary>
        /// Unregister all items.
        /// </summary>
        /// <param name="key1"></param>
        public virtual void UnRegisterItem(TKey1 key1)
        {
            if (RegistryCache.ContainsKey(key1))
            {
                RegistryCache.Remove(key1);
            }
        }

        /// <summary>
        /// Unregister an individual item.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        public virtual void UnRegisterItem(TKey1 key1, TKey2 key2)
        {
            if (RegistryCache.ContainsKey(key1))
            {
                if(RegistryCache[key1].ContainsKey(key2))
                    RegistryCache[key1].Remove(key2);
            }
        }
    }
}

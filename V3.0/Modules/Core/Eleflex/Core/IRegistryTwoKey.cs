using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a Registry of items stored in the application using two keys.
    /// </summary>
    /// <typeparam name="TKey1"></typeparam>
    /// <typeparam name="TKey2"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public partial interface IRegistryTwoKey<TKey1, TKey2, TValue>
    {

        /// <summary>
        /// The collection of registry entries.
        /// </summary>
        IDictionary<TKey1, IDictionary<TKey2, TValue>> RegistryCache { get; set; }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <returns></returns>
        TValue GetRegistryItem(TKey1 key1, TKey2 key2);

        /// <summary>
        /// Register an item.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        /// <param name="value"></param>
        void RegisterItem(TKey1 key1, TKey2 key2, TValue value);

        /// <summary>
        /// Unregister all items.
        /// </summary>
        /// <param name="key1"></param>
        void UnRegisterItem(TKey1 key1);

        /// <summary>
        /// Unregister an individual item.
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="key2"></param>
        void UnRegisterItem(TKey1 key1, TKey2 key2);
    }
}

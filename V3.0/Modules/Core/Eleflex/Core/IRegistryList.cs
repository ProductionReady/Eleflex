using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a Registry list of items stored in the application.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public partial interface IRegistryList<TKey, TValue>
    {

        /// <summary>
        /// The collection of registry entries.
        /// </summary>
        Dictionary<TKey, IList<TValue>> RegistryCache { get; set; }

        /// <summary>
        /// Get a list of items.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IList<TValue> GetRegistryList(TKey key);

        /// <summary>
        /// Register an item.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void RegisterItem(TKey key, TValue value);

        /// <summary>
        /// Unregister all items.
        /// </summary>
        /// <param name="key"></param>
        void UnRegisterItem(TKey key);

        /// <summary>
        /// Unregister a single item.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void UnRegisterItem(TKey key, TValue value);
    }
}

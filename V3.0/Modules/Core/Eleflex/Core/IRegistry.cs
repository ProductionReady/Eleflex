using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a Registry of items stored in the application.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public partial interface IRegistry<TKey, TValue>
    {

        /// <summary>
        /// The collection of registry entries.
        /// </summary>
        Dictionary<TKey, TValue> RegistryCache { get; set; }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        TValue GetRegistryItem(TKey key);

        /// <summary>
        /// Register an item.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void RegisterItem(TKey key, TValue value);

        /// <summary>
        /// Unregister an item.
        /// </summary>
        /// <param name="key"></param>
        void UnRegisterItem(TKey key);
    }
}

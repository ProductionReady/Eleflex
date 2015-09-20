using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a Registry list of items stored in the application.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public partial class RegistryList<TKey, TValue> : IRegistryList<TKey, TValue>
    {        

        /// <summary>
        /// Constructor.
        /// </summary>
        public RegistryList()
        {
            RegistryCache = new Dictionary<TKey, IList<TValue>>();
        }

        /// <summary>
        /// The collection of registry entries.
        /// </summary>
        public virtual Dictionary<TKey, IList<TValue>> RegistryCache { get; set; }

        /// <summary>
        /// Get a list of items.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual IList<TValue> GetRegistryList(TKey key)
        {
            if (RegistryCache.ContainsKey(key))
                return RegistryCache[key];
            return null;
        }

        /// <summary>
        /// Register an item.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void RegisterItem(TKey key, TValue value)
        {
            if (RegistryCache.ContainsKey(key))
            {
                IList<TValue> list = RegistryCache[key];
                foreach (var item in list)
                {
                    if (item.Equals(value))
                        return;
                }
                RegistryCache[key].Add(value);
            }
            else
            {
                IList<TValue> list = new List<TValue>();
                list.Add(value);
                RegistryCache.Add(key, list);
            }
        }

        /// <summary>
        /// Unregister all items.
        /// </summary>
        /// <param name="key"></param>
        public virtual void UnRegisterItem(TKey key)
        {
            if (RegistryCache.ContainsKey(key))
            {
                RegistryCache.Remove(key);
            }
        }

        /// <summary>
        /// Unregister an individual item.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public virtual void UnRegisterItem(TKey key, TValue value)
        {
            if (RegistryCache.ContainsKey(key))
            {
                IList<TValue> list = RegistryCache[key];
                for(int i=0;i<list.Count;i++)
                {
                    if(list[i].Equals(value))
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }                
            }
        }
    }
}

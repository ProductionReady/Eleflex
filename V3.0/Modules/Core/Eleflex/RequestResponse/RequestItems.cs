using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// A Request object containing a collection a collection of typed object items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class RequestItems<T> : Request, IRequestItems<T>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public RequestItems()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="items"></param>
        public RequestItems(IList<T> items)
        {
            Items = items;
        }

        /// <summary>
        /// The collection of request typed object items.
        /// </summary>
        public virtual IList<T> Items { get; set; }

    }
}

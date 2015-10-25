using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// A Response object containing a collection of typed object items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class ResponseItems<T> : Response, IResponseItems<T>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseItems()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="items"></param>
        public ResponseItems(IList<T> items)
        {
            Items = items;
        }

        /// <summary>
        /// The collection of response typed object items.
        /// </summary>
        public virtual IList<T> Items { get; set; }
        
    }
}

namespace Eleflex
{
    /// <summary>
    /// A Request object containing a single typed object item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class RequestItem<T> : Request, IRequestItem<T>
    {


        /// <summary>
        /// Constructor.
        /// </summary>
        public RequestItem()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="item"></param>
        public RequestItem(T item)
        {
            Item = item;
        }

        /// <summary>
        /// The typed object item.
        /// </summary>
        public virtual T Item { get; set; }

    }
}

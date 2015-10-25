namespace Eleflex
{
    /// <summary>
    /// A Response object containing a single typed object item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class ResponseItem<T> : Response, IResponseItem<T>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public ResponseItem()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="item"></param>
        public ResponseItem(T item)
        {
            Item = item;
        }

        /// <summary>
        /// The response typed object item.
        /// </summary>
        public virtual T Item { get; set; }

    }
}

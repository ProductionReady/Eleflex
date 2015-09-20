namespace Eleflex
{
    /// <summary>
    /// A Response object containing a single typed object item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class ResponseItem<T> : Response, IResponseItem<T>
    {
        /// <summary>
        /// The response typed object item.
        /// </summary>
        public virtual T Item { get; set; }

    }
}

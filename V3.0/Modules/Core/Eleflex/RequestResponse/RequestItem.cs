namespace Eleflex
{
    /// <summary>
    /// A Request object containing a single typed object item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class RequestItem<T> : Request, IRequestItem<T>
    {
        /// <summary>
        /// The typed object item.
        /// </summary>
        public virtual T Item { get; set; }

    }
}

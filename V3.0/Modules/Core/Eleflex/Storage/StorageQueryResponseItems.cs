namespace Eleflex
{
    /// <summary>
    /// Represents A ResponseItems object that contains the total count of records if using paging.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class StorageQueryResponseItems<T> : ResponseItems<T>, IStorageQueryResponseItems<T>
    {

        /// <summary>
        /// The total count of records available if using paging.
        /// </summary>
        public virtual double PagingTotalCount { get; set; }
    }
}

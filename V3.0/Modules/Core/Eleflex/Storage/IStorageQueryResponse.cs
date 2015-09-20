namespace Eleflex
{
    public partial interface IStorageQueryResponse
    {
        /// <summary>
        /// The total count of records available if using paging.
        /// </summary>
        double PagingTotalCount { get; set; }
    }
}

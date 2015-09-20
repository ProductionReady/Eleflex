namespace Eleflex
{
    /// <summary>
    /// Represents an object with paging parameters.
    /// </summary>
    public partial interface IPaging
    {
        /// <summary>
        /// The start page.
        /// </summary>
        int PagingStartPage { get; set; }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        int PagingNumberPerPage { get; set; }
    }
}

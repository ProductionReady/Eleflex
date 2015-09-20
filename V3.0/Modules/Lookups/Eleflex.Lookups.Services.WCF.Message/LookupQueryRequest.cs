namespace Eleflex.Lookups.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class LookupQueryRequest : RequestItem<StorageQuery>
    {
        public LookupQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

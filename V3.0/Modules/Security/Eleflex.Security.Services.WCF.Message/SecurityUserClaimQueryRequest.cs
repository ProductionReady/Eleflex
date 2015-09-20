namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class SecurityUserClaimQueryRequest : RequestItem<IStorageQuery>
    {
        public SecurityUserClaimQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

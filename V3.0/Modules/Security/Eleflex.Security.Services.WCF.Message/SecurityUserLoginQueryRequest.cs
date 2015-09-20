namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class SecurityUserLoginQueryRequest : RequestItem<IStorageQuery>
    {
        public SecurityUserLoginQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

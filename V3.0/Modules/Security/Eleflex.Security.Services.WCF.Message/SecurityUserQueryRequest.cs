namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class SecurityUserQueryRequest : RequestItem<IStorageQuery>
    {
        public SecurityUserQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

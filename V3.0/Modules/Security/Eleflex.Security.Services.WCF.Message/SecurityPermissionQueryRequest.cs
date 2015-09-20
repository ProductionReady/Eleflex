namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class SecurityPermissionQueryRequest : RequestItem<IStorageQuery>
    {
        public SecurityPermissionQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

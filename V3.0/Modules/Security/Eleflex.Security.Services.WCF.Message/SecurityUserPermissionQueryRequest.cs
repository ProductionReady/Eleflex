namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class SecurityUserPermissionQueryRequest : RequestItem<IStorageQuery>
    {
        public SecurityUserPermissionQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

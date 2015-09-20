namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class SecurityRolePermissionQueryRequest : RequestItem<IStorageQuery>
    {
        public SecurityRolePermissionQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

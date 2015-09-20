namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class SecurityRoleRoleQueryRequest : RequestItem<IStorageQuery>
    {
        public SecurityRoleRoleQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

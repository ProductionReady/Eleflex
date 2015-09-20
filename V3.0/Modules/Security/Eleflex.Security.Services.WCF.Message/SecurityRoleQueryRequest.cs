namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class SecurityRoleQueryRequest : RequestItem<IStorageQuery>
    {
        public SecurityRoleQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

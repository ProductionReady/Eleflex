namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class SecurityUserRoleQueryRequest : RequestItem<IStorageQuery>
    {
        public SecurityUserRoleQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

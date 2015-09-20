namespace Eleflex.Versioning.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class ModuleQueryRequest : RequestItem<IStorageQuery>
    {
        public ModuleQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

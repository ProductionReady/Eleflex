namespace Eleflex.Logging.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class LogMessageQueryRequest : RequestItem<IStorageQuery>
    {
        public LogMessageQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

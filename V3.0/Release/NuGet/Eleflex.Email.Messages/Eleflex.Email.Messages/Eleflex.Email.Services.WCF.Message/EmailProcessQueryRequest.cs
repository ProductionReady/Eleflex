namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class EmailProcessQueryRequest : RequestItem<StorageQuery>
    {
        public EmailProcessQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

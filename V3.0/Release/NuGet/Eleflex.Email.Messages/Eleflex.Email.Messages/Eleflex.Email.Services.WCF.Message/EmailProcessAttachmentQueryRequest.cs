namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Query request.
    /// </summary>
    public partial class EmailProcessAttachmentQueryRequest : RequestItem<StorageQuery>
    {
        public EmailProcessAttachmentQueryRequest()
        {
            Item = new StorageQuery();
        }
    }
}

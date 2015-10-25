using ServiceMessages = Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Represents a EmailProcessAttachment  service repository.
    /// </summary>
    public partial interface IEmailProcessAttachmentServiceRepository : IServiceRepository<ServiceMessages.EmailProcessAttachment, long>
    {
    }
}

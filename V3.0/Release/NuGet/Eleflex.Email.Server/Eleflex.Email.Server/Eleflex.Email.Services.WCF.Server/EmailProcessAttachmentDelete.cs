using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Server
{
    /// <summary>
    /// Service command to delete an object.
    /// </summary>
    [WCFCommandRegistration(typeof(EmailProcessAttachmentDeleteRequest), typeof(EmailProcessAttachmentDeleteResponse))]
    public partial class EmailProcessAttachmentDelete : WCFCommand<EmailProcessAttachmentDeleteRequest, EmailProcessAttachmentDeleteResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly IEmailProcessAttachmentBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public EmailProcessAttachmentDelete(IEmailProcessAttachmentBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(EmailProcessAttachmentDeleteRequest request, EmailProcessAttachmentDeleteResponse response)
        {
            var resp = _repository.Delete(new RequestItem<long>() { Item = request.Item });
            response.CopyResponse(resp);
        }
    }
}


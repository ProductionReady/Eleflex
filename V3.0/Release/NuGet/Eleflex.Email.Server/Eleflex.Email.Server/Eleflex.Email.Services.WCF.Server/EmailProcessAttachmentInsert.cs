using Eleflex.Services.WCF;
using System.Security.Permissions;
using DomainModel = Eleflex.Email;
using ServiceModel = Eleflex.Email.Services.WCF.Message;
using Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Server
{
    /// <summary>
    /// Service command to create an object.
    /// </summary>
    [WCFCommandRegistration(typeof(EmailProcessAttachmentInsertRequest), typeof(EmailProcessAttachmentInsertResponse))]
    public partial class EmailProcessAttachmentInsert : WCFCommand<EmailProcessAttachmentInsertRequest, EmailProcessAttachmentInsertResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly IEmailProcessAttachmentBusinessRepository _repository;
        /// <summary>
        /// The mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mappingService"></param>
        public EmailProcessAttachmentInsert(
            IEmailProcessAttachmentBusinessRepository repository,
            IMappingService mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>        
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(EmailProcessAttachmentInsertRequest request, EmailProcessAttachmentInsertResponse response)
        {
            var item = _mappingService.Map<DomainModel.EmailProcessAttachment, ServiceModel.EmailProcessAttachment>(request.Item);            
            var resp = _repository.Insert(new RequestItem<EmailProcessAttachment>() {Item = item });
            response.CopyResponse(resp);
            response.Item = _mappingService.Map<ServiceModel.EmailProcessAttachment, DomainModel.EmailProcessAttachment>(resp.Item);
        }
    }
}

using System;
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
    [WCFCommandRegistration(typeof(SendEmailRequest), typeof(ResponseItem<long>))]
    public partial class EmailServiceSendEmail : WCFCommand<SendEmailRequest, ResponseItem<long>>
    {

        protected readonly IStorageContextUnitOfWork _uow;
        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly IEmailProcessAttachmentBusinessRepository _emailProcessAttachmentRepository;
        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly IEmailProcessBusinessRepository _emailProcessRepository;
        /// <summary>
        /// The mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mappingService"></param>
        public EmailServiceSendEmail(
            IStorageContextUnitOfWork uow,
            IEmailProcessBusinessRepository emailProcessRepository,
            IEmailProcessAttachmentBusinessRepository emailProcessAttachmentRepository,
            IMappingService mappingService)
        {
            _uow = uow;
            _emailProcessRepository = emailProcessRepository;
            _emailProcessAttachmentRepository = emailProcessAttachmentRepository;
            _mappingService = mappingService;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>        
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(SendEmailRequest request, ResponseItem<long> response)
        {            
            if(request.Item == null)
            {
                response.AddMessage(true, MessageConstants.ERROR_SERVICES_CODE, "Item is null");
                return;
            }

            DomainModel.EmailProcess item = new EmailProcess();
            item.BccAddress = request.Item.BccAddress;
            item.Body = request.Item.Body;
            item.CcAddress = request.Item.CcAddress;
            item.CreateDate = DateTimeOffset.UtcNow;
            item.ErrorDate = null;
            item.ErrorMessage = null;
            item.FromAddress = request.Item.FromAddress;
            item.FutureSendDate = request.FutureSendDate;
            item.IsError = false;
            item.IsHtml = request.Item.IsHtml;
            item.IsProcessing = false;
            item.ProcessingDate = null;
            item.SentDate = null;
            item.Subject = request.Item.Subject;
            item.ToAddress = request.Item.ToAddress;
            var respInsert = _emailProcessRepository.Insert(new RequestItem<EmailProcess>(item));
            response.CopyResponse(respInsert);
            if(respInsert.Item != null)
                response.Item = respInsert.Item.EmailProcessKey;
            if(respInsert.ResponseSuccess && request.Item.Attachments != null && request.Item.Attachments.Count > 0)
            {
                foreach(Attachment reqAtt in request.Item.Attachments)
                {
                    DomainModel.EmailProcessAttachment epa = new EmailProcessAttachment();
                    epa.EmailProcessKey = respInsert.Item.EmailProcessKey;
                    epa.FileData = reqAtt.Data;
                    epa.Filename = reqAtt.Filename;
                    var respAttInsert = _emailProcessAttachmentRepository.Insert(new RequestItem<DomainModel.EmailProcessAttachment>(epa));
                    response.CopyResponse(respAttInsert);
                    if(!respAttInsert.ResponseSuccess)
                    {
                        _uow.Rollback();
                        return;
                    }
                }

            }
        }
    }
}

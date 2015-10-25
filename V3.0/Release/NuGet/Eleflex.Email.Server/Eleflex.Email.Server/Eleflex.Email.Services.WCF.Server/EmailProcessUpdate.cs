using Eleflex.Services.WCF;
using System.Security.Permissions;
using DomainModel = Eleflex.Email;
using ServiceModel = Eleflex.Email.Services.WCF.Message;
using Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Server
{
    /// <summary>
    /// Service command to update an object.
    /// </summary>
    [WCFCommandRegistration(typeof(EmailProcessUpdateRequest), typeof(EmailProcessUpdateResponse))]
    public partial class EmailProcessUpdate : WCFCommand<EmailProcessUpdateRequest, EmailProcessUpdateResponse>
    {

        /// <summary>
        /// The storage repository.
        /// </summary>
        protected readonly IEmailProcessBusinessRepository _repository;

        /// <summary>
        /// THe mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mappingService"></param>
        public EmailProcessUpdate(
            IEmailProcessBusinessRepository repository,
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
        public override void Execute(EmailProcessUpdateRequest request, EmailProcessUpdateResponse response)
        {
            var item =_mappingService.Map<DomainModel.EmailProcess, ServiceModel.EmailProcess>(request.Item);
            var resp = _repository.Update(new RequestItem<EmailProcess>() { Item = item });                        
            response.CopyResponse(resp);
            response.Item = _mappingService.Map<DomainModel.EmailProcess, ServiceModel.EmailProcess>(resp.Item);
            
        }
    }
}

using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Email.Services.WCF.Message;
using DomainModel = Eleflex.Email;
using ServiceModel = Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Server
{
    /// <summary>
    /// Service command to get an object.
    /// </summary>
    [WCFCommandRegistration(typeof(EmailProcessGetRequest), typeof(EmailProcessGetResponse))]
    public partial class EmailProcessGet : WCFCommand<EmailProcessGetRequest, EmailProcessGetResponse>
    {

        /// <summary>
        /// The storage repository.
        /// </summary>
        protected readonly IEmailProcessBusinessRepository _repository;

        /// <summary>
        /// The mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public EmailProcessGet(
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
        public override void Execute(EmailProcessGetRequest request, EmailProcessGetResponse response)
        {            
            var resp = _repository.Get(new RequestItem<long>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = _mappingService.Map<DomainModel.EmailProcess, ServiceModel.EmailProcess>(resp.Item);
        }
    }
}

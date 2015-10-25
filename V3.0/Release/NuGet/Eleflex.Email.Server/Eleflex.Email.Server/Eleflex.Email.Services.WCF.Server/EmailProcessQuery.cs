using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Email.Services.WCF.Message;
using DomainModel = Eleflex.Email;
using ServiceModel = Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Server
{
    /// <summary>
    /// Service command to query objects.
    /// </summary>        
    [WCFCommandRegistration(typeof(EmailProcessQueryRequest), typeof(EmailProcessQueryResponse))]
    public partial class EmailProcessQuery : WCFCommand<EmailProcessQueryRequest, EmailProcessQueryResponse>
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
        public EmailProcessQuery(IEmailProcessBusinessRepository repository,
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
        public override void Execute(EmailProcessQueryRequest request, EmailProcessQueryResponse response)
        {
            var resp = _repository.Query(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Items = _mappingService.Map<DomainModel.EmailProcess, ServiceModel.EmailProcess>(resp.Items);
        }
    }
}
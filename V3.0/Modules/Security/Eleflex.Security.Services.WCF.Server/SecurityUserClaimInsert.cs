using Eleflex.Services.WCF;
using System.Security.Permissions;
using DomainModel = Eleflex;
using ServiceModel = Eleflex;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Security.Services.WCF.Server
{
    /// <summary>
    /// Service command to create an object.
    /// </summary>
    [WCFCommandRegistration(typeof(SecurityUserClaimInsertRequest), typeof(SecurityUserClaimInsertResponse))]
    public partial class SecurityUserClaimInsert : WCFCommand<SecurityUserClaimInsertRequest, SecurityUserClaimInsertResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly ISecurityUserClaimBusinessRepository _repository;
        /// <summary>
        /// The mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mappingService"></param>
        public SecurityUserClaimInsert(
            ISecurityUserClaimBusinessRepository repository,
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
        public override void Execute(SecurityUserClaimInsertRequest request, SecurityUserClaimInsertResponse response)
        {
            var resp = _repository.Insert(new RequestItem<SecurityUserClaim>() {Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}

using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Security.Services.WCF.Message;
using DomainModel = Eleflex;
using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Server
{
    /// <summary>
    /// Service command to get an object.
    /// </summary>
    [WCFCommandRegistration(typeof(SecurityUserClaimGetRequest), typeof(SecurityUserClaimGetResponse))]
    public partial class SecurityUserClaimGet : WCFCommand<SecurityUserClaimGetRequest, SecurityUserClaimGetResponse>
    {

        /// <summary>
        /// The storage repository.
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
        public SecurityUserClaimGet(
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
        public override void Execute(SecurityUserClaimGetRequest request, SecurityUserClaimGetResponse response)
        {            
            var resp = _repository.Get(new RequestItem<long>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}

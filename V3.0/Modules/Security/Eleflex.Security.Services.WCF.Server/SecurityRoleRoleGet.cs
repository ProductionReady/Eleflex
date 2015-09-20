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
    [WCFCommandRegistration(typeof(SecurityRoleRoleGetRequest), typeof(SecurityRoleRoleGetResponse))]
    public partial class SecurityRoleRoleGet : WCFCommand<SecurityRoleRoleGetRequest, SecurityRoleRoleGetResponse>
    {

        /// <summary>
        /// The storage repository.
        /// </summary>
        protected readonly ISecurityRoleRoleBusinessRepository _repository;

        /// <summary>
        /// The mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public SecurityRoleRoleGet(
            ISecurityRoleRoleBusinessRepository repository,
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
        public override void Execute(SecurityRoleRoleGetRequest request, SecurityRoleRoleGetResponse response)
        {            
            var resp = _repository.Get(new RequestItem<long>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}

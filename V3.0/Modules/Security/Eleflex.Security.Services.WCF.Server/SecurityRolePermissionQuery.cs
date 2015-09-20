using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Security.Services.WCF.Message;
using DomainModel = Eleflex;
using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Server
{
    /// <summary>
    /// Service command to query objects.
    /// </summary>        
    [WCFCommandRegistration(typeof(SecurityRolePermissionQueryRequest), typeof(SecurityRolePermissionQueryResponse))]
    public partial class SecurityRolePermissionQuery : WCFCommand<SecurityRolePermissionQueryRequest, SecurityRolePermissionQueryResponse>
    {

        /// <summary>
        /// The storage repository.
        /// </summary>
        protected readonly ISecurityRolePermissionBusinessRepository _repository;

        /// <summary>
        /// The mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public SecurityRolePermissionQuery(ISecurityRolePermissionBusinessRepository repository,
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
        public override void Execute(SecurityRolePermissionQueryRequest request, SecurityRolePermissionQueryResponse response)
        {
            var resp = _repository.Query(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Items = resp.Items;
        }
    }
}

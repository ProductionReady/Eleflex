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
    [WCFCommandRegistration(typeof(SecurityPermissionQueryRequest), typeof(SecurityPermissionQueryResponse))]
    public partial class SecurityPermissionQuery : WCFCommand<SecurityPermissionQueryRequest, SecurityPermissionQueryResponse>
    {

        /// <summary>
        /// The storage repository.
        /// </summary>
        protected readonly ISecurityPermissionBusinessRepository _repository;

        /// <summary>
        /// The mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public SecurityPermissionQuery(ISecurityPermissionBusinessRepository repository,
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
        public override void Execute(SecurityPermissionQueryRequest request, SecurityPermissionQueryResponse response)
        {
            var resp = _repository.Query(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Items = resp.Items;
        }
    }
}

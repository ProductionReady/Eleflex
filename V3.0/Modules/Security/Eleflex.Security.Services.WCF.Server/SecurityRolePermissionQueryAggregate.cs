using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Security.Services.WCF.Server
{
    /// <summary>
    /// Service command to query objects for aggregate.
    /// </summary>
    [WCFCommandRegistration(typeof(SecurityRolePermissionQueryAggregateRequest), typeof(SecurityRolePermissionQueryAggregateResponse))]
    public partial class SecurityRolePermissionQueryAggregate : WCFCommand<SecurityRolePermissionQueryAggregateRequest, SecurityRolePermissionQueryAggregateResponse>
    {

        /// <summary>
        /// THe storage repository.
        /// </summary>
        private readonly ISecurityRolePermissionBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public SecurityRolePermissionQueryAggregate(ISecurityRolePermissionBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(SecurityRolePermissionQueryAggregateRequest request, SecurityRolePermissionQueryAggregateResponse response)
        {
            var resp = _repository.QueryAggregate(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}


using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Security.Services.WCF.Server
{
    /// <summary>
    /// Service command to query objects for aggregate.
    /// </summary>
    [WCFCommandRegistration(typeof(SecurityRoleQueryAggregateRequest), typeof(SecurityRoleQueryAggregateResponse))]
    public partial class SecurityRoleQueryAggregate : WCFCommand<SecurityRoleQueryAggregateRequest, SecurityRoleQueryAggregateResponse>
    {

        /// <summary>
        /// THe storage repository.
        /// </summary>
        private readonly ISecurityRoleBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public SecurityRoleQueryAggregate(ISecurityRoleBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(SecurityRoleQueryAggregateRequest request, SecurityRoleQueryAggregateResponse response)
        {
            var resp = _repository.QueryAggregate(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}


using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Security.Services.WCF.Server
{
    /// <summary>
    /// Service command to query objects for aggregate.
    /// </summary>
    [WCFCommandRegistration(typeof(SecurityUserRoleQueryAggregateRequest), typeof(SecurityUserRoleQueryAggregateResponse))]
    public partial class SecurityUserRoleQueryAggregate : WCFCommand<SecurityUserRoleQueryAggregateRequest, SecurityUserRoleQueryAggregateResponse>
    {

        /// <summary>
        /// THe storage repository.
        /// </summary>
        private readonly ISecurityUserRoleBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public SecurityUserRoleQueryAggregate(ISecurityUserRoleBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(SecurityUserRoleQueryAggregateRequest request, SecurityUserRoleQueryAggregateResponse response)
        {
            var resp = _repository.QueryAggregate(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}


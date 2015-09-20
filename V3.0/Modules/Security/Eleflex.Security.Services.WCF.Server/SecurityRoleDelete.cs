using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Security.Services.WCF.Server
{
    /// <summary>
    /// Service command to delete an object.
    /// </summary>
    [WCFCommandRegistration(typeof(SecurityRoleDeleteRequest), typeof(SecurityRoleDeleteResponse))]
    public partial class SecurityRoleDelete : WCFCommand<SecurityRoleDeleteRequest, SecurityRoleDeleteResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly ISecurityRoleBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public SecurityRoleDelete(ISecurityRoleBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(SecurityRoleDeleteRequest request, SecurityRoleDeleteResponse response)
        {
            var resp = _repository.Delete(new RequestItem<System.Guid>() { Item = request.Item });
            response.CopyResponse(resp);
        }
    }
}


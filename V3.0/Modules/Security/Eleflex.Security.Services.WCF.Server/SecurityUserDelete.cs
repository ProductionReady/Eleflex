using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Security.Services.WCF.Server
{
    /// <summary>
    /// Service command to delete an object.
    /// </summary>
    [WCFCommandRegistration(typeof(SecurityUserDeleteRequest), typeof(SecurityUserDeleteResponse))]
    public partial class SecurityUserDelete : WCFCommand<SecurityUserDeleteRequest, SecurityUserDeleteResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly ISecurityUserBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public SecurityUserDelete(ISecurityUserBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(SecurityUserDeleteRequest request, SecurityUserDeleteResponse response)
        {
            var resp = _repository.Delete(new RequestItem<System.Guid>() { Item = request.Item });
            response.CopyResponse(resp);
        }
    }
}


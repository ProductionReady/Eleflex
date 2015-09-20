using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Security.Services.WCF.Server
{
    /// <summary>
    /// Service command to delete an object.
    /// </summary>
    [WCFCommandRegistration(typeof(SecurityUserLoginDeleteRequest), typeof(SecurityUserLoginDeleteResponse))]
    public partial class SecurityUserLoginDelete : WCFCommand<SecurityUserLoginDeleteRequest, SecurityUserLoginDeleteResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly ISecurityUserLoginBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public SecurityUserLoginDelete(ISecurityUserLoginBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(SecurityUserLoginDeleteRequest request, SecurityUserLoginDeleteResponse response)
        {
            var resp = _repository.Delete(new RequestItem<long>() { Item = request.Item });
            response.CopyResponse(resp);
        }
    }
}


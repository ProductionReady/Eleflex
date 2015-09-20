using Eleflex.Services.WCF;
using System.Security.Permissions;
using DomainModel = Eleflex;
using ServiceModel = Eleflex;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Security.Services.WCF.Server
{
    /// <summary>
    /// Service command to update an object.
    /// </summary>
    [WCFCommandRegistration(typeof(SecurityRolePermissionUpdateRequest), typeof(SecurityRolePermissionUpdateResponse))]
    public partial class SecurityRolePermissionUpdate : WCFCommand<SecurityRolePermissionUpdateRequest, SecurityRolePermissionUpdateResponse>
    {

        /// <summary>
        /// The storage repository.
        /// </summary>
        protected readonly ISecurityRolePermissionBusinessRepository _repository;

        /// <summary>
        /// THe mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mappingService"></param>
        public SecurityRolePermissionUpdate(
            ISecurityRolePermissionBusinessRepository repository,
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
        public override void Execute(SecurityRolePermissionUpdateRequest request, SecurityRolePermissionUpdateResponse response)
        {
            var resp = _repository.Update(new RequestItem<SecurityRolePermission>() { Item = request.Item });                        
            response.CopyResponse(resp);
            response.Item = resp.Item;
            
        }
    }
}

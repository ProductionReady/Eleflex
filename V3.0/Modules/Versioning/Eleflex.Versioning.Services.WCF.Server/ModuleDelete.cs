using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Versioning.Services.WCF.Message;

namespace Eleflex.Versioning.Services.WCF.Server
{
    /// <summary>
    /// Service command to delete an object.
    /// </summary>
    [WCFCommandRegistration(typeof(ModuleDeleteRequest), typeof(ModuleDeleteResponse))]
    public partial class ModuleDelete : WCFCommand<ModuleDeleteRequest, ModuleDeleteResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly IModuleBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public ModuleDelete(IModuleBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(ModuleDeleteRequest request, ModuleDeleteResponse response)
        {
            var resp = _repository.Delete(new RequestItem<System.Guid>() { Item = request.Item });
            response.CopyResponse(resp);
        }
    }
}


using Eleflex.Services.WCF;
using System.Security.Permissions;
using DomainModel = Eleflex;
using ServiceModel = Eleflex;
using Eleflex.Versioning.Services.WCF.Message;

namespace Eleflex.Versioning.Services.WCF.Server
{
    /// <summary>
    /// Service command to update an object.
    /// </summary>
    [WCFCommandRegistration(typeof(ModuleUpdateRequest), typeof(ModuleUpdateResponse))]
    public partial class ModuleUpdate : WCFCommand<ModuleUpdateRequest, ModuleUpdateResponse>
    {

        /// <summary>
        /// The storage repository.
        /// </summary>
        protected readonly IModuleBusinessRepository _repository;

        /// <summary>
        /// THe mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mappingService"></param>
        public ModuleUpdate(
            IModuleBusinessRepository repository,
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
        public override void Execute(ModuleUpdateRequest request, ModuleUpdateResponse response)
        {
            var resp = _repository.Update(new RequestItem<Module>() { Item = request.Item });                        
            response.CopyResponse(resp);
            response.Item = resp.Item;
            
        }
    }
}

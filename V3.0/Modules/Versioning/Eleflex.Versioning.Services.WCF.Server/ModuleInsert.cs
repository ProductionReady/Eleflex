using Eleflex.Services.WCF;
using System.Security.Permissions;
using DomainModel = Eleflex;
using ServiceModel = Eleflex;
using Eleflex.Versioning.Services.WCF.Message;

namespace Eleflex.Versioning.Services.WCF.Server
{
    /// <summary>
    /// Service command to create an object.
    /// </summary>
    [WCFCommandRegistration(typeof(ModuleInsertRequest), typeof(ModuleInsertResponse))]
    public partial class ModuleInsert : WCFCommand<ModuleInsertRequest, ModuleInsertResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly IModuleBusinessRepository _repository;
        /// <summary>
        /// The mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mappingService"></param>
        public ModuleInsert(
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
        public override void Execute(ModuleInsertRequest request, ModuleInsertResponse response)
        {
            var resp = _repository.Insert(new RequestItem<Module>() {Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}

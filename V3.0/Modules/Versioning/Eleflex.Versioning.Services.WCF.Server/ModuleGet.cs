using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Versioning.Services.WCF.Message;
using DomainModel = Eleflex;
using ServiceModel = Eleflex;

namespace Eleflex.Versioning.Services.WCF.Server
{
    /// <summary>
    /// Service command to get an object.
    /// </summary>
    [WCFCommandRegistration(typeof(ModuleGetRequest), typeof(ModuleGetResponse))]
    public partial class ModuleGet : WCFCommand<ModuleGetRequest, ModuleGetResponse>
    {

        /// <summary>
        /// The storage repository.
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
        public ModuleGet(
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
        public override void Execute(ModuleGetRequest request, ModuleGetResponse response)
        {            
            var resp = _repository.Get(new RequestItem<System.Guid>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}

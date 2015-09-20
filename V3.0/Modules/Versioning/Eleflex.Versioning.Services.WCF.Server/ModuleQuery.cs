using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Versioning.Services.WCF.Message;
using DomainModel = Eleflex;
using ServiceModel = Eleflex;

namespace Eleflex.Versioning.Services.WCF.Server
{
    /// <summary>
    /// Service command to query objects.
    /// </summary>        
    [WCFCommandRegistration(typeof(ModuleQueryRequest), typeof(ModuleQueryResponse))]
    public partial class ModuleQuery : WCFCommand<ModuleQueryRequest, ModuleQueryResponse>
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
        public ModuleQuery(IModuleBusinessRepository repository,
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
        public override void Execute(ModuleQueryRequest request, ModuleQueryResponse response)
        {
            var resp = _repository.Query(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Items = resp.Items;
        }
    }
}

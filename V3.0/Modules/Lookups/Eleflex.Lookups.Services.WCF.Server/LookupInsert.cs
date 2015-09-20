using Eleflex.Services.WCF;
using System.Security.Permissions;
using DomainModel = Eleflex.Lookups;
using ServiceModel = Eleflex.Lookups.Services.WCF.Message;
using Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Services.WCF.Server
{
    /// <summary>
    /// Service command to create an object.
    /// </summary>
    [WCFCommandRegistration(typeof(LookupInsertRequest), typeof(LookupInsertResponse))]
    public partial class LookupInsert : WCFCommand<LookupInsertRequest, LookupInsertResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly ILookupBusinessRepository _repository;
        /// <summary>
        /// The mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mappingService"></param>
        public LookupInsert(
            ILookupBusinessRepository repository,
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
        public override void Execute(LookupInsertRequest request, LookupInsertResponse response)
        {
            var item = _mappingService.Map<DomainModel.Lookup, ServiceModel.Lookup>(request.Item);            
            var resp = _repository.Insert(new RequestItem<Lookup>() {Item = item });
            response.CopyResponse(resp);
            response.Item = _mappingService.Map<ServiceModel.Lookup, DomainModel.Lookup>(resp.Item);
        }
    }
}

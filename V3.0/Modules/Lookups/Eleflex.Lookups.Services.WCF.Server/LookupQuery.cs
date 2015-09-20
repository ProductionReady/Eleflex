using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Lookups.Services.WCF.Message;
using DomainModel = Eleflex.Lookups;
using ServiceModel = Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Services.WCF.Server
{
    /// <summary>
    /// Service command to query objects.
    /// </summary>        
    [WCFCommandRegistration(typeof(LookupQueryRequest), typeof(LookupQueryResponse))]
    public partial class LookupQuery : WCFCommand<LookupQueryRequest, LookupQueryResponse>
    {

        /// <summary>
        /// The storage repository.
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
        public LookupQuery(ILookupBusinessRepository repository,
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
        public override void Execute(LookupQueryRequest request, LookupQueryResponse response)
        {
            var resp = _repository.Query(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Items = _mappingService.Map<DomainModel.Lookup, ServiceModel.Lookup>(resp.Items);
        }
    }
}

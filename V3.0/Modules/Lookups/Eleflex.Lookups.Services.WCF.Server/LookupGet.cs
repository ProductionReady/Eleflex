using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Lookups.Services.WCF.Message;
using DomainModel = Eleflex.Lookups;
using ServiceModel = Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Services.WCF.Server
{
    /// <summary>
    /// Service command to get an object.
    /// </summary>
    [WCFCommandRegistration(typeof(LookupGetRequest), typeof(LookupGetResponse))]
    public partial class LookupGet : WCFCommand<LookupGetRequest, LookupGetResponse>
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
        public LookupGet(
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
        public override void Execute(LookupGetRequest request, LookupGetResponse response)
        {            
            var resp = _repository.Get(new RequestItem<System.Guid>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = _mappingService.Map<DomainModel.Lookup, ServiceModel.Lookup>(resp.Item);
        }
    }
}

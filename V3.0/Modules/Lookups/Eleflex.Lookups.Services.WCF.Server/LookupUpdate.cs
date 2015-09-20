using Eleflex.Services.WCF;
using System.Security.Permissions;
using DomainModel = Eleflex.Lookups;
using ServiceModel = Eleflex.Lookups.Services.WCF.Message;
using Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Services.WCF.Server
{
    /// <summary>
    /// Service command to update an object.
    /// </summary>
    [WCFCommandRegistration(typeof(LookupUpdateRequest), typeof(LookupUpdateResponse))]
    public partial class LookupUpdate : WCFCommand<LookupUpdateRequest, LookupUpdateResponse>
    {

        /// <summary>
        /// The storage repository.
        /// </summary>
        protected readonly ILookupBusinessRepository _repository;

        /// <summary>
        /// THe mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mappingService"></param>
        public LookupUpdate(
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
        public override void Execute(LookupUpdateRequest request, LookupUpdateResponse response)
        {
            var item =_mappingService.Map<DomainModel.Lookup, ServiceModel.Lookup>(request.Item);
            var resp = _repository.Update(new RequestItem<Lookup>() { Item = item });                        
            response.CopyResponse(resp);
            response.Item = _mappingService.Map<DomainModel.Lookup, ServiceModel.Lookup>(resp.Item);
            
        }
    }
}

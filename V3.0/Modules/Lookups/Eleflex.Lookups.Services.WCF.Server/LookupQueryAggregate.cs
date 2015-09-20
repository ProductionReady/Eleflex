using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Services.WCF.Server
{
    /// <summary>
    /// Service command to query objects for aggregate.
    /// </summary>
    [WCFCommandRegistration(typeof(LookupQueryAggregateRequest), typeof(LookupQueryAggregateResponse))]
    public partial class LookupQueryAggregate : WCFCommand<LookupQueryAggregateRequest, LookupQueryAggregateResponse>
    {

        /// <summary>
        /// THe storage repository.
        /// </summary>
        private readonly ILookupBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public LookupQueryAggregate(ILookupBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(LookupQueryAggregateRequest request, LookupQueryAggregateResponse response)
        {
            var resp = _repository.QueryAggregate(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}


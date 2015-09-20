using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Versioning.Services.WCF.Message;

namespace Eleflex.Versioning.Services.WCF.Server
{
    /// <summary>
    /// Service command to query objects for aggregate.
    /// </summary>
    [WCFCommandRegistration(typeof(ModuleQueryAggregateRequest), typeof(ModuleQueryAggregateResponse))]
    public partial class ModuleQueryAggregate : WCFCommand<ModuleQueryAggregateRequest, ModuleQueryAggregateResponse>
    {

        /// <summary>
        /// THe storage repository.
        /// </summary>
        private readonly IModuleBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public ModuleQueryAggregate(IModuleBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(ModuleQueryAggregateRequest request, ModuleQueryAggregateResponse response)
        {
            var resp = _repository.QueryAggregate(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}


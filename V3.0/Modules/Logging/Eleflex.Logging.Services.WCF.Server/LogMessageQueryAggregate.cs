using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Logging.Services.WCF.Message;

namespace Eleflex.Logging.Services.WCF.Server
{
    /// <summary>
    /// Service command to query objects for aggregate.
    /// </summary>
    [WCFCommandRegistration(typeof(LogMessageQueryAggregateRequest), typeof(LogMessageQueryAggregateResponse))]
    public partial class LogMessageQueryAggregate : WCFCommand<LogMessageQueryAggregateRequest, LogMessageQueryAggregateResponse>
    {

        /// <summary>
        /// THe storage repository.
        /// </summary>
        private readonly ILogMessageBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public LogMessageQueryAggregate(ILogMessageBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(LogMessageQueryAggregateRequest request, LogMessageQueryAggregateResponse response)
        {
            var resp = _repository.QueryAggregate(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}


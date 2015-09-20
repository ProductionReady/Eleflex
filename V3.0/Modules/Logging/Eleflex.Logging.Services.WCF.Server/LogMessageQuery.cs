using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Logging.Services.WCF.Message;

namespace Eleflex.Logging.Services.WCF.Server
{
    /// <summary>
    /// Service command to query objects.
    /// </summary>        
    [WCFCommandRegistration(typeof(LogMessageQueryRequest), typeof(LogMessageQueryResponse))]
    public partial class LogMessageQuery : WCFCommand<LogMessageQueryRequest, LogMessageQueryResponse>
    {

        /// <summary>
        /// The storage repository.
        /// </summary>
        protected readonly ILogMessageBusinessRepository _repository;

        /// <summary>
        /// The mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public LogMessageQuery(ILogMessageBusinessRepository repository,
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
        public override void Execute(LogMessageQueryRequest request, LogMessageQueryResponse response)
        {
            var resp = _repository.Query(new RequestItem<IStorageQuery>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Items = resp.Items;
        }
    }
}

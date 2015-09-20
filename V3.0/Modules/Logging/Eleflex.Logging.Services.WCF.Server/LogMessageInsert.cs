using Eleflex.Services.WCF;
using System.Security.Permissions;
using DomainModel = Eleflex;
using ServiceModel = Eleflex;
using Eleflex.Logging.Services.WCF.Message;

namespace Eleflex.Logging.Services.WCF.Server
{
    /// <summary>
    /// Service command to create an object.
    /// </summary>
    [WCFCommandRegistration(typeof(LogMessageInsertRequest), typeof(LogMessageInsertResponse))]
    public partial class LogMessageInsert : WCFCommand<LogMessageInsertRequest, LogMessageInsertResponse>
    {

        /// <summary>
        /// The business repository.
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
        /// <param name="mappingService"></param>
        public LogMessageInsert(
            ILogMessageBusinessRepository repository,
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
        public override void Execute(LogMessageInsertRequest request, LogMessageInsertResponse response)
        {
            var resp = _repository.Insert(new RequestItem<LogMessage>() {Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}

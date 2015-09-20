using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Logging.Services.WCF.Message;
using DomainModel = Eleflex;
using ServiceModel = Eleflex;

namespace Eleflex.Logging.Services.WCF.Server
{
    /// <summary>
    /// Service command to get an object.
    /// </summary>
    [WCFCommandRegistration(typeof(LogMessageGetRequest), typeof(LogMessageGetResponse))]
    public partial class LogMessageGet : WCFCommand<LogMessageGetRequest, LogMessageGetResponse>
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
        public LogMessageGet(
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
        public override void Execute(LogMessageGetRequest request, LogMessageGetResponse response)
        {            
            var resp = _repository.Get(new RequestItem<long>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
        }
    }
}

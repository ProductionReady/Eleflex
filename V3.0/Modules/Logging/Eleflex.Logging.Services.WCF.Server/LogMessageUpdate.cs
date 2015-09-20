using Eleflex.Services.WCF;
using System.Security.Permissions;
using DomainModel = Eleflex;
using ServiceModel = Eleflex;
using Eleflex.Logging.Services.WCF.Message;

namespace Eleflex.Logging.Services.WCF.Server
{
    /// <summary>
    /// Service command to update an object.
    /// </summary>
    [WCFCommandRegistration(typeof(LogMessageUpdateRequest), typeof(LogMessageUpdateResponse))]
    public partial class LogMessageUpdate : WCFCommand<LogMessageUpdateRequest, LogMessageUpdateResponse>
    {

        /// <summary>
        /// The storage repository.
        /// </summary>
        protected readonly ILogMessageBusinessRepository _repository;

        /// <summary>
        /// THe mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mappingService"></param>
        public LogMessageUpdate(
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
        public override void Execute(LogMessageUpdateRequest request, LogMessageUpdateResponse response)
        {            
            var resp = _repository.Update(new RequestItem<LogMessage>() { Item = request.Item });
            response.CopyResponse(resp);
            response.Item = resp.Item;
            
        }
    }
}

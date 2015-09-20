using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Logging.Services.WCF.Message;

namespace Eleflex.Logging.Services.WCF.Server
{
    /// <summary>
    /// Service command to delete an object.
    /// </summary>
    [WCFCommandRegistration(typeof(LogMessageDeleteRequest), typeof(LogMessageDeleteResponse))]
    public partial class LogMessageDelete : WCFCommand<LogMessageDeleteRequest, LogMessageDeleteResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly ILogMessageBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public LogMessageDelete(ILogMessageBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(LogMessageDeleteRequest request, LogMessageDeleteResponse response)
        {
            var resp = _repository.Delete(new RequestItem<long>() { Item = request.Item });
            response.CopyResponse(resp);
        }
    }
}


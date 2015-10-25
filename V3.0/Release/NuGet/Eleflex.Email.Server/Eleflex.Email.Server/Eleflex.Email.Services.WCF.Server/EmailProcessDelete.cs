using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Server
{
    /// <summary>
    /// Service command to delete an object.
    /// </summary>
    [WCFCommandRegistration(typeof(EmailProcessDeleteRequest), typeof(EmailProcessDeleteResponse))]
    public partial class EmailProcessDelete : WCFCommand<EmailProcessDeleteRequest, EmailProcessDeleteResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly IEmailProcessBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public EmailProcessDelete(IEmailProcessBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(EmailProcessDeleteRequest request, EmailProcessDeleteResponse response)
        {
            var resp = _repository.Delete(new RequestItem<long>() { Item = request.Item });
            response.CopyResponse(resp);
        }
    }
}


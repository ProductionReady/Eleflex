using Eleflex.Services.WCF;
using System.Security.Permissions;
using Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Services.WCF.Server
{
    /// <summary>
    /// Service command to delete an object.
    /// </summary>
    [WCFCommandRegistration(typeof(LookupDeleteRequest), typeof(LookupDeleteResponse))]
    public partial class LookupDelete : WCFCommand<LookupDeleteRequest, LookupDeleteResponse>
    {

        /// <summary>
        /// The business repository.
        /// </summary>
        protected readonly ILookupBusinessRepository _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public LookupDelete(ILookupBusinessRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Execute the command.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        [PrincipalPermission(SecurityAction.Demand, Role = "Admin")]
        public override void Execute(LookupDeleteRequest request, LookupDeleteResponse response)
        {
            var resp = _repository.Delete(new RequestItem<System.Guid>() { Item = request.Item });
            response.CopyResponse(resp);
        }
    }
}


using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Eleflex.Security.ASPNetIdentity
{
    /// <summary>
    /// Represents an object with no functionality for ASP NET identity message service.
    /// </summary>
    public partial class NoFunctionalityIdentityMessageService : IIdentityMessageService
    {

        /// <summary>
        /// Send a message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }
    }
}

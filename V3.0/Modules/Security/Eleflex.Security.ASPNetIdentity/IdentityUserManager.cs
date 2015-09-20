using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace Eleflex.Security.ASPNetIdentity
{
    /// <summary>
    /// Represents an object used for an identity user manager.
    /// </summary>
    public partial class IdentityUserManager : UserManager<IdentityUser>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="store"></param>
        public IdentityUserManager(IUserStore<IdentityUser> store)
            : base(store)
        {
        }

        /// <summary>
        /// Generate user identity.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<ClaimsIdentity> GenerateUserIdentityAsync(IdentityUser user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await this.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here            
            return userIdentity;
        }

    }
}

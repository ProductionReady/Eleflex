using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Eleflex.Security.ASPNetIdentity;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents an object for identity signin manager.
    /// </summary>
    public partial class IdentitySignInManager : SignInManager<IdentityUser, string>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="authenticationManager"></param>
        public IdentitySignInManager(IdentityUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        /// <summary>
        /// Create an identity.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public override Task<ClaimsIdentity> CreateUserIdentityAsync(IdentityUser user)
        {
            return GenerateUserIdentity((IdentityUserManager)UserManager, user);
        }

        /// <summary>
        /// Generate a user identity.
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public virtual async Task<ClaimsIdentity> GenerateUserIdentity(UserManager<IdentityUser> manager, IdentityUser user)
        {
            var identity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            return identity;
        }

    }
}
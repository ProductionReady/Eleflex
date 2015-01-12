using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Eleflex.Web
{
    public class ApplicationSignInManager : SignInManager<Eleflex.Security.User, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(Eleflex.Security.User user)
        {
            return GenerateUserIdentity((ApplicationUserManager)UserManager, user);
        }


        public virtual async Task<ClaimsIdentity> GenerateUserIdentity(UserManager<Eleflex.Security.User> manager, Eleflex.Security.User user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here            
            return userIdentity;
        }

    }
}
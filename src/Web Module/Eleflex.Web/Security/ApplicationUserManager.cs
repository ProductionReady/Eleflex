using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace Eleflex.Web
{
    public class ApplicationUserManager : UserManager<Eleflex.Security.User>
    {

        public ApplicationUserManager(IUserStore<Eleflex.Security.User> store)
            : base(store)
        {
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(Eleflex.Security.User user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await this.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here            
            return userIdentity;
        }

    }
}

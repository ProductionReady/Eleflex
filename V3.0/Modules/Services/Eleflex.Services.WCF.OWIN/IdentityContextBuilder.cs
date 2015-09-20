using System;
using System.Threading;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Security;
using System.Security.Claims;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents an object to create context objects for ASP NET Identity.
    /// </summary>
    public partial class IdentityContextBuilder : IContextBuilder
    {
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public IdentityContextBuilder()
        {
        }


        /// <summary>
        /// Set security for a context object.
        /// </summary>
        /// <param name="context"></param>
        protected virtual void GetSecurityUser(IContext context)
        {
            try
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.User != null)
                    {
                        //Check if valid user id (user may be anonymous/not authenticated)
                        string userId = HttpContext.Current.User.Identity.GetUserId();
                        if (!string.IsNullOrEmpty(userId))
                        {
                            Guid userKey;
                            if (Guid.TryParse(userId, out userKey))
                            {
                                SecurityUser user = new SecurityUser();
                                user.SecurityUserKey = userKey;
                                user.Username = HttpContext.Current.User.Identity.Name;
                                context.User = user;
                                return;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Get a context object.
        /// </summary>
        /// <returns></returns>
        public virtual IContext GetContext()
        {
            IContext context = new Context();
            GetSecurityUser(context);
            return context;
        }
    }
}

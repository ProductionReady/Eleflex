using System.Collections.Generic;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Security.Principal;
using System.Web;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Authorization policy to have WCF services use OWIN authentication context.
    /// </summary>
    public class CookieSecurityServerAuthorizationPolicy : IAuthorizationPolicy
    {

        /// <summary>
        /// The Id.
        /// </summary>
        public virtual string Id
        {
            get { return "CookieSecurityServerAuthorizationPolicy"; }
        }

        /// <summary>
        /// THe issuer.
        /// </summary>
        public virtual System.IdentityModel.Claims.ClaimSet Issuer
        {
            get { return ClaimSet.System; }
        }

        /// <summary>
        /// Evaluate.
        /// </summary>
        /// <param name="evaluationContext"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public virtual bool Evaluate(EvaluationContext evaluationContext, ref object state)
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                evaluationContext.Properties["Principal"] = context.User;
                evaluationContext.Properties["Identities"] = new List<IIdentity>() { context.User.Identity };
            }
            return true;
        }
    }
}

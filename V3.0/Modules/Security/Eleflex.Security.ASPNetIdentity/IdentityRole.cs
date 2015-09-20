using System;
using Microsoft.AspNet.Identity;
using ServiceModel = Eleflex;

namespace Eleflex.Security.ASPNetIdentity
{
    /// <summary>
    /// Represents an object used for an identity role.
    /// </summary>
    public partial class IdentityRole : ServiceModel.SecurityRole, IIdentityRole
    {
        /// <summary>
        /// The object Id created for integration with ASP NET Identity.
        /// </summary>
        public virtual string Id
        {
            get { return SecurityRoleKey.ToString(); }
            set
            {
                string val = value;
                Guid id = Guid.Empty;
                if (Guid.TryParse(Id, out id))
                    SecurityRoleKey = id;
                else
                    throw new ArgumentException("Could not parse Guid for SecurityRoleKey");
            }
        }
    }
}

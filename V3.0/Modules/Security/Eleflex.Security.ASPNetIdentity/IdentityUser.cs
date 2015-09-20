using System;
using ServiceModel = Eleflex;

namespace Eleflex.Security.ASPNetIdentity
{
    /// <summary>
    /// Represents an object used for an identity user.
    /// </summary>
    public partial class IdentityUser : ServiceModel.SecurityUser, IIdentityUser
    {

        /// <summary>
        /// The object Id created for integration with ASP NET Identity.
        /// </summary>
        public virtual string Id
        {
            get { return SecurityUserKey.ToString(); }
            set
            {
                string val = value;
                Guid id = Guid.Empty;
                if (Guid.TryParse(Id, out id))
                    SecurityUserKey = id;
                else
                    throw new ArgumentException("Could not parse Guid for SecurityUserKey");
            }
        }

        /// <summary>
        /// The UserName.
        /// </summary>
        public virtual string UserName
        {
            get { return Username; }
            set { Username = value; }
        }
    }
}

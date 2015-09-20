using Microsoft.AspNet.Identity;

namespace Eleflex.Security.ASPNetIdentity
{
    /// <summary>
    /// Represents an object used for an identity role manager.
    /// </summary>
    public partial class IdentityRoleManager : RoleManager<IdentityRole>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="store"></param>
        public IdentityRoleManager(IRoleStore<IdentityRole> store)
            : base(store)
        {
        }

    }
}

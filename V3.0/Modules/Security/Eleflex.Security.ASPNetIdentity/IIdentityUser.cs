using Microsoft.AspNet.Identity;

namespace Eleflex.Security.ASPNetIdentity
{
    /// <summary>
    /// Represents an object for an identity user.
    /// </summary>
    public partial interface IIdentityUser : IUser, ISecurityUser
    {
    }
}

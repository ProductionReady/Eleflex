using Microsoft.AspNet.Identity;
using ServiceModel = Eleflex;

namespace Eleflex.Security.ASPNetIdentity
{
    /// <summary>
    /// Represents an object defining an identity role.
    /// </summary>
    public partial interface IIdentityRole : IRole, ServiceModel.ISecurityRole
    {
    }
}

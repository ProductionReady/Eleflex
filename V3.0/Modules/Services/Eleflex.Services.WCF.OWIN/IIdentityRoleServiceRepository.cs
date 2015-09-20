using System;
using Eleflex.Security.ASPNetIdentity;
using ServiceModel = Eleflex;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents a IdentityRole service repository.
    /// </summary>
    public partial interface IIdentityRoleServiceRepository : IMappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository>
    {
    }
}

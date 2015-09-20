using System;
using Eleflex.Security.ASPNetIdentity;
using ServiceModel = Eleflex;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents a IdentityUser service repository.
    /// </summary>
    public partial interface IIdentityUserServiceRepository : IMappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository>        
    {
    }
}

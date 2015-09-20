using System;
using Eleflex.Security.ASPNetIdentity;
using ServiceModel = Eleflex;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents an object that implements the key ASP.NET Identity User store iterfaces. This object sends requests via the service repository.
    /// </summary>
    public partial class IdentityUserServiceRepository : MappingRepository<IdentityUser, Guid, ServiceModel.SecurityUser, ISecurityUserServiceRepository>, IIdentityUserServiceRepository
    {

        public IdentityUserServiceRepository(
            ISecurityUserServiceRepository serviceRepository,
            IMappingService mappingService)
            : base(
                  serviceRepository,
                  mappingService)
        {
        }

    }
}

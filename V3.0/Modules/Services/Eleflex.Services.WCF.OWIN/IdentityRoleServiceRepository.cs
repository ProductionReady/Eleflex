using System;
using Eleflex.Security.ASPNetIdentity;
using ServiceModel = Eleflex;
using Eleflex.Security.Services.WCF.Message;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents an object that implements the key ASP.NET Identity role store iterfaces. This object sends requests via the service repository.
    /// </summary>
    public partial class IdentityRoleServiceRepository : MappingRepository<IdentityRole, Guid, ServiceModel.SecurityRole, ISecurityRoleServiceRepository>,  IIdentityRoleServiceRepository
    {

        public IdentityRoleServiceRepository(
            ISecurityRoleServiceRepository serviceRepository,
            IMappingService mappingService)
            : base(
                  serviceRepository,
                  mappingService)
        {
        }

    }
}

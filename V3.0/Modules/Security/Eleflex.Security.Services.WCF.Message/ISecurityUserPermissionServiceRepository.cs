using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a SecurityUserPermission service repository.
    /// </summary>
    public partial interface ISecurityUserPermissionServiceRepository : IServiceRepository<ServiceModel.SecurityUserPermission, long>
    {
    }
}

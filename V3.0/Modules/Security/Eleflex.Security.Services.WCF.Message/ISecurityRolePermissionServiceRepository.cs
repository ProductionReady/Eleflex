using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a SecurityRolePermission service repository.
    /// </summary>
    public partial interface ISecurityRolePermissionServiceRepository : IServiceRepository<ServiceModel.SecurityRolePermission, long>
    {
    }
}

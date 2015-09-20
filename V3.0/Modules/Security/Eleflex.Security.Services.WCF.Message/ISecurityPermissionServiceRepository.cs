using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a SecurityPermission service repository.
    /// </summary>
    public partial interface ISecurityPermissionServiceRepository : IServiceRepository<ServiceModel.SecurityPermission, System.Guid>
    {
    }
}

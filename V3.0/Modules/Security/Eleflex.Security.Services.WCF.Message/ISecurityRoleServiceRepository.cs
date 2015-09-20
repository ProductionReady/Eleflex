using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a SecurityRole service repository.
    /// </summary>
    public partial interface ISecurityRoleServiceRepository : IServiceRepository<ServiceModel.SecurityRole, System.Guid>
    {
    }
}

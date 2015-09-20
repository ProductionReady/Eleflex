using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a SecurityUserRole service repository.
    /// </summary>
    public partial interface ISecurityUserRoleServiceRepository : IServiceRepository<ServiceModel.SecurityUserRole, long>
    {
    }
}

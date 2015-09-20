using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a SecurityUser service repository.
    /// </summary>
    public partial interface ISecurityUserServiceRepository : IServiceRepository<ServiceModel.SecurityUser, System.Guid>
    {
    }
}

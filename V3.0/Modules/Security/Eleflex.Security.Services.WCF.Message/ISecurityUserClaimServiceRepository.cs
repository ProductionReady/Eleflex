using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a SecurityUserClaim service repository.
    /// </summary>
    public partial interface ISecurityUserClaimServiceRepository : IServiceRepository<ServiceModel.SecurityUserClaim, long>
    {
    }
}

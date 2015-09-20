using ServiceModel = Eleflex;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents a SecurityUserLogin service repository.
    /// </summary>
    public partial interface ISecurityUserLoginServiceRepository : IServiceRepository<ServiceModel.SecurityUserLogin, long>
    {
    }
}

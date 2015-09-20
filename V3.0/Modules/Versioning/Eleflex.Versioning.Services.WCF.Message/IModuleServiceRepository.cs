using ServiceModel = Eleflex;

namespace Eleflex.Versioning.Services.WCF.Message
{
    /// <summary>
    /// Represents a Module  service repository.
    /// </summary>
    public partial interface IModuleServiceRepository : IServiceRepository<ServiceModel.Module, System.Guid>
    {
    }
}

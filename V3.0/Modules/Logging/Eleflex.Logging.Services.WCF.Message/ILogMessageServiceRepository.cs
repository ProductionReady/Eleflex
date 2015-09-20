using ServiceModel = Eleflex;

namespace Eleflex.Logging.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage service repository.
    /// </summary>
    public partial interface ILogMessageServiceRepository : IServiceRepository<ServiceModel.LogMessage, long>
    {
    }
}

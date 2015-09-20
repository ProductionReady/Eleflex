using ServiceMessages = Eleflex.Lookups.Services.WCF.Message;

namespace Eleflex.Lookups.Services.WCF.Message
{
    /// <summary>
    /// Represents a Lookup  service repository.
    /// </summary>
    public partial interface ILookupServiceRepository : IServiceRepository<ServiceMessages.Lookup, System.Guid>
    {
    }
}

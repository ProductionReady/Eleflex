using ServiceMessages = Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Represents a EmailProcess  service repository.
    /// </summary>
    public partial interface IEmailProcessServiceRepository : IServiceRepository<ServiceMessages.EmailProcess, long>
    {
    }
}

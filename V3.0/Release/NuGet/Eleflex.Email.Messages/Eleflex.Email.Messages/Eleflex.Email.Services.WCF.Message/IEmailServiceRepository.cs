using ServiceMessages = Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Represents a EmailProcess  service repository.
    /// </summary>
    public partial interface IEmailServiceRepository
    {

        /// <summary>
        /// Send email.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IResponseItem<long> SendEmail(SendEmailRequest request);        


    }
}

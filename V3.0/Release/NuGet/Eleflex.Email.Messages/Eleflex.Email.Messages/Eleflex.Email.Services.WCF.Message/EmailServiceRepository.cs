using System;
using ServiceMessages = Eleflex.Email.Services.WCF.Message;

namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Represents a LogMessage  service client.
    /// </summary>
    public partial class EmailServiceRepository : IEmailServiceRepository
    {
        
        public IResponseItem<long> SendEmail(SendEmailRequest request)
        {
            using (IEmailRequestDispatcher dispatcher = ObjectLocator.Current.GetInstance<IEmailRequestDispatcher>())
            {
                return dispatcher.ExecuteServiceCommand<ResponseItem<long>>(request);
            }
        }
    }
}

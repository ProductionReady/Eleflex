using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using Eleflex.Security.ASPNetIdentity;
using Eleflex.Email.Services.WCF.Message;
using Eleflex.Services.WCF.OWIN;

namespace Eleflex.Email.WebClient
{
    /// <summary>
    /// Represents an object used for sending emails for the ASP NET Identity message service using the email service repository.
    /// </summary>
    public partial class EmailIdentityMessageServiceRepositoryService : IEmailIdentityMessageService
    {        

        protected IEmailProcessServiceRepository _repository;
        protected string _fromEmailAddress;

        public EmailIdentityMessageServiceRepositoryService(string fromEmailAddress, IEmailProcessServiceRepository repository)
        {
            _fromEmailAddress = fromEmailAddress;
            _repository = repository;
        }

        /// <summary>
        /// Send the email message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual Task SendAsync(IdentityMessage message)
        {
            try
            {                
                EmailProcess item = new EmailProcess();
                item.Body = message.Body;
                item.CreateDate = DateTimeOffset.UtcNow;
                item.FromAddress = _fromEmailAddress;
                item.Subject = message.Subject;
                item.ToAddress = message.Destination;

                //We don't want to run in context of the current user request as request dispatcher may already be in use. Run on different thread using system impersonation.
                System.Threading.Tasks.Task.Run(() => 
                {
                    using (ImpersonateSystem adminAccess = new ImpersonateSystem())
                    {
                        _repository.Insert(new RequestItem<EmailProcess>(item));
                    }
                });
                
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EmailIdentityMessageServiceRepositoryService>("Error sending email to service repository", ex);
            }
            return Task.FromResult(0);
        }
    }
}

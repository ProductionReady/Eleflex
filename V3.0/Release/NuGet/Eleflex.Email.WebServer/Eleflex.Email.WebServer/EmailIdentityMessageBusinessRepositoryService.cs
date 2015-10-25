using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using Eleflex.Security.ASPNetIdentity;
using Eleflex.Email;
using Eleflex.Services.WCF.OWIN;

namespace Eleflex.Email.WebServer
{
    /// <summary>
    /// Represents an object used for sending emails for the ASP NET Identity message service using the email business repository.
    /// </summary>
    public partial class EmailIdentityMessageBusinessRepositoryService : IEmailIdentityMessageService
    {

        protected IEmailProcessBusinessRepository _repository;
        protected string _fromEmailAddress;

        public EmailIdentityMessageBusinessRepositoryService(string fromEmailAddress, IEmailProcessBusinessRepository repository)
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
                var resp = _repository.Insert(new RequestItem<EmailProcess>(item));
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EmailIdentityMessageBusinessRepositoryService>("Error sending email to business repository", ex);
            }
            return Task.FromResult(0);
        }
    }
}

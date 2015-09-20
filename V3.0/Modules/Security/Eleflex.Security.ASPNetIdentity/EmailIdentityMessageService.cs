using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Net.Mail;

namespace Eleflex.Security.ASPNetIdentity
{
    /// <summary>
    /// Represents an object used for sending emails for the ASP NET Identity message service.
    /// </summary>
    public partial class EmailIdentityMessageService : IEmailIdentityMessageService
    {

        /// <summary>
        /// Send the email message.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public virtual Task SendAsync(IdentityMessage message)
        {
            try
            {
                //This will use default configurations from application config file
                SmtpClient client = new SmtpClient();
                MailMessage msg = new MailMessage();
                msg.To.Add(message.Destination);
                msg.Subject = message.Subject;
                msg.Body = message.Body;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EmailIdentityMessageService>("Could not send email. Confirm application config for system.net.mailsettings", ex);                
            }
            return Task.FromResult(0);
        }
    }
}

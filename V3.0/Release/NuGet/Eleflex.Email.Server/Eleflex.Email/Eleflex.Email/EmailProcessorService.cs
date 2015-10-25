using System;
using System.IO;
using System.Net.Mail;
using System.Linq;
using System.Collections.Generic;

namespace Eleflex.Email
{

    public partial class EmailProcessorService : IEmailProcessorService
    {

        protected const int BATCH_NUMBER_EMAILS_TO_PROCESS = 1; //Process 3 at a time
        protected IStorageContextUnitOfWork _uow;
        protected IEmailProcessStorageRepository _emailProcessStorageRepository;
        protected IEmailProcessBusinessRepository _emailProcessBusinessRepository;
        protected IEmailProcessAttachmentBusinessRepository _emailProcessAttachmentBusinessRepository;

        public EmailProcessorService(
            IStorageContextUnitOfWork uow,
            IEmailProcessStorageRepository emailProcessStorageRepository,
            IEmailProcessBusinessRepository emailProcessBusinessRepository,
            IEmailProcessAttachmentBusinessRepository emailProcessAttachmentBusinessRepository)
        {
            _uow = uow;
            _emailProcessStorageRepository = emailProcessStorageRepository;
            _emailProcessBusinessRepository = emailProcessBusinessRepository;
            _emailProcessAttachmentBusinessRepository = emailProcessAttachmentBusinessRepository;
        }



        public virtual void SendEmailProcess()
        {
            try
            {
                //Batch the number of emails in case multiple processing running
                while (true)
                {

                    var resp = _emailProcessStorageRepository.GetBatchEmaiForProcessing(BATCH_NUMBER_EMAILS_TO_PROCESS);
                    if (!resp.ResponseSuccess)
                        break;

                    //Keep processing while there are emails to send
                    if (resp.Items != null && resp.Items.Count > 0)
                    {
                        //Process each record
                        foreach (var item in resp.Items)
                        {
                            //Get any attachments
                            StorageQueryBuilder attachmentFilter = new StorageQueryBuilder();
                            attachmentFilter.IsEqual("EmailProcessKey", item.EmailProcessKey.ToString());
                            var respAttachments = _emailProcessAttachmentBusinessRepository.Query(new RequestItem<IStorageQuery>(attachmentFilter.GetStorageQuery()));

                            //Try to send the email
                            SendEmail(item, respAttachments.Items);

                            //Mark emails as processed (locking so multiple processes don't pick them up)
                            item.IsProcessing = false;
                            item.ProcessingDate = null;
                            _emailProcessBusinessRepository.Update(new RequestItem<EmailProcess>(item));
                        }
                        //Commit current processing changes
                        _uow.Commit();
                    }
                    else
                        break;
                }

                //Finally, fix any emails marked processing older than an hour to be re-picked up again (in case process corrupted somehow)
                StorageQueryBuilder sqb = new StorageQueryBuilder();
                sqb.IsEqual("IsProcessing", true.ToString())
                    .IsLessThan("ProcessingDate", DateTime.UtcNow.ToString());
                var respFix = _emailProcessBusinessRepository.Query(new RequestItem<IStorageQuery>(sqb.GetStorageQuery()));
                if (respFix.Items != null && respFix.Items.Count > 0)
                {
                    foreach (var item in respFix.Items)
                    {
                        item.IsProcessing = false;
                        item.ProcessingDate = null;
                        _emailProcessBusinessRepository.Update(new RequestItem<EmailProcess>(item));
                    }
                    //Commit
                    _uow.Commit();
                }
                
            }
            catch(Exception ex)
            {
                Logger.Current.Error<EmailProcessorService>("SendEmailProcess", ex);
            }
            finally
            {
                //Final Commit
                _uow.Commit();

                //This runs periodically on different threads. Clear the storage services used to cleanup references
                _uow.StorageServices.Clear();
            }
        }

        

        protected virtual void MarkEmailSent(EmailProcess item)
        {
            item.IsError = false;
            item.ErrorDate = null;
            item.SentDate = DateTimeOffset.UtcNow;            
        }

        protected virtual void MarkEmailError(EmailProcess item, string message)
        {
            item.IsError = true;
            item.ErrorDate = DateTimeOffset.UtcNow;
            item.ErrorMessage = message;
        }

        protected virtual Response SendEmail(EmailProcess item, IList<EmailProcessAttachment> attachments)
        {
            Response resp = new Response();
            try
            {
                //This will use default configurations from application config file
                SmtpClient client = new SmtpClient();
                MailMessage msg = new MailMessage();
                msg.Body = item.Body;
                if(!string.IsNullOrEmpty(item.ToAddress))
                    msg.To.Add(item.ToAddress);
                if (!string.IsNullOrEmpty(item.CcAddress))
                    msg.CC.Add(item.CcAddress);
                if (!string.IsNullOrEmpty(item.BccAddress))
                    msg.Bcc.Add(item.BccAddress);
                msg.From = new MailAddress(item.FromAddress);
                msg.IsBodyHtml = item.IsHtml;
                msg.Subject = item.Subject;
                if(attachments!= null && attachments.Count > 0)
                {
                    foreach(var attachment in attachments)
                    {
                        MemoryStream ms = new MemoryStream(attachment.FileData);
                        Attachment a = new Attachment(ms, attachment.Filename);
                        msg.Attachments.Add(a);
                    }
                }

                client.Send(msg);
                MarkEmailSent(item);
                return resp;
            }
            catch (Exception ex)
            {
                MarkEmailError(item, ex.ToString());
            }
            return resp;
        }


        public virtual void DeleteEmails(DateTimeOffset expirationDate)
        {
            try
            {

                _emailProcessStorageRepository.PurgeHistoricalEmails(expirationDate);                
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EmailProcessorService>("DeleteEmails", ex);
            }
            finally
            {
                //Final Commit
                _uow.Commit();

                //This runs periodically on different threads. Clear the storage services used to cleanup references
                _uow.StorageServices.Clear();
            }
        }

    }
}

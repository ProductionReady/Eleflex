using System;
using Eleflex;
using Eleflex.Email;
using Eleflex.Web;

namespace $rootnamespace$.App_Start.Eleflex_Start.EleflexEmail
{
    /// <summary>
    /// Represents a startup task for configuring routes in the web application.
    /// </summary>
    public partial class WebServerProcessStartupTask : StartupTask
    {

        /// <summary>
        /// Used to hold a reference for the send email process
        /// </summary>
        protected static EleflexWebProcess _sendEmailProcess = null;
        /// <summary>
        /// Used to hold a reference for the purge email process.
        /// </summary>
        protected static EleflexWebProcess _purgeEmailProcess = null;

        /// <summary>
        /// Constructor.
        /// </summary>
        public WebServerProcessStartupTask() : base()
        {
            Description = @"This task starts the background processes for the email module.";
            Priority = StartupConstants.PRIORITY_CUSTOM;
        }       

        /// <summary>
        /// Start processing logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {
            //This process sends emails in the queue
            if (_sendEmailProcess == null)
            {
                _sendEmailProcess = new EleflexWebProcess(60000, //Run every minute (high priority)
                    () =>
                    {
                        try
                        {
                            IEmailProcessorService emailProcessorService = ObjectLocator.Current.GetInstance<IEmailProcessorService>();
                            emailProcessorService.SendEmailProcess();
                        }
                        catch { }
                    });
            }

            //This process deletes old emails
            if (_purgeEmailProcess == null)
            {
                _purgeEmailProcess = new EleflexWebProcess(60000 * 60 * 6, //Run every 6 hours (low priority)
                    () =>
                    {
                        try
                        {
                            IEmailProcessorService emailProcessorService = ObjectLocator.Current.GetInstance<IEmailProcessorService>();
                            emailProcessorService.DeleteEmails(DateTime.UtcNow.AddDays(-30)); //Delete emails older than 30 days
                        }
                        catch { }
                    });
            }
            return base.Start(taskOptions);
        }

    }

}

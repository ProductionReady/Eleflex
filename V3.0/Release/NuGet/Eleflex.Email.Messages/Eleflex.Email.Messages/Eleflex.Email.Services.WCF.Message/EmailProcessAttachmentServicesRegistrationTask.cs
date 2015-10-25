using System.Linq;
using Eleflex.Services.WCF;

namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the EmailProcessAttachment object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class EmailProcessAttachmentServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public EmailProcessAttachmentServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the EmailProcessAttachment object.";
        }

        /// <summary>
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentDeleteRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentDeleteResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentGetRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentGetResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentInsertRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentInsertResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentQueryAggregateRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentQueryAggregateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentQueryRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentQueryResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentUpdateRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessAttachmentUpdateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessAttachmentUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


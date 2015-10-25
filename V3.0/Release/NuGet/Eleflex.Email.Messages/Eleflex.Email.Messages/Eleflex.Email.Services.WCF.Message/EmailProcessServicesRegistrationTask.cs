using System.Linq;
using Eleflex.Services.WCF;

namespace Eleflex.Email.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the EmailProcess object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class EmailProcessServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public EmailProcessServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the EmailProcess object.";
        }

        /// <summary>
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessDeleteRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessDeleteResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessGetRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessGetResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessInsertRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessInsertResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessQueryAggregateRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessQueryAggregateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessQueryRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessQueryResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessUpdateRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(EmailProcessUpdateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(EmailProcessUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


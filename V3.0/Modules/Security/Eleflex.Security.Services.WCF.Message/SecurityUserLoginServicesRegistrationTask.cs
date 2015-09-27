using System.Linq;
using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityUserLogin object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityUserLoginServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserLoginServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityUserLogin object.";
        }

        /// <summary>
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginDeleteRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginDeleteResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginGetRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginGetResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginInsertRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginInsertResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginQueryAggregateRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginQueryAggregateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginQueryRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginQueryResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginUpdateRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserLoginUpdateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


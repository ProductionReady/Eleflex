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
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserLoginUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserLoginUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


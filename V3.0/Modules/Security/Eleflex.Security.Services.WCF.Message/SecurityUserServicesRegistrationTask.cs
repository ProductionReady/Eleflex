using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityUser object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityUserServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityUser object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


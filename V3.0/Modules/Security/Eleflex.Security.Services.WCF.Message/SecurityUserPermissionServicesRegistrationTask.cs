using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityUserPermission object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityUserPermissionServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserPermissionServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityUserPermission object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserPermissionUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


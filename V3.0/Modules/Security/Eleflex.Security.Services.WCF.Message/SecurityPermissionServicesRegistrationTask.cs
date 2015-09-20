using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityPermission object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityPermissionServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityPermissionServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityPermission object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityPermissionUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


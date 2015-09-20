using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityUserRole object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityUserRoleServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserRoleServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityUserRole object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserRoleUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


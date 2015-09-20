using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityRole object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityRoleServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityRoleServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityRole object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


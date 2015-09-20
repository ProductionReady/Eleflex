using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityRoleRole object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityRoleRoleServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityRoleRoleServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityRoleRole object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRoleRoleUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


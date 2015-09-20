using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityRolePermission object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityRolePermissionServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityRolePermissionServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityRolePermission object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityRolePermissionUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRolePermissionUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


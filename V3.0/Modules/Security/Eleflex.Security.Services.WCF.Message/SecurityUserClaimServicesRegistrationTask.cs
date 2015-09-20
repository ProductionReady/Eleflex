using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityUserClaim object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityUserClaimServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserClaimServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityUserClaim object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(SecurityUserClaimUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserClaimUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


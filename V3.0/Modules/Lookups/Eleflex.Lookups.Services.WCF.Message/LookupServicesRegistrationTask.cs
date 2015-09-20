using Eleflex.Services.WCF;

namespace Eleflex.Lookups.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the Lookup object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class LookupServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public LookupServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the Lookup object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(LookupQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(LookupUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LookupUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


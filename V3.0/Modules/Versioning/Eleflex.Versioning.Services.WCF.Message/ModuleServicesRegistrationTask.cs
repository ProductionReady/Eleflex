using Eleflex.Services.WCF;

namespace Eleflex.Versioning.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the Module object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class ModuleServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public ModuleServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the Module object.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(ModuleQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(ModuleUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(ModuleUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


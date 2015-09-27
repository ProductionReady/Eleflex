using System.Linq;
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
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleDeleteRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleDeleteResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleGetRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleGetResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleInsertRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleInsertResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleQueryAggregateRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleQueryAggregateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleQueryRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(ModuleQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleQueryResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleUpdateRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(ModuleUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(ModuleUpdateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(ModuleUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


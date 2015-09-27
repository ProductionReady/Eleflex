using System.Linq;
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
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupDeleteRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupDeleteResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupGetRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupGetResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupInsertRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupInsertResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupQueryAggregateRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupQueryAggregateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupQueryRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(LookupQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupQueryResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupUpdateRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(LookupUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LookupUpdateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LookupUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


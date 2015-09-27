using System.Linq;
using Eleflex.Services.WCF;

namespace Eleflex.Logging.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the LogMessage object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class LogMessageServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public LogMessageServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the LogMessage object.";
        }

        /// <summary>
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {		
            if(!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageDeleteRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageDeleteResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageGetRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageGetResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageInsertRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageInsertResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageQueryAggregateRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageQueryAggregateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageQueryRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageQueryResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageUpdateRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(LogMessageUpdateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


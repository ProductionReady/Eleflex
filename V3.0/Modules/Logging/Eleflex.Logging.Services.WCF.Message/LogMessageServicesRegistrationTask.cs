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
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageDeleteRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageDeleteResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageGetRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageGetResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageInsertRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageInsertResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageQueryAggregateRequest)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageQueryAggregateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageQueryRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageQueryResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageUpdateRequest)))
			    WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.ContainsKey(typeof(LogMessageUpdateResponse)))
                WCFCommandRegistry.Current.RegisterItem(typeof(LogMessageUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


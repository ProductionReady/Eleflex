using System.Linq;
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
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleDeleteRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleDeleteResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleGetRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleGetResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleInsertRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleInsertResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleQueryAggregateRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleQueryAggregateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleQueryRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleQueryResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleUpdateRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityRoleRoleUpdateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityRoleRoleUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


using System.Linq;
using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityUserRole object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityUserRoleServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserRoleServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityUserRole object.";
        }

        /// <summary>
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleDeleteRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleDeleteResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleGetRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleGetResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleInsertRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleInsertResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleQueryAggregateRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleQueryAggregateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleQueryRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleQueryResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleUpdateRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserRoleUpdateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserRoleUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


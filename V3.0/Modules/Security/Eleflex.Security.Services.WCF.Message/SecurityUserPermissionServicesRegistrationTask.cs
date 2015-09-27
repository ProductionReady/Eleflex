using System.Linq;
using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityUserPermission object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityUserPermissionServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUserPermissionServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityUserPermission object.";
        }

        /// <summary>
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionDeleteRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionDeleteResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionGetRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionGetResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionInsertRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionInsertResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionQueryAggregateRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionQueryAggregateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionQueryRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionQueryResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionUpdateRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityUserPermissionUpdateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityUserPermissionUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


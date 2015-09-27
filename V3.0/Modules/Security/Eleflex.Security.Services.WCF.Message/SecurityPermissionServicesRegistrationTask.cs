using System.Linq;
using Eleflex.Services.WCF;

namespace Eleflex.Security.Services.WCF.Message
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system for the SecurityPermission object.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class SecurityPermissionServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityPermissionServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the SecurityPermission object.";
        }

        /// <summary>
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if(!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionDeleteRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionDeleteRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionDeleteResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionDeleteResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionGetRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionGetRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionGetResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionGetResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionInsertRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionInsertRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionInsertResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionInsertResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionQueryAggregateRequest).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionQueryAggregateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionQueryAggregateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionQueryAggregateResponse), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionQueryRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionQueryRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionQueryResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionQueryResponse), null);
			if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionUpdateRequest).FullName).Any())
			    WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionUpdateRequest), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x=> x.FullName == typeof(SecurityPermissionUpdateResponse).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(SecurityPermissionUpdateResponse), null);

            return base.Register(taskOptions);
        }
    }
}


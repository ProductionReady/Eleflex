using System.Linq;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Represents an object used for configuring WCF services registration in the system.
    /// </summary>
    [WCFServicesRegistrationTask]
    public partial class WCFServicesRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public WCFServicesRegistrationTask()
        {
            Description = "This tasks registers WCF service registration for the system.";
        }


        /// <summary>
        /// Execute the startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x => x.FullName == typeof(Response).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(Response), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x => x.FullName == typeof(StorageQuery).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(StorageQuery), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x => x.FullName == typeof(StorageQueryFilter).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(StorageQueryFilter), null);
            if (!WCFCommandRegistry.Current.RegistryCache.Keys.Where(x => x.FullName == typeof(ValidationMessage).FullName).Any())
                WCFCommandRegistry.Current.RegisterItem(typeof(ValidationMessage), null);

            return base.Register(taskOptions);
        }
    }
}

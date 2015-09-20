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
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            WCFCommandRegistry.Current.RegisterItem(typeof(ValidationMessage), null);
            WCFCommandRegistry.Current.RegisterItem(typeof(StorageQuery), null);
            WCFCommandRegistry.Current.RegisterItem(typeof(StorageQueryFilter), null);

            return base.Register(taskOptions);
        }
    }
}

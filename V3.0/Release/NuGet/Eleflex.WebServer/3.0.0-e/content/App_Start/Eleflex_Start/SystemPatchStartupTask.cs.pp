using Eleflex;

namespace $rootnamespace$.App_Start.Eleflex_Start
{
    /// <summary>
    /// Represents a start task used to update and patch the Eleflex system.
    /// </summary>
    public partial class SystemPatchStartupTask : StartupTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SystemPatchStartupTask() : base()
        {
            Description = @"This startup task is used to patch and update the system.";
            Priority = StartupConstants.STARTUP_PRIORITY_SYSTEMPATCHING;
        }

        /// <summary>
        /// Start processing logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {            
            //Get system patch manager for updating the system
            ISystemPatchManager patchManager = new SystemPatchManager();
            bool success = patchManager.Update();
            bool baseSuccess = base.Start(taskOptions);
            if (success)
                return baseSuccess;
            else
                return false;
        }
    }
}

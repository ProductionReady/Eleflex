using System;
using Eleflex;

namespace WebServer.App_Start.Eleflex_Start
{
    /// <summary>
    /// Represents a shutdown task when the applications ends to cleanup resources.
    /// </summary>
    public partial class SystemShutdownTask : ShutdownTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SystemShutdownTask() : base()
        {
            Description = @"This task shuts down the system.";
            Priority = StartupConstants.PRIORITY_FINAL;
        }

        /// <summary>
        /// Stop processing logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Stop(ITaskOptions taskOptions)
        {
            //Shutdown logging
            if (Common.Logging.LogManager.Adapter is IDisposable)
                (Common.Logging.LogManager.Adapter as IDisposable).Dispose();

            //Shutdown service locator
            if (ObjectLocator.Container is IDisposable)
                (ObjectLocator.Container as IDisposable).Dispose();

            return true;
        }
    }
}

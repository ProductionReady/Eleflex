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
            try
            {
                //Shutdown logging
                if (Common.Logging.LogManager.Adapter is IDisposable)
                    (Common.Logging.LogManager.Adapter as IDisposable).Dispose();
                Logger.Current = null;

                //Shutdown service locator
                if (ObjectLocator.Container is IDisposable)
                    (ObjectLocator.Container as IDisposable).Dispose();
                ObjectLocator.Current = null;
                return true;
            }
            catch { }
            return false;
        }
    }
}

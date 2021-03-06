﻿namespace Eleflex
{
    /// <summary>
    /// Represents an object that is loaded at application start and executed.
    /// </summary>
    public abstract partial class ShutdownTask : IShutdownTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public ShutdownTask()
        {
            Name = this.GetType().FullName;
            Priority = StartupConstants.PRIORITY_AFTERSYSTEM;
        }

        /// <summary>
        /// The name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The description.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// THe execution priority.
        /// </summary>
        public virtual int Priority { get; set; }

        /// <summary>
        /// Perform start logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public virtual bool Stop(ITaskOptions taskOptions)
        {
            return true;
        }

    }
}

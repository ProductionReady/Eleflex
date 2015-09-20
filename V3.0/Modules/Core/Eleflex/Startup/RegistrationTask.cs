using System;

namespace Eleflex
{
    /// <summary>
    /// Represents an object that performs registration logic when executed.
    /// </summary>
    public abstract partial class RegistrationTask : IRegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public RegistrationTask()
        {
            Name = this.GetType().FullName;
            Priority = StartupConstants.PRIORITY_AFTERSYSTEM;
        }

        /// <summary>
        /// The description
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// The name. By default, the name is the type fullname.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The execution priority.
        /// </summary>
        public virtual int Priority { get; set; }

        /// <summary>
        /// Perform registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public virtual bool Register(ITaskOptions taskOptions)
        {
            return true;
        }

    }
}

namespace Eleflex
{
    /// <summary>
    /// Represents an object that performs registration logic when executed.
    /// </summary>
    public partial interface IRegistrationTask
    {
        /// <summary>
        /// The name. By default, the name is the type fullname.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The execution priority.
        /// </summary>
        int Priority { get; set; }

        /// <summary>
        /// Perform registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        bool Register(ITaskOptions taskOptions);
    }
}

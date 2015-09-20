namespace Eleflex
{
    /// <summary>
    /// Represents an object that is loaded at application start and executed.
    /// </summary>
    public partial interface IShutdownTask
    {
        
        /// <summary>
        /// THe name. By default, this is the type fullname.
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
        /// Perform startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        bool Stop(ITaskOptions taskOptions);
    }
}

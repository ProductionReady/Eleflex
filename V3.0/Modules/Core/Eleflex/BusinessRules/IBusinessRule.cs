namespace Eleflex
{
    /// <summary>
    /// Represents a rule enforcing business logic.
    /// </summary>
    public partial interface IBusinessRule
    {
        /// <summary>
        /// The name of the business rule. By default, the name is the type fullname.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The user error message.
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// Determine if processing should stop when an error is returned. Default is true.
        /// </summary>
        bool StopOnError { get; set; }

        /// <summary>
        /// Execution priority of the rule.
        /// </summary>
        int Priority { get; set; }

        /// <summary>
        /// Execute the business rule logic.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IResponse Execute(IRequestItem<IContext> request);
    }
}

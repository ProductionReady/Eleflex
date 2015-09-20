namespace Eleflex
{
    /// <summary>
    /// Represents a rule enforcing business logic.
    /// </summary>
    public partial class BusinessRule : IBusinessRule
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public BusinessRule()
        {
            Priority = StartupConstants.PRIORITY_SYSTEM;
            StopOnError = true;
        }

        /// <summary>
        /// The name of the business rule. By default, the name is the type fullname.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The user error message.
        /// </summary>
        public virtual string ErrorMessage { get; set; }

        /// <summary>
        /// Determine if processing should stop when an error is returned. Default is true.
        /// </summary>
        public virtual bool StopOnError { get; set; }

        /// <summary>
        /// Execution priority of the rule.
        /// </summary>
        public virtual int Priority { get; set; }

        /// <summary>
        /// Execute the business rule logic.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponse Execute(IRequestItem<IContext> request)
        {
            return new Response();
        }
    }
}

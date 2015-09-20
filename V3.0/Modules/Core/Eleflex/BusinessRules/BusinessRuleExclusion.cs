namespace Eleflex
{
    /// <summary>
    /// Represents a rule enforcing business logic.
    /// </summary>
    public partial class BusinessRuleExclusion : BusinessRule, IBusinessRuleExclusion
    {


        /// <summary>
        /// The name of the business rule to exclude.
        /// </summary>
        public virtual string ExcludedBusinessRuleName { get; set; }

    }
}

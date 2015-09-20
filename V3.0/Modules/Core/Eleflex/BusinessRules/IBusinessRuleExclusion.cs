namespace Eleflex
{
    /// <summary>
    /// Represents a rule enforcing business logic.
    /// </summary>
    public partial interface IBusinessRuleExclusion : IBusinessRule
    {
        /// <summary>
        /// The name of the business rule to exclude.
        /// </summary>
        string ExcludedBusinessRuleName { get; set; }
        
    }
}

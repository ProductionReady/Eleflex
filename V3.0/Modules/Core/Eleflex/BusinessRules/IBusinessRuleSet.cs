namespace Eleflex
{
    /// <summary>
    /// Represents a list of business rule objects to operates like an individual business rules.
    /// </summary>
    public partial interface IBusinessRuleSet : IItems<IBusinessRule>, IBusinessRule
    {
    }
}

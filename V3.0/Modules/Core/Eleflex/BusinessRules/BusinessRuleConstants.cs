namespace Eleflex
{
    /// <summary>
    /// A static class containing constants for business rules.
    /// </summary>
    public static partial class BusinessRuleConstants
    {

        /// <summary>
        /// Option to disable business rules during execution. This is a key stored in the context Data property that contains 
        /// a generic IList of strings with business rule names [Note: class type fullnames by default] to be disabled.
        /// </summary>
        public const string OPTION_DISABLE_BUSINESS_RULE_TYPES = "OPTIONDISABLEBUSINESSRULETYPES";

    }
}

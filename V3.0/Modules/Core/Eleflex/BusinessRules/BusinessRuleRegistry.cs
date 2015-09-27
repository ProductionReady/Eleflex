using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a registry of all business rules by type in the application.
    /// </summary>
    public partial class BusinessRuleRegistry
    {

        /// <summary>
        /// Static current instance of the business rule registry.
        /// </summary>
        public static IBusinessRuleRegistryService Current = new BusinessRuleRegistryService();
        
    }
}

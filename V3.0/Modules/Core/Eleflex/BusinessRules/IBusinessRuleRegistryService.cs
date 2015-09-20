using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a registry of all business rules by type in the application.
    /// </summary>
    public partial interface IBusinessRuleRegistryService : IRegistryList<Type, Type>
    {
    }
}

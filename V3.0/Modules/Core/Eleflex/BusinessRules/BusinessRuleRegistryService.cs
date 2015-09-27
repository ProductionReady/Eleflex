using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents a registry of all business rules by type in the application.
    /// </summary>
    public partial class BusinessRuleRegistryService : RegistryList<Type, Type>, IBusinessRuleRegistryService
    {        
    }
}

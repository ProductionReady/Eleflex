using System;

namespace Eleflex
{
    /// <summary>
    /// Class attribute used to provide registration for BusinessRules.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public partial class BusinessRuleRegistrationTaskAttribute : Attribute
    {        
    }
}

using System;

namespace Eleflex
{
    /// <summary>
    /// Class attribute used to denote that a BusinessRule object should be processed by the system. Add this to an individual rule or on to a rulset.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public partial class BusinessRuleProcessAttribute : Attribute
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="objectType">The type of object that should be processed.</param>
        public BusinessRuleProcessAttribute(Type objectType)
            : base()
        {
            ObjectType = objectType;
        }

        /// <summary>
        /// The type of object that should be processed.
        /// </summary>
        public virtual Type ObjectType { get; set; }
    }
}

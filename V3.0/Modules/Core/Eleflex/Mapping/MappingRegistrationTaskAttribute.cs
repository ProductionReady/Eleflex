using System;

namespace Eleflex
{
    /// <summary>
    /// Class attribute used to provide registration for mappings.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public partial class MappingRegistrationTaskAttribute : Attribute
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public MappingRegistrationTaskAttribute()
            : base()
        {
        }
    }
}

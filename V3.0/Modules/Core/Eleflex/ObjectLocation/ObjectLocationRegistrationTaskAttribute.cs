using System;

namespace Eleflex
{
    /// <summary>
    /// Class attribute used to provide registration for object location.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public partial class ObjectLocationRegistrationTaskAttribute : Attribute
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public ObjectLocationRegistrationTaskAttribute()
            : base()
        {
        }
    }
}

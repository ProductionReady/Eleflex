using System;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Class attribute used to provide registration for BusinessRules.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public partial class WCFServicesRegistrationTaskAttribute : Attribute
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public WCFServicesRegistrationTaskAttribute()
            : base()
        {
        }

    }
}

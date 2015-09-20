using System;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Represents an attribute to register a WCF command in the system.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public partial class WCFCommandRegistrationAttribute : Attribute
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="requestType"></param>
        /// <param name="responseType"></param>
        public WCFCommandRegistrationAttribute(Type requestType, Type responseType)
            : base()
        {
            RequestType = requestType;
            ResponseType = responseType;
        }

        /// <summary>
        /// The request type.
        /// </summary>
        public virtual Type RequestType { get; set; }

        /// <summary>
        /// The response type.
        /// </summary>
        public virtual Type ResponseType { get; set; }
    }
}

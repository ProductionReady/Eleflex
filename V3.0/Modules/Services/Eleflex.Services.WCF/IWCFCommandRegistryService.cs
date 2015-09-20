using System;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Represents an object defining a WCF command registry.
    /// </summary>
    public partial interface IWCFCommandRegistryService : IRegistry<Type, Type>
    {
        /// <summary>
        /// Get the service command handler defined by the request type.
        /// </summary>
        /// <param name="requestType"></param>
        /// <returns></returns>
        IWCFCommand GetCommand(Type requestType);
    }
}

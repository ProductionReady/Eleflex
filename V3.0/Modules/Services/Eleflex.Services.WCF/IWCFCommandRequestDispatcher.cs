using System;

namespace Eleflex.Services.WCF
{
    /// <summary>
    /// Represents an object used for all service command request dispatchers in the application. Allows abstraction in case of multiple endpoints.
    /// </summary>
    public partial interface IWCFCommandRequestDispatcher : IWCFCommand, IDisposable
    {
        /// <summary>
        /// Execute a service command.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        T ExecuteServiceCommand<T>(Request request) where T : class, IResponse, new();
    }
}

using System;

namespace Eleflex
{
    /// <summary>
    /// Represents an object exposing object location / service locator pattern for the application.
    /// </summary>
    public partial interface IObjectLocationService
    {

        /// <summary>
        /// Get an object instance of the specified type.
        /// </summary>
        /// <typeparam name="TServiceType"></typeparam>
        /// <returns></returns>
        TServiceType GetInstance<TServiceType>();

        /// <summary>
        /// Get an object instance by the specifed type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object GetInstance(Type type);

    }
}

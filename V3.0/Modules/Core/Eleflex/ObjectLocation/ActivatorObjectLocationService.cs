using System;

namespace Eleflex
{
    /// <summary>
    /// Represents an object exposing object location / service locator pattern for the application. 
    /// This class uses Reflection.Activator.CreateInstance() to create requested types. (Simple functionality)
    /// </summary>
    public partial class ActivatorObjectLocationService : IObjectLocationService
    {

        /// <summary>
        /// Get an object instance of the specified type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual object GetInstance(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch//(Exception ex)
            {
                //Don't fill up log with errors
                //Logger.Current.Error<ActivatorObjectLocationService>(ex);
                return null;
            }
        }

        /// <summary>
        /// Get an object instance of the specified type.
        /// </summary>
        /// <typeparam name="TServiceType"></typeparam>
        /// <returns></returns>
        public virtual TServiceType GetInstance<TServiceType>()
        {
            try
            {
                var obj = Activator.CreateInstance(typeof(TServiceType));
                return (TServiceType)obj;
            }
            catch//(Exception ex)
            {
                //Don't fill up log with errors
                //Logger.Current.Error<ActivatorObjectLocationService>(ex);
                return default(TServiceType);
            }
        }
    }
}

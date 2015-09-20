using System;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.ObjectLocation.CSL
{
    /// <summary>
    /// Represents the object location service using Microsoft's CommonServiceLocator.
    /// </summary>
    public partial class CommonServiceLocatorService : IObjectLocationService
    {

        /// <summary>
        /// Get an object instance.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public virtual object GetInstance(Type type)
        {
            try
            {
                return ServiceLocator.Current.GetInstance(type);
            }
            catch(Exception ex)
            {
                Logger.Current.Error<CommonServiceLocatorService>(ex);
                return null;
            }
        }

        /// <summary>
        /// Get an object instance.
        /// </summary>
        /// <typeparam name="TServiceType"></typeparam>
        /// <returns></returns>
        public TServiceType GetInstance<TServiceType>()
        {
            try
            {
                return ServiceLocator.Current.GetInstance<TServiceType>();
            }
            catch(Exception ex)
            {
                Logger.Current.Error<CommonServiceLocatorService>(ex);
                return default(TServiceType);
            }
        }
    }
}

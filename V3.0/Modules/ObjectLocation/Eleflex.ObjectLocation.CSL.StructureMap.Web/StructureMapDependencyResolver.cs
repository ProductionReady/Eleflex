using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace Eleflex.ObjectLocation.CSL.StructureMap.Web
{
    /// <summary>
    /// Dependency resolvers for Mvc controllers to use structuremap.
    /// </summary>
    public partial class StructureMapDependencyResolver : IDependencyResolver
    {        

        /// <summary>
        /// Constructor.
        /// </summary>
        public StructureMapDependencyResolver()
        {
        }

        /// <summary>
        /// Get a service.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public virtual object GetService(Type serviceType)
        {
            if (serviceType == null)
                return null;
            try
            {
                if (serviceType.IsAbstract || serviceType.IsInterface)
                    return (ObjectLocator.Container as IContainer).TryGetInstance(serviceType);
                else
                    return (ObjectLocator.Container as IContainer).GetInstance(serviceType);
            }
            catch(Exception ex)
            {
                Logger.Current.Error<StructureMapDependencyResolver>(ex);
                return null;
            }
        }

        /// <summary>
        /// Get a list of services.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public virtual IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return (ObjectLocator.Container as IContainer).GetAllInstances(serviceType).Cast<object>();
            }
            catch(Exception ex)
            {
                Logger.Current.Error<StructureMapDependencyResolver>(ex);
                return null;
            }
        }

    }
}
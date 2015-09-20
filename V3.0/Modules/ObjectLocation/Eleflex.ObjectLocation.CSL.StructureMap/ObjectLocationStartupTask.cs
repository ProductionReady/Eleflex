using Microsoft.Practices.ServiceLocation;
using StructureMap;
using StructureMap.Graph;
using CommonServiceLocator.StructureMapAdapter.Unofficial;

namespace Eleflex.ObjectLocation.CSL.StructureMap
{
    /// <summary>
    /// Represents an object location startup task for configuring CommonServiceLocator.
    /// </summary>
    public partial class ObjectLocationStartupTask : StartupTaskWithRegistration<ObjectLocationRegistrationTaskAttribute>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public ObjectLocationStartupTask()
        {
            Description = @"This task registers all object location configurations needed by the system.";
            Priority = StartupConstants.STARTUP_PRIORITY_OBJECTLOCATION;
        }

        /// <summary>
        /// Execute startup logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Start(ITaskOptions taskOptions)
        {
            //Create a new container
            IContainer container = new Container();

            //Use default naming conventions for dynamic resolution
            container.Configure(x =>

                x.Scan(scan =>
                {
                    scan.AssembliesFromApplicationBaseDirectory();
                    scan.WithDefaultConventions();
                })
            );

            //Can now use CommonServiceLocator in the rest of the application for IOC instance creation
            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator(container));

            //Set global container reference and setup CSL ObjectLocator instance
            ObjectLocator.Container = container;
            ObjectLocator.Current = new CommonServiceLocatorService();

            return base.Start(taskOptions);
        }
    }
}

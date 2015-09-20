using StructureMap;
using StructureMap.Pipeline;
using StructureMap.Web;
using StructureMap.Web.Pipeline;

namespace Eleflex.ObjectLocation.CSL.StructureMap.Web
{
    /// <summary>
    /// Represents an object location registration task for unit of work configurations.
    /// </summary>
    [ObjectLocationRegistrationTask]
    public partial class UnitOfWorkRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public UnitOfWorkRegistrationTask()
        {
            Description = "This tasks registers unit of work for the system.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            IContainer container = ObjectLocator.Container as IContainer;
            container.Configure(x =>
            {

                x.For<StorageContextUnitOfWork>().HybridHttpOrThreadLocalScoped();

                x.For<IUnitOfWork>().LifecycleIs<HttpContextLifecycle>().Use(() => container.GetInstance<StorageContextUnitOfWork>());
                x.For<IStorageContextUnitOfWork>().LifecycleIs<HttpContextLifecycle>().Use(() => container.GetInstance<StorageContextUnitOfWork>());

                x.For<IUnitOfWork>().LifecycleIs<ThreadLocalStorageLifecycle>().Use(() => container.GetInstance<StorageContextUnitOfWork>());
                x.For<IStorageContextUnitOfWork>().LifecycleIs<ThreadLocalStorageLifecycle>().Use(() => container.GetInstance<StorageContextUnitOfWork>());
            });

            return base.Register(taskOptions);
        }
    }
}

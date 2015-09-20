using StructureMap;
using StructureMap.Pipeline;
using StructureMap.Web.Pipeline;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// Represents a business rule registration task to configure context builders.
    /// </summary>
    [ObjectLocationRegistrationTask()]
    public partial class ContextBuilderObjectLocationRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public ContextBuilderObjectLocationRegistrationTask()
        {
            Description = "This task registers context building.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            //Setup storage endpoints
            IContainer container = ObjectLocator.Container as IContainer;
            container.Configure(x =>
            {
                x.For<IContextBuilder>().LifecycleIs<HttpContextLifecycle>().Use(() => container.GetInstance<IdentityContextBuilder>());
                x.For<IContextBuilder>().LifecycleIs<ThreadLocalStorageLifecycle>().Use(() => container.GetInstance<IdentityContextBuilder>());                
            });

            return base.Register(taskOptions);
        }
    }
}

using StructureMap;

namespace Eleflex.ObjectLocation.CSL.StructureMap
{
    /// <summary>
    /// Represents an object location registration task for business rule structuremap configurations.
    /// </summary>
    [ObjectLocationRegistrationTask]
    public partial class BusinessRuleObjectLocationRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public BusinessRuleObjectLocationRegistrationTask()
        {
            Description = "This tasks registers business rule object location configurations for the system.";
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
                //BusinessRuleRegistry also exists, however it is used to store the current service instance.
                //Use BusinessRuleRegistryService which is just a passthrough to call the BusinessRuleRegistry.Current methods.
                x.For<IBusinessRuleRegistryService>().Use<BusinessRuleRegistryService>();
            });

            return base.Register(taskOptions);
        }
    }
}

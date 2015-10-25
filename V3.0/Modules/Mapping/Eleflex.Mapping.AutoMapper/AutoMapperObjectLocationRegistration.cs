
namespace Eleflex.Mapping.AutoMapper
{
    /// <summary>
    /// Represents an object used for object location registration to register AutoMapper in the system.
    /// </summary>
    [ObjectLocationRegistrationTask]
    public partial class AutoMapperObjectLocationRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public AutoMapperObjectLocationRegistrationTask()
        {
            Description = "This tasks registers automapper mapping for the system.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {            
            //Setup mapping service
            StructureMap.IContainer container = ObjectLocator.Container as StructureMap.IContainer;

            container.Configure(x =>
            {
                x.For<IMappingService>().Use<AutoMapperMappingService>();
            });

            return base.Register(taskOptions);
        }
    }
}

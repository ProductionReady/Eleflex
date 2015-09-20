namespace Eleflex.ObjectLocation.CSL.StructureMap.Web
{
    /// <summary>
    /// Represents a object location registration task for configuring web dependency resolver.
    /// </summary>
    [ObjectLocationRegistrationTask]
    public partial class DependencyResolverRegistrationTask : RegistrationTask
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public DependencyResolverRegistrationTask()
        {
            Description = "This tasks registers MVC's dependency resolver.";
        }

        /// <summary>
        /// Execute registration logic.
        /// </summary>
        /// <param name="taskOptions"></param>
        /// <returns></returns>
        public override bool Register(ITaskOptions taskOptions)
        {
            System.Web.Mvc.DependencyResolver.SetResolver(new Eleflex.ObjectLocation.CSL.StructureMap.Web.StructureMapDependencyResolver());
            return base.Register(taskOptions);
        }
    }
}

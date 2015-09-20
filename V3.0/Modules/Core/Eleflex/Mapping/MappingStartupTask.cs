namespace Eleflex
{
    /// <summary>
    /// Startup task for mapping objects that additionally processes registration tasks.
    /// </summary>
    public partial class MappingStartupTask : StartupTaskWithRegistration<MappingRegistrationTaskAttribute>
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public MappingStartupTask() : base()
        {            
            Description = @"This task registers all mapping configurations for the system.";
            Priority = StartupConstants.STARTUP_PRIORITY_MAPPING;
        }  
    }
}

namespace Eleflex
{
    /// <summary>
    /// A static class containing constants for startup.
    /// </summary>
    public static partial class StartupConstants
    {

        /// <summary>
        /// Execution priority before system.
        /// </summary>
        public const int PRIORITY_BEFORESYSTEM = 1;
        /// <summary>
        /// Execution priority system.
        /// </summary>
        public const int PRIORITY_SYSTEM = 1000;
        /// <summary>
        /// Execution priority system.
        /// </summary>
        public const int PRIORITY_SYSTEM_CUSTOM = STARTUP_PRIORITY_LOGGING + 1;
        /// <summary>
        /// Execution priority after system.
        /// </summary>
        public const int PRIORITY_AFTERSYSTEM = 2000;
        /// <summary>
        /// Execution priority after system to load custom components.
        /// </summary>
        public const int PRIORITY_CUSTOM = 3000;
        /// <summary>
        /// Execution priority the last items to execute.
        /// </summary>
        public const int PRIORITY_FINAL = 4000;

        /// <summary>
        /// Predefined startup priorities for system component object location.
        /// </summary>
        public const int STARTUP_PRIORITY_OBJECTLOCATION = PRIORITY_SYSTEM;
        /// <summary>
        /// Predefined startup priorities for system component mapping.
        /// </summary>
        public const int STARTUP_PRIORITY_MAPPING = PRIORITY_SYSTEM + 1;
        /// <summary>
        /// Predefined startup priorities for system component business rules.
        /// </summary>
        public const int STARTUP_PRIORITY_BUSINESSRULES = PRIORITY_SYSTEM + 2;
        /// <summary>
        /// Predefined startup priorities for system component service communication.
        /// </summary>
        public const int STARTUP_PRIORITY_SERVICECOMMUNICATION = PRIORITY_SYSTEM + 3;
        /// <summary>
        /// Predefined startup priorities for system component patching.
        /// </summary>
        public const int STARTUP_PRIORITY_SYSTEMPATCHING = PRIORITY_SYSTEM + 4;
        /// <summary>
        /// Predefined startup priorities for system component logging.
        /// </summary>
        public const int STARTUP_PRIORITY_LOGGING = PRIORITY_AFTERSYSTEM;
        
    }
}

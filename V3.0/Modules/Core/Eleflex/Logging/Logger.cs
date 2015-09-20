namespace Eleflex
{
    /// <summary>
    /// Static class used to store the current logging service instance.
    /// </summary>
    public static partial class Logger
    {

        /// <summary>
        /// The current logging service.
        /// </summary>
        public static ILogService Current { get; set; }

    }
}

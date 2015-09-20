namespace Eleflex
{
    /// <summary>
    /// Static class used to store the current instance of the object location service.
    /// </summary>
    public static partial class ObjectLocator
    {

        /// <summary>
        /// The IoC container handling configurations.
        /// </summary>
        public static object Container { get; set; }

        /// <summary>
        /// The current instance of the object location service.
        /// </summary>
        public static IObjectLocationService Current { get; set; }
    }
}

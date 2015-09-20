namespace Eleflex
{
    /// <summary>
    /// Represents an object that can create the context.
    /// </summary>
    public partial interface IContextBuilder
    {

        /// <summary>
        /// Get the context object.
        /// </summary>
        /// <returns></returns>
        IContext GetContext();
    }
}

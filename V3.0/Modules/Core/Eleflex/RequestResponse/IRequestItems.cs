namespace Eleflex
{
    /// <summary>
    /// Represents an IRequest object containing a collection of typed object items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IRequestItems<T> : IRequest, IItems<T>
    {
    }
}

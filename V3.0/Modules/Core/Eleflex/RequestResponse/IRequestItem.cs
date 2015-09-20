namespace Eleflex
{
    /// <summary>
    /// Represents an IRequest object containing a single typed object item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IRequestItem<T> : IRequest, IItem<T>
    {
    }
}

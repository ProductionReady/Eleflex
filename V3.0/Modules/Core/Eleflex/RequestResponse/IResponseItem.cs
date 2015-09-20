namespace Eleflex
{
    /// <summary>
    /// Represents an IResponse object containing a single typed object item.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IResponseItem<T> : IResponse, IItem<T>
    {
    }
}

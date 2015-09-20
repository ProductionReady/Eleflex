namespace Eleflex
{
    /// <summary>
    /// Represents an IResponse object containing a collection of typed object items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IResponseItems<T> : IResponse, IItems<T>
    {
    }
}

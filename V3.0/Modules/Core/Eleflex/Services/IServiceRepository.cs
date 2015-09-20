namespace Eleflex
{
    /// <summary>
    /// Represents an object repository for service communication.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TPkDataType"></typeparam>
    public partial interface IServiceRepository<TObject, TPkDataType> : IRepository<TObject, TPkDataType>
        where TObject : class
    {
    }
}

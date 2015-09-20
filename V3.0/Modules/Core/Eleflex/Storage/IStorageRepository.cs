namespace Eleflex
{
    /// <summary>
    /// Represents an object repository pattern for seperation between domain and storage service.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TPkDataType"></typeparam>
    public partial interface IStorageRepository<TObject, TPkDataType> : IRepository<TObject, TPkDataType>, IUnitOfWork //Note: IUnitOfWork added for transaction usage integrated with unit of work provider
        where TObject : class
    {
    }
}
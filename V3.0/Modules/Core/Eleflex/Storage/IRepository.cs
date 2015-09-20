namespace Eleflex
{
    /// <summary>
    /// Represents a repository pattern for entities with a single primary key value.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TPkDataType"></typeparam>
    public partial interface IRepository<TObject, TPkDataType> 
        where TObject : class        
    {
        /// <summary>
        /// Insert an entity.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IResponseItem<TObject> Insert(IRequestItem<TObject> request);

        /// <summary>
        /// Get an entity by its key.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IResponseItem<TObject> Get(IRequestItem<TPkDataType> request);

        /// <summary>
        /// Update an entity. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IResponseItem<TObject> Update(IRequestItem<TObject> request);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="request"></param>
        IResponse Delete(IRequestItem<TPkDataType> request);

        /// <summary>
        /// Query entities.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IStorageQueryResponseItems<TObject> Query(IRequestItem<IStorageQuery> request);

        /// <summary>
        /// Query entities for an aggregate result.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request);
    }
}
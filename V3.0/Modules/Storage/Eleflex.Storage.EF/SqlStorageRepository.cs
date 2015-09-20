namespace Eleflex.Storage.EF
{
    /// <summary>
    /// Sql Server generic sql repository pattern for an entity.
    /// </summary>
    /// <typeparam name="TStorageService"></typeparam>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TDataType"></typeparam>
    public abstract class SqlStorageRepository<TStorageService, TObject, TDataType> : IStorageRepository<TObject, TDataType>
        where TObject : class
        where TStorageService : IStorageService
    {

        /// <summary>
        /// The storage provider.
        /// </summary>
        protected readonly TStorageService _storageService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="storageService"></param>
        protected SqlStorageRepository(TStorageService storageService)
        {
            _storageService = storageService;
        }

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        public abstract IResponseItem<TObject> Insert(IRequestItem<TObject> request);

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract IResponseItem<TObject> Get(IRequestItem<TDataType> request);

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public abstract IResponseItem<TObject> Update(IRequestItem<TObject> request);

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="key"></param>
        public abstract IResponse Delete(IRequestItem<TDataType> request);

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public abstract IStorageQueryResponseItems<TObject> Query(IRequestItem<IStorageQuery> request);

        /// <summary>
        /// Query aggregate.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public abstract IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request);

        /// <summary>
        /// Commit.
        /// </summary>
        public virtual void Commit()
        {
            _storageService.Commit();
        }

        /// <summary>
        /// Rollback.
        /// </summary>
        public virtual void Rollback()
        {
            _storageService.Rollback();
        }
    }
}
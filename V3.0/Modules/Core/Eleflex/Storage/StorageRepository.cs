namespace Eleflex
{
    /// <summary>
    /// Abstract base class for storage repositories.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TPkDataType"></typeparam>
    public abstract partial class StorageRepository<TObject, TPkDataType> : IStorageRepository<TObject, TPkDataType>
        where TObject : class
    {
        /// <summary>
        /// Internal repository.
        /// </summary>
        protected readonly IStorageRepository<TObject, TPkDataType> _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public StorageRepository(IStorageRepository<TObject, TPkDataType> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Insert.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TObject> Insert(IRequestItem<TObject> request)
        {
            return _repository.Insert(request);
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TObject> Get(IRequestItem<TPkDataType> request)
        {
            return _repository.Get(request);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TObject> Update(IRequestItem<TObject> request)
        {
            return _repository.Update(request);
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="request"></param>
        public virtual IResponse Delete(IRequestItem<TPkDataType> request)
        {
            return _repository.Delete(request);
        }

        /// <summary>
        /// Query entities.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<TObject> Query(IRequestItem<IStorageQuery> request)
        {
            return _repository.Query(request);
        }

        /// <summary>
        /// Query entities for an aggregate.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request)
        {
            return _repository.QueryAggregate(request);
        }

        /// <summary>
        /// Commit.
        /// </summary>
        public virtual void Commit()
        {
            _repository.Commit();
        }

        /// <summary>
        /// Rollback.
        /// </summary>
        public virtual void Rollback()
        {
            _repository.Rollback();
        }
    }
}

namespace Eleflex.Services
{
    /// <summary>
    /// Represents an object repository for service communication.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TPkDataType"></typeparam>
    public partial class ServiceRepository<TObject, TPkDataType> : IServiceRepository<TObject, TPkDataType>
        where TObject : class
    {

        /// <summary>
        /// Service repository.
        /// </summary>
        protected readonly IServiceRepository<TObject, TPkDataType> _serviceRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public ServiceRepository(IServiceRepository<TObject, TPkDataType> serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        /// <summary>
        /// Insert.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TObject> Insert(IRequestItem<TObject> request)
        {
            return _serviceRepository.Insert(request);
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TObject> Get(IRequestItem<TPkDataType> request)
        {
            return _serviceRepository.Get(request);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TObject> Update(IRequestItem<TObject> request)
        {
            return _serviceRepository.Update(request);
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="request"></param>
        public virtual IResponse Delete(IRequestItem<TPkDataType> request)
        {
            return _serviceRepository.Delete(request);
        }

        /// <summary>
        /// Query entities.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<TObject> Query(IRequestItem<IStorageQuery> request)
        {
            return _serviceRepository.Query(request);
        }

        /// <summary>
        /// Query entities for an aggregate.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request)
        {
            return _serviceRepository.QueryAggregate(request);
        }


    }
}

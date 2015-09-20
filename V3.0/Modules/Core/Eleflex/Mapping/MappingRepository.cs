using System;

namespace Eleflex
{
    /// <summary>
    /// Represents an object for repositories that map to another object repository. Primary keys must be identical between the source and destination objects.
    /// </summary>
    /// <typeparam name="TSourceObject"></typeparam>
    /// <typeparam name="TSourcePkDataType"></typeparam>
    /// <typeparam name="TDestinationObject"></typeparam>
    public partial class MappingRepository<TSourceObject, TSourcePkDataType, TDestinationObject, TDestinationRepositoryType> : IMappingRepository<TSourceObject, TSourcePkDataType, TDestinationObject, TDestinationRepositoryType>
        where TSourceObject : class
        where TDestinationObject : class
        where TDestinationRepositoryType : IRepository<TDestinationObject, TSourcePkDataType>
    {

        /// <summary>
        /// Destination repository that commands will passthrough to.
        /// </summary>
        protected readonly IRepository<TDestinationObject, TSourcePkDataType> _repository;
        /// <summary>
        /// Service that maps information between two objects.
        /// </summary>
        protected readonly IMappingService _mappingService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository">Destination repository that commands will passthrough to.</param>
        /// <param name="mappingService">Service that maps information between two objects.</param>
        public MappingRepository(
            TDestinationRepositoryType repository,
            IMappingService mappingService)
        {            
            _mappingService = mappingService;
            _repository = repository;
        }

        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TSourceObject> Insert(IRequestItem<TSourceObject> request)
        {
            var response = new ResponseItem<TSourceObject>();
            try
            {
                //Map to destination
                TDestinationObject destination = _mappingService.Map<TSourceObject, TDestinationObject>(request.Item);

                //Call destination repository
                var req = new RequestItem<TDestinationObject>() { Item = destination };
                var resp = _repository.Insert(req);

                //Copy and map data back to source
                response.CopyResponse(resp);
                if (resp.Item != null)
                    response.Item = _mappingService.Map<TSourceObject, TDestinationObject>(resp.Item);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TSourceObject, TSourcePkDataType>>(ex);
                    response.AddMessage(true, MessageConstants.ERROR_BUSINESS_RULE_CODE, MessageConstants.ERROR_BUSINESS_RULE_TEXT);
                }
            }
            return response;
        }

        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TSourceObject> Get(IRequestItem<TSourcePkDataType> request)
        {
            var response = new ResponseItem<TSourceObject>();
            try
            {
                //Map to destination
                TSourcePkDataType destination = request.Item;

                //Call destination repository
                var req = new RequestItem<TSourcePkDataType>() { Item = destination };
                var resp = _repository.Get(req);

                //Copy and map data back to source
                response.CopyResponse(resp);
                if (resp.Item != null)
                    response.Item = _mappingService.Map<TSourceObject, TDestinationObject>(resp.Item);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TSourceObject, TSourcePkDataType>>(ex);
                    response.AddMessage(true, MessageConstants.ERROR_BUSINESS_RULE_CODE, MessageConstants.ERROR_BUSINESS_RULE_TEXT);
                }
            }
            return response;
        }

        /// <summary>
        /// Update an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TSourceObject> Update(IRequestItem<TSourceObject> request)
        {
            var response = new ResponseItem<TSourceObject>();
            try
            {
                //Map to destination
                TDestinationObject destination = _mappingService.Map<TSourceObject, TDestinationObject>(request.Item);

                //Call destination repository
                var req = new RequestItem<TDestinationObject>() { Item = destination };
                var resp = _repository.Update(req);

                //Copy and map data back to source
                response.CopyResponse(resp);
                if (resp.Item != null)
                    response.Item = _mappingService.Map<TSourceObject, TDestinationObject>(resp.Item);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TSourceObject, TSourcePkDataType>>(ex);
                    response.AddMessage(true, MessageConstants.ERROR_BUSINESS_RULE_CODE, MessageConstants.ERROR_BUSINESS_RULE_TEXT);
                }
            }
            return response;
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="request"></param>
        public virtual IResponse Delete(IRequestItem<TSourcePkDataType> request)
        {
            var response = new Response();
            try
            {
                //Map to destination
                TSourcePkDataType destination = request.Item;

                //Call destination repository
                var req = new RequestItem<TSourcePkDataType>() { Item = destination };
                var resp = _repository.Delete(req);

                //Copy and map data back to source
                response.CopyResponse(resp);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TSourceObject, TSourcePkDataType>>(ex);
                    response.AddMessage(true, MessageConstants.ERROR_BUSINESS_RULE_CODE, MessageConstants.ERROR_BUSINESS_RULE_TEXT);
                }
            }
            return response;
        }

        /// <summary>
        /// Query items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IStorageQueryResponseItems<TSourceObject> Query(IRequestItem<IStorageQuery> request)
        {
            var response = new StorageQueryResponseItems<TSourceObject>();
            try
            {
                //Call destination repository
                var req = new RequestItem<IStorageQuery>() { Item = request.Item };
                var resp = _repository.Query(req);

                //Copy and map data back to source
                response.CopyResponse(resp);
                if (resp.Items != null)
                    response.Items = _mappingService.Map<TSourceObject, TDestinationObject>(resp.Items);
                return response;
            }
            catch (Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TSourceObject, TSourcePkDataType>>(ex);
                    response.AddMessage(true, MessageConstants.ERROR_BUSINESS_RULE_CODE, MessageConstants.ERROR_BUSINESS_RULE_TEXT);
                }
            }
            return response;
        }

        /// <summary>
        /// Query items for an aggregate result.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request)
        {
            var response = new ResponseItem<double>();
            try
            {
                //Call destination repository
                var req = new RequestItem<IStorageQuery>() { Item = request.Item };
                var resp = _repository.QueryAggregate(req);

                //Copy and map data back to source
                response.CopyResponse(resp);
                response.Item = resp.Item;
                return response;
            }
            catch (Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TSourceObject, TSourcePkDataType>>(ex);
                    response.AddMessage(true, MessageConstants.ERROR_BUSINESS_RULE_CODE, MessageConstants.ERROR_BUSINESS_RULE_TEXT);
                }
            }
            return response;
        }

    }
}

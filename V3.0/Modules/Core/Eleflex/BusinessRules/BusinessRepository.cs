using System;

namespace Eleflex
{
    /// <summary>
    /// Represets an object repository that perform business rule processing and storage. This is the main repository that all domain objects should use.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TPkDataType"></typeparam>
    public abstract partial class BusinessRepository<TObject, TPkDataType> : IBusinessRepository<TObject, TPkDataType>
        where TObject : class
    {

        /// <summary>
        /// Business rule processing service.
        /// </summary>
        protected IBusinessRuleService _businessRuleService;
        /// <summary>
        /// Storage repository for the object.
        /// </summary>
        protected IStorageRepository<TObject, TPkDataType> _storageRepository;
        /// <summary>
        /// The context builder.
        /// </summary>
        protected IContextBuilder _contextBuilder;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService">Business rule processing service.</param>
        /// <param name="storageRepository">Storage repository for the object.</param>
        public BusinessRepository(
            IBusinessRuleService businessRuleService,
            IContextBuilder contextBuilder,
            IStorageRepository<TObject, TPkDataType> storageRepository)
        {
            _businessRuleService = businessRuleService;
            _contextBuilder = contextBuilder;
            _storageRepository = storageRepository;
        }


        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TObject> Insert(IRequestItem<TObject> request)
        {
            IResponseItem<TObject> response = new ResponseItem<TObject>();
            RequestItem<IContext> bizRequest = new RequestItem<IContext>();
            try
            {
                //Process generic object business rules
                bizRequest.Item = _contextBuilder.GetContext();
                bizRequest.Item.Item = request.Item;
                IResponse bizResp = _businessRuleService.ExecuteBusinessRules(bizRequest);
                response.CopyResponse(bizResp);
                if (!response.ResponseSuccess)
                    return response;

                //Process storage provider
                var storageResponse = _storageRepository.Insert(request);
                response.CopyResponse(storageResponse);
                response.Item = storageResponse.Item;
            }
            catch(Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TObject, TPkDataType>>(ex);
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
        public virtual IResponseItem<TObject> Get(IRequestItem<TPkDataType> request)
        {
            IResponseItem<TObject> response = new ResponseItem<TObject>();
            try
            {
                //Process storage provider
                var storageResponse = _storageRepository.Get(request);
                response.CopyResponse(storageResponse);
                response.Item = storageResponse.Item;
            }
            catch (Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TObject, TPkDataType>>(ex);
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
        public virtual IResponseItem<TObject> Update(IRequestItem<TObject> request)
        {
            IResponseItem<TObject> response = new ResponseItem<TObject>();
            RequestItem<IContext> bizRequest = new RequestItem<IContext>();
            try
            {
                //Process generic object business rules
                bizRequest.Item = _contextBuilder.GetContext();
                bizRequest.Item.Item = request.Item;
                IResponse bizResp = _businessRuleService.ExecuteBusinessRules(bizRequest);
                response.CopyResponse(bizResp);
                if (!response.ResponseSuccess)
                    return response;

                //Process storage provider
                var storageResponse = _storageRepository.Update(request);
                response.CopyResponse(storageResponse);
                response.Item = storageResponse.Item;
            }
            catch (Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TObject, TPkDataType>>(ex);
                    response.AddMessage(true, MessageConstants.ERROR_BUSINESS_RULE_CODE, MessageConstants.ERROR_BUSINESS_RULE_TEXT);
                }
            }
            return response;
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="request"></param>
        public virtual IResponse Delete(IRequestItem<TPkDataType> request)
        {
            IResponseItem<TObject> response = new ResponseItem<TObject>();
            try
            {          
                //Process storage provider
                var storageResponse = _storageRepository.Delete(request);
                response.CopyResponse(storageResponse);
            }
            catch (Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TObject, TPkDataType>>(ex);
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
        public virtual IStorageQueryResponseItems<TObject> Query(IRequestItem<IStorageQuery> request)
        {
            IStorageQueryResponseItems<TObject> response = new StorageQueryResponseItems<TObject>();
            try
            {
                //Process storage provider
                var storageResponse = _storageRepository.Query(request);
                response.CopyResponse(storageResponse);
                response.Items = storageResponse.Items;
            }
            catch (Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TObject, TPkDataType>>(ex);
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
            IResponseItem<double> response = new ResponseItem<double>();
            try
            {
                //Process storage provider
                var storageResponse = _storageRepository.QueryAggregate(request);
                response.CopyResponse(storageResponse);
                response.Item = storageResponse.Item;
            }
            catch (Exception ex)
            {
                if (ex is IEleflexException)
                    response.CopyResponse(((IEleflexException)ex));
                else
                {
                    Logger.Current.Error<BusinessRepository<TObject, TPkDataType>>(ex);
                    response.AddMessage(true, MessageConstants.ERROR_BUSINESS_RULE_CODE, MessageConstants.ERROR_BUSINESS_RULE_TEXT);
                }
            }
            return response;
        }

        /// <summary>
        /// Commit.
        /// </summary>
        public virtual void Commit()
        {
            _storageRepository.Commit();
        }

        /// <summary>
        /// Rollback.
        /// </summary>
        public virtual void Rollback()
        {
            _storageRepository.Rollback();
        }
    }
}

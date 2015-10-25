using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Eleflex.Storage.EF
{
    /// <summary>
    /// Entity framework storage provider to map a domain object to storage.
    /// </summary>
    /// <typeparam name="TStorageService"></typeparam>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TDataType"></typeparam>
    /// <typeparam name="TStorageObject"></typeparam>
    public abstract partial class EntityMapStorageRepository<TStorageService, TObject, TDataType, TStorageObject> : IStorageRepository<TObject, TDataType>
        where TStorageService : IStorageService
        where TObject : class
        where TStorageObject : class
    {

        /// <summary>
        /// The storage provider.
        /// </summary>
        protected readonly TStorageService _storageService;
        /// <summary>
        /// The mapping service.
        /// </summary>
        protected readonly IMappingService _mappingService;
        /// <summary>
        /// Business rule processing service.
        /// </summary>
        protected IBusinessRuleService _businessRuleService;
        /// <summary>
        /// Context builder.
        /// </summary>
        protected IContextBuilder _contextBuilder;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="businessRuleService"></param>
        /// <param name="contextBuilder"></param>
        /// <param name="storageService"></param>
        /// <param name="mappingService"></param>
        protected EntityMapStorageRepository(
            IBusinessRuleService businessRuleService,
            IContextBuilder contextBuilder,
            TStorageService storageService,
            IMappingService mappingService)
        {
            _businessRuleService = businessRuleService;
            _contextBuilder = contextBuilder;
            _storageService = storageService;
            _mappingService = mappingService;
        }


        /// <summary>
        /// Insert an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TObject> Insert(IRequestItem<TObject> request)
        {
            ResponseItem<TObject> response = new ResponseItem<TObject>();
            RequestItem<IContext> bizRequest = new RequestItem<IContext>();
            try
            {
                //Process specific repository events. We don't raise the event because we don't want it to throw exceptions.
                bizRequest.Item = _contextBuilder.GetContext();
                bizRequest.Item.Item = new RepositoryInsertEvent<TObject>() { Item = request.Item };
                var repoResp = _businessRuleService.ExecuteBusinessRules(bizRequest);
                response.CopyResponse(repoResp);
                if (!response.ResponseSuccess)
                    return response;

                //Process storage.
                var session = _storageService.GetSession().Session as DbContext;
                TStorageObject item = _mappingService.Map<TObject, TStorageObject>(request.Item);
                session.Entry<TStorageObject>(item).State = EntityState.Added;
                session.SaveChanges(); //Required to get identity value created from DB (unit of work can rollback if needed)
                response.Item = _mappingService.Map<TObject, TStorageObject>(item);
            }
            catch (Exception ex)
            {                
                try
                {
                    //Try to detach item if can't be saved so other actions can be completed
                    var session = _storageService.GetSession().Session as DbContext;
                    TStorageObject item = _mappingService.Map<TObject, TStorageObject>(request.Item);
                    DetachCachedObject(session, item);
                } //Don't care about this exception because this is a fallback plan
                catch { } 

                Logger.Current.Error<EntityMapStorageRepository<TStorageService, TObject, TDataType, TStorageObject>>(ex);
                response.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
            }
            return response;
        }


        /// <summary>
        /// Get an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<TObject> Get(IRequestItem<TDataType> request)
        {
            ResponseItem<TObject> response = new ResponseItem<TObject>();
            RequestItem<IContext> bizRequest = new RequestItem<IContext>();
            try
            {
                //Process specific repository events. We don't raise the event because we don't want it to throw exceptions.
                bizRequest.Item = _contextBuilder.GetContext();
                bizRequest.Item.Item = new RepositoryGetEvent<TObject, TDataType>() { PK = request.Item };
                var repoResp = _businessRuleService.ExecuteBusinessRules(bizRequest);
                response.CopyResponse(repoResp);
                if (!response.ResponseSuccess)
                    return response;

                //Process storage.
                var session = _storageService.GetSession().Session as DbContext;                
                TStorageObject item = session.Set<TStorageObject>().Find(request.Item);
                response.Item = _mappingService.Map<TObject, TStorageObject>(item);                
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EntityMapStorageRepository<TStorageService, TObject, TDataType, TStorageObject>>(ex);
                response.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
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
            ResponseItem<TObject> response = new ResponseItem<TObject>();
            RequestItem<IContext> bizRequest = new RequestItem<IContext>();
            try
            {
                //Process specific repository events. We don't raise the event because we don't want it to throw exceptions.
                bizRequest.Item = _contextBuilder.GetContext();
                bizRequest.Item.Item = new RepositoryUpdateEvent<TObject>() { Item = request.Item };
                var repoResp = _businessRuleService.ExecuteBusinessRules(bizRequest);
                response.CopyResponse(repoResp);
                if (!response.ResponseSuccess)
                    return response;

                //Process storage.
                var session = _storageService.GetSession().Session as DbContext;
                TStorageObject item = _mappingService.Map<TObject, TStorageObject>(request.Item);
                DetachCachedObject(session, item);                
                session.Set<TStorageObject>().Attach(item);
                session.Entry<TStorageObject>(item).State = EntityState.Modified;
                response.Item = _mappingService.Map<TObject, TStorageObject>(item);
            }
            catch (Exception ex)
            {
                try
                {
                    //Try to detach item if can't be saved so other actions can be completed
                    var session = _storageService.GetSession().Session as DbContext;
                    TStorageObject item = _mappingService.Map<TObject, TStorageObject>(request.Item);
                    DetachCachedObject(session, item);
                } //Don't care about this exception because this is a fallback plan
                catch { }

                Logger.Current.Error<EntityMapStorageRepository<TStorageService, TObject, TDataType, TStorageObject>>(ex);
                response.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
            }
            return response;
        }

        /// <summary>
        /// Fix for EF 6 when attaching an entity to the dbset and properties have been changed, an exception is thrown.
        /// We will try to find the cached entity object in the dbset.Local cache and detach it first prior to attaching.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="entity"></param>
        protected virtual void DetachCachedObject(DbContext session, TStorageObject entity)
        {
            ObjectContext context = ((IObjectContextAdapter)session).ObjectContext;
            ObjectSet<TStorageObject> tempSet = context.CreateObjectSet<TStorageObject>();
            EntityKey entityKey = context.CreateEntityKey(tempSet.EntitySet.Name, entity);
            foreach (TStorageObject t in session.Set<TStorageObject>().Local)
            {
                var tempKey = context.CreateEntityKey(tempSet.EntitySet.Name, t);
                if (tempKey.Equals(entityKey))
                {
                    session.Entry<TStorageObject>(t).State = EntityState.Detached;
                    break;
                }
            }
        }

        /// <summary>
        /// Delete an item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponse Delete(IRequestItem<TDataType> request)
        {
            Response response = new Response();
            RequestItem<IContext> bizRequest = new RequestItem<IContext>();
            try
            {
                //Process specific repository events. We don't raise the event because we don't want it to throw exceptions.
                bizRequest.Item = _contextBuilder.GetContext();
                bizRequest.Item.Item = new RepositoryDeleteEvent<TObject, TDataType>() { PK = request.Item };
                var repoResp = _businessRuleService.ExecuteBusinessRules(bizRequest);
                response.CopyResponse(repoResp);
                if (!response.ResponseSuccess)
                    return response;

                //Process storage.
                var session = _storageService.GetSession().Session as DbContext;
                TStorageObject entity = session.Set<TStorageObject>().Find(request.Item);
                if (entity != null)
                    session.Entry<TStorageObject>(entity).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EntityMapStorageRepository<TStorageService, TObject, TDataType, TStorageObject>>(ex);
                response.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
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
            StorageQueryResponseItems<TObject> response = new StorageQueryResponseItems<TObject>();
            RequestItem<IContext> bizRequest = new RequestItem<IContext>();
            try
            {
                //Process specific repository events. We don't raise the event because we don't want it to throw exceptions.
                bizRequest.Item = _contextBuilder.GetContext();
                IStorageQuery sq = request.Item;
                if (sq == null)
                    sq = new StorageQuery();
                bizRequest.Item.Item = new RepositoryQueryEvent<TObject>() { Item = sq };
                var repoResp = _businessRuleService.ExecuteBusinessRules(bizRequest);
                response.CopyResponse(repoResp);
                if (!response.ResponseSuccess)
                    return response;

                //Process storage.
                var session = _storageService.GetSession().Session as DbContext;                
                IOrderedQueryable<TStorageObject> query = EntityQueryBuilder.Query<TStorageObject>(session.Set<TStorageObject>() as IQueryable<TStorageObject>, sq);
                IList<TStorageObject> pagingList = new List<TStorageObject>();                
                if (sq.PagingNumberPerPage > 0 && sq.PagingNumberPerPage < int.MaxValue)
                {
                    if (sq.PagingStartPage > 0)
                    {
                        int skip = ((sq.PagingStartPage - 1) * sq.PagingNumberPerPage);
                        if (skip > 0)
                            query = query.Skip(skip) as IOrderedQueryable<TStorageObject>;
                        int rowCount = 1;
                        foreach (var pageItem in query)
                        {
                            if (rowCount > sq.PagingNumberPerPage)
                                break;
                            pagingList.Add(pageItem);
                            rowCount++;
                        }
                        response.Items = _mappingService.Map<TObject, TStorageObject>(pagingList);
                        response.PagingTotalCount = query.Count();
                        return response;
                    }
                    response.Items = _mappingService.Map<TObject, TStorageObject>(query.Take(sq.PagingNumberPerPage).ToList());
                    response.PagingTotalCount = query.Count();
                    return response;
                }
                response.Items = _mappingService.Map<TObject, TStorageObject>(query.ToList());
                response.PagingTotalCount = response.Items.Count;
                return response;
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EntityMapStorageRepository<TStorageService, TObject, TDataType, TStorageObject>>(ex);
                response.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
            }
            return response;
        }

        /// <summary>
        /// Query aggregate.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IResponseItem<double> QueryAggregate(IRequestItem<IStorageQuery> request)
        {
            ResponseItem<double> response = new ResponseItem<double>();
            RequestItem<IContext> bizRequest = new RequestItem<IContext>();
            try
            {
                //Process specific repository events. We don't raise the event because we don't want it to throw exceptions.
                bizRequest.Item = _contextBuilder.GetContext();
                bizRequest.Item.Item = new RepositoryQueryAggregateEvent<TObject>() { Item = request.Item };
                var repoResp = _businessRuleService.ExecuteBusinessRules(bizRequest);
                response.CopyResponse(repoResp);
                if (!response.ResponseSuccess)
                    return response;

                //Process storage.
                var session = _storageService.GetSession().Session as DbContext;
                IOrderedQueryable<TStorageObject> query = EntityQueryBuilder.Query<TStorageObject>(session.Set<TStorageObject>() as IQueryable<TStorageObject>, request.Item);
                response.Item = query.Count();
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EntityMapStorageRepository<TStorageService, TObject, TDataType, TStorageObject>>(ex);
                response.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
            }
            return response;
        }

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
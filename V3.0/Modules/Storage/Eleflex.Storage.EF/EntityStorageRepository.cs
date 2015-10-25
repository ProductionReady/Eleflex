using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Eleflex.Storage.EF
{
    /// <summary>
    /// Sql Server Entity Framework repository pattern for an entity.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public abstract class EntityStorageRepository<TStorageService, TObject, TDataType> : IStorageRepository<TObject, TDataType>
        where TStorageService : IStorageService
        where TObject : class        
    {

        /// <summary>
        /// Storage service.
        /// </summary>
        protected TStorageService _storageService;
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
        /// <param name="storageService"></param>
        protected EntityStorageRepository(
            IBusinessRuleService businessRuleService,
            IContextBuilder contextBuilder,
            TStorageService storageService)
        {
            _businessRuleService = businessRuleService;
            _contextBuilder = contextBuilder;
            _storageService = storageService;
        }

        /// <summary>
        /// Insert an entity.
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
                session.Entry<TObject>(request.Item).State = EntityState.Added;
                session.SaveChanges(); //Required to get identity value created from DB (unit of work can rollback if needed)
                response.Item = request.Item;
            }
            catch (Exception ex)
            {
                try
                {
                    //Try to detach item if can't be saved so other actions can be completed
                    var session = _storageService.GetSession().Session as DbContext;
                    DetachCachedObject(session, request.Item);
                } //Don't care about this exception because this is a fallback plan
                catch { }

                Logger.Current.Error<EntityStorageRepository<TStorageService, TObject, TDataType>>(ex);
                response.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
            }
            return response;
        }


        /// <summary>
        /// Get an entity
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
                TObject item = session.Set<TObject>().Find(request.Item);
                response.Item = item;
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EntityStorageRepository<TStorageService, TObject, TDataType>>(ex);
                response.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
            }
            return response;
        }

        /// <summary>
        /// Update an entity.
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
                DetachCachedObject(session, request.Item);
                session.Set<TObject>().Attach(request.Item);
                session.Entry<TObject>(request.Item).State = EntityState.Modified;
                response.Item = request.Item;
            }
            catch (Exception ex)
            {
                try
                {
                    //Try to detach item if can't be saved so other actions can be completed
                    var session = _storageService.GetSession().Session as DbContext;                    
                    DetachCachedObject(session, request.Item);
                } //Don't care about this exception because this is a fallback plan
                catch { }

                Logger.Current.Error<EntityStorageRepository<TStorageService, TObject, TDataType>>(ex);
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
        protected virtual void DetachCachedObject(DbContext session, TObject entity)
        {
            ObjectContext context = ((IObjectContextAdapter)session).ObjectContext;
            ObjectSet<TObject> tempSet = context.CreateObjectSet<TObject>();
            EntityKey entityKey = context.CreateEntityKey(tempSet.EntitySet.Name, entity);
            foreach (TObject t in session.Set<TObject>().Local)
            {
                var tempKey = context.CreateEntityKey(tempSet.EntitySet.Name, t);
                if (tempKey.Equals(entityKey))
                {
                    session.Entry<TObject>(t).State = EntityState.Detached;
                    break;
                }
            }
        }

        /// <summary>
        /// Delete an entity.
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
                TObject entity = session.Set<TObject>().Find(request.Item);
                if (entity != null)
                    session.Entry<TObject>(entity).State = EntityState.Deleted;
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EntityStorageRepository<TStorageService, TObject, TDataType>>(ex);
                response.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
            }
            return response;
        }

        /// <summary>
        /// Query entities.
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
                IOrderedQueryable<TObject> query = EntityQueryBuilder.Query<TObject>(session.Set<TObject>() as IQueryable<TObject>, sq);
                List<TObject> pagingList = new List<TObject>();
                if (sq.PagingNumberPerPage > 0 && sq.PagingNumberPerPage < int.MaxValue)
                {
                    if (sq.PagingStartPage > 0)
                    {
                        int skip = ((sq.PagingStartPage - 1) * sq.PagingNumberPerPage);
                        if (skip > 0)
                            query = query.Skip(skip) as IOrderedQueryable<TObject>;
                        int rowCount = 1;
                        foreach (var pageItem in query)
                        {
                            if (rowCount > sq.PagingNumberPerPage)
                                break;
                            pagingList.Add(pageItem);
                            rowCount++;
                        }
                        response.Items = pagingList;
                        response.PagingTotalCount = query.Count();
                        return response;                        
                    }
                    response.Items = query.Take(sq.PagingNumberPerPage).ToList();
                    response.PagingTotalCount = query.Count();
                    return response;
                }
                response.Items = query.ToList();
                response.PagingTotalCount = response.Items.Count;
                return response;
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EntityStorageRepository<TStorageService, TObject, TDataType>>(ex);
                response.AddMessage(true, MessageConstants.ERROR_STORAGE_CODE, MessageConstants.ERROR_STORAGE_TEXT);
            }
            return response;
        }

        /// <summary>
        /// Query Aggregate entities.
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
                IOrderedQueryable<TObject> query = EntityQueryBuilder.Query<TObject>(session.Set<TObject>() as IQueryable<TObject>, request.Item);
                response.Item = query.Count();
            }
            catch (Exception ex)
            {
                Logger.Current.Error<EntityStorageRepository<TStorageService, TObject, TDataType>>(ex);
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
#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//Copyright © 2015 Production Ready, LLC. All Rights Reserved.
//For more information, visit http://www.ProductionReady.com
//This file is part of PRODUCTION READY® ELEFLEX®.
//
//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU Affero General Public License as
//published by the Free Software Foundation, either version 3 of the
//License, or (at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU Affero General Public License for more details.
//
//You should have received a copy of the GNU Affero General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using Eleflex.Storage;
using LinqKit;

namespace Eleflex.Storage.EntityFramework
{
    /// <summary>
    /// Sql Server Entity Framework repository pattern for an entity.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public abstract class EntityStorageRepository<TDomain, TEntity, TStorageProvider, TDataType> : IStorageRepository<TDomain, TDataType>
        where TDomain : class
        where TEntity : class
        where TStorageProvider : IStorageProvider        
    {
        protected readonly TStorageProvider _storageProvider;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="storageProvider"></param>
        protected EntityStorageRepository(TStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        /// <summary>
        /// Insert an entity.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        public virtual TDomain Insert(TDomain domain)
        {
            try
            { 
            var session = _storageProvider.GetSession().Session as DbContext;
            TEntity entity = AutoMapper.Mapper.Map<TEntity>(domain);
            session.Entry<TEntity>(entity).State = EntityState.Added;
            session.SaveChanges(); //Required to get identity value created from DB (unit of work can rollback if needed)
            return AutoMapper.Mapper.Map<TDomain>(entity);
            }
            catch(Exception ex)
            {
                string a = ex.ToString();
                throw;
            }

        }

        /// <summary>
        /// GetStorageProvider an entity by it's key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TDomain Get(TDataType id)
        {
            var session = _storageProvider.GetSession().Session as DbContext;
            TEntity entity = session.Set<TEntity>().Find(id);            
            return AutoMapper.Mapper.Map<TDomain>(entity);
        }

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public virtual TDomain Update(TDomain domain)
        {
            var session = _storageProvider.GetSession().Session as DbContext;
            TEntity entity = AutoMapper.Mapper.Map<TEntity>(domain);
            DetachCachedObject(session, entity);
            session.Set<TEntity>().Attach(entity);
            session.Entry<TEntity>(entity).State = EntityState.Modified;
            return domain;
        }

        /// <summary>
        /// Fix for EF 6 when attaching an entity to the dbset and properties have been changed, an exception is thrown.
        /// We will try to find the cached entity object in the dbset.Local cache and detach it first prior to attaching.
        /// </summary>
        /// <param name="session"></param>
        /// <param name="entity"></param>
        protected virtual void DetachCachedObject(DbContext session, TEntity entity)
        {
            ObjectContext context = ((IObjectContextAdapter)session).ObjectContext;
            ObjectSet<TEntity> tempSet = context.CreateObjectSet<TEntity>();            
            EntityKey entityKey = context.CreateEntityKey(tempSet.EntitySet.Name, entity);
            foreach (TEntity t in session.Set<TEntity>().Local)
            {
                var tempKey = context.CreateEntityKey(tempSet.EntitySet.Name, t);
                if (tempKey.Equals(entityKey))
                {
                    session.Entry<TEntity>(t).State = EntityState.Detached;
                    break;
                }
            }            
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="key"></param>
        public virtual void Delete(TDataType key)
        {
            var session = _storageProvider.GetSession().Session as DbContext;
            TEntity entity = session.Set<TEntity>().Find(key);
            if (entity != null)
                session.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// Query entities.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public virtual IList<TDomain> Query(IStorageQuery storageQuery)
        {
            var session = _storageProvider.GetSession().Session as DbContext;
            IOrderedQueryable<TEntity> query = EntityQueryBuilder.Query<TEntity>(session.Set<TEntity>() as IQueryable<TEntity>,storageQuery);
            List<TEntity> pagingList = new List<TEntity>();
            if (storageQuery.NumberPerPage > 0 && storageQuery.NumberPerPage < int.MaxValue)
            {
                if(storageQuery.StartPage>0)
                {                    
                    int skip = ((storageQuery.StartPage - 1) * storageQuery.NumberPerPage);
                    if(skip>0)
                        query = query.Skip(skip) as IOrderedQueryable<TEntity>;
                    int rowCount = 1;
                    foreach(var pageItem in query)
                    {
                        if (rowCount > storageQuery.NumberPerPage)
                            break;
                        pagingList.Add(pageItem);
                        rowCount++;
                    }
                    return AutoMapper.Mapper.Map<List<TDomain>>(pagingList);
                }
                return AutoMapper.Mapper.Map<List<TDomain>>(query.Take(storageQuery.NumberPerPage).ToList());
            }
            return AutoMapper.Mapper.Map<List<TDomain>>(query.ToList());
        }

        /// <summary>
        /// Query entities.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public virtual double QueryAggregate(IStorageQuery storageQuery)
        {
            var session = _storageProvider.GetSession().Session as DbContext;
            IOrderedQueryable<TEntity> query = EntityQueryBuilder.Query<TEntity>(session.Set<TEntity>() as IQueryable<TEntity>, storageQuery);
            return query.Count();
        }

        /// <summary>
        /// Commit.
        /// </summary>
        public void Commit()
        {
            _storageProvider.Commit();
        }

        /// <summary>
        /// Rollback.
        /// </summary>
        public void Rollback()
        {
            _storageProvider.Rollback();
        }
    }
}
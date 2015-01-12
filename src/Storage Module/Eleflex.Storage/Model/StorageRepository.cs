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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Storage
{
    /// <summary>
    /// Abstract base class for storage repositories.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public abstract class StorageRepository<TEntity, TId> : IStorageRepository<TEntity, TId>
        where TEntity : class
    {
        /// <summary>
        /// Internal repository.
        /// </summary>
        protected readonly IStorageRepository<TEntity, TId> _repository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="repository"></param>
        public StorageRepository(IStorageRepository<TEntity, TId> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Insert.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Insert(TEntity entity)
        {
            return _repository.Insert(entity);
        }

        /// <summary>
        /// Get.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity Get(TId id)
        {
            return _repository.Get(id);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Update(TEntity entity)
        {
            return _repository.Update(entity);
        }

        /// <summary>
        /// Delete.
        /// </summary>
        /// <param name="id"></param>
        public virtual void Delete(TId id)
        {
            _repository.Delete(id);
        }

        /// <summary>
        /// Query entities.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public virtual IList<TEntity> Query(IStorageQuery storageQuery)
        {
            return _repository.Query(storageQuery);
        }

        /// <summary>
        /// Query entities for an aggregate.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public virtual double QueryAggregate(IStorageQuery storageQuery)
        {
            return _repository.QueryAggregate(storageQuery);
        }

        /// <summary>
        /// Commit.
        /// </summary>
        public void Commit()
        {
            _repository.Commit();
        }

        /// <summary>
        /// Rollback.
        /// </summary>
        public void Rollback()
        {
            _repository.Rollback();
        }
    }
}

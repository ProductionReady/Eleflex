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
using Eleflex;

namespace Eleflex.Storage
{
    /// <summary>
    /// Interface defining a repository pattern for seperation between domain and storage provider. Will throw exceptions.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public interface IStorageRepository<TEntity, in TId> : IUnitOfWork where TEntity : class
    {
        /// <summary>
        /// Insert an entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);

        /// <summary>
        /// Get an entity by its key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(TId id);

        /// <summary>
        /// Update an entity. 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="id"></param>
        void Delete(TId id);

        /// <summary>
        /// Query entities.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        IList<TEntity> Query(IStorageQuery storageQuery);

        /// <summary>
        /// Query entities for an aggregate.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        double QueryAggregate(IStorageQuery storageQuery);
    }
}
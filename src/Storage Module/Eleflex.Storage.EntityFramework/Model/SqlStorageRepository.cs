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
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Eleflex.Storage;

namespace Eleflex.Storage.EntityFramework
{
    /// <summary>
    /// Sql Server generic sql repository pattern for an entity.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public abstract class SqlStorageRepository<TDomain, TStorageProvider, TDataType> : IStorageRepository<TDomain, TDataType>
        where TDomain : class
        where TStorageProvider : IStorageProvider
    {
        protected readonly TStorageProvider _storageProvider;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="storageProvider"></param>
        protected SqlStorageRepository(TStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        /// <summary>
        /// Insert an entity.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="commit"></param>
        /// <returns></returns>
        public abstract TDomain Insert(TDomain domain);

        /// <summary>
        /// GetStorageProvider an entity by it's key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public abstract TDomain Get(TDataType id);

        /// <summary>
        /// Update an entity.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public abstract TDomain Update(TDomain domain);

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="key"></param>
        public abstract void Delete(TDataType key);

        /// <summary>
        /// Query entities.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public abstract IList<TDomain> Query(IStorageQuery storageQuery);

        /// <summary>
        /// Query entities for an aggregate.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        public abstract double QueryAggregate(IStorageQuery storageQuery);

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
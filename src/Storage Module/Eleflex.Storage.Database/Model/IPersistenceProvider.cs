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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines the methods a supported provider must implement to be used by the framework.
    /// </summary>
    public partial interface IPersistenceProvider : IDisposable
    {        

        /// <summary>
        /// Execute an update statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IPersistenceResponseItem<IEleflexPersistenceObject> Update(IPersistenceRequest request);

        /// <summary>
        /// Execute an insert statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IPersistenceResponseItem<IEleflexPersistenceObject> Insert(IPersistenceRequest request);

        /// <summary>
        /// Execute a get statement to return one item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IPersistenceResponseItem<IEleflexPersistenceObject> GetItem(IPersistenceRequest request);

        /// <summary>
        /// Execute a get statement to return a list of items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IPersistenceResponseItems<IEleflexPersistenceObject> GetItems(IPersistenceRequest request);

        /// <summary>
        /// Execute a statement to return an aggregated number.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IPersistenceResponseItem<double> GetAggregate(IPersistenceRequest request);

        /// <summary>
        /// Execute a delete statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IPersistenceResponseItem<int> Delete(IPersistenceRequest request);

        /// <summary>
        /// Execute a bulk update statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IPersistenceResponseItem<int> BulkUpdate(IPersistenceRequest request);

        /// <summary>
        /// Execute a bulk delete statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IPersistenceResponseItem<int> BulkDelete(IPersistenceRequest request);

        /// <summary>
        /// Begin a transaction.
        /// </summary>
        /// <returns></returns>
        IPersistenceTransaction BeginTransaction();

    }
}

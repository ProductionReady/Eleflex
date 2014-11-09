#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
//Copyright © 2014 Production Ready, LLC. All Rights Reserved.
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
    /// Maintains the overall state of work being processed over the request lifetime and manages storage providers.
    /// </summary>
    public interface IStorageProviderUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// GetStorageProvider the list of storage providers managed by this unit of work manager.
        /// </summary>
        List<IStorageProvider> StorageProviders { get; set; }

        /// <summary>
        /// StartStorageProvider managing a storage provider
        /// </summary>
        /// <param name="storageProvider"></param>
        void StartStorageProvider(IStorageProvider storageProvider);

        /// <summary>
        /// GetStorageProvider a manager storage provider if exists.
        /// </summary>
        /// <param name="storageProviderKey"></param>
        /// <returns></returns>
        IStorageProvider GetStorageProvider(string storageProviderKey);

        /// <summary>
        /// Commit changes for a specific session provider.
        /// </summary>
        /// <param name="storageProviderKey"></param>
        /// <param name="sessionKey"></param>
        void Commit(string storageProviderKey, Guid sessionKey);

    }
}

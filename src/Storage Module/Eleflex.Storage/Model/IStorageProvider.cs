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
    /// Interface for a storage mechanism provider.
    /// </summary>
    public interface IStorageProvider : IDisposable
    {
        /// <summary>
        /// GetStorageProvider the storage provider key. This is usually the module name to find the correct storage provider.
        /// </summary>
        string StorageProviderKey { get; set; }

        /// <summary>
        /// GetStorageProvider the list of storage sessions managed by this provider.
        /// </summary>
        List<IStorageSession> Sessions { get; set; }

        /// <summary>
        /// GetStorageProvider the current session from the unit of work if it exists, otherwise create a new session.
        /// </summary>
        /// <returns></returns>
        IStorageSession GetSession();

        /// <summary>
        /// GetStorageProvider the specific session from the unit of work if it exists, otherwise return null.
        /// </summary>
        /// <returns></returns>
        IStorageSession GetSession(Guid sessionKey);

        /// <summary>
        /// Create a new session and manage it's lifetime.
        /// </summary>
        /// <returns></returns>
        IStorageSession CreateSession();

        /// <summary>
        /// Create a new session from the underlying storage provider but don't manage the session. UnitofWork commits and rollbacks do not affect this session and must be handled yourself.
        /// </summary>
        /// <returns></returns>
        IStorageSession CreateNonManagedSession();

        /// <summary>
        /// Get the connection string key for the provider.
        /// </summary>
        string ConnectionStringKey { get; }

        /// <summary>
        /// Get the connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Get the connection string tailored to provider's requirements.
        /// </summary>
        string ProviderConnectionString { get; }

        /// <summary>
        /// Rollback changes for all session providers.
        /// </summary>
        void Rollback();

        /// <summary>
        /// Rollback changes for a specific session provider.
        /// </summary>
        void Rollback(Guid sessionKey);

        /// <summary>
        /// Commit changes for all session providers.
        /// </summary>
        void Commit();

        /// <summary>
        /// Commit changes for a specific session provider.
        /// </summary>
        void Commit(Guid sessionKey);
    }
}

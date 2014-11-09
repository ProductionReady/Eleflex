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
using System.Configuration;
using System.Data.Entity;
using Eleflex;
using Eleflex.Storage;
using Eleflex.Storage.EntityFramework;

namespace Eleflex.Storage.EntityFramework
{
    /// <summary>
    /// Provides storage provider base class for generic sql connection and utility functions.
    /// </summary>
    public abstract class SqlStorageProvider : StorageProvider
    {
        private readonly IStorageProviderUnitOfWork _storageProviderUnitOfWork;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="providerKey"></param>
        /// <param name="connectionStringKey"></param>
        /// <param name="storageProviderUnitOfWork"></param>
        public SqlStorageProvider(string providerKey, string connectionStringKey, IStorageProviderUnitOfWork storageProviderUnitOfWork)
            : base(providerKey, connectionStringKey)
        {
            _storageProviderUnitOfWork = storageProviderUnitOfWork;
            IStorageProvider provider = _storageProviderUnitOfWork.GetStorageProvider(providerKey);
            if (provider == null)
            {
                _storageProviderUnitOfWork.StartStorageProvider(this);
                SessionOwner = true;
            }
            else
            {
                this.Sessions = provider.Sessions; //Get the session list from UOW master provider.
                SessionOwner = false;
            }
        }

        /// <summary>
        /// Get the connection string from the app/web.config file
        /// </summary>
        public override string ProviderConnectionString
        {
            get
            {
                return ConnectionString;
            }
        }

    }
}

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

namespace Eleflex.Storage
{
    /// <summary>
    /// Maintains the overall state of work being processed over the request lifetime and manages storage providers.
    /// </summary>
    public class StorageProviderUnitOfWork : IStorageProviderUnitOfWork
    {        
        /// <summary>
        /// Constructor.
        /// </summary>
        public StorageProviderUnitOfWork()
        {            
            StorageProviders = new List<IStorageProvider>();
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public virtual void Dispose()
        {
            foreach (IStorageProvider item in StorageProviders)
                item.Dispose();
            StorageProviders.Clear();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// List of managed storage providers.
        /// </summary>
        public List<IStorageProvider> StorageProviders { get; set; }

        /// <summary>
        /// StartStorageProvider managing a storage provider.
        /// </summary>
        /// <param name="storageProvider"></param>
        public void StartStorageProvider(IStorageProvider storageProvider)
        {
            StorageProviders.Add(storageProvider);
        }

        /// <summary>
        /// GetStorageProvider a manager storage provider if exists.
        /// </summary>
        /// <param name="storageProviderKey"></param>
        /// <returns></returns>
        public virtual IStorageProvider GetStorageProvider(string storageProviderKey)
        {
            return StorageProviders.Where(x => x.StorageProviderKey == storageProviderKey).FirstOrDefault();
        }

        /// <summary>
        /// Rollback a storage provider or a specific session in a storage provider.
        /// </summary>
        /// <param name="storageProviderKey"></param>
        /// <param name="sessionKey"></param>
        public virtual void Rollback(string storageProviderKey, Guid sessionKey = default(Guid))
        {
            for (int i = 0; i < StorageProviders.Count; i++)
            {
                if (StorageProviders[i].StorageProviderKey == storageProviderKey)
                {
                    if(sessionKey == Guid.Empty)
                        StorageProviders[i].Rollback();
                    else
                        StorageProviders[i].Rollback(sessionKey);
                    break;
                }
            }
        }

        /// <summary>
        /// Rollback all changes.
        /// </summary>
        public virtual void Rollback()
        {
            foreach (IStorageProvider item in StorageProviders)
            {
                item.Rollback();
            }
        }

        /// <summary>
        /// Commit a storage provider or a specific session in a storage provider.
        /// </summary>
        /// <param name="storageProviderKey"></param>
        /// <param name="sessionKey"></param>
        public virtual void Commit(string storageProviderKey, Guid sessionKey = default(Guid))
        {
            for (int i = 0; i < StorageProviders.Count; i++)
            {
                if (StorageProviders[i].StorageProviderKey == storageProviderKey)
                {
                    if (sessionKey == Guid.Empty)
                        StorageProviders[i].Commit();
                    else
                        StorageProviders[i].Commit(sessionKey);
                    break;
                }
            }
        }

        /// <summary>
        /// Commit all changes.
        /// </summary>
        public virtual void Commit()
        {
            foreach (IStorageProvider item in StorageProviders)
            {
                item.Commit();                
            }
        }
    }
}

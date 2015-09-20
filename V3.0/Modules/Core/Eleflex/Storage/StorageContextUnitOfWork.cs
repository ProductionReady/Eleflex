using System;
using System.Collections.Generic;
using System.Linq;

namespace Eleflex
{
    /// <summary>
    /// Represents an object that maintains the overall state of work being processed over the request lifetime and manages storage services.
    /// </summary>
    public partial class StorageContextUnitOfWork : IStorageContextUnitOfWork
    {        

        /// <summary>
        /// Constructor.
        /// </summary>
        public StorageContextUnitOfWork()
        {            
            StorageServices = new List<IStorageService>();
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public virtual void Dispose()
        {
            foreach (IStorageService item in StorageServices)
                item.Dispose();
            StorageServices.Clear();
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Collection of managed storage services.
        /// </summary>
        public virtual List<IStorageService> StorageServices { get; set; }

        /// <summary>
        /// Register a storage service.
        /// </summary>
        /// <param name="storageService"></param>
        public virtual void RegisterStorageService(IStorageService storageService)
        {
            StorageServices.Add(storageService);
        }

        /// <summary>
        /// Get a storage service.
        /// </summary>
        /// <param name="storageServiceKey"></param>
        /// <returns></returns>
        public virtual IStorageService GetStorageService(string storageServiceKey)
        {
            return StorageServices.Where(x => x.StorageServiceKey == storageServiceKey).FirstOrDefault();
        }

        /// <summary>
        /// Rollback a storage service or a specific session in a storage provider.
        /// </summary>
        /// <param name="storageServiceKey"></param>
        /// <param name="sessionKey"></param>
        public virtual void Rollback(string storageServiceKey, string sessionKey = null)
        {
            for (int i = 0; i < StorageServices.Count; i++)
            {
                if (StorageServices[i].StorageServiceKey == storageServiceKey)
                {
                    if (string.IsNullOrEmpty(sessionKey))
                        StorageServices[i].Rollback();
                    else
                        StorageServices[i].RollbackSession(sessionKey);
                    break;
                }
            }
        }

        /// <summary>
        /// Rollback all changes.
        /// </summary>
        public virtual void Rollback()
        {
            foreach (IStorageService item in StorageServices)
            {
                item.Rollback();
            }
        }

        /// <summary>
        /// Commit a storage provider or a specific session in a storage provider.
        /// </summary>
        /// <param name="storageServiceKey"></param>
        /// <param name="sessionKey"></param>
        public virtual void Commit(string storageServiceKey, string sessionKey = null)
        {
            for (int i = 0; i < StorageServices.Count; i++)
            {
                if (StorageServices[i].StorageServiceKey == storageServiceKey)
                {                    
                    if ( string.IsNullOrEmpty(sessionKey))
                        StorageServices[i].Commit();
                    else
                        StorageServices[i].CommitSession(sessionKey);
                    break;
                }
            }
        }

        /// <summary>
        /// Commit a storage provider or a specific session in a storage provider.
        /// </summary>
        /// <param name="storageServiceKey"></param>
        /// <param name="sessionKey"></param>
        public virtual void Commit(string storageServiceKey)
        {
            for (int i = 0; i < StorageServices.Count; i++)
            {
                if (StorageServices[i].StorageServiceKey == storageServiceKey)
                {
                    StorageServices[i].Commit();
                    break;
                }
            }
        }

        /// <summary>
        /// Commit all changes.
        /// </summary>
        public virtual void Commit()
        {
            foreach (IStorageService item in StorageServices)
            {
                item.Commit();                
            }
        }

    }
}

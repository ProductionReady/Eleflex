using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object that maintains overall transaction state of a storage service.
    /// </summary>
    public partial interface IStorageContextUnitOfWork : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// Get the list of storage services managed by this unit of work manager.
        /// </summary>
        List<IStorageService> StorageServices { get; set; }

        /// <summary>
        /// Register a storage service.
        /// </summary>
        /// <param name="storageService"></param>
        void RegisterStorageService(IStorageService storageService);

        /// <summary>
        /// Get a storage service if exists during this context.
        /// </summary>
        /// <param name="storageServiceKey"></param>
        /// <returns></returns>
        IStorageService GetStorageService(string storageServiceKey);
        
    }
}

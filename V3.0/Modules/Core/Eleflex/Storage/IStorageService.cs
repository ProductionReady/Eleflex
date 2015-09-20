using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a storage mechanism service.
    /// </summary>
    public partial interface IStorageService : IStorageServiceSessionManagement, IUnitOfWork, IDisposable
    {
        /// <summary>
        /// Get the storage service key.
        /// </summary>
        string StorageServiceKey { get; set; }

        /// <summary>
        /// Get the storage configuration key used for versioning.
        /// </summary>
        string VersioningStorageConfig { get; set; }
    }
}

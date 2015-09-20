using System;

namespace Eleflex
{
    /// <summary>
    /// Represents a storage service's session connection to the underlying data store.
    /// </summary>
    public interface IStorageServiceSession : IUnitOfWork, IDisposable
    {
        /// <summary>
        /// Unique ID to distinguish this session from others.
        /// </summary>
        string SessionKey { get; }

        /// <summary>
        /// Determine if the session and transaction are currently alive.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// The provider-specific session implementation.
        /// </summary>
        object Session { get; }

        /// <summary>
        /// The transaction.
        /// </summary>
        object Transaction { get; set; }
    }
}

using System;

namespace Eleflex
{
    /// <summary>
    /// Abstract class for a storage session.
    /// </summary>
    public abstract partial class StorageServiceSession : IStorageServiceSession
    {

        /// <summary>
        /// Constructor.
        /// </summary>        
        public StorageServiceSession()
        {
            SessionKey = Guid.NewGuid().ToString();
            IsActive = true;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Determine if session and transaction are active.
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Unique ID to distinguish this session from others.
        /// </summary>
        public virtual string SessionKey { get; set; }

        /// <summary>
        /// The provider-specific session implementation.
        /// </summary>
        public virtual object Session { get; set; }

        /// <summary>
        /// The transaction.
        /// </summary>
        public virtual object Transaction { get; set; }

        /// <summary>
        /// Commit the changes.
        /// </summary>
        public abstract void Commit();

        /// <summary>
        /// Rollback the changes.
        /// </summary>
        public abstract void Rollback();

    }
}

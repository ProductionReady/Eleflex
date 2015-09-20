using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object that provides session management for a storage service.
    /// </summary>
    public partial interface IStorageServiceSessionManagement : IUnitOfWork
    {
        /// <summary>
        /// Determine if this service owns the session list (or is just using the reference)
        /// </summary>
        bool SessionOwner { get; set; }

        /// <summary>
        /// The list of storage sessions managed by this service.
        /// </summary>
        List<IStorageServiceSession> Sessions { get; set; }

        /// <summary>
        /// The current session from the unit of work if it exists, otherwise create a new session.
        /// </summary>
        /// <returns></returns>
        IStorageServiceSession GetSession();

        /// <summary>
        /// The specific session from the unit of work if it exists, otherwise return null.
        /// </summary>
        /// <returns></returns>
        IStorageServiceSession GetSession(string sessionKey);

        /// <summary>
        /// Create a new session and manage it's lifetime.
        /// </summary>
        /// <returns></returns>
        IStorageServiceSession CreateSession();

        /// <summary>
        /// Create a new session from the underlying storage service but don't manage the session. UnitofWork commits and rollbacks do not affect this session and must be handled yourself.
        /// </summary>
        /// <returns></returns>
        IStorageServiceSession CreateNonManagedSession();

        /// <summary>
        /// Rollback changes for a specific session.
        /// </summary>
        void RollbackSession(string sessionKey);

        /// <summary>
        /// Commit changes for a specific session.
        /// </summary>
        void CommitSession(string sessionKey);
    }
}

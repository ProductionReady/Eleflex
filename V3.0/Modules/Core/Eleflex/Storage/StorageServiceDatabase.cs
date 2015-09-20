using System;
using System.Collections.Generic;
using System.Linq;

namespace Eleflex
{
    /// <summary>
    /// Abstract class for a database-driven storage service.
    /// </summary>
    public abstract partial class StorageServiceDatabase : IStorageServiceDatabase
    {
        
        /// <summary>
        /// Constructor.
        /// </summary>">
        public StorageServiceDatabase()
        {            
            SessionOwner = true;
            Sessions = new List<IStorageServiceSession>();
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public virtual void Dispose()
        {
            if (Sessions != null)
            {
                if (SessionOwner)
                {
                    foreach (IStorageServiceSession session in Sessions)
                        session.Dispose();
                    Sessions.Clear();
                }
                else
                    Sessions = null;
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Get the storage service key.
        /// </summary>
        public virtual string StorageServiceKey { get; set; }

        /// <summary>
        /// Get the storage configuration key used for versioning.
        /// </summary>
        public virtual string VersioningStorageConfig { get; set; }

        /// <summary>
        /// Get the list of storage sessions managed by this service.
        /// </summary>
        public virtual List<IStorageServiceSession> Sessions { get; set; }

        /// <summary>
        /// Determine if this service owns the session list (or is just using the reference)
        /// </summary>
        public virtual bool SessionOwner { get; set; }

        /// <summary>
        /// Get a connection string from the app/web.config file.
        /// </summary>
        public virtual string SimpleConnectionString { get; set; }

        /// <summary>
        /// Get the provider specific connection string.
        /// </summary>
        public virtual string ProviderConnectionString { get; set; }


        /// <summary>
        /// Create a session not managed by the internal storage service.
        /// </summary>
        /// <returns></returns>
        public abstract IStorageServiceSession CreateNonManagedSession();

        /// <summary>
        /// Create a session managed by the storage service.
        /// </summary>
        /// <returns></returns>
        public virtual IStorageServiceSession CreateSession()
        {
            IStorageServiceSession session = CreateNonManagedSession();
            Sessions.Add(session);
            return session;
        }

        /// <summary>
        /// Get the current session from the unit of work if it exists, otherwise create a new session.
        /// The first session created will become the default session for the storage service and will be shared with
        /// others processing the request (the first item in the list).
        /// </summary>
        /// <returns></returns>
        public virtual IStorageServiceSession GetSession()
        {
            if (Sessions.Count > 0)
            {
                //May have called commit/rollback/dispose on session object itself, rather than through the provider (weren't notified)
                if (!Sessions[0].IsActive)
                {
                    Sessions[0].Dispose();
                    Sessions[0] = CreateNonManagedSession();
                }
                return Sessions[0];
            }
            return CreateSession();
        }

        /// <summary>
        /// Get the specific session from the unit of work if it exists, otherwise return null.
        /// </summary>
        /// <returns></returns>
        public virtual IStorageServiceSession GetSession(string sessionKey)
        {
            return Sessions.Where(x => x.SessionKey == sessionKey && x.IsActive == true).FirstOrDefault();
        }

        /// <summary>
        /// Rollback changes for all session providers.
        /// </summary>
        public virtual void Rollback()
        {
            foreach (IStorageServiceSession session in Sessions)
            {
                if(session.IsActive)
                    session.Rollback();
                session.Dispose();
            }
            Sessions.Clear();
        }

        /// <summary>
        /// Rollback changes for a specific session service.
        /// </summary>
        public virtual void RollbackSession(string sessionKey)
        {
            for (int i = 0; i < Sessions.Count; i++)
            {
                if (Sessions[i].SessionKey == sessionKey)
                {
                    if(Sessions[i].IsActive)
                        Sessions[i].Rollback();
                    Sessions[i].Dispose();
                    Sessions.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// Commit changes for all session services.
        /// </summary>
        public virtual void Commit()
        {
            foreach (IStorageServiceSession session in Sessions)
            {
                if(session.IsActive)
                    session.Commit();
                session.Dispose();
            }
            Sessions.Clear();            
        }

        /// <summary>
        /// Commit changes for a specific session service.
        /// </summary>
        public virtual void CommitSession(string sessionKey)
        {
            for (int i = 0; i < Sessions.Count; i++)
            {
                if (Sessions[i].SessionKey == sessionKey)
                {
                    if(Sessions[i].IsActive)
                        Sessions[i].Commit();
                    Sessions[i].Dispose();
                    Sessions.RemoveAt(i);
                    break;
                }
            }
        }

    }
}

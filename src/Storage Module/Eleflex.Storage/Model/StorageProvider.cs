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
using System.Configuration;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Eleflex.Storage;

namespace Eleflex.Storage
{
    /// <summary>
    /// Abstract base class for a storage provider.
    /// </summary>
    public abstract class StorageProvider : IStorageProvider
    {

        /// <summary>
        /// The default key for the default eleflex database.
        /// </summary>
        public const string ELEFLEX_DEFAULT_CONNECTION_STRING_KEY = "EleflexDefault";

        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="storageProviderKey"></param>
        /// <param name="connectionStringKey"></param>
        public StorageProvider(string storageProviderKey, string connectionStringKey)
        {
            StorageProviderKey = storageProviderKey;
            ConnectionStringKey = connectionStringKey;
            Sessions = new List<IStorageSession>();
            SessionOwner = true;
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
                    foreach (IStorageSession session in Sessions)
                        session.Dispose();
                    Sessions.Clear();
                }
                else
                    Sessions = null;
            }
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// GetStorageProvider the storage provider key.
        /// </summary>
        public virtual string StorageProviderKey { get; set; }

        /// <summary>
        /// GetStorageProvider the list of storage sessions managed by this provider.
        /// </summary>
        public virtual List<IStorageSession> Sessions { get; set; }

        /// <summary>
        /// Determine if this provider owns the session list (or is just using the reference)
        /// </summary>
        public virtual bool SessionOwner { get; set; }

        /// <summary>
        /// Get a connection string from the app/web.config file.
        /// </summary>
        public virtual string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString; }
        }

        /// <summary>
        /// Get the connection string key for the app/web.config file.
        /// </summary>
        public virtual string ConnectionStringKey { get; set; }

        /// <summary>
        /// Get the provider specific connection string.
        /// </summary>
        public virtual string ProviderConnectionString { get; set; }
        

        /// <summary>
        /// Create a session not managed by the internal storage provider
        /// </summary>
        /// <returns></returns>
        public abstract IStorageSession CreateNonManagedSession();

        /// <summary>
        /// Create a session managed by the storage provider.
        /// </summary>
        /// <returns></returns>
        public virtual IStorageSession CreateSession()
        {
            IStorageSession session = CreateNonManagedSession();
            Sessions.Add(session);
            return session;
        }

        /// <summary>
        /// GetStorageProvider the current session from the unit of work if it exists, otherwise create a new session.
        /// The first session created will become the default session for the storage provider and will be shared with
        /// others processing the request (the first item in the list).
        /// </summary>
        /// <returns></returns>
        public virtual IStorageSession GetSession()
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
        /// GetStorageProvider the specific session from the unit of work if it exists, otherwise return null.
        /// </summary>
        /// <returns></returns>
        public virtual IStorageSession GetSession(Guid sessionKey)
        {
            return Sessions.Where(x => x.SessionKey == sessionKey && x.IsActive == true).FirstOrDefault();
        }

        /// <summary>
        /// Rollback changes for all session providers.
        /// </summary>
        public virtual void Rollback()
        {
            foreach (IStorageSession session in Sessions)
            {
                if(session.IsActive)
                    session.Rollback();
                session.Dispose();
            }
            Sessions.Clear();
        }

        /// <summary>
        /// Rollback changes for a specific session provider.
        /// </summary>
        public virtual void Rollback(Guid sessionKey)
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
        /// Commit changes for all session providers.
        /// </summary>
        public virtual void Commit()
        {
            foreach (IStorageSession session in Sessions)
            {
                if(session.IsActive)
                    session.Commit();
                session.Dispose();
            }
            Sessions.Clear();            
        }

        /// <summary>
        /// Commit changes for a specific session provider.
        /// </summary>
        public virtual void Commit(Guid sessionKey)
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

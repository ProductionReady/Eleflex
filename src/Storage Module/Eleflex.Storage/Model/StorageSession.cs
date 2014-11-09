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

namespace Eleflex.Storage
{
    /// <summary>
    /// Abstract base class for a storage session.
    /// </summary>
    public abstract class StorageSession : IStorageSession
    {
        /// <summary>
        /// Constructor.
        /// </summary>        
        public StorageSession()
        {
            SessionKey = Guid.NewGuid();
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
        public virtual Guid SessionKey { get; set; }

        /// <summary>
        /// The provider-specific session implementation.
        /// </summary>
        public virtual object Session { get; set; }

        /// <summary>
        /// The provider-specific session implementation.
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

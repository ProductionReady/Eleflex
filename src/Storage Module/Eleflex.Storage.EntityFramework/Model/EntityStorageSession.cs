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
using System.Data.Entity;
using Eleflex.Storage;

namespace Eleflex.Storage.EntityFramework
{
    /// <summary>
    /// SQL Server Entity Framework session implementation.
    /// </summary>
    public class EntityStorageSession : StorageSession
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EntityStorageSession() : base() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="session"></param>
        public EntityStorageSession(DbContext session, DbContextTransaction transaction) : base()
        {
            Session = session;
            Transaction = transaction;
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            IsActive = false;
            if (Session != null)
                ((DbContext)Session).Dispose();
            if (Transaction != null)
                ((DbContextTransaction)Transaction).Dispose();

            Session = null;
            Transaction = null;
            base.Dispose();            
        }

        /// <summary>
        /// Rollback the changes.
        /// </summary>
        public override void Rollback()
        {
            IsActive = false;
            if (Transaction != null)
                ((DbContextTransaction)Transaction).Rollback();
        }

        /// <summary>
        /// Commit the changes.
        /// </summary>
        public override void Commit()
        {
            IsActive = false;
            if (Session != null)
                ((DbContext)Session).SaveChanges();
            if (Transaction != null && ((DbContextTransaction)Transaction).UnderlyingTransaction.Connection != null)
                ((DbContextTransaction)Transaction).Commit();
        }
        
    }
}

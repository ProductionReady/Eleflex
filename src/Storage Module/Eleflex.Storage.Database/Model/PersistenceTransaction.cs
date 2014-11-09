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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a managed transaction in the framework.
    /// </summary>
    public abstract partial class PersistenceTransaction : IPersistenceTransaction, IDisposable
    {

        /// <summary>
        /// Interanl TransactionKey.
        /// </summary>
        protected string _transactionKey;

        /// <summary>
        /// Internal IsAlive.
        /// </summary>
        protected bool _isAlive;


        /// <summary>
        /// The unique key used to identify the transaction.
        /// </summary>
        public string TransactionKey
        {
            get { return _transactionKey; }
        }

        /// <summary>
        /// Determine if the transaction is in use.
        /// </summary>
        public virtual bool IsAlive
        {
            get { return _isAlive; }
        }


        /// <summary>
        /// Disposal.
        /// </summary>
        public abstract void Dispose();


        /// <summary>
        /// Start a transaction.
        /// </summary>
        public virtual void BeginTransaction()
        {
            _isAlive = true;
            _transactionKey = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Commit the transaction.
        /// </summary>
        public virtual void Commit()
        {
            _isAlive = false;
        }

        /// <summary>
        /// Rollback the transaction.
        /// </summary>
        public virtual void Rollback()
        {
            _isAlive = false;
        }


    }
}

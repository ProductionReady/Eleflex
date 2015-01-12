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
using System.Data.Common;

namespace Eleflex.Storage.Database
{
   
    /// <summary>
    /// Defines a transaction defined by a database storage provider.
    /// </summary>
    public partial class DatabaseTransaction : PersistenceTransaction, IDisposable
    {

        /// <summary>
        /// Internal CatalogName.
        /// </summary>
        protected string _catalogName;
        /// <summary>
        /// Internal Connection.
        /// </summary>
        protected DbConnection _connection;
        /// <summary>
        /// Internal Transaction.
        /// </summary>
        protected DbTransaction _transaction;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="catalogName"></param>
        public DatabaseTransaction(string catalogName) :
            this(catalogName, null, null)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="catalogName"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        public DatabaseTransaction(string catalogName, 
            DbConnection connection, 
            DbTransaction transaction): base()
        {
            _catalogName = catalogName;
            _connection = connection;
            _transaction = transaction;

            //Signal to the base class we are inheriting a transaction and the
            //transaction is now in use.
            if(_connection!=null && _transaction!=null)
                base.BeginTransaction();
        }

        /// <summary>
        /// Disposal.
        /// </summary>
        public override void Dispose()
        {
            Rollback();

            GC.SuppressFinalize(this);            
        }


        /// <summary>
        /// The name of the database catalog this transaction belongs to.
        /// </summary>
        public virtual string CatalogName
        {
            get { return _catalogName; }
        }

        /// <summary>
        /// The database provider's connection in use with the transaction.
        /// </summary>
        public virtual DbConnection Connection
        {
            get { return _connection; }
        }

        /// <summary>
        /// The database provider's transaction.
        /// </summary>
        public virtual DbTransaction Transaction
        {
            get { return _transaction; }
        }
        

        /// <summary>
        /// Start a transaction.
        /// </summary>
        public override void BeginTransaction()
        {
            IPersistenceProvider provider = InterfaceCache<IPersistenceProvider>.Get(_catalogName);
            if (provider == null)
                return;
            IDatabaseProvider dbProvider = provider as IDatabaseProvider;
            if (dbProvider == null)
                return;
            DatabaseTransaction providerTransaction = dbProvider.BeginDatabaseTransaction();

            //Only copy over the properties we need
            _connection = providerTransaction.Connection;
            _transaction = providerTransaction.Transaction;
            base.BeginTransaction();
        }

        /// <summary>
        /// Commit a transcation.
        /// </summary>
        public override void Commit()
        {
            try
            {
                if (_transaction != null)
                    _transaction.Commit();
            }
            finally
            {
                try
                {
                    if (_connection != null)
                        _connection.Close();
                }
                finally
                {
                    _transaction = null;
                    _connection = null;
                }
                base.Commit();
            }
        }

        /// <summary>
        /// Rollback a transaction.
        /// </summary>
        public override void Rollback()
        {
            try
            {
                if (_transaction != null)
                    _transaction.Rollback();
            }
            finally
            {
                try
                {
                    if (_connection != null)
                        _connection.Close();
                }
                finally
                {
                    _transaction = null;
                    _connection = null;
                }
                base.Rollback();
            }
        }

    }
}

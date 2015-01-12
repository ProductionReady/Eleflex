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
using System.Data;
using System.Data.Common;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a command to be executed on a database provider.
    /// </summary>
    public partial class DatabaseCommand
    {

        /// <summary>
        /// Internal CatalogName.
        /// </summary>
        protected string _catalogName;
        /// <summary>
        /// Internal connection.
        /// </summary>
        protected IDbConnection _connection;
        /// <summary>
        /// Internal IsStoredProc.
        /// </summary>
        protected bool _isStoredProc;
        /// <summary>
        /// Internal SQL.
        /// </summary>
        protected string _sql;
        /// <summary>
        /// Internal Timeout.
        /// </summary>
        protected int _timeout;
        /// <summary>
        /// Internal InParameters.
        /// </summary>
        protected List<IDatabaseCommandParameter> _inParameters = new List<IDatabaseCommandParameter>();
        /// <summary>
        /// Internal OutParameters.
        /// </summary>
        protected List<IDatabaseCommandParameter> _outParameters = new List<IDatabaseCommandParameter>();


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="catalogName"></param>
        /// <param name="isStoredProc"></param>
        /// <param name="timeout"></param>
        /// <param name="sql"></param>
        /// <param name="inParameters"></param>
        /// <param name="outParameters"></param>
        public DatabaseCommand(string catalogName, bool isStoredProc, int timeout, string sql, List<IDatabaseCommandParameter> inParameters, List<IDatabaseCommandParameter> outParameters)
        {
            _catalogName = catalogName;
            _isStoredProc = isStoredProc;
            _timeout = timeout;
            _sql = sql;            
            _inParameters = inParameters;
            _outParameters = outParameters;            
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="catalogName"></param>
        /// <param name="connection"></param>
        /// <param name="isStoredProc"></param>
        /// <param name="timeout"></param>
        /// <param name="sql"></param>
        /// <param name="inParameters"></param>
        /// <param name="outParameters"></param>
        public DatabaseCommand(string catalogName, IDbConnection connection, bool isStoredProc, int timeout, string sql, List<IDatabaseCommandParameter> inParameters, List<IDatabaseCommandParameter> outParameters)
        {
            _catalogName = catalogName;
            _connection = connection;
            _isStoredProc = isStoredProc;
            _timeout = timeout;
            _sql = sql;
            _inParameters = inParameters;
            _outParameters = outParameters;
        }

        /// <summary>
        /// The database catalog this command is associated to.
        /// </summary>
        public virtual string CatalogName
        {
            get { return _catalogName; }
            set { _catalogName = value; }
        }

        /// <summary>
        /// Database connection.
        /// </summary>
        public virtual IDbConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

        /// <summary>
        /// Determines if executing a stored procedure.
        /// </summary>
        public virtual bool IsStoredProc
        {
            get { return _isStoredProc; }
            set { _isStoredProc = value; }
        }

        /// <summary>
        /// The language-specific SQL statement.
        /// </summary>
        public virtual string Sql
        {
            get { return _sql; }
            set { _sql = value; }
        }

        /// <summary>
        /// The timeout value for the request (in seconds).
        /// </summary>
        public virtual int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// The list of input parameters.
        /// </summary>
        public virtual List<IDatabaseCommandParameter> InParameters
        {
            get { return _inParameters; }
            set { _inParameters = value; }
        }

        /// <summary>
        /// The list of output parameters.
        /// </summary>
        public virtual List<IDatabaseCommandParameter> OutParameters
        {
            get { return _outParameters; }
            set { _outParameters = value; }
        }

    }
}

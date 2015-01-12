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
using System.Data;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a database provider and the methods that must be implemented to be
    /// used within the framework.
    /// </summary>
    public partial interface IDatabaseProvider : IDisposable
    {

        /// <summary>
        /// Get the catalog name of the provider.
        /// </summary>
        /// <returns></returns>
        string GetCatalogName();

        /// <summary>
        /// Get the connection string used to connect to the database.
        /// </summary>
        /// <returns></returns>
        string GetConnectionString();

        /// <summary>
        /// Get the SQL language generator associated with the provider.
        /// </summary>
        /// <returns></returns>
        IDatabaseFilterToSQLGenerator GetSQLGenerator();

        /// <summary>
        /// Get the catalog gnerator to list all objects in the database.
        /// </summary>
        /// <returns></returns>
        IDatabaseCatalogGenerator GetCatalogGenerator();

        /// <summary>
        /// Create a DB command.
        /// </summary>
        /// <returns></returns>
        IDbCommand CreateDbCommand();

        /// <summary>
        /// Start a transaction.
        /// </summary>
        /// <returns></returns>
        DatabaseTransaction BeginDatabaseTransaction();        

        /// <summary>
        /// Execute a command and return a data reader.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(DatabaseCommand command, DatabaseTransaction transaction);

        /// <summary>
        /// Execute a command and return the scaler output.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        object ExecuteScaler(DatabaseCommand command, DatabaseTransaction transaction);

        /// <summary>
        /// Execute a command and return a data set.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        DataSet ExecuteDataSet(DatabaseCommand command, DatabaseTransaction transaction);

        /// <summary>
        /// Execute a command and return a count of affected records.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        int ExecuteNonQuery(DatabaseCommand command, DatabaseTransaction transaction);

        /// <summary>
        /// Validate data for use with the database provider.
        /// </summary>
        /// <param name="frameworkValue"></param>
        /// <returns></returns>
        bool ValidateData(IEleflexDataType frameworkValue);

        /// <summary>
        /// Get the SQL statement to get a computed value.
        /// </summary>
        /// <returns></returns>
        string GetComputedValueSQL();

        /// <summary>
        /// Get the managed data type given a database returned value.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IEleflexDataType GetFrameworkValue(IEleflexProperty property, object value);

    }
}

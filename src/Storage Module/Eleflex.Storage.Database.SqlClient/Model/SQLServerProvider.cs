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
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.SqlClient
{

    /// <summary>
    /// Microsoft database provider supporting SQL Server 2000, 2005, and 2008.
    /// </summary>
    public partial class SQLServerProvider : DatabaseProviderBase
    {

        /// <summary>
        /// Minimum date value for database engine.
        /// </summary>
        public static readonly DateTime DefaultMinimumDate = new DateTime(1753, 1, 1);

        /// <summary>
        /// Internal CatalogName.
        /// </summary>
        private string _catalogName;

        /// <summary>
        /// Internal ConnectionString.
        /// </summary>
        private string _connectionString;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="catalogName"></param>
        /// <param name="connectionString"></param>
        public SQLServerProvider(string catalogName, string connectionString)
        {
            _catalogName = catalogName;
            _connectionString = connectionString;
        }

        /// <summary>
        /// Disposal.
        /// </summary>
        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Get the catalog name.
        /// </summary>
        /// <returns></returns>
        public override string GetCatalogName()
        {
            return _catalogName;
        }

        /// <summary>
        /// Get the connection string used to connect to the database.
        /// </summary>
        /// <returns></returns>
        public override string GetConnectionString()
        {
            return _connectionString;
        }


        /// <summary>
        /// Get the SQL language generator associated with the provider.
        /// </summary>
        /// <returns></returns>
        public override IDatabaseFilterToSQLGenerator GetSQLGenerator()
        {
            return new TSQLDatabaseFilterGenerator();
        }

        /// <summary>
        /// Get the catalog gnerator to list all objects in the database.
        /// </summary>
        /// <returns></returns>
        public override IDatabaseCatalogGenerator GetCatalogGenerator()
        {
            return new SQLServerDatabaseCatalogGenerator(_catalogName, _connectionString);
        }

        /// <summary>
        /// Create a DB command.
        /// </summary>
        /// <returns></returns>
        public override IDbCommand CreateDbCommand()
        {
            SqlCommand command = new SqlCommand();
            command.Connection = CreateConnection() as SqlConnection;
            command.CommandType = CommandType.Text;
            return command;
        }

        /// <summary>
        /// Start a transaction.
        /// </summary>
        /// <returns></returns>
        public override DatabaseTransaction BeginDatabaseTransaction()
        {
            SqlConnection sqlConnection = CreateConnection() as SqlConnection;
            SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
            DatabaseTransaction transaction = new DatabaseTransaction(_catalogName, sqlConnection, sqlTransaction);
            return transaction;
        }

        /// <summary>
        /// Execute a command and return a data reader.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public override IDataReader ExecuteReader(DatabaseCommand command, DatabaseTransaction transaction)
        {
            if (command == null)
                throw new Exception("databasecommand object null");

            //Get connection
            SqlConnection connection = null;
            if (command.Connection != null)
                connection = command.Connection as SqlConnection;
            if (transaction != null)
            {
                if (!transaction.IsAlive)
                    throw new Exception("transaction not alive");
                connection = (SqlConnection)transaction.Connection;
            }
            if (connection == null)
                connection = CreateConnection() as SqlConnection;
            
            //Get command
            SqlCommand sqlCommand = (SqlCommand)GetCommand(command);
            sqlCommand.Connection = connection;
            if (transaction != null)
                sqlCommand.Transaction = (SqlTransaction)transaction.Transaction;
            try
            {
                if (transaction == null)
                    return sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                else
                    return sqlCommand.ExecuteReader(CommandBehavior.Default);
            }
            finally
            {
                if (transaction == null && connection != null && command.Connection == null)
                {
                    //Disposing the connection would terminate the reader. We are relying on the programmer
                    //to make sure they dispose of the reader, which will in-turn release the connection.
                    //If the reader returned from this function is not disposed, we will leak the connection. 
                }
                if (sqlCommand != null)
                {
                    if (sqlCommand.Parameters != null)
                        CopyCommandParameterValues(sqlCommand, command);
                    sqlCommand.Dispose();
                }
                connection = null;
                sqlCommand = null;
            }
        }

        /// <summary>
        /// Execute a command and return the scaler output.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public override object ExecuteScaler(DatabaseCommand command, DatabaseTransaction transaction)
        {
            if (command == null)
                throw new Exception("databasecommand object null");

            //Get connection
            SqlConnection connection = null;
            if (command.Connection != null)
                connection = command.Connection as SqlConnection;
            if (transaction != null)
            {
                if (!transaction.IsAlive)
                    throw new Exception("transaction not alive");
                connection = (SqlConnection)transaction.Connection;
            }
            if (connection == null)
                connection = CreateConnection() as SqlConnection;

            //Get command
            SqlCommand sqlCommand = (SqlCommand)GetCommand(command);
            sqlCommand.Connection = connection;
            if (transaction != null)
                sqlCommand.Transaction = (SqlTransaction)transaction.Transaction;
            try
            {
                return sqlCommand.ExecuteScalar();
            }
            finally
            {
                if (transaction == null && connection != null && command.Connection == null)
                {
                    connection.Dispose();
                }
                if (sqlCommand != null)
                {
                    if (sqlCommand.Parameters != null)
                        CopyCommandParameterValues(sqlCommand, command);
                    sqlCommand.Dispose();
                }
                sqlCommand = null;
                connection = null;
            }
        }

        /// <summary>
        /// Execute a command and return a data set.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public override DataSet ExecuteDataSet(DatabaseCommand command, DatabaseTransaction transaction)
        {
            if (command == null)
                throw new Exception("databasecommand object null");

            //Get connection
            SqlConnection connection = null;
            if (command.Connection != null)
                connection = command.Connection as SqlConnection;
            if (transaction != null)
            {
                if (!transaction.IsAlive)
                    throw new Exception("transaction not alive");
                connection = (SqlConnection)transaction.Connection;
            }
            if (connection == null)
                connection = CreateConnection() as SqlConnection;

            //Get command
            SqlCommand sqlCommand = (SqlCommand)GetCommand(command);
            sqlCommand.Connection = connection;
            if (transaction != null)
                sqlCommand.Transaction = (SqlTransaction)transaction.Transaction;
            SqlDataAdapter adapter = null;
            DataSet dataSet = null;
            try
            {
                adapter = new SqlDataAdapter(sqlCommand);
                dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
            finally
            {
                if (transaction == null && connection != null && command.Connection == null)
                {
                    connection.Dispose();
                }
                if (adapter != null)
                    adapter.Dispose();
                if (sqlCommand != null)
                {
                    if (sqlCommand.Parameters != null)
                        CopyCommandParameterValues(sqlCommand, command);
                    sqlCommand.Dispose();
                }
                adapter = null;
                sqlCommand = null;
                connection = null;
            }
        }

        /// <summary>
        /// Execute a command and return a count of affected records.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public override int ExecuteNonQuery(DatabaseCommand command, DatabaseTransaction transaction)
        {
            if (command == null)
                throw new Exception("databasecommand object null");

            //Get connection
            SqlConnection connection = null;
            if (command.Connection != null)
                connection = command.Connection as SqlConnection;
            if (transaction != null)
            {
                if (!transaction.IsAlive)
                    throw new Exception("transaction not alive");
                connection = (SqlConnection)transaction.Connection;
            }            
            if(connection == null)
                connection = CreateConnection() as SqlConnection;

            //Get command
            SqlCommand sqlCommand = (SqlCommand)GetCommand(command);
            sqlCommand.Connection = connection;
            if (transaction != null)
                sqlCommand.Transaction = (SqlTransaction)transaction.Transaction;
            try
            {
                return sqlCommand.ExecuteNonQuery();
            }
            finally
            {
                if (transaction == null && connection != null && command.Connection == null)
                {
                    connection.Dispose();
                }
                if (sqlCommand != null)
                {
                    if (sqlCommand.Parameters != null)
                        CopyCommandParameterValues(sqlCommand, command);
                    sqlCommand.Dispose();
                }
                connection = null;
                sqlCommand = null;
            }
        }        

        /// <summary>
        /// Validate data for use with the database provider.
        /// </summary>
        /// <param name="frameworkValue"></param>
        /// <returns></returns>
        public override bool ValidateData(IEleflexDataType frameworkValue)
        {
            try
            {
                switch (frameworkValue.DataTypeName)
                {
                    default:
                        return true;//It will always throw an exception if it is not valid, just let it pass.
                    case EleflexDataTypeConstant.DateTime:
                        IEleflexValue<DateTime> tempDateTime = (IEleflexValue<DateTime>)frameworkValue;
                        if (tempDateTime == null)
                            return false;
                        if (DateTime.Compare(tempDateTime.Value, DefaultMinimumDate) < 0)
                            return false;
                        return true;
                    case EleflexDataTypeConstant.DateTimeNull:
                        IEleflexValue<Nullable<DateTime>> tempDateTimeNull = (IEleflexValue<Nullable<DateTime>>)frameworkValue;
                        if (tempDateTimeNull == null)
                            return true;
                        if (tempDateTimeNull.Value.HasValue)
                        {
                            if (DateTime.Compare(tempDateTimeNull.Value.Value, DefaultMinimumDate) < 0)
                                return false;
                        }
                        return true;
                }
            }
            catch (Exception ie)
            {
                string a = ie.ToString();
                return false;
            }
        }

        /// <summary>
        /// Get the SQL statement to get a computed value.
        /// </summary>
        /// <returns></returns>
        public override string GetComputedValueSQL()
        {
            return "SELECT @@IDENTITY";
        }

        /// <summary>
        /// Create a connection to the database.
        /// </summary>
        /// <returns></returns>
        public override IDbConnection CreateConnection()
        {
            string connectionString = GetConnectionString();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        /// <summary>
        /// Create a command object.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected virtual DbCommand GetCommand(DatabaseCommand command)
        {
            SqlCommand sqlCommand = null;
            if (command.IsStoredProc)
            {
                sqlCommand = new SqlCommand(command.Sql);
                sqlCommand.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                sqlCommand = new SqlCommand(command.Sql);
                sqlCommand.CommandType = CommandType.Text;
            }
            sqlCommand.CommandText = command.Sql;

            if (command.InParameters != null)
            {
                for (int i = 0; i < command.InParameters.Count; i++)
                    AddParameter(sqlCommand, command.InParameters[i], ParameterDirection.Input);
            }

            if (command.OutParameters != null)
            {
                for (int i = 0; i < command.OutParameters.Count; i++)
                    AddParameter(sqlCommand, command.OutParameters[i], ParameterDirection.Output);

            }

            //Return the command
            return sqlCommand;
        }

        /// <summary>
        /// Add a command parameter.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameter"></param>
        /// <param name="direction"></param>
        protected virtual void AddParameter(
            DbCommand command,
            IDatabaseCommandParameter parameter,
            ParameterDirection direction)
        {
            DbParameter sqlParameter = new SqlParameter();
            if (parameter.IProperty == null)
            {
                sqlParameter.DbType = parameter.DbType;
            }
            else
            {
                if (parameter.IProperty is IEBOProperty)
                {
                    if (direction != ParameterDirection.Input)
                        sqlParameter.Size = (parameter.IProperty as IEBOProperty).Size;
                }

                IEDOProperty dbProp = parameter.IProperty as IEDOProperty;
                if(dbProp!=null)
                    sqlParameter.DbType = dbProp.DbType;
                
                sqlParameter.IsNullable = parameter.IProperty.IsNullable;
            }

            sqlParameter.Direction = direction;
            sqlParameter.ParameterName = parameter.Name;
            sqlParameter.SourceVersion = DataRowVersion.Default;

            //validation parameter value
            if(!ValidateData(parameter.Value))
                throw new Exception("Database validate data: " + parameter.Name);

            object value = parameter.Value.ObjectValue;
            if (value == null)
                sqlParameter.Value = DBNull.Value;
            else
                sqlParameter.Value = value;
            command.Parameters.Add(sqlParameter);
        }

        /// <summary>
        /// Used to copy command parameters.
        /// </summary>
        /// <param name="sqlCommand"></param>
        /// <param name="command"></param>
        protected virtual void CopyCommandParameterValues(
            SqlCommand sqlCommand,
            DatabaseCommand command)
        {
            bool inParamsFinished = false;
            for (int i = 0; i < sqlCommand.Parameters.Count; i++)
            {
                bool found = false;
                if (!inParamsFinished)
                {
                    for (int j = 0; j < command.InParameters.Count; j++)
                    {
                        if (command.InParameters[j].Name == sqlCommand.Parameters[i].ParameterName)
                        {
                            if (command.InParameters[j].IProperty == null)
                                continue;
                            found = true;
                            command.InParameters[j].Value =
                                GetFrameworkValue(
                                command.InParameters[j].IProperty,
                                sqlCommand.Parameters[i].Value);
                            break;
                        }
                    }
                }
                if (found)
                    continue;
                else
                    inParamsFinished = true;

                if (command.OutParameters == null)
                    continue;
                for (int j = 0; j < command.OutParameters.Count; j++)
                {
                    if (command.OutParameters[j].Name == sqlCommand.Parameters[i].ParameterName)
                    {
                        if (command.OutParameters[j].IProperty == null)
                            continue;
                        found = true;
                        command.OutParameters[j].Value = GetFrameworkValue(
                            command.OutParameters[j].IProperty,
                            sqlCommand.Parameters[i].Value);
                        break;
                    }
                }
            }
        }

    }
}

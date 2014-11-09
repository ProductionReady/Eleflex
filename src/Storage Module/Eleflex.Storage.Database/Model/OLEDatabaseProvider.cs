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
using System.Data.OleDb;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Database provider supporting OLE.
    /// </summary>
    public partial class OLEDatabaseProvider : DatabaseProviderBase
    {

        /// <summary>
        /// Internal CatalogName.
        /// </summary>
        protected string _catalogName;
        /// <summary>
        /// Internal ConnectionString.
        /// </summary>
        protected string _connectionString;
        /// <summary>
        /// Internal SQL Generator.
        /// </summary>
        protected IDatabaseFilterToSQLGenerator _sqlGenerator;
        /// <summary>
        /// Internal Catalog Generator.
        /// </summary>
        protected IDatabaseCatalogGenerator _catalogGenerator;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="catalogName"></param>
        /// <param name="connectionString"></param>
        /// <param name="filterGenerator"></param>
        /// <param name="catalogGenerator"></param>
        public OLEDatabaseProvider(string catalogName, string connectionString, IDatabaseFilterToSQLGenerator filterGenerator, IDatabaseCatalogGenerator catalogGenerator)
        {
            _catalogName = catalogName;
            _connectionString = connectionString;
            _sqlGenerator = filterGenerator;
            _catalogGenerator = catalogGenerator;
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
            return _sqlGenerator;
        }

        /// <summary>
        /// Get the catalog generator.
        /// </summary>
        /// <returns></returns>
        public override IDatabaseCatalogGenerator GetCatalogGenerator()
        {
            return _catalogGenerator;
        }

        /// <summary>
        /// Create a DB command.
        /// </summary>
        /// <returns></returns>
        public override IDbCommand CreateDbCommand()
        {
            OleDbCommand command = new OleDbCommand();
            command.Connection = CreateConnection() as OleDbConnection;
            command.CommandType = CommandType.Text;
            return command;
        }

        /// <summary>
        /// Start a transaction.
        /// </summary>
        /// <returns></returns>
        public override DatabaseTransaction BeginDatabaseTransaction()
        {
            OleDbConnection OleDbConnection = CreateConnection() as OleDbConnection;
            OleDbTransaction OleDbTransaction = OleDbConnection.BeginTransaction();
            DatabaseTransaction transaction = new DatabaseTransaction(_catalogName, OleDbConnection, OleDbTransaction);
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
            OleDbConnection connection = null;
            if (transaction != null)
            {
                if (!transaction.IsAlive)
                    throw new Exception("transaction not alive");
                connection = (OleDbConnection)transaction.Connection;
            }
            else
                connection = (OleDbConnection)CreateConnection();

            //Get command
            OleDbCommand OleDbCommand = (OleDbCommand)GetCommand(command);
            OleDbCommand.Connection = connection;
            if (transaction != null)
                OleDbCommand.Transaction = (OleDbTransaction)transaction.Transaction;
            try
            {
                if (transaction == null)
                    return OleDbCommand.ExecuteReader(CommandBehavior.CloseConnection);
                else
                    return OleDbCommand.ExecuteReader(CommandBehavior.Default);
            }
            finally
            {
                if (transaction == null && connection != null)
                {
                    //Disposing the connection would terminate the reader. We are relying on the programmer
                    //to make sure they dispose of the reader, which will in-turn release the connection.
                    //If the reader returned from this function is not disposed, we will leak the connection. 
                }
                if (OleDbCommand != null)
                {
                    if (OleDbCommand.Parameters != null)
                        CopyCommandParameterValues(OleDbCommand, command);
                    OleDbCommand.Dispose();
                }
                connection = null;
                OleDbCommand = null;
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
            OleDbConnection connection = null;
            if (transaction != null)
            {
                if (!transaction.IsAlive)
                    throw new Exception("transaction not alive");
                connection = (OleDbConnection)transaction.Connection;
            }
            else
                connection = (OleDbConnection)CreateConnection();

            //Get command
            OleDbCommand OleDbCommand = (OleDbCommand)GetCommand(command);
            OleDbCommand.Connection = connection;
            if (transaction != null)
                OleDbCommand.Transaction = (OleDbTransaction)transaction.Transaction;
            try
            {
                return OleDbCommand.ExecuteScalar();
            }
            finally
            {
                if (transaction == null && connection != null)
                {
                    connection.Dispose();
                }
                if (OleDbCommand != null)
                {
                    if (OleDbCommand.Parameters != null)
                        CopyCommandParameterValues(OleDbCommand, command);
                    OleDbCommand.Dispose();
                }
                OleDbCommand = null;
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
            OleDbConnection connection = null;
            if (transaction != null)
            {
                if (!transaction.IsAlive)
                    throw new Exception("transaction not alive");
                connection = (OleDbConnection)transaction.Connection;
            }
            else
                connection = (OleDbConnection)CreateConnection();

            //Get command
            OleDbCommand OleDbCommand = (OleDbCommand)GetCommand(command);
            OleDbCommand.Connection = connection;
            if (transaction != null)
                OleDbCommand.Transaction = (OleDbTransaction)transaction.Transaction;
            OleDbDataAdapter adapter = null;
            DataSet dataSet = null;
            try
            {
                adapter = new OleDbDataAdapter(OleDbCommand);
                dataSet = new DataSet();
                adapter.Fill(dataSet);
                return dataSet;
            }
            finally
            {
                if (transaction == null && connection != null)
                {
                    connection.Dispose();
                }
                if (adapter != null)
                    adapter.Dispose();
                if (OleDbCommand != null)
                {
                    if (OleDbCommand.Parameters != null)
                        CopyCommandParameterValues(OleDbCommand, command);
                    OleDbCommand.Dispose();
                }
                adapter = null;
                OleDbCommand = null;
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
            OleDbConnection connection = null;
            if (transaction != null)
            {
                if (!transaction.IsAlive)
                    throw new Exception("transaction not alive");
                connection = (OleDbConnection)transaction.Connection;
            }
            else
                connection = (OleDbConnection)CreateConnection();

            //Get command
            OleDbCommand OleDbCommand = (OleDbCommand)GetCommand(command);
            OleDbCommand.Connection = connection;
            if (transaction != null)
                OleDbCommand.Transaction = (OleDbTransaction)transaction.Transaction;
            try
            {
                return OleDbCommand.ExecuteNonQuery();
            }
            finally
            {
                if (transaction == null && connection != null)
                {
                    connection.Dispose();
                }
                if (OleDbCommand != null)
                {
                    if (OleDbCommand.Parameters != null)
                        CopyCommandParameterValues(OleDbCommand, command);
                    OleDbCommand.Dispose();
                }
                connection = null;
                OleDbCommand = null;
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
                        if (DateTime.Compare(tempDateTime.Value, new DateTime(1753, 1, 1)) < 0)
                            return false;
                        return true;
                    case EleflexDataTypeConstant.DateTimeNull:
                        IEleflexValue<Nullable<DateTime>> tempDateTimeNull = (IEleflexValue<Nullable<DateTime>>)frameworkValue;
                        if (tempDateTimeNull == null)
                            return true;
                        if (tempDateTimeNull.Value.HasValue)
                        {
                            if (DateTime.Compare(tempDateTimeNull.Value.Value, new DateTime(1753, 1, 1)) < 0)
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
            OleDbConnection connection = new OleDbConnection(connectionString);
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
            OleDbCommand OleDbCommand = null;
            if (command.IsStoredProc)
            {
                OleDbCommand = new OleDbCommand(command.Sql);
                OleDbCommand.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                OleDbCommand = new OleDbCommand(command.Sql);
                OleDbCommand.CommandType = CommandType.Text;
            }
            OleDbCommand.CommandText = command.Sql;

            if (command.InParameters != null)
            {
                for (int i = 0; i < command.InParameters.Count; i++)
                    AddParameter(OleDbCommand, command.InParameters[i], ParameterDirection.Input);
            }

            if (command.OutParameters != null)
            {
                for (int i = 0; i < command.OutParameters.Count; i++)
                    AddParameter(OleDbCommand, command.OutParameters[i], ParameterDirection.Output);

            }

            //Change parameter names
            ReplaceParameterName(OleDbCommand);

            //Return the command
            return OleDbCommand;
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
            DbParameter OleDbParameter = new OleDbParameter();
            if (parameter.IProperty == null)
            {
                OleDbParameter.DbType = parameter.DbType;
            }
            else
            {
                if (parameter.IProperty is IEBOProperty)
                {
                    if (direction != ParameterDirection.Input)
                        OleDbParameter.Size = (parameter.IProperty as IEBOProperty).Size;
                }
                IEDOProperty dbProp = parameter.IProperty as IEDOProperty;
                if (dbProp != null)
                    OleDbParameter.DbType = dbProp.DbType;

                OleDbParameter.IsNullable = parameter.IProperty.IsNullable;
            }

            OleDbParameter.Direction = direction;
            OleDbParameter.ParameterName = parameter.Name;
            OleDbParameter.SourceVersion = DataRowVersion.Default;

            //validation parameter value
            if (!ValidateData(parameter.Value))
                throw new Exception("Database validate data: " + parameter.Name);

            object value = parameter.Value.ObjectValue;
            if (value == null)
                OleDbParameter.Value = DBNull.Value;
            else
                OleDbParameter.Value = value;
            command.Parameters.Add(OleDbParameter);
        }

        /// <summary>
        /// Used to copy command parameters.
        /// </summary>
        /// <param name="OleDbCommand"></param>
        /// <param name="command"></param>
        protected virtual void CopyCommandParameterValues(
            OleDbCommand OleDbCommand,
            DatabaseCommand command)
        {
            bool inParamsFinished = false;
            for (int i = 0; i < OleDbCommand.Parameters.Count; i++)
            {
                bool found = false;
                if (!inParamsFinished)
                {
                    for (int j = 0; j < command.InParameters.Count; j++)
                    {
                        if (command.InParameters[j].Name == OleDbCommand.Parameters[i].ParameterName)
                        {
                            if (command.InParameters[j].IProperty == null)
                                continue;
                            found = true;
                            command.InParameters[j].Value =
                                GetFrameworkValue(
                                command.InParameters[j].IProperty,
                                OleDbCommand.Parameters[i].Value);
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
                    if (command.OutParameters[j].Name == OleDbCommand.Parameters[i].ParameterName)
                    {
                        if (command.OutParameters[j].IProperty == null)
                            continue;
                        found = true;
                        command.OutParameters[j].Value = GetFrameworkValue(
                            command.OutParameters[j].IProperty,
                            OleDbCommand.Parameters[i].Value);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Get replacement parameter names for this type of provider.
        /// </summary>
        /// <param name="command"></param>
        public virtual void ReplaceParameterName(OleDbCommand command)
        {
            string newSQL = command.CommandText;
            for (int i = 0; i < command.Parameters.Count; i++)
                newSQL = newSQL.Replace(command.Parameters[i].ParameterName, "?");
            command.CommandText = newSQL;
        }

    }
}

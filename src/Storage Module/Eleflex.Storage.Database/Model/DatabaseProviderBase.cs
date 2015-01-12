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
using Eleflex.Storage.Database.Filters;
using Eleflex.Storage.Database.Values;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a database provider and the methods that must be implemented to be
    /// used within the framework.
    /// </summary>
    public abstract partial class DatabaseProviderBase : PersistenceProviderBase, IDatabaseProvider
    {

        /// <summary>
        /// Constant used for logging.
        /// </summary>
        public new const string CLASSNAME = "PR.Eleflex.Persistence.Database.DatabaseProviderBase" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Get the catalog name.
        /// </summary>
        /// <returns></returns>
        public abstract string GetCatalogName();

        /// <summary>
        /// Get the connection string used to connect to the database.
        /// </summary>
        /// <returns></returns>
        public abstract string GetConnectionString();

        /// <summary>
        /// Get the SQL language generator associated with the provider.
        /// </summary>
        /// <returns></returns>
        public abstract IDatabaseFilterToSQLGenerator GetSQLGenerator();

        /// <summary>
        /// Get the catalog generator used to list all objects in a database.
        /// </summary>
        /// <returns></returns>
        public abstract IDatabaseCatalogGenerator GetCatalogGenerator();

        /// <summary>
        /// Create a DB command.
        /// </summary>
        /// <returns></returns>
        public abstract IDbConnection CreateConnection();

        /// <summary>
        /// Create a DB command.
        /// </summary>
        /// <returns></returns>
        public abstract IDbCommand CreateDbCommand();

        /// <summary>
        /// Start a transaction.
        /// </summary>
        /// <returns></returns>
        public abstract DatabaseTransaction BeginDatabaseTransaction();
        
        /// <summary>
        /// Execute a command and return a data reader.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public abstract IDataReader ExecuteReader(DatabaseCommand command, DatabaseTransaction transaction);

        /// <summary>
        /// Execute a command and return the scaler output.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public abstract object ExecuteScaler(DatabaseCommand command, DatabaseTransaction transaction);

        /// <summary>
        /// Execute a command and return a data set.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public abstract DataSet ExecuteDataSet(DatabaseCommand command, DatabaseTransaction transaction);

        /// <summary>
        /// Execute a command and return a count of affected records.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery(DatabaseCommand command, DatabaseTransaction transaction);

        /// <summary>
        /// Validate data for use with the database provider.
        /// </summary>
        /// <param name="frameworkValue"></param>
        /// <returns></returns>
        public abstract bool ValidateData(IEleflexDataType frameworkValue);

        /// <summary>
        /// Get the SQL statement to get a computed value.
        /// </summary>
        /// <returns></returns>
        public abstract string GetComputedValueSQL();


        /// <summary>
        /// Start a transaction.
        /// </summary>
        /// <returns></returns>
        public override IPersistenceTransaction BeginTransaction()
        {
            return BeginDatabaseTransaction();
        }

        /// <summary>
        /// Execute a bulk update statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override IPersistenceResponseItem<int> BulkUpdate(IPersistenceRequest request)
        {
            const string methodName = CLASSNAME + "BulkUpdate";

            //Validate input
            base.BulkUpdate(request);
            if (request == null || request.Context == null || request.Context.IsError)
                return null;

            //Use transaction for modifications
            DatabaseCommand command = null;
            try
            {
                //Business rule processing
                request.EPO.EPOTriggerPreBulkUpdate(request);
                if (request.Context.IsError)
                    return null;

                //Get sql generator
                IDatabaseFilterToSQLGenerator sqlGenerator = this.GetSQLGenerator();
                if (sqlGenerator == null)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_ISQLGeneratorNotFound));
                    return null;
                }

                //Get the sql statement
                IDatabaseSQLResponse response = sqlGenerator.GetUpdateStatement(request);
                if (request.Context.IsError)
                    return null;

                //Get the command
                command = new DatabaseCommand(
                    request.EPO.EPOGetCatalogName(),
                    false,
                    request.TimeoutSecs,
                    response.SQL,
                    response.Parameters,
                    null);

                if (request.Context.IsError)
                    return null;

                //Execute the command
                int rowsAffected = this.ExecuteNonQuery(
                    command,
                    request.Transaction as DatabaseTransaction);
                if (request.Context.IsError)
                    return null;

                //Business rule processing
                request.EPO.EPOTriggerPostBulkUpdate(request);
                if (request.Context.IsError)
                    return null;

                //Return the item
                return new PersistenceResponseItem<int>(Convert.ToInt32(rowsAffected));
            }
            catch (Exception ie)
            {
                //Log exception and set error
                if (command != null)
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_SQLException2, command.Sql, ie.ToString()));
                else
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_Exception1, ie.ToString()));                

                return null;
            }
        }

        /// <summary>
        /// Execute a bulk delete statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override IPersistenceResponseItem<int> BulkDelete(IPersistenceRequest request)
        {
            const string methodName = CLASSNAME + "BulkDelete";

            //Validate input
            base.BulkDelete(request);
            if (request == null || request.Context == null || request.Context.IsError)
                return null;

            //Use transaction for modifications
            DatabaseCommand command = null;
            try
            {
                //Business rule processing
                request.EPO.EPOTriggerPreBulkDelete(request);
                if (request.Context.IsError)
                    return null;

                //Get sql generator
                IDatabaseFilterToSQLGenerator sqlGenerator = this.GetSQLGenerator();
                if (sqlGenerator == null)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_ISQLGeneratorNotFound));
                    return null;
                }

                //Get the sql statement
                IDatabaseSQLResponse response = sqlGenerator.GetDeleteStatement(request);
                if (request.Context.IsError)
                    return null;

                //Get the command
                command = new DatabaseCommand(
                    request.EPO.EPOGetCatalogName(),
                    false,
                    request.TimeoutSecs,
                    response.SQL,
                    response.Parameters,
                    null);

                if (request.Context.IsError)
                    return null;

                //Execute the command
                int rowsAffected = this.ExecuteNonQuery(
                    command,
                    request.Transaction as DatabaseTransaction);
                if (request.Context.IsError)
                    return null;

                //Business rule processing
                request.EPO.EPOTriggerPostBulkDelete(request);
                if (request.Context.IsError)
                    return null;

                //Return the item
                return new PersistenceResponseItem<int>(rowsAffected);
            }
            catch (Exception ie)
            {
                //Log exception and set error
                if (command != null)
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_SQLException2, command.Sql, ie.ToString()));
                else
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_Exception1, ie.ToString()));

                return null;
            }
        }

        /// <summary>
        /// Execute an update statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override IPersistenceResponseItem<IEleflexPersistenceObject> Update(IPersistenceRequest request)
        {
            const string methodName = CLASSNAME + "Update";

            //Validate input
            base.Update(request);
            if (request == null || request.Context == null || request.Context.IsError)
                return null;

            DatabaseCommand command = null;
            try
            {
                //Remove the incoming where filters and replace with only the record's pk values
                List<IPersistenceFilter> scrubbedFilters = request.Filters.FindAll(delegate(IPersistenceFilter filter)
                {
                    return !filter.IsWhereClause;
                });
                request.Filters = scrubbedFilters;

                //Create filters for the record's pk values                
                List<IEleflexProperty> properties = request.EPO.EleflexGetProperties();
                List<IEleflexProperty> primaryKeys = properties.FindAll(delegate(IEleflexProperty property)
                {
                    if (property is IEBOProperty)
                        return (property as IEBOProperty).IsKey;
                    else
                        return false;
                });
                foreach (IEleflexProperty property in primaryKeys)
                {
                    IEleflexDataType value;
                    if (!request.EPO.EleflexGetPropertyValue(property.Name, out value))
                    {
                        request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_PropertyGet1, property.Name));
                        return null;
                    }
                    request.Filters.Add(
                        new FilterCompare(property, ComparisonEnum.EQUAL, value));
                }

                //Business rule processing
                request.EPO.EPOTriggerPreUpdate(request);
                if (request.Context.IsError)
                    return null;

                //Get sql generator
                IDatabaseFilterToSQLGenerator sqlGenerator = this.GetSQLGenerator();
                if (sqlGenerator == null)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_ISQLGeneratorNotFound));
                    return null;
                }

                //Get the sql statement
                IDatabaseSQLResponse response = sqlGenerator.GetUpdateStatement(request);
                if (request.Context.IsError)
                    return null;

                //Get the command
                command = new DatabaseCommand(
                    request.EPO.EPOGetCatalogName(),
                    false,
                    request.TimeoutSecs,
                    response.SQL,
                    response.Parameters,
                    null);

                if (request.Context.IsError)
                    return null;

                //Execute the command
                int rowsAffected = this.ExecuteNonQuery(
                    command,
                    request.Transaction as DatabaseTransaction);
                if (request.Context.IsError)
                    return null;
                if (rowsAffected == 0)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_NoRecordsModified1, command.Sql));
                    return null;
                }

                //Business rule processing
                request.EPO.EPOTriggerPostUpdate(request);
                if (request.Context.IsError)
                    return null;

                //Return the item
                return new PersistenceResponseItem<IEleflexPersistenceObject>(request.EPO);
            }
            catch (Exception ie)
            {
                //Log exception and set error
                if (command != null)
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_SQLException2, command.Sql, ie.ToString()));
                else
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_Exception1, ie.ToString()));

                return null;
            }
        }

        /// <summary>
        /// Execute an insert statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override IPersistenceResponseItem<IEleflexPersistenceObject> Insert(IPersistenceRequest request)
        {
            const string methodName = CLASSNAME + "Insert";

            //Validate input
            base.Insert(request);
            if (request == null || request.Context == null || request.Context.IsError)
                return null;

            IDbConnection connection = null;
            DatabaseCommand command = null;
            try
            {
                if (request.Transaction == null)
                    connection = CreateConnection();

                //Business rule processing
                request.EPO.EPOTriggerPreInsert(request);
                if (request.Context.IsError)
                    return null;

                //Get sql generator
                IDatabaseFilterToSQLGenerator sqlGenerator = this.GetSQLGenerator();
                if (sqlGenerator == null)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_ISQLGeneratorNotFound));
                    return null;
                }

                //Get the sql statement
                IDatabaseSQLResponse response = sqlGenerator.GetInsertStatement(request);
                if (request.Context.IsError)
                    return null;

                //Get the command                                
                command = new DatabaseCommand(
                    request.EPO.EPOGetCatalogName(),
                    connection,
                    false,
                    request.TimeoutSecs,
                    response.SQL,
                    response.Parameters,
                    null);

                if (request.Context.IsError)
                    return null;

                //Execute the command                
                int rowsAffected = this.ExecuteNonQuery(
                    command,
                    request.Transaction as DatabaseTransaction);
                if (request.Context.IsError)
                    return null;

                if (rowsAffected == 0)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_NoRecordsModified1, command.Sql));
                    return null;
                }

                //Get column definitions
                List<IEleflexProperty> properties = request.EPO.EleflexGetProperties();

                //Determine if we need to get the computed value from the database
                List<IEleflexProperty> computedColumns = properties.FindAll(delegate(IEleflexProperty property)
                {
                    if (property is IEBOProperty)
                        return (property as IEBOProperty).IsComputed;
                    else
                        return false;
                });

                //We need to set the computed value back to the object so we can retrieve 
                //the record just inserted.
                if (computedColumns.Count > 0)
                {
                    //Get computed value sql statement                    
                    string sql = this.GetComputedValueSQL();
                    command = new DatabaseCommand(
                        request.EPO.EPOGetCatalogName(),
                        connection,
                        false,
                        request.TimeoutSecs,
                        sql,
                        null,
                        null);

                    if (request.Context.IsError)
                        return null;

                    //Execute the computed value statment
                    object result = this.ExecuteScaler(
                        command,
                        request.Transaction as DatabaseTransaction);
                    if (request.Context.IsError)
                        return null;

                    //Get the value and set to the dto
                    IEleflexDataType computedValue = this.GetFrameworkValue(computedColumns[0], result);
                    if (!request.EPO.EleflexSetPropertyValue(computedColumns[0].Name, computedValue))
                    {
                        request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_PropertySet1, computedColumns[0].Name));
                        return null;
                    }
                }

                //Business rule processing
                request.EPO.EPOTriggerPostInsert(request);
                if (request.Context.IsError)
                    return null;

                //Return the response
                return new PersistenceResponseItem<IEleflexPersistenceObject>(request.EPO);
            }
            catch (Exception ie)
            {
                //Log exception and set error
                if (command != null)
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_SQLException2, command.Sql, ie.ToString()));
                else
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_Exception1, ie.ToString()));
                return null;
            }
            finally
            {
                if (request.Transaction == null && connection != null)
                    connection.Dispose();

            }
        }

        /// <summary>
        /// Execute a get statement returning a list of items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override IPersistenceResponseItems<IEleflexPersistenceObject> GetItems(IPersistenceRequest request)
        {
            const string methodName = CLASSNAME + "GetItems";

            //Validate input
            base.GetItems(request);
            if (request == null || request.Context == null || request.Context.IsError)
                return null;

            IDataReader reader = null;
            DatabaseCommand command = null;
            try
            {
                //Business rule processing
                request.EPO.EPOTriggerPreGet(request);
                if (request.Context.IsError)
                    return null;

                //Get sql generator
                IDatabaseFilterToSQLGenerator sqlGenerator = this.GetSQLGenerator();
                if (sqlGenerator == null)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_ISQLGeneratorNotFound));
                    return null;
                }

                //Get the sql statement
                IDatabaseSQLResponse response = sqlGenerator.GetSelectStatement(request);
                if (request.Context.IsError)
                    return null;

                //Get the command
                command = new DatabaseCommand(
                    request.EPO.EPOGetCatalogName(),
                    false,
                    request.TimeoutSecs,
                    response.SQL,
                    response.Parameters,
                    null);

                //Execute the command
                if (request.Context.IsError)
                    return null;
                reader = this.ExecuteReader(
                    command,
                    request.Transaction as DatabaseTransaction);
                if (request.Context.IsError)
                    return null;

                //Populate objects
                List<IEleflexPersistenceObject> list = PopulateObjects(request, reader);
                if (request.Context.IsError)
                    return null;

                //Business rule processing
                request.EPO.EPOTriggerPostGet(request, list);
                if (request.Context.IsError)
                    return null;

                //Return result
                PersistenceResponseItems<IEleflexPersistenceObject> responseList = new PersistenceResponseItems<IEleflexPersistenceObject>();
                responseList.Items = list;
                return responseList;
            }
            catch (Exception ie)
            {
                //Log exception and set error
                if (command != null)
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_SQLException2, command.Sql, ie.ToString()));
                else
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_Exception1, ie.ToString()));

                return null;
            }
            finally
            {
                //Cleanup
                if (reader != null)
                    reader.Dispose();
            }
        }

        /// <summary>
        /// Execute a statement to return an aggregated number.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override IPersistenceResponseItem<double> GetAggregate(IPersistenceRequest request)
        {
            const string methodName = CLASSNAME + "GetAggregate";

            //Validate input
            base.GetAggregate(request);
            if (request == null || request.Context == null || request.Context.IsError)
                return null;
            DatabaseCommand command = null;
            try
            {
                //Business rule processing
                request.EPO.EPOTriggerPreGetAggregate(request);
                if (request.Context.IsError)
                    return null;

                //Get sql generator
                IDatabaseFilterToSQLGenerator sqlGenerator = this.GetSQLGenerator();
                if (sqlGenerator == null)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_ISQLGeneratorNotFound));
                    return null;
                }

                //Get the sql statement
                IDatabaseSQLResponse response = sqlGenerator.GetSelectStatement(request);
                if (request.Context.IsError)
                    return null;

                //Get the command
                command = new DatabaseCommand(
                    request.EPO.EPOGetCatalogName(),
                    false,
                    request.TimeoutSecs,
                    response.SQL,
                    response.Parameters,
                    null);

                //Execute the command
                if (request.Context.IsError)
                    return null;
                object result = this.ExecuteScaler(
                    command,
                    request.Transaction as DatabaseTransaction);
                if (request.Context.IsError)
                    return null;

                //Return the item
                if (result == null || result == DBNull.Value)
                    return new PersistenceResponseItem<double>(0);

                PersistenceResponseItem<double> resp = new PersistenceResponseItem<double>(Convert.ToDouble(result));

                //Business rule processing
                request.EPO.EPOTriggerPostGetAggregate(request, resp.Item);
                if (request.Context.IsError)
                    return null;

                return resp;
            }
            catch (Exception ie)
            {
                //Log exception and set error
                if (command != null)
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_SQLException2, command.Sql, ie.ToString()));
                else
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_Exception1, ie.ToString()));                

                return null;
            }
        }

        /// <summary>
        /// Execute a get statement returning one item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override IPersistenceResponseItem<IEleflexPersistenceObject> GetItem(IPersistenceRequest request)
        {
            const string methodName = CLASSNAME + "GetItem";

            //Validate input
            base.GetItem(request);
            if (request == null || request.Context == null || request.Context.IsError)
                return null;

            try
            {
                //We remove the incoming where filters and replace with only the record's pk values
                List<IPersistenceFilter> scrubbedFilters = request.Filters.FindAll(delegate(IPersistenceFilter filter)
                {
                    return !filter.IsWhereClause;
                });
                request.Filters = scrubbedFilters;

                //Create a filter for the record's pk values
                List<IEleflexProperty> properties = request.EPO.EleflexGetProperties();
                List<IEleflexProperty> primaryKeys = properties.FindAll(delegate(IEleflexProperty property)
                {
                    if (property is IEBOProperty)
                        return (property as IEBOProperty).IsKey;
                    else
                        return false;
                });
                foreach (IEleflexProperty property in primaryKeys)
                {
                    IEleflexDataType value;
                    if (!request.EPO.EleflexGetPropertyValue(property.Name, out value))
                    {
                        request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_PropertyGet1, property.Name));
                        return null;
                    }
                    request.Filters.Add(
                        new FilterCompare(property, ComparisonEnum.EQUAL, value));
                }

                //Call GetItems
                IPersistenceResponseItems<IEleflexPersistenceObject> respGet = GetItems(request);
                if (request.Context.IsError)
                    return null;

                //Return response
                if (respGet.Items.Count > 0)
                    return new PersistenceResponseItem<IEleflexPersistenceObject>(respGet.Items[0]);
                return new PersistenceResponseItem<IEleflexPersistenceObject>(null);
            }
            catch (Exception ie)
            {
                //Log exception and set error
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_Exception1, ie.ToString()));
                return null;
            }
        }

        /// <summary>
        /// Execute a delete statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override IPersistenceResponseItem<int> Delete(IPersistenceRequest request)
        {
            const string methodName = CLASSNAME + "Delete";

            //Validate input
            base.Delete(request);
            if (request == null || request.Context == null || request.Context.IsError)
                return null;
            DatabaseCommand command = null;
            try
            {
                //Create a filter for the record's pk values
                request.Filters = new List<IPersistenceFilter>();
                List<IEleflexProperty> properties = request.EPO.EleflexGetProperties();
                List<IEleflexProperty> primaryKeys = properties.FindAll(delegate(IEleflexProperty property)
                {
                    if (property is IEBOProperty)
                        return (property as IEBOProperty).IsKey;
                    else
                        return false;
                });
                foreach (IEleflexProperty property in primaryKeys)
                {
                    IEleflexDataType value;
                    if (!request.EPO.EleflexGetPropertyValue(property.Name, out value))
                    {
                        request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_PropertyGet1, property.Name));
                        return null;
                    }
                    request.Filters.Add(
                        new FilterCompare(property, ComparisonEnum.EQUAL, value));
                }

                //Business rule processing
                request.EPO.EPOTriggerPreDelete(request);
                if (request.Context.IsError)
                    return null;

                //Get sql generator
                IDatabaseFilterToSQLGenerator sqlGenerator = this.GetSQLGenerator();
                if (sqlGenerator == null)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_ISQLGeneratorNotFound));
                    return null;
                }

                //Get the sql statement
                IDatabaseSQLResponse response = sqlGenerator.GetDeleteStatement(request);
                if (request.Context.IsError)
                    return null;

                //Get the command
                command = new DatabaseCommand(
                    request.EPO.EPOGetCatalogName(),
                    false,
                    request.TimeoutSecs,
                    response.SQL,
                    response.Parameters,
                    null);

                //Execute the command
                if (request.Context.IsError)
                    return null;
                int rowsAffected = this.ExecuteNonQuery(
                    command,
                    request.Transaction as DatabaseTransaction);
                if (request.Context.IsError)
                    return null;

                //Business rule processing
                request.EPO.EPOTriggerPostDelete(request, rowsAffected);
                if (request.Context.IsError)
                    return null;

                //Return the response
                return new PersistenceResponseItem<int>(rowsAffected);
            }
            catch (Exception ie)
            {
                //Log exception and set error
                if (command != null)
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, DatabaseConstants.SystemMessage_SQLException2, command.Sql, ie.ToString()));
                else
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_Exception1, ie.ToString()));                

                return null;
            }
        }

        /// <summary>
        /// Populate EPO objects from the execution response.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="reader"></param>
        /// <returns></returns>
        public virtual List<IEleflexPersistenceObject> PopulateObjects(
            IPersistenceRequest request,
            IDataReader reader)
        {
            //const string methodName = CLASSNAME + "PopulateObjects";

            //Determine our start and ending records            
            int startRecord = 1;
            int lastRecord = request.NumberPerPage;
            if (request.NumberPerPage > 0 && request.StartPage > 0)
            {
                startRecord = request.NumberPerPage * (request.StartPage - 1) + 1;
                lastRecord = (startRecord - 1) + request.NumberPerPage;
            }
            else
            {
                startRecord = 1;
                lastRecord = request.NumberPerPage;
            }

            //Get the list of select filters                     
            List<IPersistenceFilter> selectFilters = request.Filters.FindAll(delegate(IPersistenceFilter filter)
            { return filter.IsSelectClause && !filter.IsExpression; });

            //Get a list of properties we will be populating
            List<IEleflexProperty> selectProperties = new List<IEleflexProperty>();
            foreach (IPersistenceFilter filter in selectFilters)
                selectProperties.Add(filter.Properties[0]);

            //Find properties used for faster setting of data to object, rather than using EleflexSetPropertyValue() method
            IEleflexPersistenceObject tempItem = request.EPO.EleflexCreate() as IEleflexPersistenceObject;            
            List<IEleflexProperty> allProps = tempItem.EleflexGetProperties();
            List<IEleflexProperty> propsNotSelected = new List<IEleflexProperty>();
            foreach (IEleflexProperty allProp in allProps)
            {
                bool found = false;
                foreach (IEleflexProperty selectProp in selectProperties)
                {
                    if (selectProp.Name == allProp.Name)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    propsNotSelected.Add(allProp);
            }            
            Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> tempStorage = tempItem.EleflexGetStorage();

            //Iterate through reader records
            List<IEleflexPersistenceObject> list = new List<IEleflexPersistenceObject>();
            int recordCount = 0;
            while (reader.Read())
            {
                //Increment record count
                recordCount++;

                //Determine if this is a valid record to store
                if (recordCount >= startRecord)
                {
                    if (recordCount > lastRecord)
                        break;
                }
                else
                    continue;

                //Create a new instance needed to populate the values
                IEleflexPersistenceObject item = request.EPO.EleflexCreate() as IEleflexPersistenceObject;
                Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> storage = new Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>>();                

                //Populate EPO object
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    if (i >= selectProperties.Count)
                        break; //Don't error if more data is returned than we expect

                    object value = reader.GetValue(i);
                    if (value == null || value == DBNull.Value)
                    {
                        //Null values can be returned when left-joining results, resulting in
                        //an incompatibility between the allowable values of a datatype, hence
                        //why the value is not explicitly set.

                        //Set the dafault value for the object regardless so it is contained in storage
                        storage.Add(selectProperties[i].Name, new Tuple<IEleflexDataType, IEleflexProperty>(tempStorage[selectProperties[i].Name].Item1, selectProperties[i]));
                        continue;
                    }
                    else
                    {
                        IEleflexDataType frameworkValue = this.GetFrameworkValue(selectProperties[i], value);
                        storage.Add(selectProperties[i].Name, new Tuple<IEleflexDataType, IEleflexProperty>(frameworkValue, selectProperties[i]));                        
                    }
                }

                //Set all remainder props not selected
                foreach (IEleflexProperty prop in propsNotSelected)
                {                    
                    storage.Add(prop.Name, new Tuple<IEleflexDataType, IEleflexProperty>(tempStorage[prop.Name].Item1, prop));
                }
                item.EleflexSetStorage(storage);

                //Add EPO to list
                list.Add(item);
            }

            //Return EPO list
            return list;
        }

        /// <summary>
        /// Get a framework value from the database return data type.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IEleflexDataType GetFrameworkValue(IEleflexProperty property, object value)
        {
            const string methodName = CLASSNAME + "GetFrameworkValue";

            switch (property.DataTypeName)
            {
                default:
                    throw new Exception("EleflexDataTypeConstant " + property.DataTypeName + " not found");
                case EleflexDataTypeConstant.ByteArray:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null byte array " + property.Name);
                    return new ValueByteArray((byte[])value);
                case EleflexDataTypeConstant.ByteArrayNull:
                    if (value == null || value == DBNull.Value)
                        return new ValueByteArrayNull(null);
                    else
                        return new ValueByteArrayNull((byte[])value);
                case EleflexDataTypeConstant.Boolean:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null bool " + property.Name);
                    return new ValueBoolean(Convert.ToBoolean(value));
                case EleflexDataTypeConstant.BooleanNull:
                    if (value == null || value == DBNull.Value)
                        return new ValueBooleanNull(null);
                    else
                        return new ValueBooleanNull(Convert.ToBoolean(value));
                case EleflexDataTypeConstant.String:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null string " + property.Name);
                    return new ValueString(Convert.ToString(value));
                case EleflexDataTypeConstant.StringNull:
                    if (value == null || value == DBNull.Value)
                        return new ValueStringNull(null);
                    else
                        return new ValueStringNull(Convert.ToString(value));
                case EleflexDataTypeConstant.Byte:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null byte " + property.Name);
                    return new ValueByte(Convert.ToByte(value));
                case EleflexDataTypeConstant.ByteNull:
                    if (value == null || value == DBNull.Value)
                        return new ValueByteNull(null);
                    return new ValueByteNull(Convert.ToByte(value));
                case EleflexDataTypeConstant.DateTime:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null datetime " + property.Name);
                    return new ValueDateTime(Convert.ToDateTime(value));
                case EleflexDataTypeConstant.DateTimeNull:
                    if (value == null || value == DBNull.Value)
                        return new ValueDateTimeNull(null);
                    else
                        return new ValueDateTimeNull(Convert.ToDateTime(value));
                case EleflexDataTypeConstant.Single:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null Single " + property.Name);
                    return new ValueSingle(Convert.ToSingle(value));
                case EleflexDataTypeConstant.SingleNull:
                    if (value == null || value == DBNull.Value)
                        return new ValueSingleNull(null);
                    else
                        return new ValueSingleNull(Convert.ToSingle(value));
                case EleflexDataTypeConstant.Decimal:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null decimal " + property.Name);
                    return new ValueDecimal(Convert.ToDecimal(value));
                case EleflexDataTypeConstant.DecimalNull:
                    if (value == null || value == DBNull.Value)
                        return new ValueDecimalNull(null);
                    else
                        return new ValueDecimalNull(Convert.ToDecimal(value));
                case EleflexDataTypeConstant.Double:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null double " + property.Name);
                    return new ValueDouble(Convert.ToDouble(value));
                case EleflexDataTypeConstant.DoubleNull:
                    if (value == null || value == DBNull.Value)
                        return new ValueDoubleNull(null);
                    else
                        return new ValueDoubleNull(Convert.ToDouble(value));
                case EleflexDataTypeConstant.Int16:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null int16 " + property.Name);
                    return new ValueInt16(Convert.ToInt16(value));
                case EleflexDataTypeConstant.Int16Null:
                    if (value == null || value == DBNull.Value)
                        return new ValueInt16Null(null);
                    else
                        return new ValueInt16Null(Convert.ToInt16(value));
                case EleflexDataTypeConstant.Int32:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null Int32 " + property.Name);
                    return new ValueInt32(Convert.ToInt32(value));
                case EleflexDataTypeConstant.Int32Null:
                    if (value == null || value == DBNull.Value)
                        return new ValueInt32Null(null);
                    else
                        return new ValueInt32Null(Convert.ToInt32(value));
                case EleflexDataTypeConstant.Int64:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null Int64 " + property.Name);
                    return new ValueInt64(Convert.ToInt64(value));
                case EleflexDataTypeConstant.Int64Null:
                    if (value == null || value == DBNull.Value)
                        return new ValueInt64Null(null);
                    else
                        return new ValueInt64Null(Convert.ToInt64(value));
                case EleflexDataTypeConstant.Guid:
                    if (value == null || value == DBNull.Value)
                        throw new Exception(methodName + "Cannot convert null to non-null Guid " + property.Name);
                    return new ValueGuid((Guid)value);
                case EleflexDataTypeConstant.GuidNull:
                    if (value == null || value == DBNull.Value)
                        return new ValueGuidNull(null);
                    else
                        return new ValueGuidNull((Guid)value);
            }
        }

    }
}

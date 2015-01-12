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
using System.Text;
using Eleflex.Storage.Database.Filters;
using Eleflex.Storage.Database.Values;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a generator producing a compliant SQL statement based on its implemented language.
    /// </summary>
    public partial class TSQLDatabaseFilterGenerator : IDatabaseFilterToSQLGenerator
    {

        /// <summary>
        /// Constant used for denoting this class.
        /// </summary>
        public const string CLASSNAME = "PR.Eleflex.Persistence.Database.TSQL.TSQLDatabaseFilterGenerator" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Column alias prefix used for returning data from statements.
        /// </summary>
        private const string ColumnAliasPrefix = "a";

        /// <summary>
        /// Parameter alias prefix used for command parameters.
        /// </summary>
        private const string ParameterAliasPrefix = "p";

        /// <summary>
        /// The default prefix character for command parameters.
        /// </summary>
        private const string DefaultCommandParameterPrefix = "@";

        /// <summary>
        /// Prefix for database command parameters (SQLServer).
        /// </summary>
        private string _commandParameterPrefix = DefaultCommandParameterPrefix;
        

        /// <summary>
        /// Constructor.
        /// </summary>
        public TSQLDatabaseFilterGenerator() { }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandParameterPrefix"></param>
        public TSQLDatabaseFilterGenerator(string commandParameterPrefix)
        {
            _commandParameterPrefix = commandParameterPrefix;
        }


        /// <summary>
        /// Get a select statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IDatabaseSQLResponse GetSelectStatement(IPersistenceRequest request)
        {
            IDatabaseSQLResponse response = new DatabaseSQLResponse();            

            response.SQL = BuildSelectFragment(request);
            if (request.Context.IsError)
                return null;

            List<IPersistenceRelation> relations;
            response.SQL += BuildFromFragment(request, true, out relations);
            if (request.Context.IsError)
                return null;

            List<IDatabaseCommandParameter> commandParams;
            response.SQL += BuildWhereFragment(request, true, relations, out commandParams);
            if (request.Context.IsError)
                return null;
            response.Parameters = commandParams;

            response.SQL += BuildOrderByFragment(request);
            if (request.Context.IsError)
                return null;

            return response;            
        }

        /// <summary>
        /// Get an update statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IDatabaseSQLResponse GetUpdateStatement(IPersistenceRequest request)
        {
            //Input validated by DatabaseProvider
            DatabaseSQLResponse response = new DatabaseSQLResponse();

            List<IPersistenceRelation> relations;
            string from = BuildFromFragment(request, true, out relations);

            List<IDatabaseCommandParameter> commandParams;
            string where = BuildWhereFragment(request, true, relations, out commandParams);
            if (request.Context.IsError)
                return null;
            response.Parameters = commandParams;

            string update = BuildUpdateFragment(request, response.Parameters);
            if (request.Context.IsError)
                return null;            

            //validation
            if (response.Parameters.Count == 0)
            {
                const string methodName = CLASSNAME + "GetUpdateStatement";
                request.Context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_FilterUpdatePropertiesNotSpecified));
                return null;
            }

            //string was assembled backwards, assemble correctly
            response.SQL = update + from + where;

            return response;
        }

        /// <summary>
        /// Get a insert statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IDatabaseSQLResponse GetInsertStatement(IPersistenceRequest request)
        {
            DatabaseSQLResponse response = new DatabaseSQLResponse();

            List<IDatabaseCommandParameter> commandParams;
            response.SQL = BuildInsertFragment(request, out commandParams);
            if (request.Context.IsError)
                return null;

            //Validation
            if (commandParams.Count == 0)
            {
                const string methodName = CLASSNAME + "GetInsertStatement";
                request.Context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_FilterInsertPropertiesNotSpecified));
                return null;
            }
            response.Parameters = commandParams;

            return response;
        }

        /// <summary>
        /// Get a delete statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IDatabaseSQLResponse GetDeleteStatement(IPersistenceRequest request)
        {
            DatabaseSQLResponse response = new DatabaseSQLResponse();
            response.SQL = "DELETE ";

            List<IPersistenceRelation> relations;
            response.SQL += BuildFromFragment(request, false, out relations);
            if (request.Context.IsError)
                return null;

            List<IDatabaseCommandParameter> commandParams;
            string whereSQL = BuildWhereFragment(request, false, relations, out commandParams);
            if (request.Context.IsError)
                return null;
            response.Parameters = commandParams;            

            response.SQL += whereSQL;

            return response;
        }

        /// <summary>
        /// Build the select fragment.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual string BuildSelectFragment(IPersistenceRequest request)
        {
            const string methodName = CLASSNAME + "BuildSelectFragment";

            //Begin select statement
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT ");

            IEleflexDatabaseObject dto = request.EPO as IEleflexDatabaseObject;
            if (dto == null)
            {
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }

            if (request.Filters == null || request.Filters.Count==0)
            {                
                request.Context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_FilterSelectPropertiesNotSpecified));
                return null;
            }

            //Find select clause filters
            List<IPersistenceFilter> selectFilters = request.Filters.FindAll(delegate(IPersistenceFilter filter)
            { return filter.IsSelectClause && !filter.IsExpression; });

            //No select filters
            if (selectFilters.Count == 0)
            {
                request.Context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_FilterSelectPropertiesNotSpecified));
                return null;
            }

            //Find select clause filters that are expressions
            List<IPersistenceFilter> selectExpressionFilters = request.Filters.FindAll(delegate(IPersistenceFilter filter)
            { return filter.IsSelectClause && filter.IsExpression; });
            if (selectExpressionFilters.Count > 0)
            {
                for (int i = 0; i < selectExpressionFilters.Count; i++)
                {
                    IDatabaseFilter filter = GetSQLStorageFilter(request.Context, selectExpressionFilters[i]);
                    if (request.Context.IsError)
                        return null;
                    sql.Append(filter.GetSQLString(request.Context,null,null));
                }
            }
            
            //Iterate over select filters
            for (int filterCount = 0; filterCount < selectFilters.Count; filterCount++)
            {
                IDatabaseFilter filter = GetSQLStorageFilter(request.Context, selectFilters[filterCount]);
                if (request.Context.IsError)
                    return null;

                //Add seperator
                if (filterCount > 0)
                    sql.Append(",");

                //Get column name replacements
                List<string> columnNames = GetFilterReplacementNames(filter.Properties, true);

                //Get filter select string
                string tempSql = filter.GetSQLString(
                    request.Context, 
                    columnNames, 
                    new List<string>{ColumnAliasPrefix + filterCount.ToString()});
                if (request.Context.IsError)
                    return null;
                sql.Append(tempSql);
            }

            return sql.ToString();
        }

        /// <summary>
        /// Build the where fragment.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="useAlias"></param>
        /// <param name="relations"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected virtual string BuildWhereFragment(
            IPersistenceRequest request,
            bool useAlias,
            List<IPersistenceRelation> relations,
            out List<IDatabaseCommandParameter> parameters)
        {
            const string methodName = CLASSNAME + "BuildWhereFragment";

            //Parameter validation
            parameters = new List<IDatabaseCommandParameter>();
            if (request.Filters == null || request.Filters.Count == 0)
                return string.Empty;

            //Find where filters
            List<IPersistenceFilter> whereFilters = request.Filters.FindAll(delegate(IPersistenceFilter filter)
            { return filter.IsWhereClause; });
            if (whereFilters.Count == 0)
                return string.Empty;

            IEleflexDatabaseObject dto = request.EPO as IEleflexDatabaseObject;
            if (dto == null)
            {
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }

            //Build sql statement
            StringBuilder sql = new StringBuilder();
            sql.Append(" WHERE ");

            //Iterate through all the filters
            string paramDatabasePrefix = _commandParameterPrefix;

            bool joinExpressionNeeded = false;
            const string defaultJoinExpression = " AND ";
            bool groupingStarted = false;
            for (int filterCount = 0; filterCount < whereFilters.Count; filterCount++)
            {
                //Item
                IDatabaseFilter filter = GetSQLStorageFilter(request.Context, whereFilters[filterCount]);
                if (request.Context.IsError)
                    return null;

                //Determine whether we need a join expression for subsequent statements
                if (joinExpressionNeeded)
                {
                    if (filter.IsExpression)
                    {
                        if (filter.IsGroupingExpression && !filter.IsGroupingStartExpression)
                            groupingStarted = false;

                        joinExpressionNeeded = false;

                        //Write and continue
                        sql.Append(filter.GetSQLString(request.Context, null, null));
                        continue;
                    }
                    else
                    {
                        //We are forgiving and add a " And " expression. Document this.
                        sql.Append(defaultJoinExpression);
                        joinExpressionNeeded = true;
                        //Keep processing this filter...
                    }
                }
                else
                {
                    if (filter.IsJoinExpression)
                    {
                        //Write and continue
                        sql.Append(filter.GetSQLString(request.Context, null, null));
                        continue;
                    }
                    joinExpressionNeeded = true;
                }

                if (filter.IsGroupingExpression)
                {
                    groupingStarted = filter.IsGroupingStartExpression;
                    if (filter.IsGroupingStartExpression)
                        joinExpressionNeeded = false;
                    else
                        joinExpressionNeeded = true;

                    //Write and continue
                    sql.Append(filter.GetSQLString(request.Context, null, null));
                    continue;
                }

                //Assemble column and parameter names for generating the filter's sql
                List<string> parameterNames = new List<string>();

                //Get column name replacements
                List<string> columnNames = GetFilterReplacementNames(filter.Properties, useAlias);
                if (request.Context.IsError)
                    return null;

                //Create substitute names for the parameters               
                if (filter.Values.Count > 0)
                {
                    for (int i = 0; i < filter.Values.Count; i++)
                    {
                        //Create parameter and reset name, we are simply using a numeric count
                        //for the new alias name.
                        DatabaseCommandParameter param = GetDatabaseCommandParameter(
                            request.Context,
                            request.EPO,
                            filter.Properties[0] as IEDOProperty, 
                            filter.Values[i]);
                        if (request.Context.IsError)
                            return null;
                        param.Name = paramDatabasePrefix + ParameterAliasPrefix + parameters.Count.ToString();

                        //Add parameter name
                        parameterNames.Add(param.Name);

                        //Add output parameter
                        parameters.Add(param);
                    }
                }

                //Get the filer's sql with replacement values
                sql.Append(filter.GetSQLString(request.Context, columnNames, parameterNames));
                if (request.Context.IsError)
                    return null;
            }

            //We are forgiving and close the expression. Document this.
            if (groupingStarted)
                sql.Append(" ) ");

            return sql.ToString();
        }

        /// <summary>
        /// Build the order by fragment.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        protected virtual string BuildOrderByFragment(IPersistenceRequest request)
        {
            const string methodName = CLASSNAME + "BuildOrderByFragment";

            if (request.Filters == null || request.Filters.Count == 0)
                return string.Empty;

            IEleflexDatabaseObject dto = request.EPO as IEleflexDatabaseObject;
            if (dto == null)
            {
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }

            //Find sort filters
            List<IPersistenceFilter> sortFilters = request.Filters.FindAll(delegate(IPersistenceFilter filter)
            { return filter.IsSortClause; });
            if (sortFilters.Count == 0)
                return string.Empty;

            //Find select filters that are groupings (excludes use of order by clause)
            List<IPersistenceFilter> selectFilters = request.Filters.FindAll(delegate(IPersistenceFilter filter)
            { return filter.IsSelectClause && filter.IsGroupingExpression; });
            if (selectFilters.Count > 0)
                return string.Empty;

            //Begin building sql string
            string tempSQL = " ORDER BY ";

            //Iterate through sort filters
            for (int filterCount = 0; filterCount < sortFilters.Count; filterCount++)
            {
                //Add seperator for subsequent filters
                if (filterCount > 0)
                    tempSQL += " , ";

                //Find actual database names for column names
                IDatabaseFilter filter = GetSQLStorageFilter(request.Context, sortFilters[filterCount]);
                if (request.Context.IsError)
                    return null;

                List<string> columnNames = GetFilterReplacementNames(filter.Properties, true);

                //Get the sql fragment
                tempSQL += filter.GetSQLString(request.Context, columnNames, null);
                if (request.Context.IsError)
                    return null;                
            }
            return tempSQL;
        }

        /// <summary>
        /// Build the update fragment.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected virtual string BuildUpdateFragment(
            IPersistenceRequest request,
            List<IDatabaseCommandParameter> parameters)
        {
            const string methodName = CLASSNAME + "BuildUpdateFragment";

            IEleflexDatabaseObject dto = request.EPO as IEleflexDatabaseObject;
            if (dto == null)
            {
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }

            if (request.Filters == null || request.Filters.Count == 0)
            {
                request.Context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_FilterUpdatePropertiesNotSpecified));
                return null;
            }

            List<IPersistenceFilter> updateFilters = request.Filters.FindAll(delegate(IPersistenceFilter filter)
            { return filter.IsUpdateClause; });

            if (updateFilters.Count == 0)
            {
                request.Context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_FilterUpdatePropertiesNotSpecified));
                return null;
            }

            List<IPersistenceFilter> whereFilters = request.Filters.FindAll(delegate(IPersistenceFilter filter)
            { return filter.IsWhereClause; });

            List<IEleflexProperty> properties = request.EPO.EleflexGetProperties();
            List<IEleflexProperty> validUpdateProperties = properties.FindAll(delegate(IEleflexProperty property)
            {
                if (property is IEPOProperty)
                {
                    //We do not support computed key updating
                    if (property is IEBOProperty)
                        return !(property as IEBOProperty).IsComputed;
                    else
                        return true;
                }
                return false;
            });

            StringBuilder sql = new StringBuilder();

            //Begin update statement
            sql.Append("UPDATE [");
            sql.Append(dto.EDOGetSchema().Name);
            sql.Append(dto.EDOGetName());
            sql.Append("] SET ");

            string paramDatabasePrefix = _commandParameterPrefix;

            //Iterate over update filters
            for (int filterCount = 0; filterCount < updateFilters.Count; filterCount++)
            {
                IDatabaseFilter filter = GetSQLStorageFilter(request.Context, updateFilters[filterCount]);
                if (filter == null)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterNull));
                    return null;
                }

                //Make sure it is a valid property
                bool isValidProperty = false;
                for (int i = 0; i < validUpdateProperties.Count; i++)
                {
                    if (filter.Properties[0].ParentObject.EleflexGetObjectKey() == validUpdateProperties[i].ParentObject.EleflexGetObjectKey() &&
                        filter.Properties[0].Name == validUpdateProperties[i].Name)
                    {
                        isValidProperty = true;
                        break;
                    }
                }
                if (!isValidProperty)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_PropertyInvalid1, filter.Properties[0].Name));
                    return null;
                }

                //Add seperator
                if (filterCount > 0)
                    sql.Append(",");

                //Get column name replacements
                List<string> columnNames = GetFilterReplacementNames(filter.Properties, true);
                List<string> paramNames = new List<string>(){
                    paramDatabasePrefix + ParameterAliasPrefix + (parameters.Count + filterCount).ToString()};

                //Get filter update string
                string tempSql = filter.GetSQLString(request.Context, columnNames, paramNames);
                if (request.Context.IsError)
                    return null;
                sql.Append(tempSql);

                //Add command parameter
                IEleflexDataType value = null;
                if (!request.EPO.EleflexGetPropertyValue(filter.Properties[0].Name, out value))
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_PropertyGet1, filter.Properties[0].Name));
                    return null;
                }

                //Validate data                
                if (value != null)
                {
                    //Validate with property definition
                    SecurityContext mappingContext = new SecurityContext(null, filter.Properties[0].Name, request.Context.SecurityUserID);
                    bool success = PropertyValidator.Validate(mappingContext, filter.Properties[0] as IEBOProperty, value);
                    request.Context.AddContext(mappingContext);
                    if(!success)
                        return null;
                }

                DatabaseCommandParameter param = GetDatabaseCommandParameter(
                    request.Context,
                    request.EPO,
                    filter.Properties[0] as IEDOProperty,
                    value);
                if (request.Context.IsError)
                    return null;
                param.Name = paramDatabasePrefix + ParameterAliasPrefix + (parameters.Count + filterCount).ToString();
                parameters.Add(param);
            }

            //Return sql fragment
            return sql.ToString();
        }

        /// <summary>
        /// Build the insert fragment.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected virtual string BuildInsertFragment(
            IPersistenceRequest request,
            out List<IDatabaseCommandParameter> parameters)
        {
            const string methodName = CLASSNAME + "BuildInsertFragment";

            parameters = new List<IDatabaseCommandParameter>();

            IEleflexDatabaseObject dto = request.EPO as IEleflexDatabaseObject;
            if (dto == null)
            {
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }

            StringBuilder sql = new StringBuilder();

            //Begin insert statement
            sql.Append("INSERT INTO [");
            sql.Append(dto.EDOGetSchema().Name);
            sql.Append("].[");
            sql.Append(dto.EDOGetName());
            sql.Append("] ( ");

            List<IEleflexProperty> properties = request.EPO.EleflexGetProperties();
            List<IEleflexProperty> validProperties = properties.FindAll(delegate(IEleflexProperty property)
            {
                if (property is IEDOProperty)
                {
                    if (property is IEBOProperty)
                        return !(property as IEBOProperty).IsComputed;
                    else
                        return true;
                }
                return false;
            });

            //Add column names to be inserted
            int colsAdded = 0;
            IEDOProperty prop = null;
            for (int i = 0; i < validProperties.Count; i++)
            {
                prop = validProperties[i] as IEDOProperty;
                if (colsAdded > 0)
                    sql.Append(", ");
                colsAdded++;

                sql.Append("[");
                sql.Append(prop.DatabasePropertyName);
                sql.Append("]");
            }

            //Add parameter names            
            sql.Append(" ) VALUES ( ");
            colsAdded = 0;
            foreach (IEleflexProperty insertProperty in validProperties)
            {
                if (colsAdded > 0)
                    sql.Append(", ");
                colsAdded++;

                //Get the value
                IEleflexDataType value = null;
                if (!request.EPO.EleflexGetPropertyValue(insertProperty.Name, out value))
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_PropertyGet1, insertProperty.Name));
                    return null;
                }

                //We know they accept null...
                string paramDatabasePrefix = _commandParameterPrefix;

                if (value != null)
                {
                    //Validate with property definition
                    SecurityContext mappingContext = new SecurityContext(null, insertProperty.Name, request.Context.SecurityUserID);
                    if (!PropertyValidator.Validate(mappingContext, insertProperty as IEBOProperty, value))
                        return null;
                }

                //Build the command parameter
                DatabaseCommandParameter param = GetDatabaseCommandParameter(
                    request.Context,
                    request.EPO,
                    insertProperty as IEDOProperty, 
                    value);
                if (request.Context.IsError)
                    return null;
                param.Name = paramDatabasePrefix + ParameterAliasPrefix + parameters.Count.ToString();
                parameters.Add(param);
                sql.Append(param.Name);
            }

            sql.Append(" )");

            return sql.ToString();
        }

        /// <summary>
        /// Build the from fragment.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="useAlias"></param>
        /// <param name="relations"></param>
        /// <returns></returns>
        protected virtual string BuildFromFragment(IPersistenceRequest request, bool useAlias, out List<IPersistenceRelation> relations)
        {
            const string methodName = CLASSNAME + "BuildFromFragment";

            relations = new List<IPersistenceRelation>();
            IEleflexDatabaseObject dto = request.EPO as IEleflexDatabaseObject;
            if (dto == null)
            {
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append(" FROM [");
            sql.Append(dto.EDOGetSchema().Name);
            sql.Append("].[");
            sql.Append(dto.EDOGetName());
            sql.Append("] ");
            if (useAlias)
            {
                sql.Append("AS [");
                sql.Append(dto.EDOGetSchema().Name);
                sql.Append(dto.EDOGetName());
                sql.Append("] ");
            }

            if (request.Filters == null || request.Filters.Count == 0)
                return sql.ToString();

            List<IPersistenceFilter> fromFilters = request.Filters.FindAll(delegate(IPersistenceFilter filter)
            {
                return filter.IsFromClause;
            });
            if (fromFilters.Count == 0)
                return sql.ToString();
            
            for (int i = 0; i < fromFilters.Count; i++)
            {
                IDatabaseFilter filter = GetSQLStorageFilter(request.Context, fromFilters[i]);
                if (request.Context.IsError)
                    return null;

                //Currently only support Relation in from clause
                Relation relation = filter as Relation;
                if (relation == null)
                {
                    request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterNull));
                    return null;
                }

                relations.Add(relation.Relationship);
                List<string> replacementNames = new List<string>();
                List<string> tempReplacements = null;
                IEleflexDatabaseObject tempEPO = null;
                List<IEleflexProperty> curProps = relation.Relationship.EPRGetParentProperties();
                List<IEleflexProperty> fkProps = relation.Relationship.EPRGetForeignProperties();                

                //Get base table
                //Relation property may not contain all info. Get actual property from EPO.
                tempEPO = curProps[0].ParentObject as IEleflexDatabaseObject;
                List<IEleflexProperty> tempProps = tempEPO.EleflexGetProperties();
                for (int j = 0; j < curProps.Count; j++)
                {
                    foreach(IEleflexProperty tempProp in tempProps)
                    {
                        if (curProps[j].Name == tempProp.Name)
                        {
                            curProps[j] = tempProp;
                            break;
                        }
                    }
                }
                tempReplacements = GetFilterReplacementNames(curProps, true);
                replacementNames.AddRange(tempReplacements);

                //Get foreign table
                //Relation property may not contain all info. Get actual property from EPO.
                tempEPO = fkProps[0].ParentObject as IEleflexDatabaseObject;
                tempProps = tempEPO.EleflexGetProperties();
                for (int j = 0; j < fkProps.Count; j++)
                {
                    foreach (IEleflexProperty tempProp in tempProps)
                    {
                        if (fkProps[j].Name == tempProp.Name)
                        {
                            fkProps[j] = tempProp;
                            break;
                        }
                    }
                }
                tempReplacements = GetFilterReplacementNames(fkProps, true);
                replacementNames.AddRange(tempReplacements);

                sql.Append(filter.GetSQLString(request.Context, replacementNames, null));
                if (request.Context.IsError)
                    return null;
            }

            return sql.ToString();
        }

        /// <summary>
        /// Get a list of replacement names for the given properties.
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="useAlias"></param>
        /// <returns></returns>
        protected virtual List<string> GetFilterReplacementNames(
            List<IEleflexProperty> properties, 
            bool useAlias)
        {
            List<string> replacementNames = new List<string>();
            IEDOProperty property = null;
            for (int i = 0; i < properties.Count; i++)
            {
                property = properties[i] as IEDOProperty;                
                IEleflexDatabaseObject tempEPO = property.ParentObject as IEleflexDatabaseObject;
                string tempSQL = string.Empty;
                if(useAlias)
                {
                    tempSQL = "[" +
                        tempEPO.EDOGetSchema().Name +
                        tempEPO.EDOGetName() + 
                    "].";
                }

                tempSQL += "[" + property.DatabasePropertyName + "]";
                replacementNames.Add(tempSQL);
            }
            return replacementNames;
        }

        /// <summary>
        /// Get a command parameter.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dto"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual DatabaseCommandParameter GetDatabaseCommandParameter(
            IEleflexContext context,
            IEleflexPersistenceObject dto,
            IEDOProperty property,
            IEleflexDataType value)
        {
            const string methodName = CLASSNAME + "GetDatabaseCommandParameter";

            //EPOProperty has been casted
            if (property == null)
            {
                context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_PropertyNull));
                return null;
            }

            //Create parameter and set values
            DatabaseCommandParameter param = new DatabaseCommandParameter();
            string paramPrefix = _commandParameterPrefix;

            param.IProperty = property;
            param.Name = paramPrefix + property.Name;
            param.DbType = property.DbType;
            param.Value = value;
            return param;
        }

        /// <summary>
        /// Get the converted storage filter.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dataFilter"></param>
        /// <returns></returns>
        protected virtual IDatabaseFilter GetSQLStorageFilter(IEleflexContext context, IPersistenceFilter dataFilter)
        {
            const string methodName = CLASSNAME + "GetSQLStorageFilter";

            //Check if filter parameters are valid
            if (!dataFilter.IsValid)
                return null;

            //Check service filters first
            switch (dataFilter.FilterEnum)
            {
                default:
                case FilterEnum.NotSupported:
                    return null;
                case FilterEnum.Aggregate:
                    FilterAggregate aFilter = dataFilter as FilterAggregate;
                    if (aFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Aggregate.ToString()));
                        return null;
                    }
                    return new Aggregate(aFilter);
                case FilterEnum.Between:
                    FilterBetween bFilter = dataFilter as FilterBetween;
                    if (bFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Between.ToString()));
                        return null;
                    }
                    return new Between(bFilter);
                case FilterEnum.Compare:
                    FilterCompare cFilter = dataFilter as FilterCompare;
                    if (cFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Compare.ToString()));
                        return null;
                    }
                    return new Compare(cFilter);
                case FilterEnum.Distinct:
                    FilterDistinct dFilter = dataFilter as FilterDistinct;
                    if (dFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Distinct.ToString()));
                        return null;
                    }
                    return new Distinct();
                case FilterEnum.Expression:
                    FilterExpression eFilter = dataFilter as FilterExpression;
                    if (eFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Expression.ToString()));
                        return null;
                    }
                    return new Expression(eFilter);
                case FilterEnum.Like:
                    FilterLike lFilter = dataFilter as FilterLike;
                    if (lFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Like.ToString()));
                        return null;
                    }
                    return new Like(lFilter);
                case FilterEnum.Null:
                    FilterNull nFilter = dataFilter as FilterNull;
                    if (nFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Null.ToString()));
                        return null;
                    }
                    return new Null(nFilter);
                case FilterEnum.PropertyCompare:
                    FilterPropertyCompare pFilter = dataFilter as FilterPropertyCompare;
                    if (pFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.PropertyCompare.ToString()));
                        return null;
                    }
                    return new PropertyCompare(pFilter);
                case FilterEnum.Select:
                    FilterSelect selectFilter = dataFilter as FilterSelect;
                    if (selectFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Select.ToString()));
                        return null;
                    }
                    return new Select(selectFilter);
                case FilterEnum.Set:
                    FilterSet sFilter = dataFilter as FilterSet;
                    if (sFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Set.ToString()));
                        return null;
                    }
                    return new Set(sFilter);
                case FilterEnum.Sort:
                    FilterSort sortFilter = dataFilter as FilterSort;
                    if (sortFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Sort.ToString()));
                        return null;
                    }
                    return new Sort(sortFilter);
                case FilterEnum.Update:
                    FilterUpdate updateFilter = dataFilter as FilterUpdate;
                    if (updateFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Update.ToString()));
                        return null;
                    }
                    return new Update(updateFilter);
                case FilterEnum.Relation:
                    FilterRelation relationFilter = dataFilter as FilterRelation;
                    if (relationFilter == null)
                    {
                        context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterConversion1, FilterEnum.Relation.ToString()));
                        return null;
                    }
                    return new Relation(relationFilter);
            }
 
        }

    }
}

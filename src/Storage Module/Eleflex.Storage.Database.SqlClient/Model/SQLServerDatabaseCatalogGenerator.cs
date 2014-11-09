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
using System.Data;
using Eleflex.Storage.Database;
using Eleflex.Storage.Database.Values;

namespace Eleflex.Storage.Database.SqlClient
{

    /// <summary>
    /// TSQL language database catalog creation and inspection.
    /// </summary>
    public partial class SQLServerDatabaseCatalogGenerator : IDatabaseCatalogGenerator
    {

        /// <summary>
        /// Database provider.
        /// </summary>
        protected IDatabaseProvider _databaseProvider;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="catalogName"></param>
        /// <param name="connectionString"></param>
        public SQLServerDatabaseCatalogGenerator(string catalogName, string connectionString)
        {
            _databaseProvider = new SQLServerProvider(catalogName, connectionString);
        }

        /// <summary>
        /// Create the database catalog.
        /// </summary>
        /// <param name="catalog"></param>
        public virtual void CreateDatabaseCatalog(IDatabaseCatalog catalog)
        {
            //TODO
        }

        /// <summary>
        /// Get the .Net DbType based on the SQL Server specific data types.
        /// </summary>
        public virtual DbType GetDbTypeFromDataTypeName(string name)
        {
            switch (name.ToLower())
            {
                default:                
                case "text":
                case "xml":
                case "nchar":
                case "ntext":
                case "nvarchar":
                    return DbType.String;
                case "char":
                case "varchar":
                    return DbType.AnsiString;
                case "tinyint":
                    return DbType.Byte;
                case "bit":
                    return DbType.Boolean;
                case "datetime":
                case "smalldatetime":
                    return DbType.DateTime;
                case "numeric":
                case "decimal":
                case "money":
                case "smallmoney":
                    return DbType.Decimal;
                case "real":
                    return DbType.Double;
                case "float":
                    return DbType.Single;
                case "image":
                case "varbinary":
                case "sql_variant":
                case "binary":
                case "timestamp":
                    return DbType.Binary;
                case "uniqueidentifier":
                    return DbType.Guid;
                case "smallint":
                    return DbType.Int16;
                case "int":
                    return DbType.Int32;
                case "bigint":
                    return DbType.Int64;
            }
        }

        /// <summary>
        /// Get the catalog name.
        /// </summary>
        /// <returns></returns>
        public virtual IDatabaseCatalog GetDatabaseCatalog()
        {
            DatabaseCatalog catalog = new DatabaseCatalog();
            catalog.ConnectionString = _databaseProvider.GetConnectionString();

            IDataReader reader = null;
            try
            {
                IDbCommand command = _databaseProvider.CreateDbCommand();

                //Schemas
                command.CommandText = GetSchemasSQL;
                reader = command.ExecuteReader();
                catalog.Schemas = LoadSchemas(reader);
                reader.Dispose();

                //Data types
                command.CommandText = GetDataTypesSQL;
                reader = command.ExecuteReader();
                catalog.DataTypes = LoadDataTypes(reader);
                reader.Dispose();

                //Tables
                command.CommandText = GetTablesSQL;
                reader = command.ExecuteReader();
                catalog.DatabaseObjects = LoadTables(reader, catalog.Schemas, catalog.DataTypes);
                reader.Dispose();

                //Constraints
                command.CommandText = GetConstraintsSQL;
                reader = command.ExecuteReader();
                Dictionary<string,List<IDatabaseConstraint>> constraints = LoadConstraints(reader, catalog.DatabaseObjects);
                reader.Dispose();

                //Indexes
                command.CommandText = GetIndexesSQL;
                reader = command.ExecuteReader();
                Dictionary<string, List<IDatabaseIndex>> indexes = LoadIndexes(reader, catalog.DatabaseObjects);
                reader.Dispose();

                //FKs
                command.CommandText = GetForeignKeysSQL;
                reader = command.ExecuteReader();
                Dictionary<string, List<IDatabaseForeignKey>> foreignKeys = LoadForeignKeys(reader, catalog.DatabaseObjects);
                reader.Dispose();

                //Load extended attributes of tables
                LoadExtendedAttributes(catalog.DatabaseObjects, constraints, indexes, foreignKeys);

                command.CommandText = GetViewsSQL;
                reader = command.ExecuteReader();
                List<IEleflexDatabaseObject> views = LoadViews(reader, catalog.Schemas, catalog.DataTypes);
                catalog.DatabaseObjects.AddRange(views);
                reader.Dispose();

                command.CommandText = GetProceduresSQL;
                reader = command.ExecuteReader();
                List<IEleflexDatabaseObject> procs = LoadProcedures(reader, catalog.Schemas, catalog.DataTypes);
                catalog.DatabaseObjects.AddRange(procs);
                reader.Dispose();
                reader = null;
               
                return catalog;
            }
            catch (Exception ex) { throw ex; }
            finally
            {
                if (reader != null)
                    reader.Dispose();
            }
        }



        /// <summary>
        /// SQL to get list of schemas.
        /// </summary>
        public const string GetSchemasSQL = @"
SELECT 
    S.name AS NAME, 
    P.name AS OWNER 
FROM 
    SYS.SCHEMAS AS S
    INNER JOIN SYS.SCHEMAS AS P ON P.schema_id = S.principal_id
ORDER BY S.name
";

        /// <summary>
        /// Load schema objects.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected virtual List<IDatabaseSchema> LoadSchemas(IDataReader reader)
        {
            List<IDatabaseSchema> list = new List<IDatabaseSchema>();
            while (reader.Read())
            {
                IDatabaseSchema item = new DatabaseSchema();
                list.Add(item);
                item.Name = Convert.ToString(reader["NAME"]);
                item.Owner = Convert.ToString(reader["OWNER"]);
            }
            return list;
        }


        /// <summary>
        /// SQL to get list of data types.
        /// </summary>
        protected const string GetDataTypesSQL = @"
SELECT 
    name AS NAME,
    length AS MAX_LENGTH,
    ISNULL(prec, 0) AS PRECISION,
    ISNULL(scale, 0) AS SCALE,
    allownulls AS ALLOW_NULL,
    TYPE_NAME(xtype) AS PARENT_NAME
FROM 
    SYS.SYSTYPES
ORDER BY name
";

        /// <summary>
        /// Load data types.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected virtual List<IEDOProperty> LoadDataTypes(IDataReader reader)
        {
            List<IEDOProperty> list = new List<IEDOProperty>();

            string parentKey = "tempparentname";
            while (reader.Read())
            {
                IEDOProperty item = new EDOProperty();
                list.Add(item);
                item.DatabasePropertyName = Convert.ToString(reader["NAME"]);
                item.Name = item.DatabasePropertyName;
                item.MaxLength = Convert.ToInt32(reader["MAX_LENGTH"]);
                item.Precision = Convert.ToInt32(reader["PRECISION"]);
                item.Scale = Convert.ToInt32(reader["SCALE"]);
                item.IsNullable = Convert.ToBoolean(reader["ALLOW_NULL"]);
                item.DbType = GetDbTypeFromDataTypeName(item.Name);
                item.EleflexSetPropertyValue(parentKey,new ValueStringNull(Convert.ToString(reader["PARENT_NAME"])));                
            }

            foreach (IEDOProperty item in list)
            {
                IEleflexDataType dataType;
                item.EleflexGetPropertyValue(parentKey, out dataType);
                ValueStringNull parentName = dataType as ValueStringNull;

                if (string.IsNullOrEmpty(parentName.Value))
                    continue;

                foreach (IEDOProperty findItem in list)
                {
                    if (findItem.Name == parentName.Value)
                    {
                        item.ParentObject = findItem;
                        break;
                    }
                }
            }

            return list;
        }


        /// <summary>
        /// SQL to get list of tables.
        /// </summary>
        protected const string GetTablesSQL = @"
SELECT
    S.name AS SCHEMA_NAME,
    T.name AS TABLE_NAME,
    C.name AS COLUMN_NAME,
    TYPE_NAME(C.system_type_id) AS DATATYPE_NAME,
    C.column_id AS COLUMN_ORDER,    
    ISNULL(C.max_length,0) AS MAX_LENGTH,
    ISNULL(C.precision,0) AS PRECISION,
    ISNULL(C.scale,0) AS SCALE,
    C.is_nullable AS IS_NULLABLE,
    C.is_identity AS IS_IDENTITY
FROM
    SYS.TABLES AS T
    INNER JOIN SYS.SCHEMAS AS S ON S.schema_id = T.schema_id
    INNER JOIN SYS.COLUMNS AS C ON C.object_id = T.object_id
ORDER BY S.name, T.name, C.column_id
";


        /// <summary>
        /// Load tables.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="schemas"></param>
        /// <param name="dataTypes"></param>
        /// <returns></returns>
        protected virtual List<IEleflexDatabaseObject> LoadTables(IDataReader reader, List<IDatabaseSchema> schemas, List<IEDOProperty> dataTypes)
        {
            List<IEleflexDatabaseObject> tables = new List<IEleflexDatabaseObject>();
            EleflexDatabaseObject item = null;
            int columnOrdinal = 1;
            while (reader.Read())
            {
                //Get data
                string schemaName = Convert.ToString(reader["SCHEMA_NAME"]);
                string tableName = Convert.ToString(reader["TABLE_NAME"]);

                if (item == null || (item.EDOGetName() != tableName || item.EDOGetSchema().Name != schemaName))
                {
                    columnOrdinal = 1;
                    //Find schema                    
                    IDatabaseSchema tableSchema = new DatabaseSchema(schemaName,null,null);
                    foreach (IDatabaseSchema schema in schemas)
                    {
                        if (schema.Name == schemaName)
                        {
                            tableSchema = schema;
                            break;
                        }
                    }
                    item = new EleflexDatabaseObject(_databaseProvider.GetCatalogName(), tableSchema, tableName, DatabaseObjectType.Table);                        
                    tables.Add(item);
                }
                IEDOProperty col = LoadColumn(reader, dataTypes);
                col.ParentObject = item;
                item.EleflexSetPropertyValue(col.Name, EleflexDataTypeFactory.Create(col.DataTypeName), col);                
                columnOrdinal++;
            }

            return tables;
        }


        /// <summary>
        /// Load columns.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="dataTypes"></param>
        /// <returns></returns>
        protected virtual IEDOProperty LoadColumn(IDataReader reader, List<IEDOProperty> dataTypes)
        {
            EDOProperty item = new EDOProperty();
            item.Name = Convert.ToString(reader["COLUMN_NAME"]);
            string dataTypeName = Convert.ToString(reader["DATATYPE_NAME"]);
            foreach (IEDOProperty dataType in dataTypes)
            {
                if (dataType.Name == dataTypeName)
                {
                    item.DbType = dataType.DbType;
                    break;
                }
            }            
            item.Ordinal = Convert.ToInt32(reader["COLUMN_ORDER"]);
            item.MaxLength = Convert.ToInt32(reader["MAX_LENGTH"]);
            item.Precision = Convert.ToInt32(reader["PRECISION"]);
            item.Scale = Convert.ToInt32(reader["SCALE"]);
            item.IsNullable = Convert.ToBoolean(reader["IS_NULLABLE"]);
            item.IsComputed = Convert.ToBoolean(reader["IS_IDENTITY"]);

            if (item.DbType == DbType.String)
                item.MaxLength = item.MaxLength / 2;
            return item;
        }


        /// <summary>
        /// SQL to get list of views.
        /// </summary>
        protected const string GetViewsSQL = @"
SELECT
    S.name AS SCHEMA_NAME,
    V.name AS VIEW_NAME,
    C.name AS COLUMN_NAME,
    TYPE_NAME(C.system_type_id) AS DATATYPE_NAME,
    C.column_id AS COLUMN_ORDER,    
    ISNULL(C.max_length,0) AS MAX_LENGTH,
    ISNULL(C.precision,0) AS PRECISION,
    ISNULL(C.scale,0) AS SCALE,
    C.is_nullable AS IS_NULLABLE,
    C.is_identity AS IS_IDENTITY
FROM
    SYS.VIEWS AS V
    INNER JOIN SYS.SCHEMAS AS S ON S.schema_id = V.schema_id
    INNER JOIN SYS.COLUMNS AS C ON C.object_id = V.object_id
ORDER BY S.name, V.name, C.column_id
";


        /// <summary>
        /// Load views.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="schemas"></param>
        /// <param name="dataTypes"></param>
        /// <returns></returns>
        protected virtual List<IEleflexDatabaseObject> LoadViews(IDataReader reader, List<IDatabaseSchema> schemas, List<IEDOProperty> dataTypes)
        {
            List<IEleflexDatabaseObject> views = new List<IEleflexDatabaseObject>();
            EleflexDatabaseObject item = null;
            int columnOrdinal = 1;
            while (reader.Read())
            {
                //Get data
                string schemaName = Convert.ToString(reader["SCHEMA_NAME"]);
                string viewName = Convert.ToString(reader["VIEW_NAME"]);

                if (item == null || (item.EDOGetName() != viewName || item.EDOGetSchema().Name != schemaName))
                {
                    columnOrdinal = 1;
                    //Find schema                    
                    IDatabaseSchema viewSchema = new DatabaseSchema(schemaName,null,null);
                    foreach (IDatabaseSchema schema in schemas)
                    {
                        if (schema.Name == schemaName)
                        {
                            viewSchema = schema;
                            break;
                        }
                    }
                    item = new EleflexDatabaseObject(_databaseProvider.GetCatalogName(), viewSchema, viewName, DatabaseObjectType.View);
                    views.Add(item);
                }
                IEDOProperty col = LoadColumn(reader, dataTypes);
                col.ParentObject = item;
                item.EleflexSetPropertyValue(col.Name, EleflexDataTypeFactory.Create(col.DataTypeName), col);                
                columnOrdinal++;
            }
            return views;
        }


        /// <summary>
        /// SQL to get list of indexes.
        /// </summary>
        protected const string GetIndexesSQL = @"
SELECT
    S.name AS SCHEMA_NAME,
    T.name as TABLE_NAME,
    I.name AS INDEX_NAME,
    CASE WHEN I.type = 1 THEN 1 ELSE 0 END AS IS_CLUSTERED,
    IC.column_id AS COLUMN_ORDER,
    I.is_unique AS IS_UNIQUE,
	IC.index_column_id AS INDEX_ORDER,
	I.is_primary_key AS PRIMARY_KEY
FROM
	SYS.TABLES AS T
	INNER JOIN SYS.SCHEMAS AS S ON S.schema_id = T.schema_id
	INNER JOIN SYS.INDEXES AS I ON I.object_id = T.object_id
	INNER JOIN SYS.INDEX_COLUMNS AS IC ON IC.object_id = I.object_id AND I.index_id = IC.index_id
ORDER BY S.name, T.name, I.name, IC.index_column_id
";


        /// <summary>
        /// Load indexes.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="tables"></param>
        protected virtual Dictionary<string, List<IDatabaseIndex>> LoadIndexes(IDataReader reader, List<IEleflexDatabaseObject> tables)
        {
            Dictionary<string, List<IDatabaseIndex>> list = new Dictionary<string, List<IDatabaseIndex>>();
            DatabaseIndex item = null;
            while (reader.Read())
            {
                string indexName = Convert.ToString(reader["INDEX_NAME"]);
                string schemaName = Convert.ToString(reader["SCHEMA_NAME"]);
                string tableName = Convert.ToString(reader["TABLE_NAME"]);
                bool IsPK = Convert.ToBoolean(reader["PRIMARY_KEY"]);
                int columnOrder = Convert.ToInt32(reader["COLUMN_ORDER"]);
                if (IsPK)
                {
                    foreach (IEleflexDatabaseObject edo in tables)
                    {
                        if (edo.EDOGetSchema().Name == schemaName && edo.EDOGetName() == tableName)
                        {
                            List<IEDOProperty> props = edo.EDOGetProperties();
                            foreach (IEDOProperty prop in props)
                            {
                                if (prop.Ordinal == columnOrder)
                                {
                                    prop.IsKey = true;
                                    edo.EleflexSetPropertyValue(prop.Name, EleflexDataTypeFactory.Create(prop.DataTypeName), prop);
                                    break;
                                }
                            }
                        }
                    }
                    continue;
                }
                if (item == null || item.Name != indexName)
                {
                    item = new DatabaseIndex();
                    item.Name = indexName;
                    item.IsClustered = Convert.ToBoolean(reader["IS_CLUSTERED"]);
                    item.IsUnique = Convert.ToBoolean(reader["IS_UNIQUE"]);                    
                    string objectName = _databaseProvider.GetCatalogName() + EleflexProperty.Field_Seperator + schemaName + EleflexProperty.Field_Seperator + tableName + EleflexProperty.Field_Seperator;

                    foreach(IEleflexDatabaseObject table in tables)
                    {
                        if(table.EDOGetName() == tableName && table.EDOGetSchema().Name == schemaName)
                        {
                            item.Parent = table;
                            break;
                        }
                    }

                    if (!list.ContainsKey(objectName))
                        list.Add(objectName, new List<IDatabaseIndex>() { item });
                    else
                        list[objectName].Add(item);
                }                

                if (item.Parent != null)
                {
                    List<IEleflexProperty> properties = item.Parent.EleflexGetProperties();
                    foreach (IEDOProperty column in properties)
                    {
                        if (column.Ordinal == columnOrder)
                        {
                            item.Columns.Add(column);
                            break;
                        }
                    }
                }

            }
            return list;
        }


        /// <summary>
        /// SQL to get list of foreign keys.
        /// </summary>
        protected const string GetForeignKeysSQL = @"
SELECT
	FKS.name AS KEY_NAME,
	FKS.key_index_id AS KEY_ORDER,
	FS.name AS FOREIGN_SCHEMA_NAME,
	FT.name AS FOREIGN_TABLE_NAME,
	FKC.parent_column_id AS FOREIGN_COLUMN_ORDER,
	PS.name AS PRIMARY_SCHEMA_NAME,
	PT.name AS PRIMARY_TABLE_NAME,
	FKC.referenced_column_id AS PRIMARY_COLUMN_ORDER,
    FKS.delete_referential_action AS DELETE_ACTION,
    FKS.update_referential_action AS UPDATE_ACTION
FROM 
	SYS.FOREIGN_KEY_COLUMNS AS FKC
	INNER JOIN SYS.FOREIGN_KEYS AS FKS ON FKS.object_id = FKC.constraint_object_id
	INNER JOIN SYS.TABLES AS FT ON FT.object_id = FKC.parent_object_id
	INNER JOIN SYS.SCHEMAS AS FS ON FS.schema_id = FT.schema_id
	INNER JOIN SYS.TABLES AS PT ON PT.object_id = FKC.referenced_object_id
	INNER JOIN SYS.SCHEMAS AS PS ON PS.schema_id = PT.schema_id
ORDER BY FKS.name, FKS.key_index_id
";

        /// <summary>
        /// Load foreign keys.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="tables"></param>
        protected virtual Dictionary<string, List<IDatabaseForeignKey>> LoadForeignKeys(IDataReader reader, List<IEleflexDatabaseObject> tables)
        {
            Dictionary<string, List<IDatabaseForeignKey>> list = new Dictionary<string, List<IDatabaseForeignKey>>();
            IDatabaseForeignKey item = null;
            while (reader.Read())
            {
                string keyName = Convert.ToString(reader["KEY_NAME"]);
                if (item == null || item.Name != keyName)
                {
                    item = new DatabaseForeignKey();
                    item.Name = keyName;
                    string foreignSchema = Convert.ToString(reader["FOREIGN_SCHEMA_NAME"]);
                    string foreignTable = Convert.ToString(reader["FOREIGN_TABLE_NAME"]);
                    foreach (IEleflexDatabaseObject table in tables)
                    {
                        if (table.EDOGetSchema().Name == foreignSchema && table.EDOGetName() == foreignTable && table.EDOGetType() == DatabaseObjectType.Table)
                        {
                            item.Foreign = table;
                            break;
                        }
                    }
                    //save item to foreign table table
                    string objectName = _databaseProvider.GetCatalogName() + EleflexProperty.Field_Seperator + foreignSchema + EleflexProperty.Field_Seperator + foreignTable + EleflexProperty.Field_Seperator;
                    if (!list.ContainsKey(objectName))
                        list.Add(objectName, new List<IDatabaseForeignKey>() { item });
                    else
                        list[objectName].Add(item);                    

                    string primarySchema = Convert.ToString(reader["PRIMARY_SCHEMA_NAME"]);
                    string primaryTable = Convert.ToString(reader["PRIMARY_TABLE_NAME"]);
                    foreach (IEleflexDatabaseObject table in tables)
                    {
                        if (table.EDOGetSchema().Name == primarySchema && table.EDOGetName() == primaryTable && table.EDOGetType() == DatabaseObjectType.Table)
                        {
                            item.Primary = table;
                            break;
                        }
                    }
                    ////save item to primary table name
                    //objectName = _databaseProvider.GetCatalogName() + EleflexProperty.Field_Seperator + primarySchema + EleflexProperty.Field_Seperator + primaryTable + EleflexProperty.Field_Seperator;
                    //if (!list.ContainsKey(objectName))
                    //    list.Add(objectName, new List<IDatabaseForeignKey>() { item });
                    //else
                    //    list[objectName].Add(item);                    
                    

                    int deleteAction = Convert.ToInt32(reader["DELETE_ACTION"]);
                    switch (deleteAction)
                    {
                        default:
                        case 0:
                            item.DeleteAction = DatabaseForeignKeyAction.None;
                            break;
                        case 1:
                            item.DeleteAction = DatabaseForeignKeyAction.Cascade;
                            break;
                        case 2:
                            item.DeleteAction = DatabaseForeignKeyAction.SetNull;
                            break;
                        case 3:
                            item.DeleteAction = DatabaseForeignKeyAction.SetDefault;
                            break;
                    }

                    int updateAction = Convert.ToInt32(reader["UPDATE_ACTION"]);
                    switch (updateAction)
                    {
                        default:
                        case 0:
                            item.DeleteAction = DatabaseForeignKeyAction.None;
                            break;
                        case 1:
                            item.DeleteAction = DatabaseForeignKeyAction.Cascade;
                            break;
                        case 2:
                            item.DeleteAction = DatabaseForeignKeyAction.SetNull;
                            break;
                        case 3:
                            item.DeleteAction = DatabaseForeignKeyAction.SetDefault;
                            break;
                    }
                }

                int foreignColumn = Convert.ToInt32(reader["FOREIGN_COLUMN_ORDER"]);
                List<IEDOProperty> properties = item.Foreign.EDOGetProperties();
                foreach (IEDOProperty column in properties)
                {
                    if (column.Ordinal == foreignColumn)
                    {
                        item.ForeignColumns.Add(column);
                        break;
                    }
                }

                int primaryColumn = Convert.ToInt32(reader["PRIMARY_COLUMN_ORDER"]);
                properties = item.Primary.EDOGetProperties();
                foreach (IEDOProperty column in properties)
                {
                    if (column.Ordinal == primaryColumn)
                    {
                        item.PrimaryColumns.Add(column);
                        break;
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// SQL to get list of constraints.
        /// </summary>
        protected const string GetConstraintsSQL = @"
SELECT
	C.name AS CONSTRAINT_NAME,
	S.name AS SCHEMA_NAME,
	T.name AS TABLE_NAME,
	C.parent_column_id AS COLUMN_ORDER,
	C.definition AS DEFINITION
FROM 
	SYS.CHECK_CONSTRAINTS AS C
	INNER JOIN SYS.TABLES AS T ON T.object_id = C.parent_object_id
	INNER JOIN SYS.SCHEMAS AS S ON S.schema_id = T.schema_id
ORDER BY C.name
";

        /// <summary>
        /// Load constraints.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="tables"></param>
        protected virtual Dictionary<string, List<IDatabaseConstraint>> LoadConstraints(IDataReader reader, List<IEleflexDatabaseObject> tables)
        {
            Dictionary<string, List<IDatabaseConstraint>> list = new Dictionary<string, List<IDatabaseConstraint>>();
            while (reader.Read())
            {
                IDatabaseConstraint item = new DatabaseConstraint();
                item.Name = Convert.ToString(reader["CONSTRAINT_NAME"]);
                item.SQL = Convert.ToString(reader["DEFINITION"]);

                string tableName = Convert.ToString(reader["TABLE_NAME"]);
                string schemaName = Convert.ToString(reader["SCHEMA_NAME"]);
                string objectName = _databaseProvider.GetCatalogName() + EleflexProperty.Field_Seperator + schemaName + EleflexProperty.Field_Seperator + tableName + EleflexProperty.Field_Seperator;
                if (!list.ContainsKey(objectName))
                    list.Add(objectName, new List<IDatabaseConstraint>() { item });
                else
                    list[objectName].Add(item); 
            }
            return list;
        }

        /// <summary>
        /// Load extended attributes.
        /// </summary>
        /// <param name="tables"></param>
        /// <param name="constraints"></param>
        /// <param name="indexes"></param>
        /// <param name="foreignKeys"></param>
        public virtual void LoadExtendedAttributes(List<IEleflexDatabaseObject> tables, Dictionary<string, List<IDatabaseConstraint>> constraints, Dictionary<string, List<IDatabaseIndex>> indexes, Dictionary<string, List<IDatabaseForeignKey>> foreignKeys)
        {            
            foreach (IEleflexDatabaseObject table in tables)
            {
                string objectKey = table.EleflexGetObjectKey();
                List<IDatabaseConstraint> tableConstraints = new List<IDatabaseConstraint>();
                List<IDatabaseIndex> tableIndexes = new List<IDatabaseIndex>();
                List<IDatabaseForeignKey> tableForeignKeys = new List<IDatabaseForeignKey>();
                if (constraints.ContainsKey(objectKey))
                    tableConstraints = constraints[objectKey];
                if (indexes.ContainsKey(objectKey))
                    tableIndexes = indexes[objectKey];
                if (foreignKeys.ContainsKey(objectKey))
                    tableForeignKeys = foreignKeys[objectKey];

                List<IPersistenceRelation> relations = new List<IPersistenceRelation>();
                foreach (IDatabaseForeignKey fk in tableForeignKeys)
                    relations.Add(fk);
                table.EDOSetExtendedAttrbutes(tableConstraints, tableIndexes, relations);
            }
        }


        /// <summary>
        /// SQL to get list of procedures.
        /// </summary>
        protected const string GetProceduresSQL = @"
SELECT
    S.name AS SCHEMA_NAME,
    P.name AS PROCEDURE_NAME,
    PS.name AS COLUMN_NAME,
    TYPE_NAME(PS.system_type_id) AS DATATYPE_NAME,
    PS.parameter_id AS COLUMN_ORDER,    
    ISNULL(PS.max_length,0) AS MAX_LENGTH,
    ISNULL(PS.precision,0) AS PRECISION,
    ISNULL(PS.scale,0) AS SCALE,
    0 AS IS_NULLABLE,
    0 AS IS_IDENTITY
FROM
    SYS.PROCEDURES AS P
    INNER JOIN SYS.SCHEMAS AS S ON S.schema_id = P.schema_id
    INNER JOIN SYS.PARAMETERS AS PS ON PS.object_id = P.object_id
ORDER BY S.name, P.name, PS.parameter_id
";

        /// <summary>
        /// Load procedures.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="schemas"></param>
        /// <param name="dataTypes"></param>
        /// <returns></returns>
        protected virtual List<IEleflexDatabaseObject> LoadProcedures(IDataReader reader, List<IDatabaseSchema> schemas, List<IEDOProperty> dataTypes)
        {
            List<IEleflexDatabaseObject> procs = new List<IEleflexDatabaseObject>();
            IEleflexDatabaseObject item = null;
            int columnOrdinal = 1;
            while (reader.Read())
            {
                //Get data
                string schemaName = Convert.ToString(reader["SCHEMA_NAME"]);
                string procName = Convert.ToString(reader["PROCEDURE_NAME"]);

                if (item == null || (item.EDOGetName() != procName || item.EDOGetSchema().Name != schemaName))
                {
                    columnOrdinal = 1;
                    //Find schema                    
                    IDatabaseSchema procSchema = new DatabaseSchema(schemaName, null, null);
                    foreach (IDatabaseSchema schema in schemas)
                    {
                        if (schema.Name == schemaName)
                        {
                            procSchema = schema;
                            break;
                        }
                    }
                    item = new EleflexDatabaseObject(_databaseProvider.GetCatalogName(), procSchema, procName, DatabaseObjectType.StoredProcedure);
                    procs.Add(item);
                }
                IEDOProperty col = LoadColumn(reader, dataTypes);
                col.ParentObject = item;
                item.EleflexSetPropertyValue(col.Name, EleflexDataTypeFactory.Create(col.DataTypeName), col);
                columnOrdinal++;
            }
            return procs;
        }



    }
}

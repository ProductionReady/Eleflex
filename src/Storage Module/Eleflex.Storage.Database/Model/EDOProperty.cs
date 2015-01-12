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
using System.Runtime.Serialization;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a property from the database.
    /// </summary>    
    public partial class EDOProperty : EPOProperty, IEDOProperty
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public EDOProperty() : base() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentObject"></param>
        /// <param name="ordinal"></param>
        /// <param name="name"></param>
        /// <param name="databasePropertyName"></param>
        /// <param name="dbType"></param>
        /// <param name="isNullable"></param>
        public EDOProperty(
            IEleflexObject parentObject,
            int ordinal,
            string name,
            string databasePropertyName,
            DbType dbType,
            bool isNullable)
            : this(parentObject, ordinal, name, databasePropertyName, dbType, isNullable, false, false, int.MaxValue, int.MaxValue, int.MaxValue)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentObject"></param>
        /// <param name="ordinal"></param>
        /// <param name="name"></param>
        /// <param name="databasePropertyName"></param>
        /// <param name="dbType"></param>
        /// <param name="isNullable"></param>
        /// <param name="isKey"></param>
        /// <param name="isComputed"></param>
        /// <param name="maxLength"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        public EDOProperty(
            IEleflexObject parentObject,
            int ordinal,
            string name,
            string databasePropertyName,
            DbType dbType,
            bool isNullable,
            bool isKey,
            bool isComputed,
            int maxLength,
            int precision,
            int scale)
            : this(parentObject, ordinal, name, databasePropertyName, dbType, isNullable, isKey, isComputed, maxLength, precision, scale, false, false, false)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentObject"></param>
        /// <param name="ordinal"></param>
        /// <param name="name"></param>
        /// <param name="databasePropertyName"></param>
        /// <param name="dbType"></param>
        /// <param name="isNullable"></param>
        /// <param name="isKey"></param>
        /// <param name="isComputed"></param>
        /// <param name="maxLength"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="isConcurrency"></param>
        /// <param name="isAuditInsert"></param>
        /// <param name="isAuditUpdate"></param>
        public EDOProperty(
            IEleflexObject parentObject,
            int ordinal,
            string name,
            string databasePropertyName,
            DbType dbType,
            bool isNullable,
            bool isKey,
            bool isComputed,
            int maxLength,
            int precision,
            int scale,
            bool isConcurrency,
            bool isAuditInsert,
            bool isAuditUpdate)
            : this(parentObject, ordinal, name, databasePropertyName, dbType, isNullable, isKey, isComputed, maxLength, precision, scale, isConcurrency, isAuditInsert, isAuditUpdate, false)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentObject"></param>
        /// <param name="ordinal"></param>
        /// <param name="name"></param>
        /// <param name="databasePropertyName"></param>
        /// <param name="dbType"></param>
        /// <param name="isNullable"></param>
        /// <param name="isKey"></param>
        /// <param name="isComputed"></param>
        /// <param name="maxLength"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="isConcurrency"></param>
        /// <param name="isAuditInsert"></param>
        /// <param name="isAuditUpdate"></param>
        /// <param name="isSecurityUser"></param>
        public EDOProperty(
            IEleflexObject parentObject,
            int ordinal,
            string name,
            string databasePropertyName,
            DbType dbType,
            bool isNullable,
            bool isKey,
            bool isComputed,
            int maxLength,
            int precision,
            int scale,
            bool isConcurrency,
            bool isAuditInsert,
            bool isAuditUpdate,
            bool isSecurityUser)
            : base(parentObject, ordinal, name, string.Empty, isNullable, isKey, isComputed, maxLength, precision, scale, isConcurrency, isAuditInsert, isAuditUpdate, isSecurityUser)
        {
            DatabasePropertyName = databasePropertyName;
            DbType = dbType;
        }


        /// <summary>
        /// The datatype name.
        /// </summary>
        public override string DataTypeName
        {
            get
            {
                return GetEleflexDataTypeName(DbType);
            }
        }

        /// <summary>
        /// The name of the property.
        /// </summary>        
        public virtual string DatabasePropertyName { get; set; }        
        
        /// <summary>
        /// The database data type.
        /// </summary>
        public virtual DbType DbType { get; set; }

        /// <summary>
        /// Determines if has a default value.
        /// </summary>
        public virtual bool HasDefaultValue { get; set; }

        /// <summary>
        /// The provider-specific SQL defining the default value.
        /// </summary>
        public virtual string DefaultSQL { get; set; }

        /// <summary>
        /// Direction.
        /// </summary>
        public virtual DatabaseColumnDirection DatabaseColumnDirection { get; set; }


        /// <summary>
        /// Get the Eleflex datatype name (value name) from a database DbType.
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public virtual string GetEleflexDataTypeName(DbType dbType)
        {
            switch (dbType)
            {
                default:
                    if (IsNullable)
                        return EleflexDataTypeConstant.StringNull;
                    return EleflexDataTypeConstant.String;
                case System.Data.DbType.AnsiString:
                    if (IsNullable)
                        return EleflexDataTypeConstant.StringNull;
                    return EleflexDataTypeConstant.String;
                case System.Data.DbType.AnsiStringFixedLength:
                    if (IsNullable)
                        return EleflexDataTypeConstant.StringNull;
                    return EleflexDataTypeConstant.String;
                case System.Data.DbType.Binary:
                    if (IsNullable)
                        return EleflexDataTypeConstant.ByteArrayNull;
                    return EleflexDataTypeConstant.ByteArray;
                case System.Data.DbType.Boolean:
                    if (IsNullable)
                        return EleflexDataTypeConstant.BooleanNull;
                    return EleflexDataTypeConstant.Boolean;
                case System.Data.DbType.Byte:
                    if (IsNullable)
                        return EleflexDataTypeConstant.ByteNull;
                    return EleflexDataTypeConstant.Byte;
                case System.Data.DbType.Currency:
                    if (IsNullable)
                        return EleflexDataTypeConstant.DecimalNull;
                    return EleflexDataTypeConstant.Decimal;
                case System.Data.DbType.Date:
                    if (IsNullable)
                        return EleflexDataTypeConstant.DateTimeNull;
                    return EleflexDataTypeConstant.DateTime;
                case System.Data.DbType.DateTime:
                    if (IsNullable)
                        return EleflexDataTypeConstant.DateTimeNull;
                    return EleflexDataTypeConstant.DateTime;
                case System.Data.DbType.DateTime2:
                    if (IsNullable)
                        return EleflexDataTypeConstant.DateTimeNull;
                    return EleflexDataTypeConstant.DateTime;
                case System.Data.DbType.DateTimeOffset:
                    if (IsNullable)
                        return EleflexDataTypeConstant.DoubleNull;
                    return EleflexDataTypeConstant.Double;
                case System.Data.DbType.Decimal:
                    if (IsNullable)
                        return EleflexDataTypeConstant.DecimalNull;
                    return EleflexDataTypeConstant.Decimal;
                case System.Data.DbType.Double:
                    if (IsNullable)
                        return EleflexDataTypeConstant.DoubleNull;
                    return EleflexDataTypeConstant.Double;
                case System.Data.DbType.Guid:
                    if (IsNullable)
                        return EleflexDataTypeConstant.GuidNull;
                    return EleflexDataTypeConstant.Guid;
                case System.Data.DbType.Int16:
                    if (IsNullable)
                        return EleflexDataTypeConstant.Int16Null;
                    return EleflexDataTypeConstant.Int16;
                case System.Data.DbType.Int32:
                    if (IsNullable)
                        return EleflexDataTypeConstant.Int32Null;
                    return EleflexDataTypeConstant.Int32;
                case System.Data.DbType.Int64:
                    if (IsNullable)
                        return EleflexDataTypeConstant.Int64Null;
                    return EleflexDataTypeConstant.Int64;
                case System.Data.DbType.Object:
                    if (IsNullable)
                        return EleflexDataTypeConstant.ObjectNull;
                    return EleflexDataTypeConstant.Object;
                case System.Data.DbType.SByte:
                    if (IsNullable)
                        return EleflexDataTypeConstant.SByteNull;
                    return EleflexDataTypeConstant.SByte;
                case System.Data.DbType.Single:
                    if (IsNullable)
                        return EleflexDataTypeConstant.SingleNull;
                    return EleflexDataTypeConstant.Single;
                case System.Data.DbType.String:
                    if (IsNullable)
                        return EleflexDataTypeConstant.StringNull;
                    return EleflexDataTypeConstant.String;
                case System.Data.DbType.StringFixedLength:
                    if (IsNullable)
                        return EleflexDataTypeConstant.StringNull;
                    return EleflexDataTypeConstant.String;
                case System.Data.DbType.Time:
                    if (IsNullable)
                        return EleflexDataTypeConstant.DateTimeNull;
                    return EleflexDataTypeConstant.DateTime;
                case System.Data.DbType.UInt16:
                    if (IsNullable)
                        return EleflexDataTypeConstant.UInt16Null;
                    return EleflexDataTypeConstant.UInt16;
                case System.Data.DbType.UInt32:
                    if (IsNullable)
                        return EleflexDataTypeConstant.UInt32Null;
                    return EleflexDataTypeConstant.UInt32;
                case System.Data.DbType.UInt64:
                    if (IsNullable)
                        return EleflexDataTypeConstant.UInt64Null;
                    return EleflexDataTypeConstant.UInt64;
                case System.Data.DbType.VarNumeric:
                    if (IsNullable)
                        return EleflexDataTypeConstant.Double;
                    return EleflexDataTypeConstant.Double;
                case System.Data.DbType.Xml:
                    if (IsNullable)
                        return EleflexDataTypeConstant.StringNull;
                    return EleflexDataTypeConstant.String;
            }
        }

    }
}

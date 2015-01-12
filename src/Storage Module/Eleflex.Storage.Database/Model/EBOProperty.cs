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
using System.Runtime.Serialization;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines the data type elements of a managed property.
    /// </summary>
    public partial class EBOProperty : EleflexProperty, IEBOProperty
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public EBOProperty() : base() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentObject"></param>
        /// <param name="ordinal"></param>
        /// <param name="name"></param>
        /// <param name="dataTypeName"></param>
        /// <param name="isNullable"></param>
        public EBOProperty(
            IEleflexObject parentObject,
            int ordinal,
            string name,
            string dataTypeName,
            bool isNullable)
            : this(
                parentObject,
                ordinal,
                name,
                dataTypeName,
                isNullable,
                false,
                false,
                int.MaxValue,
                int.MaxValue,
                int.MaxValue)
        {
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentObject"></param>
        /// <param name="ordinal"></param>
        /// <param name="name"></param>
        /// <param name="dataTypeName"></param>
        /// <param name="isNullable"></param>
        /// <param name="isKey"></param>
        /// <param name="isComputed"></param>
        /// <param name="maxLength"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        public EBOProperty(
            IEleflexObject parentObject,
            int ordinal,
            string name,
            string dataTypeName,
            bool isNullable,
            bool isKey,
            bool isComputed,
            int maxLength,
            int precision,
            int scale) : this(parentObject,ordinal,name,dataTypeName,isNullable, isKey, isComputed, maxLength, precision, scale, false, false, false)
        {            
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentObject"></param>
        /// <param name="ordinal"></param>
        /// <param name="name"></param>
        /// <param name="dataTypeName"></param>
        /// <param name="isNullable"></param>
        /// <param name="isKey"></param>
        /// <param name="isComputed"></param>
        /// <param name="maxLength"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="isConcurrency"></param>
        /// <param name="isAuditInsert"></param>
        /// <param name="isAuditUpdate"></param>
        public EBOProperty(
            IEleflexObject parentObject,
            int ordinal,
            string name,
            string dataTypeName,
            bool isNullable,
            bool isKey,
            bool isComputed,
            int maxLength,
            int precision,
            int scale,
            bool isConcurrency,
            bool isAuditInsert,
            bool isAuditUpdate)
            : base(parentObject, ordinal, name, dataTypeName, isNullable)
        {
            IsKey = isKey;
            IsComputed = isComputed;
            MaxLength = maxLength;
            Precision = precision;
            Scale = scale;
            IsConcurrency = isConcurrency;
            IsAuditCreate = isAuditInsert;
            IsAuditUpdate = isAuditUpdate;
        }


        /// <summary>
        /// Determines if the property is used to uniquely identify the object.
        /// </summary>
        public virtual bool IsKey { get; set; }

        /// <summary>
        /// Determines if the value is computed.
        /// </summary>
        public virtual bool IsComputed { get; set; }

        /// <summary>
        /// The maximum length of the data type.
        /// </summary>
        public virtual int MaxLength { get; set; }

        /// <summary>
        /// The precision of the data type.
        /// </summary>
        public virtual int Precision { get; set; }

        /// <summary>
        /// The scale of the data type.
        /// </summary>
        public virtual int Scale { get; set; }

        /// <summary>
        /// The size of the data type.
        /// </summary>        
        public virtual int Size
        {
            get
            {
                int max = 0;
                if (MaxLength > max)
                    max = MaxLength;
                if (Precision > max)
                    max = Precision;
                if (Scale > max)
                    max = Scale;
                if (max <= 0)
                    return int.MaxValue;
                return max;
            }
            set { }
        }

        /// <summary>
        /// Determine if is for concurrency.
        /// </summary>
        public virtual bool IsConcurrency { get; set; }

        /// <summary>
        /// Determine if is for creation.
        /// </summary>
        public virtual bool IsAuditCreate { get; set; }

        /// <summary>
        /// Determine if is for modifications.
        /// </summary>
        public virtual bool IsAuditUpdate { get; set; }

    }
}

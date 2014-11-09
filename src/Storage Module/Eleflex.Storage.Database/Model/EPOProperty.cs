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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines the data type elements of a managed property.
    /// </summary>
    public partial class EPOProperty : ESOProperty, IEPOProperty
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public EPOProperty() : base() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentObject"></param>
        /// <param name="ordinal"></param>
        /// <param name="name"></param>
        /// <param name="dataTypeName"></param>
        /// <param name="isNullable"></param>
        public EPOProperty(
            IEleflexObject parentObject,
            int ordinal,
            string name,
            string dataTypeName,
            bool isNullable)
            : this(parentObject, ordinal, name, dataTypeName, isNullable, false, false, int.MaxValue, int.MaxValue, int.MaxValue)
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
        public EPOProperty(
            IEleflexObject parentObject,
            int ordinal,
            string name,
            string dataTypeName,
            bool isNullable,
            bool isKey,
            bool isComputed,
            int maxLength,
            int precision,
            int scale)
            : this(parentObject, ordinal, name, dataTypeName, isNullable, isKey, isComputed, maxLength, precision, scale, false, false, false)
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
        public EPOProperty(
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
            : this(parentObject, ordinal, name, dataTypeName, isNullable, isKey, isComputed, maxLength, precision, scale, isConcurrency, isAuditInsert, isAuditUpdate, false)
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
        /// <param name="isSecurityUser"></param>
        public EPOProperty(
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
            bool isAuditUpdate,
            bool isSecurityUser)
            : base(parentObject, ordinal, name, dataTypeName, isNullable, isKey, isComputed, maxLength, precision, scale, isConcurrency, isAuditInsert, isAuditUpdate, isSecurityUser)
        {
        }

    }
}

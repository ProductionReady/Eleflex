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
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.Values
{

    /// <summary>
    /// Defines a managed value (data type) in the framework.
    /// </summary>
    public partial class ValueCustom<T> : ValueBase<T>
    {

        /// <summary>
        /// The default value of the data type.
        /// </summary>
        public const object DefaultValue = null;


        /// <summary>
        /// Constructor.
        /// </summary>
        public ValueCustom() : this(default(T)) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value"></param>
        public ValueCustom(T value) : this(true, value) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isNullable"></param>
        /// <param name="value"></param>
        public ValueCustom(bool isNullable, T value) :this(typeof(T).Name, isNullable, value) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataTypeName"></param>
        /// <param name="isNullable"></param>
        /// <param name="value"></param>
        public ValueCustom(string dataTypeName, bool isNullable, T value)
        {
            _dataTypeName = dataTypeName;
            _isNullable = isNullable;
            _value = value;
        }

        /// <summary>
        /// Convert a string value to the base data type and indicate if the conversion is successful.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override bool Convert(
            string input,
            IFormatProvider format,
            out IEleflexDataType output)
        {
            output = null;
            return false;
        }

        /// <summary>
        /// Create an instance of the implementing object.
        /// </summary>
        /// <returns></returns>
        public override IEleflexDataType EleflexCreate()
        {
            return new ValueCustom<T>();
        }

    }
}

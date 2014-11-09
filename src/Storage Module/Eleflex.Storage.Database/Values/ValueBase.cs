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
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.Values
{

    /// <summary>
    /// Defines an abstract base value (data type) in the framework.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class ValueBase<T> : IEleflexValue<T>
    {
                
        /// <summary>
        /// Internal FrameworkDataType.
        /// </summary>
        protected string _dataTypeName;
        /// <summary>
        /// Internal IsNullable.
        /// </summary>
        protected bool _isNullable;
        /// <summary>
        /// Internal Value.
        /// </summary>
        protected T _value;


        /// <summary>
        /// The name of the base data type.
        /// </summary>
        public virtual string DataTypeName
        {
            get { return _dataTypeName; }
        }

        /// <summary>
        /// Determines if the base type allows a null reference.
        /// </summary>
        public virtual bool IsNullable
        {
            get { return _isNullable; }
        }

        /// <summary>
        /// Get the object value.
        /// </summary>
        public virtual object ObjectValue
        {
            get { return _value; }
        }

        /// <summary>
        /// Get the type-specific data type.
        /// </summary>
        public virtual T Value
        {
            get { return _value; }
            set { _value = value; }
        }        


        /// <summary>
        /// Convert a string value to the base data type and indicate if the conversion is successful.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool Convert(string data, out IEleflexDataType value)
        {
            return Convert(data, null, out value);
        }

        /// <summary>
        /// Convert a string value to the base data type and indicate if the conversion is successful.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="provider"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public abstract bool Convert(string data, IFormatProvider provider, out IEleflexDataType value);

        /// <summary>
        /// Create an instance of the implementing object.
        /// </summary>
        /// <returns></returns>
        public abstract IEleflexDataType EleflexCreate();

        /// <summary>
        /// Return the value as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (_value == null)
                return null;

            IConvertible convert = _value as IConvertible;
            if (convert != null)
                return convert.ToString(System.Globalization.CultureInfo.InvariantCulture);
            else
                return _value.ToString();
        }

    }
}

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
    public partial class ValueByteArray : ValueBase<Byte[]>
    {

        /// <summary>
        /// The default value of the data type.
        /// </summary>
        public static byte[] DefaultValue = new byte[0];


        /// <summary>
        /// Constructor.
        /// </summary>
        public ValueByteArray() : this(DefaultValue) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="value"></param>
        public ValueByteArray(Byte[] value)
        {
            _dataTypeName = EleflexDataTypeConstant.ByteArray;
            _isNullable = false;
            _value = value;
        }


        /// <summary>
        /// Convert an input to output base data type and indicate if the conversion is successful.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static bool Convert(IEleflexDataType input, ref byte[] output)
        {
            if (input == null)
                return false;

            if (input.DataTypeName == EleflexDataTypeConstant.ByteArray)
            {
                output = (input as ValueByteArray).Value;
                return true;
            }
            return ConvertObject(input.ObjectValue, ref output);
        }

        /// <summary>
        /// Convert an input to output base data type and indicate if the conversion is successful.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public static bool ConvertObject(object input, ref byte[] output)
        {
            if (input == null)
                return false;

            Byte[] temp = input as Byte[];
            if (temp != null)
            {
                output = temp;
                return true;
            }

            string data = input.ToString();
            if (string.IsNullOrEmpty(data))
                return false;

            try
            {
                output = System.Convert.FromBase64String(data);                
                return true;
            }
            catch 
            {
                try
                {
                    output = System.Text.Encoding.UTF8.GetBytes(data);
                    return true;
                }
                catch { return false; }
            }
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
            if (string.IsNullOrEmpty(input))
                return false;

            Byte[] temp;
            try
            {
                temp = System.Convert.FromBase64String(input);
                output = new ValueByteArray(temp);
                return true;
            }
            catch
            {
                try
                {
                    temp = System.Text.Encoding.UTF8.GetBytes(input);
                    output = new ValueByteArray(temp);
                    return true;
                }
                catch { return false; }
            }
        }

        /// <summary>
        /// Create an instance of the implementing object.
        /// </summary>
        /// <returns></returns>
        public override IEleflexDataType EleflexCreate()
        {
            return new ValueByteArray();
        }

    }
}

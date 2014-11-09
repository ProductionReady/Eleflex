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
    /// Defines a managed data type used within the framework.
    /// </summary>
    public partial interface IEleflexDataType
    {

        /// <summary>
        /// The name of the data type.
        /// </summary>
        string DataTypeName { get; }

        /// <summary>
        /// Determine if the base type allows a null reference.
        /// </summary>
        bool IsNullable { get; }        

        /// <summary>
        /// The object value of the base data type. Value is stored natively and this function
        /// should only be called when a boxing operation is required. Rather, cast to the IEleflexValue superclass
        /// to operate on the explicit value natively.
        /// </summary>
        object ObjectValue { get; }

        /// <summary>
        /// Convert a string value to the base data type and indicate if the conversion is successful.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        bool Convert(string input, out IEleflexDataType output);

        /// <summary>
        /// Convert a string value to the base data type and indicate if the conversion is successful.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="format"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        bool Convert(string input, IFormatProvider format, out IEleflexDataType output);

        /// <summary>
        /// Create an instance of the implementing object.
        /// </summary>
        /// <returns></returns>
        IEleflexDataType EleflexCreate();

    }
}

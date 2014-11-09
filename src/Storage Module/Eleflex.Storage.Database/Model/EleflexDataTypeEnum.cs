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
    /// Enumeration for managed data types.
    /// </summary>
    public enum EleflexDataTypeEnum
    {
        /// <summary>
        /// Represents a boolean value.
        /// </summary>
        Boolean,
        /// <summary>
        /// Represents a Boolean value that can be assigned null like a reference type.
        /// </summary>
        BooleanNull,
        /// <summary>
        /// Represents a 8-bit unsigned integer.
        /// </summary>
        Byte,
        /// <summary>
        /// Represents a Byte value that can be assigned null like a reference type.
        /// </summary>
        ByteNull,
        /// <summary>
        /// Represents a 8-bit unsigned integer array.
        /// </summary>
        ByteArray,
        /// <summary>
        /// Represents a Byte[] value that can be assigned null like a reference type.
        /// </summary>
        ByteArrayNull,
        /// <summary>
        /// Represents a character.
        /// </summary>
        Char,
        /// <summary>
        /// Represents a character value that can be assigned null like a reference type.
        /// </summary>
        CharNull,
        /// <summary>
        /// Represents an instance in time.
        /// </summary>
        DateTime,
        /// <summary>
        /// Represents a DateTime value that can be assigned null like a reference type.
        /// </summary>
        DateTimeNull,
        /// <summary>
        /// Represents a decimal number.
        /// </summary>
        Decimal,
        /// <summary>
        /// Represents a Decimal value that can be assigned null like a reference type.
        /// </summary>
        DecimalNull,
        /// <summary>
        /// Represents a double-precision floating-point number.
        /// </summary>
        Double,
        /// <summary>
        /// Represents a Double value that can be assigned null like a reference type.
        /// </summary>
        DoubleNull,
        /// <summary>
        /// Represents a globally unique identifier.
        /// </summary>
        Guid,
        /// <summary>
        /// Represents an Guid value that can be assigned null like a reference type.
        /// </summary>
        GuidNull,
        /// <summary>
        /// Represents a 16-bit signed integer.
        /// </summary>
        Int16,
        /// <summary>
        /// Represents an Int16 value that can be assigned null like a reference type.
        /// </summary>
        Int16Null,
        /// <summary>
        /// Represents a 32-bit signed integer.
        /// </summary>
        Int32,
        /// <summary>
        /// Represents an Int32 value that can be assigned null like a reference type.
        /// </summary>
        Int32Null,
        /// <summary>
        /// Represents a 64-bit signed integer.
        /// </summary>
        Int64,
        /// <summary>
        /// Represents an Int64 value that can be assigned null like a reference type.
        /// </summary>
        Int64Null,
        /// <summary>
        /// Represents a custom defined object not supported by the framework.
        /// </summary>
        Object,
        /// <summary>
        /// Represents a custom defined object not supported by the framework that can be assigned null.
        /// </summary>
        ObjectNull,
        /// <summary>
        /// Represents a 8-bit signed integer.
        /// </summary>
        SByte,
        /// <summary>
        /// Represents a SByte value that can be assigned null like a reference type.
        /// </summary>
        SByteNull,
        /// <summary>
        /// Represents a Single value.
        /// </summary>
        Single,
        /// <summary>
        /// Represents a Single value that can be assigned null like a reference type.
        /// </summary>
        SingleNull,
        /// <summary>
        /// Represents a unicode text value.
        /// </summary>
        String,
        /// <summary>
        /// Represents a String value that can be assigned null like a reference type.
        /// </summary>
        StringNull,
        /// <summary>
        /// Represents a TimeSpan value.
        /// </summary>
        TimeSpan,
        /// <summary>
        /// Represents a TimeSpan value that can be assigned null like a reference type.
        /// </summary>
        TimeSpanNull,
        /// <summary>
        /// Represents a 16-bit unsigned integer.
        /// </summary>
        UInt16,
        /// <summary>
        /// Represents a UInt16 value that can be assigned null like a reference type.
        /// </summary>
        UInt16Null,
        /// <summary>
        /// Represents a 32-bit unsigned integer.
        /// </summary>
        UInt32,
        /// <summary>
        /// Represents a UInt32 value that can be assigned null like a reference type.
        /// </summary>
        UInt32Null,
        /// <summary>
        /// Represents a 64-bit unsigned integer.
        /// </summary>
        UInt64,
        /// <summary>
        /// Represents a UInt64 value that can be assigned null like a reference type.
        /// </summary>
        UInt64Null
    }
}

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
using Eleflex.Storage.Database.Values;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Create datatype objects using factory pattern.
    /// </summary>
    public partial class EleflexDataTypeFactory
    {

        /// <summary>
        /// Create a dataype.
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static IEleflexDataType Create(string dataType)
        {
            EleflexDataTypeEnum item;
            if (!Enum.TryParse(dataType, out item))
                return null;
            return Create(item);
        }

        /// <summary>
        /// Create a datatype.
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static IEleflexDataType Create(EleflexDataTypeEnum dataType)
        {
            switch (dataType)
            {
                default:
                    return null;
                case EleflexDataTypeEnum.Boolean:
                    return new ValueBoolean();
                case EleflexDataTypeEnum.BooleanNull:
                    return new ValueBooleanNull();
                case EleflexDataTypeEnum.Byte:
                    return new ValueByte();
                case EleflexDataTypeEnum.ByteArray:
                    return new ValueByteArray();
                case EleflexDataTypeEnum.ByteArrayNull:
                    return new ValueByteArrayNull();
                case EleflexDataTypeEnum.ByteNull:
                    return new ValueByteNull();
                case EleflexDataTypeEnum.Char:
                    return new ValueChar();
                case EleflexDataTypeEnum.CharNull:
                    return new ValueCharNull();
                case EleflexDataTypeEnum.DateTime:
                    return new ValueDateTime();
                case EleflexDataTypeEnum.DateTimeNull:
                    return new ValueDateTimeNull();
                case EleflexDataTypeEnum.Decimal:
                    return new ValueDecimal();
                case EleflexDataTypeEnum.DecimalNull:
                    return new ValueDecimalNull();
                case EleflexDataTypeEnum.Double:
                    return new ValueDouble();
                case EleflexDataTypeEnum.DoubleNull:
                    return new ValueDoubleNull();
                case EleflexDataTypeEnum.Guid:
                    return new ValueGuid();
                case EleflexDataTypeEnum.GuidNull:
                    return new ValueGuidNull();
                case EleflexDataTypeEnum.Int16:
                    return new ValueInt16();
                case EleflexDataTypeEnum.Int16Null:
                    return new ValueInt16Null();
                case EleflexDataTypeEnum.Int32:
                    return new ValueInt32();
                case EleflexDataTypeEnum.Int32Null:
                    return new ValueInt32Null();
                case EleflexDataTypeEnum.Int64:
                    return new ValueInt64();
                case EleflexDataTypeEnum.Int64Null:
                    return new ValueInt64Null();
                case EleflexDataTypeEnum.Object:
                    return new ValueObject();
                case EleflexDataTypeEnum.ObjectNull:
                    return new ValueObjectNull();
                case EleflexDataTypeEnum.SByte:
                    return new ValueSByte();
                case EleflexDataTypeEnum.SByteNull:
                    return new ValueSByteNull();
                case EleflexDataTypeEnum.Single:
                    return new ValueSingle();
                case EleflexDataTypeEnum.SingleNull:
                    return new ValueSingleNull();
                case EleflexDataTypeEnum.String:
                    return new ValueString();
                case EleflexDataTypeEnum.StringNull:
                    return new ValueStringNull();
                case EleflexDataTypeEnum.TimeSpan:
                    return new ValueTimeSpan();
                case EleflexDataTypeEnum.TimeSpanNull:
                    return new ValueTimeSpanNull();
                case EleflexDataTypeEnum.UInt16:
                    return new ValueUInt16();
                case EleflexDataTypeEnum.UInt16Null:
                    return new ValueUInt16Null();
                case EleflexDataTypeEnum.UInt32:
                    return new ValueUInt32();
                case EleflexDataTypeEnum.UInt32Null:
                    return new ValueUInt32Null();
                case EleflexDataTypeEnum.UInt64:
                    return new ValueUInt64();
                case EleflexDataTypeEnum.UInt64Null:
                    return new ValueUInt64Null();
            }
        }


        /// <summary>
        /// Get a new concurrency value.
        /// </summary>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static IEleflexDataType GetConcurrency(IEleflexDataType dataType)
        {
            if (dataType == null)
                return null;

            IEleflexDataType output = dataType.EleflexCreate();
            switch (dataType.DataTypeName)
            {
                default:
                    return null;
                case EleflexDataTypeConstant.ByteArray:
                    ValueByteArray valueByteArray = output as ValueByteArray;
                    if (valueByteArray.Value == null || valueByteArray.Value.Length == 0)
                        valueByteArray.Value = new byte[1] { byte.MinValue };
                    else
                    {
                        if (valueByteArray.Value[0] == byte.MaxValue)
                            valueByteArray.Value[0] = byte.MinValue;
                        else
                            valueByteArray.Value[0]++;
                    }
                    return output;
                case EleflexDataTypeConstant.ByteArrayNull:
                    ValueByteArrayNull valueByteArrayNull = output as ValueByteArrayNull;
                    if (valueByteArrayNull.Value == null || valueByteArrayNull.Value.Length == 0)
                        valueByteArrayNull.Value = new byte[1] { byte.MinValue };
                    else
                    {
                        if (valueByteArrayNull.Value[0] == byte.MaxValue)
                            valueByteArrayNull.Value[0] = byte.MinValue;
                        else
                            valueByteArrayNull.Value[0]++;
                    }
                    return output;
                case EleflexDataTypeConstant.Boolean:
                    ValueBoolean valueBoolean = output as ValueBoolean;
                    valueBoolean.Value = !valueBoolean.Value;
                    return output;
                case EleflexDataTypeConstant.BooleanNull:
                    ValueBooleanNull valueBooleanNull = output as ValueBooleanNull;
                    if (!valueBooleanNull.Value.HasValue)
                        valueBooleanNull.Value = new Nullable<bool>();
                    else
                        valueBooleanNull.Value = new Nullable<bool>(!valueBooleanNull.Value.Value);
                    return output;
                case EleflexDataTypeConstant.String:
                    ValueString valueString = output as ValueString; //65 to 90
                    if (valueString.Value == null || valueString.Value.Length == 0)
                        valueString.Value = "A";
                    else
                    {
                        char c = valueString.Value[0];
                        if (char.GetNumericValue(c) < 65 || char.GetNumericValue(c) >= 90)
                            valueString.Value = char.ConvertFromUtf32(65);
                        else
                            valueString.Value = char.ConvertFromUtf32((int)char.GetNumericValue(c) + 1);
                    }
                    return output;
                case EleflexDataTypeConstant.StringNull:
                    ValueStringNull valueStringNull = output as ValueStringNull; //65 to 90
                    if (valueStringNull.Value == null || valueStringNull.Value.Length == 0)
                        valueStringNull.Value = "A";
                    else
                    {
                        char c = valueStringNull.Value[0];
                        if (char.GetNumericValue(c) < 65 || char.GetNumericValue(c) >= 90)
                            valueStringNull.Value = char.ConvertFromUtf32(65);
                        else
                            valueStringNull.Value = char.ConvertFromUtf32((int)char.GetNumericValue(c) + 1);
                    }
                    return output;
                case EleflexDataTypeConstant.Byte:
                    ValueByte valueByte = output as ValueByte;
                    if (valueByte.Value == byte.MaxValue)
                        valueByte.Value = byte.MinValue;
                    else
                        valueByte.Value++;
                    return output;
                case EleflexDataTypeConstant.ByteNull:
                    ValueByteNull valueByteNull = output as ValueByteNull;
                    if (!valueByteNull.Value.HasValue)
                        valueByteNull.Value = new Nullable<byte>(byte.MinValue);
                    else
                    {
                        if (valueByteNull.Value == byte.MaxValue)
                            valueByteNull.Value = byte.MinValue;
                        else
                            valueByteNull.Value++;
                    }
                    return output;

                case EleflexDataTypeConstant.DateTime:
                    ValueDateTime valueDateTime = output as ValueDateTime;
                    valueDateTime.Value = DateTime.UtcNow;
                    return output;
                case EleflexDataTypeConstant.DateTimeNull:
                    ValueDateTimeNull valueDateTimeNull = output as ValueDateTimeNull;
                    valueDateTimeNull.Value = new Nullable<DateTime>(DateTime.UtcNow);
                    return output;
                case EleflexDataTypeConstant.Single:
                    ValueSingle valueSingle = output as ValueSingle;
                    if (valueSingle.Value == Single.MaxValue)
                        valueSingle.Value = Single.MinValue;
                    else
                        valueSingle.Value++;
                    return output;
                case EleflexDataTypeConstant.SingleNull:
                    ValueSingleNull valueSingleNull = output as ValueSingleNull;
                    if (!valueSingleNull.Value.HasValue)
                        valueSingleNull.Value = new Nullable<Single>(Single.MinValue);
                    else
                    {
                        if (valueSingleNull.Value == Single.MaxValue)
                            valueSingleNull.Value = Single.MinValue;
                        else
                            valueSingleNull.Value++;
                    }
                    return output;
                case EleflexDataTypeConstant.Decimal:
                    ValueDecimal valueDecimal = output as ValueDecimal;
                    if (valueDecimal.Value == Decimal.MaxValue)
                        valueDecimal.Value = Decimal.MinValue;
                    else
                        valueDecimal.Value++;
                    return output;
                case EleflexDataTypeConstant.DecimalNull:
                    ValueDecimalNull valueDecimalNull = output as ValueDecimalNull;
                    if (!valueDecimalNull.Value.HasValue)
                        valueDecimalNull.Value = new Nullable<Decimal>(Decimal.MinValue);
                    else
                    {
                        if (valueDecimalNull.Value == Decimal.MaxValue)
                            valueDecimalNull.Value = Decimal.MinValue;
                        else
                            valueDecimalNull.Value++;
                    }
                    return output;
                case EleflexDataTypeConstant.Double:
                    ValueDouble valueDouble = output as ValueDouble;
                    if (valueDouble.Value == Double.MaxValue)
                        valueDouble.Value = Double.MinValue;
                    else
                        valueDouble.Value++;
                    return output;
                case EleflexDataTypeConstant.DoubleNull:
                    ValueDoubleNull valueDoubleNull = output as ValueDoubleNull;
                    if (!valueDoubleNull.Value.HasValue)
                        valueDoubleNull.Value = new Nullable<Double>(Double.MinValue);
                    else
                    {
                        if (valueDoubleNull.Value == Double.MaxValue)
                            valueDoubleNull.Value = Double.MinValue;
                        else
                            valueDoubleNull.Value++;
                    }
                    return output;
                case EleflexDataTypeConstant.Int16:
                    ValueInt16 valueInt16 = output as ValueInt16;
                    if (valueInt16.Value == Int16.MaxValue)
                        valueInt16.Value = Int16.MinValue;
                    else
                        valueInt16.Value++;
                    return output;
                case EleflexDataTypeConstant.Int16Null:
                    ValueInt16Null valueInt16Null = output as ValueInt16Null;
                    if (!valueInt16Null.Value.HasValue)
                        valueInt16Null.Value = new Nullable<Int16>(Int16.MinValue);
                    else
                    {
                        if (valueInt16Null.Value == Int16.MaxValue)
                            valueInt16Null.Value = Int16.MinValue;
                        else
                            valueInt16Null.Value++;
                    }
                    return output;
                case EleflexDataTypeConstant.Int32:
                    ValueInt32 valueInt32 = output as ValueInt32;
                    if (valueInt32.Value == Int16.MaxValue)
                        valueInt32.Value = Int16.MinValue;
                    else
                        valueInt32.Value++;
                    return output;
                case EleflexDataTypeConstant.Int32Null:
                    ValueInt32Null valueInt32Null = output as ValueInt32Null;
                    if (!valueInt32Null.Value.HasValue)
                        valueInt32Null.Value = new Nullable<Int32>(Int32.MinValue);
                    else
                    {
                        if (valueInt32Null.Value == Int32.MaxValue)
                            valueInt32Null.Value = Int32.MinValue;
                        else
                            valueInt32Null.Value++;
                    }
                    return output;
                case EleflexDataTypeConstant.Int64:
                    ValueInt64 valueInt64 = output as ValueInt64;
                    if (valueInt64.Value == Int64.MaxValue)
                        valueInt64.Value = Int64.MinValue;
                    else
                        valueInt64.Value++;
                    return output;
                case EleflexDataTypeConstant.Int64Null:
                    ValueInt64Null valueInt64Null = output as ValueInt64Null;
                    if (!valueInt64Null.Value.HasValue)
                        valueInt64Null.Value = new Nullable<Int64>(Int64.MinValue);
                    else
                    {
                        if (valueInt64Null.Value == Int64.MaxValue)
                            valueInt64Null.Value = Int64.MinValue;
                        else
                            valueInt64Null.Value++;
                    }
                    return output;
                case EleflexDataTypeConstant.Guid:
                    ValueGuid valueGuid = output as ValueGuid;
                    valueGuid.Value = Guid.NewGuid();
                    return output;
                case EleflexDataTypeConstant.GuidNull:
                    ValueGuidNull valueGuidNull = output as ValueGuidNull;
                    valueGuidNull.Value = Guid.NewGuid();
                    return output;
            }
        }

    }
}

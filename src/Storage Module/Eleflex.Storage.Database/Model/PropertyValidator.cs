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
    /// Used to validate a property definition and its supplied value.
    /// </summary>
    public static partial class PropertyValidator
    {

        /// <summary>
        /// Class name used for logging.
        /// </summary>
        public const string CLASSNAME = "PR.Eleflex.Mapping.PropertyValidator" + EleflexProperty.Field_Seperator;
        

        /// <summary>
        /// Validate that a property's value conforms with its defintion, however can truncate if needed.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <param name="truncate"></param>
        /// <returns></returns>
        public static bool Validate(IMappingContext context, IEBOProperty property, IEleflexDataType value, bool truncate = false)
        {
            if (context == null || property == null || value == null)
                return false;

            const string methodName = CLASSNAME + "Validate";

            //Convert value to property's dataype for proper validation
            if (string.Compare(property.DataTypeName, value.DataTypeName, true) != 0)
            {
                IEleflexDataType dataType = EleflexDataTypeFactory.Create(property.DataTypeName);
                if (dataType == null)
                {
                    context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_DataTypeFactoryCreation1, property.DataTypeName));
                    return false;
                }
                if (!dataType.Convert(value.ToString(), out value))
                {
                    context.AddMessage(new EleflexMessage(true, false, methodName, MappingConstants.UserMessage_DataConversion1, context.MappingPropertyContext));
                    return false;
                }
            }

            switch(value.DataTypeName)
            {
                case EleflexDataTypeConstant.String:
                    if ((value as ValueString).Value.Length > property.MaxLength && property.MaxLength > 0)
                    {
                        if (truncate)
                        {
                            (value as ValueString).Value = (value as ValueString).Value.Substring(0, property.MaxLength);
                            return true;
                        }
                        context.AddMessage(new EleflexMessage(true, false, methodName, MappingConstants.UserMessage_DataExceedsLength2, context.MappingPropertyContext, property.MaxLength.ToString()));
                        return false;
                    }
                    return true;
                case EleflexDataTypeConstant.StringNull:
                    if ((value as ValueStringNull).Value == null)
                        return property.IsNullable;
                    if ((value as ValueStringNull).Value.Length > property.MaxLength && property.MaxLength > 0)
                    {
                        if (truncate)
                        {
                            (value as ValueStringNull).Value = (value as ValueStringNull).Value.Substring(0, property.MaxLength);
                            return true;
                        }
                        context.AddMessage(new EleflexMessage(true, false, methodName, MappingConstants.UserMessage_DataExceedsLength2, context.MappingPropertyContext, property.MaxLength.ToString()));
                        return false;
                    }
                    return true;
                case EleflexDataTypeConstant.ByteArray:
                    if ((value as ValueByteArray).Value.Length > property.MaxLength && property.MaxLength > 0)
                    {
                        if (truncate)
                        {
                            byte[] tempBa = new byte[property.MaxLength];
                            Array.Copy((value as ValueByteArray).Value,tempBa,property.MaxLength);
                            (value as ValueByteArray).Value = tempBa;
                            return true;
                        }
                        context.AddMessage(new EleflexMessage(true, false, methodName, MappingConstants.UserMessage_DataExceedsLength2, context.MappingPropertyContext, property.MaxLength.ToString()));
                        return false;
                    }
                    return true;
                case EleflexDataTypeConstant.ByteArrayNull:
                    if ((value as ValueByteArrayNull).Value == null)
                        return property.IsNullable;
                    if ((value as ValueByteArrayNull).Value.Length > property.MaxLength && property.MaxLength > 0)
                    {
                        if (truncate)
                        {
                            byte[] tempBa = new byte[property.MaxLength];
                            Array.Copy((value as ValueByteArrayNull).Value, tempBa, property.MaxLength);
                            (value as ValueByteArrayNull).Value = tempBa;
                            return true;
                        }
                        context.AddMessage(new EleflexMessage(true, false, methodName, MappingConstants.UserMessage_DataExceedsLength2, context.MappingPropertyContext, property.MaxLength.ToString()));
                        return false;
                    }
                    return true;
            }
            return true;
        }

    }
}

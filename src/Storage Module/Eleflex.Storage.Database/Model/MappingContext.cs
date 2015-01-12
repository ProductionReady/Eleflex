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
using System.Text;
using Eleflex.Storage.Database.Values;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Used to store mapping information.
    /// </summary>
    public partial class MappingContext : EleflexContext, IMappingContext
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.Mapping.MappingContext" + EleflexProperty.Field_Seperator;

        /// <summary>
        /// (ValueStringNull) MappingOperation unique key denoting the property.
        /// </summary>
        public const string _MappingOperation = EleflexObjectKey + "MappingOperation" + EleflexProperty.Field_Seperator;

        /// <summary>
        /// (ValueStringNull) MappingPropertyContext unique key denoting the property.
        /// </summary>
        public const string _MappingPropertyContext = EleflexObjectKey + "MappingPropertyContext" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Constructor.
        /// </summary>
        public MappingContext() : this(null, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="propertyContext"></param>
        public MappingContext(string operation, string propertyContext)
            : base()
        {
            _eleflexObjectKey = EleflexObjectKey;
            MappingOperation = operation;
            MappingPropertyContext = propertyContext;
        }


        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new MappingContext();
        }

        /// <summary>
        /// Initialize an object with defaults
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();

            EleflexSetPropertyValue(
                _MappingOperation,
                new ValueStringNull(),
                new EleflexProperty(this, 3, _MappingOperation, EleflexDataTypeConstant.StringNull, true));

            EleflexSetPropertyValue(
                _MappingPropertyContext,
                new ValueStringNull(),
                new EleflexProperty(this, 4, _MappingPropertyContext, EleflexDataTypeConstant.StringNull, true));

        }

        /// <summary>
        /// Mapping operation.
        /// </summary>
        public virtual string MappingOperation
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_MappingOperation, out output))
                    return (output as ValueStringNull).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_MappingOperation, new ValueStringNull(value));
            }
        }

        /// <summary>
        /// Mapping property context.
        /// </summary>
        public virtual string MappingPropertyContext
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_MappingPropertyContext, out output))
                    return (output as ValueStringNull).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_MappingPropertyContext, new ValueStringNull(value));
            }
        }

        /// <summary>
        /// Override ToString() to output data.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToString());
            sb.Append("[MAPPINGOPERATION:");
            sb.Append(MappingOperation);
            sb.Append("]");
            sb.Append("[MAPPINGPROPERTYCONTEXT:");
            sb.Append(MappingPropertyContext);
            sb.Append("]");
            return sb.ToString();
        }

    }
}

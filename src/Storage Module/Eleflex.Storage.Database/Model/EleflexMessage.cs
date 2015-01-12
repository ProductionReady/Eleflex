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
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using Eleflex.Storage.Database.Values;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a managed system message used within the framework.
    /// </summary>
    public partial class EleflexMessage : EleflexBusinessObject, IEleflexMessage
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.EleflexMessage" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueStringNull) EleflexDataKey unique key denoting the property.
        /// </summary>
        public const string _EleflexDataKey = EleflexObjectKey + "EleflexDataKey" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueStringNull) Application unique key denoting the property.
        /// </summary>
        public const string _Application = EleflexObjectKey + "Application" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueStringNull) Computer unique key denoting the property.
        /// </summary>
        public const string _Computer = EleflexObjectKey + "Computer" + EleflexProperty.Field_Seperator;        
        /// <summary>
        /// (ValueDateTime) CreateDate unique key denoting the property.
        /// </summary>
        public const string _CreateDate = EleflexObjectKey + "CreateDate" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueBoolean) IsError unique key denoting the property.
        /// </summary>
        public const string _IsError = EleflexObjectKey + "IsError" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueBoolean) IsSystem unique key denoting the property.
        /// </summary>
        public const string _IsSystem = EleflexObjectKey + "IsSystem" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueStringNull) Source unique key denoting the property.
        /// </summary>
        public const string _Source = EleflexObjectKey + "Source" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueStringNull) Detail unique key denoting the property.
        /// </summary>
        public const string _Detail = EleflexObjectKey + "Detail" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueCustom[List[string]]) PropertyContext unique key denoting the property.
        /// </summary>
        public const string _PropertyContext = EleflexObjectKey + "PropertyContext" + EleflexProperty.Field_Seperator;



        /// <summary>
        /// Contructor.
        /// </summary>
        public EleflexMessage() : this(false, true, null, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isError"></param>
        /// <param name="isSystem"></param>
        /// <param name="source"></param>
        /// <param name="detail"></param>
        /// <param name="properties"></param>
        public EleflexMessage(bool isError, bool isSystem, string source, string detail, params string[] properties) : base()
        {
            _eleflexObjectKey = EleflexObjectKey;
            CreateDate = DateTime.UtcNow;
            IsError = isError;
            IsSystem = isSystem;
            Source = source;
            Detail = detail;
            PropertyContext = new List<string>();
            if (properties != null)
                PropertyContext.AddRange(properties);
        }


        /// <summary>
        /// Create an instance of the implementing object.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new EleflexMessage();
        }

        /// <summary>
        /// Initialize an object with defaults.
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();            

            EleflexSetPropertyValue(
                _EleflexDataKey,
                new ValueStringNull(),
                new EleflexProperty(this, 1, _EleflexDataKey, EleflexDataTypeConstant.StringNull, true));

            EleflexSetPropertyValue(
                _Application,
                new ValueStringNull(),
                new EleflexProperty(this, 2, _Application, EleflexDataTypeConstant.StringNull, true));

            EleflexSetPropertyValue(
                _Computer,
                new ValueStringNull(),
                new EleflexProperty(this, 3, _Computer, EleflexDataTypeConstant.StringNull, true));

            EleflexSetPropertyValue(
                _CreateDate,
                new ValueDateTime(DateTime.UtcNow),
                new EleflexProperty(this, 4, _CreateDate, EleflexDataTypeConstant.DateTime, false));

            EleflexSetPropertyValue(
                _IsError,
                new ValueBoolean(),
                new EleflexProperty(this, 5, _IsError, EleflexDataTypeConstant.Boolean, false));

            EleflexSetPropertyValue(
                _IsSystem,
                new ValueBoolean(),
                new EleflexProperty(this, 6, _IsSystem, EleflexDataTypeConstant.Boolean, false));

            EleflexSetPropertyValue(
                _Source,
                new ValueStringNull(),
                new EleflexProperty(this, 7, _Source, EleflexDataTypeConstant.StringNull, true));

            EleflexSetPropertyValue(
                _Detail,
                new ValueStringNull(),
                new EleflexProperty(this, 8, _Detail, EleflexDataTypeConstant.StringNull, true));

            EleflexSetPropertyValue(
                _PropertyContext,
                new ValueCustom<List<string>>(),
                new EleflexProperty(this, 9, _PropertyContext, EleflexDataTypeConstant.Custom, true));
        }

        /// <summary>
        /// Serialize data based on exposed properies, not dynamic means.
        /// </summary>
        public override List<EleflexKeyValue> EleflexKeyValues { get; set; }

        /// <summary>
        /// EleflexDataKey.
        /// </summary>
        public virtual string EleflexDataKey
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_EleflexDataKey, out output))
                    return (output as ValueStringNull).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_EleflexDataKey, new ValueStringNull(value));
            }
        }

        /// <summary>
        /// Application.
        /// </summary>
        public virtual string Application
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_Application, out output))
                    return (output as ValueStringNull).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_Application, new ValueStringNull(value));
            }
        }

        /// <summary>
        /// Computer.
        /// </summary>
        public virtual string Computer
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_Computer, out output))
                    return (output as ValueStringNull).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_Computer, new ValueStringNull(value));
            }
        }

        /// <summary>
        /// Computer's current date message was created.
        /// </summary>
        public virtual DateTime CreateDate
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_CreateDate, out output))
                    return (output as ValueDateTime).Value;
                return ValueDateTime.DefaultValue;
            }
            set
            {
                this.EleflexSetPropertyValue(_CreateDate, new ValueDateTime(value));
            }
        }

        /// <summary>
        /// Determines if the message is an error or informational.
        /// </summary>
        public virtual bool IsError
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_IsError, out output))
                    return (output as ValueBoolean).Value;
                return ValueBoolean.DefaultValue;
            }
            set
            {
                this.EleflexSetPropertyValue(_IsError, new ValueBoolean(value));
            }
        }

        /// <summary>
        /// Determines if the message is for the system or user.
        /// </summary>
        public virtual bool IsSystem
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_IsSystem, out output))
                    return (output as ValueBoolean).Value;
                return ValueBoolean.DefaultValue;
            }
            set
            {
                this.EleflexSetPropertyValue(_IsSystem, new ValueBoolean(value));
            }
        }

        /// <summary>
        /// Source/method occuring from for detailed tracking.
        /// </summary>
        public virtual string Source
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_Source, out output))
                    return (output as ValueStringNull).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_Source, new ValueStringNull(value));
            }
        }

        /// <summary>
        /// Any detail relating to the message.
        /// </summary>
        public virtual string Detail
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_Detail, out output))
                    return (output as ValueStringNull).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_Detail, new ValueStringNull(value));
            }
        }

        /// <summary>
        /// EleflexProperty context.
        /// </summary>
        public virtual List<string> PropertyContext
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_PropertyContext, out output))
                    return (output as ValueCustom<List<string>>).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_PropertyContext, new ValueCustom<List<string>>(value));
            }
        }


        /// <summary>
        /// Get the final formatted message.
        /// </summary>
        /// <returns></returns>
        public virtual string GetFormattedDetail()
        {
            //string.format() does not always work based on detail string
            string detail = Detail;
            if (PropertyContext != null && PropertyContext.Count > 0)
            {
                for (int i = 0; i < PropertyContext.Count; i++)
                    detail = detail.Replace("{" + i.ToString() + "}", PropertyContext[i]);
            }
            return detail;
        }

        /// <summary>
        /// Override ToString() to output data.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[APPLICATION:");
            sb.Append(Application);
            sb.Append("]");
            sb.Append("[COMPUTER:");
            sb.Append(Computer);
            sb.Append("]");
            sb.Append("[CREATEDATE:");
            sb.Append(CreateDate.ToString());
            sb.Append("]");
            sb.Append("[ISERROR:");
            sb.Append(IsError);
            sb.Append("]");
            sb.Append("[ISSYSTEM:");
            sb.Append(IsSystem);
            sb.Append("]");
            sb.Append("[SOURCE:");
            sb.Append(Source);
            sb.Append("]");
            sb.Append("[DETAIL:");
            sb.Append(Detail);
            sb.Append("]");
            sb.Append("[PROPERTYCONTEXT:");
            for (int i = 0; i < PropertyContext.Count; i++)
            {
                sb.Append("[");
                sb.Append(PropertyContext[i]);
                sb.Append("]");
            }
            sb.Append("]");
            return sb.ToString();
        }

    }
}

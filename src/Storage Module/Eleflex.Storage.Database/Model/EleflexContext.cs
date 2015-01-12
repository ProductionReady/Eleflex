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
using System.Runtime.Serialization;
using System.Text;
using Eleflex.Storage.Database.Values;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines the execution context of the request.
    /// We also define is object as a eleflex object, so that if we wish to send
    /// additional information and then operate on it, we have a way of doing so.
    /// </summary>
    public partial class EleflexContext : EleflexBusinessObject, IEleflexContext
    {
        
        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.EleflexContext" + EleflexProperty.Field_Seperator;

        /// <summary>
        /// (ValueBoolean) IsError unique key denoting the property.
        /// </summary>
        public const string _IsError = EleflexObjectKey + "IsError" + EleflexProperty.Field_Seperator;

        /// <summary>
        /// (ValueCustom[List[IEleflexMessage]]) ContextMessages unique key denoting the property.
        /// </summary>
        public const string _ContextMessages = EleflexObjectKey + "ContextMessages" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexContext() : base() 
        {
            _eleflexObjectKey = EleflexObjectKey;
        }


        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new EleflexContext();
        }

        /// <summary>
        /// Initialize an object with defaults
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();            

            EleflexSetPropertyValue(
                _IsError,
                new ValueBoolean(false),
                new EleflexProperty(this, 1, _IsError, EleflexDataTypeConstant.Boolean, false));

            EleflexSetPropertyValue(
                _ContextMessages,
                new ValueCustom<List<IEleflexMessage>>(new List<IEleflexMessage>()),
                new EleflexProperty(this, 2, _ContextMessages, EleflexDataTypeConstant.Custom, false));

        }

        /// <summary>
        /// Don't serialize EleflexKeyValues (so we can restrict serialization in inherited classes).
        /// </summary>
        public override List<EleflexKeyValue> EleflexKeyValues { get; set; }

        /// <summary>
        /// Determine if an error occured.
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
        /// Get context messages.
        /// </summary>
        public virtual List<IEleflexMessage> ContextMessages
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_ContextMessages, out output))
                    return (output as ValueCustom<List<IEleflexMessage>>).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_ContextMessages, new ValueCustom<List<IEleflexMessage>>(value));
            }
        }

        /// <summary>
        /// List of messages
        /// </summary>
        public virtual List<IEleflexMessage> GetMessages()
        {
            return ContextMessages;
        }

        /// <summary>
        /// Add a message to the context.
        /// </summary>
        /// <param name="message"></param>
        public virtual void AddMessage(IEleflexMessage message)
        {
            if (message == null)
                return;
            if (!IsError && message.IsError)
                IsError = true;            
            if (ContextMessages == null)
                ContextMessages = new List<IEleflexMessage>();
            ContextMessages.Add(message);
        }

        /// <summary>
        /// Add context information into this instance.
        /// </summary>
        /// <param name="context"></param>
        public virtual void AddContext(IEleflexContext context)
        {
            List<IEleflexMessage> messages = context.GetMessages();
            if (!this.IsError && context.IsError)
                this.IsError = true;

            if (messages != null && messages.Count > 0)
            {
                foreach (IEleflexMessage message in messages)
                    this.AddMessage(message);
            }
        }

        /// <summary>
        /// Override ToString() to output data.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ISERROR:");
            sb.Append(IsError);
            sb.Append("]");
            List<IEleflexMessage> list = ContextMessages;
            if (list != null && list.Count > 0)
            {
                foreach (EleflexMessage item in list)
                {
                    sb.Append("[CONTEXTMESSAGE:");
                    sb.Append(item.ToString());
                    sb.Append("]");
                }
            }
            return sb.ToString();
        }

    }
}

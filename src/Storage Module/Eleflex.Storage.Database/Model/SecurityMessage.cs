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
using System.Text;
using System.Runtime.Serialization;
using Eleflex.Storage.Database.Values;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a logging message.
    /// </summary>
    public partial class SecurityMessage : EleflexMessage, ISecurityMessage
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.Security.SecurityMessage" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueStringNull) SecurityUserID unique key denoting the property.
        /// </summary>
        public const string _SecurityUserID = EleflexObjectKey + "SecurityUserID" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueStringNull) SecurityUserIPAddress unique key denoting the property.
        /// </summary>
        public const string _SecurityUserIPAddress = EleflexObjectKey + "SecurityUserIPAddress" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Contructor.
        /// </summary>
        public SecurityMessage() : this(false, true, null, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isError"></param>
        /// <param name="isSystem"></param>
        /// <param name="source"></param>
        /// <param name="detail"></param>
        /// <param name="properties"></param>
        public SecurityMessage(bool isError, bool isSystem, string source, string detail, params string[] properties)
            : this(isError, null, isSystem, source, detail, properties)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isError"></param>
        /// <param name="securityUserIdentifier"></param>
        /// <param name="isSystem"></param>
        /// <param name="source"></param>
        /// <param name="detail"></param>
        /// <param name="properties"></param>
        public SecurityMessage(bool isError, string securityUserIdentifier, bool isSystem, string source, string detail, params string[] properties)
            : base(isError, isSystem, source, detail, properties)
        {
            _eleflexObjectKey = EleflexObjectKey;
            SecurityUserID = securityUserIdentifier;
        }


        /// <summary>
        /// Create an instance of the implementing object.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new SecurityMessage();
        }

        /// <summary>
        /// Initialize an object with defaults.
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();            

            EleflexSetPropertyValue(
                _SecurityUserID,
                new ValueStringNull(),
                new EleflexProperty(this, 10, _SecurityUserID, EleflexDataTypeConstant.StringNull, true));

            EleflexSetPropertyValue(
                _SecurityUserIPAddress,
                new ValueStringNull(),
                new EleflexProperty(this, 11, _SecurityUserIPAddress, EleflexDataTypeConstant.StringNull, true));

        }

        /// <summary>
        /// Security User identifier.
        /// </summary>        
        public virtual string SecurityUserID
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_SecurityUserID, out output))
                    return (output as ValueStringNull).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_SecurityUserID, new ValueStringNull(value));
            }
        }

        /// <summary>
        /// Security User IP address.
        /// </summary>        
        public virtual string SecurityUserIPAddress
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_SecurityUserIPAddress, out output))
                    return (output as ValueStringNull).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_SecurityUserIPAddress, new ValueStringNull(value));
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
            sb.Append("[SECURITYUSERIDENTIFIER:");
            sb.Append(SecurityUserID);
            sb.Append("]");
            sb.Append("[SECURITYUSERIPADDRESS:");
            sb.Append(SecurityUserIPAddress);
            sb.Append("]");
            return sb.ToString();
        }

    }
}

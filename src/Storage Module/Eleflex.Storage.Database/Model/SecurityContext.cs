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
    public partial class SecurityContext : MappingContext, ISecurityContext
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.Security.SecurityContext" + EleflexProperty.Field_Seperator;

        /// <summary>
        /// (ValueStringNull) SecurityUserID unique key denoting the property.
        /// </summary>
        public const string _SecurityUserIdentifier = EleflexObjectKey + "SecurityUserID" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityContext() : this(null, null, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="propertyContext"></param>
        public SecurityContext(string operation, string propertyContext) : this(operation, propertyContext, null) { }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="propertyContext"></param>
        /// <param name="securityUserKey"></param>
        public SecurityContext(string operation, string propertyContext, string securityUserKey)
            : base(operation, propertyContext)
        {
            _eleflexObjectKey = EleflexObjectKey;
            SecurityUserID = securityUserKey;
        }


        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new SecurityContext();
        }

        /// <summary>
        /// Initialize an object with defaults
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();            

            EleflexSetPropertyValue(
                _SecurityUserIdentifier,
                new ValueStringNull(),
                new EleflexProperty(this, 5, _SecurityUserIdentifier, EleflexDataTypeConstant.StringNull, true));

        }

        /// <summary>
        /// User identifier.
        /// </summary>
        public virtual string SecurityUserID
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_SecurityUserIdentifier, out output))
                    return (output as ValueStringNull).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_SecurityUserIdentifier, new ValueStringNull(value));
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
            return sb.ToString();
        }

    }
}

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
using Eleflex.Storage.Database.Values;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a storage request.
    /// </summary>
    public partial class PersistenceResponse : SecurityResponse, IPersistenceResponse
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.Persistence.PersistenceResponse" + EleflexProperty.Field_Seperator;

        /// <summary>
        /// (ValueDouble) TotalCount unique key denoting the property.
        /// </summary>
        public const string _TotalCount = EleflexObjectKey + "TotalCount" + EleflexProperty.Field_Seperator;

        /// <summary>
        /// Constructor.
        /// </summary>
        public PersistenceResponse() : base() 
        {
            _eleflexObjectKey = EleflexObjectKey;
        }


        /// <summary>
        /// Initialize an object with defaults
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();            

            EleflexSetPropertyValue(
                _TotalCount,
                new ValueDoubleNull(),
                new EleflexProperty(this, 3, _TotalCount, EleflexDataTypeConstant.DoubleNull, false));
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new PersistenceResponse();
        }


        /// <summary>
        /// Determine if an error occured.
        /// </summary>
        public virtual double? TotalCount
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_TotalCount, out output))
                    return (output as ValueDoubleNull).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_TotalCount, new ValueDoubleNull(value));
            }
        }

    }
}

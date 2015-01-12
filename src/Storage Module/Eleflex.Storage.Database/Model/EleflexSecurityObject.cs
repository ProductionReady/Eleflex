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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// The foundation object of the Eleflex reflectionless design.
    /// </summary>
    public partial class EleflexSecurityObject : EleflexBusinessObject, IEleflexSecurityObject
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.EleflexSecurityObject" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexSecurityObject() : base()
        {
            _eleflexObjectKey = EleflexObjectKey;
        }

        /// <summary>
        /// Create an instance of this type.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new EleflexSecurityObject();
        }

        /// <summary>
        /// Initialize an object with defaults.
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();            
        }        

    }
}

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
using System.Runtime.Serialization;
using Eleflex.Storage.Database.Values;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a request with the framework.
    /// We also define is object as a eleflex object, so that if we wish to send
    /// additional information and then operate on it, we have a way of doing so.
    /// </summary>
    public partial class EleflexRequest : EleflexObject, IEleflexRequest<IEleflexContext>
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.EleflexRequest" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueCustom[IEleflexContext]) EleflexContext unique key denoting the property.
        /// </summary>
        public const string _Context = EleflexObjectKey + "Context" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexRequest() :
            this(null)
        {            
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        public EleflexRequest(IEleflexContext context)
        {
            _eleflexObjectKey = EleflexObjectKey;
            if (context == null)
                Context = new EleflexContext();
            else
                Context = context;
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new EleflexRequest();
        }


        /// <summary>
        /// Initialize an object with defaults
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();            

            EleflexSetPropertyValue(
                _Context,
                new ValueCustom<IEleflexContext>(new EleflexContext()),
                new EleflexProperty(this,1, _Context, EleflexDataTypeConstant.Custom, true));

        }

        /// <summary>
        /// The context of the request. Not ment to be serialied over the wire. Additioanl info sent via request object (or other implementation style).
        /// </summary>
        public virtual IEleflexContext Context
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_Context, out output))
                    return (output as ValueCustom<IEleflexContext>).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_Context, new ValueCustom<IEleflexContext>(value));
            }
        }

    }
}

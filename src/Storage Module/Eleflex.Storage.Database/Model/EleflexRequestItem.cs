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
    public partial class EleflexRequestItem<T1> : EleflexRequest, IEleflexRequestItem<IEleflexContext, T1>
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.EleflexRequestItem" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueCustom[T1]) Item unique key denoting the property.
        /// </summary>
        public const string _Item = EleflexObjectKey + "Item" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexRequestItem() : this(null, default(T1)) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        public EleflexRequestItem(IEleflexContext context) : this(context, default(T1)) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="item"></param>
        public EleflexRequestItem(T1 item) : this(null, item) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="item"></param>
        public EleflexRequestItem(IEleflexContext context, T1 item) : base(context)
        {
            _eleflexObjectKey = EleflexObjectKey;
            Item = item;
        }

        /// <summary>
        /// Create a new instance.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new EleflexRequestItem<T1>();
        }


        /// <summary>
        /// Initialize an object with defaults
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();            

            EleflexSetPropertyValue(
                _Context,
                new ValueCustom<T1>(default(T1)),
                new EleflexProperty(this, 2, _Item, EleflexDataTypeConstant.Custom, true));

        }

        /// <summary>
        /// The item.
        /// </summary>
        public virtual T1 Item
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_Item, out output))
                    return (output as ValueCustom<T1>).Value;
                return default(T1);
            }
            set
            {
                this.EleflexSetPropertyValue(_Item, new ValueCustom<T1>(value));
            }
        }
        
    }
}

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
using Eleflex.Storage.Database.Values;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a response with a list of items.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public partial class EleflexResponseItems<T1> : EleflexResponse, IEleflexResponseItems<T1>
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.EleflexResponseItems" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// (ValueCustom[List[T1]]) Items unique key denoting the property.
        /// </summary>
        public const string _Items = EleflexObjectKey + "Items" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexResponseItems()
            : base()
        {
            _eleflexObjectKey = EleflexObjectKey;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="items"></param>
        public EleflexResponseItems(List<T1> items)
            : base()
        {
            _eleflexObjectKey = EleflexObjectKey;
            Items = items;
        }


        /// <summary>
        /// Initialize an object with defaults
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();            

            EleflexSetPropertyValue(
                _Items,
                new ValueCustom<T1>(),
                new EleflexProperty(this, 3, _Items, EleflexDataTypeConstant.Custom, false));

        }

        /// <summary>
        /// The items.
        /// </summary>
        public virtual List<T1> Items
        {
            get
            {
                IEleflexDataType output = null;
                if (this.EleflexGetPropertyValue(_Items, out output))
                    return (output as ValueCustom<List<T1>>).Value;
                return null;
            }
            set
            {
                this.EleflexSetPropertyValue(_Items, new ValueCustom<List<T1>>(value));
            }
        }

    }
}

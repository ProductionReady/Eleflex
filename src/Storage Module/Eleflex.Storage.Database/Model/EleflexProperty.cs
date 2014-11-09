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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines the data type elements of a managed property.
    /// </summary>
    public partial class EleflexProperty : EleflexObject, IEleflexProperty
    {

        /// <summary>
        /// Character used for seperating and spliting unqiue names.
        /// </summary>
        public const string Field_Seperator = ":";


        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexProperty() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parentObject"></param>
        /// <param name="ordinal"></param>
        /// <param name="name"></param>
        /// <param name="dataTypeName"></param>
        /// <param name="isNullable"></param>
        public EleflexProperty(
            IEleflexObject parentObject,
            int ordinal,            
            string name,
            string dataTypeName,
            bool isNullable)
        {
            ParentObject = parentObject;
            Ordinal = ordinal;
            Name = name;
            DataTypeName = dataTypeName;
            IsNullable = isNullable;
        }



        /// <summary>
        /// The order the property is defined by the object.
        /// No not expose
        /// </summary>
        public virtual IEleflexObject ParentObject { get; set; }

        /// <summary>
        /// The order the property is defined by the object.
        /// </summary>
        public virtual int Ordinal { get; set; }

        /// <summary>
        /// The base name of the property.
        /// </summary>
        public virtual string Name { get; set; }
        
        /// <summary>
        /// The name of the base data type.
        /// </summary>
        public virtual string DataTypeName { get; set; }

        /// <summary>
        /// Determines if the data type allows a null reference.
        /// </summary>
        public virtual bool IsNullable { get; set; }

        
    }
}

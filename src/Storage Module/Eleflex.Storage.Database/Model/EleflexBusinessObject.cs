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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// The foundation object of the Eleflex reflectionless design.
    /// </summary>
    public partial class EleflexBusinessObject : EleflexObject, IEleflexBusinessObject
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.EleflexBusinessObject" + EleflexProperty.Field_Seperator;

        /// <summary>
        /// Internal PropertiesChanged.
        /// </summary>
        protected List<string> _propertiesChanged = new List<string>();


        /// <summary>
        /// IEBONotifyProperty event.
        /// </summary>
        public event EventHandler EBOPropertyChangedEvent;


        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexBusinessObject() : base()  
        {
            _eleflexObjectKey = EleflexObjectKey;            
        }


        /// <summary>
        /// Create an instance of this type.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new EleflexBusinessObject();
        }

        /// <summary>
        /// Initialize an object with defaults.
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();
            _propertiesChanged = new List<string>();
        }        

        /// <summary>
        /// Signal the DTO that loading its values have been completed.
        /// </summary>
        public virtual void EBOPropertyChangedLoadComplete()
        {
            EBOPropertyChangedReset();
        }

        /// <summary>
        /// Mark the property as being changed.
        /// </summary>
        /// <param name="propertyName"></param>
        public virtual void EBOOnPropertyChanged(string propertyName)
        {
            if (!_propertiesChanged.Contains(propertyName))
                _propertiesChanged.Add(propertyName);
            if (EBOPropertyChangedEvent != null)
                EBOPropertyChangedEvent(propertyName, EventArgs.Empty);
        }

        /// <summary>
        /// Un-Mark the property as being changed.
        /// </summary>
        public virtual void EBOPropertyChangedReset()
        {
            _propertiesChanged.Clear();
        }

        /// <summary>
        /// Get a list of changed properties.
        /// </summary>
        /// <returns></returns>
        public virtual List<string> EBOPropertyChangedGet()
        {
            return _propertiesChanged;
        }

        /// <summary>
        /// Set a value to the managed object.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public override bool EleflexSetPropertyValue(string propertyName, IEleflexDataType value, IEleflexProperty property)
        {
            if (base.EleflexSetPropertyValue(propertyName, value, property))
            {
                EBOOnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Set a value to the managed object.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool EleflexSetPropertyValue(string propertyName, string value)
        {
            if (base.EleflexSetPropertyValue(propertyName, value))
            {
                EBOOnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

    }
}

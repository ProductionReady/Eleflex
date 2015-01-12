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
    /// The foundation object of the Eleflex reflectionless design.
    /// </summary>
    public partial class EleflexObject : IEleflexObject
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public const string EleflexObjectKey = "PR.Eleflex.Eleflex" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Internal EleflexObjectKey.
        /// </summary>
        protected string _eleflexObjectKey = EleflexObjectKey;
        /// <summary>
        /// Internal ExtendedData.
        /// </summary>
        protected Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> _dynamicStorage = null;


        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexObject() 
        {            
        }


        /// <summary>
        /// Provides the primary means of serializing/deserializing dynamic data.
        /// </summary>
        public virtual List<EleflexKeyValue> EleflexKeyValues
        {
            get
            {
                if (_dynamicStorage == null)
                    EleflexInitialize();

                List<EleflexKeyValue> list = new List<EleflexKeyValue>();
                foreach (string key in _dynamicStorage.Keys)
                    list.Add(new EleflexKeyValue(key, _dynamicStorage[key].Item1.ToString()));
                return list;
            }
            set
            {
                List<EleflexKeyValue> list = value;
                if (list == null)
                    return;
                foreach (EleflexKeyValue keyval in list)
                    EleflexSetPropertyValue(keyval.Key, new ValueStringNull(keyval.Value));
            }
        }


        /// <summary>
        /// Create an instance of the implementing object.
        /// </summary>
        /// <returns></returns>
        public virtual IEleflexObject EleflexCreate()
        {
            return new EleflexObject();
        }

        /// <summary>
        /// Initialize an object with defaults.
        /// </summary>
        public virtual void EleflexInitialize()
        {
            _dynamicStorage = new Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>>();
        }        

        /// <summary>
        /// Get the key used to distinguish this object from other managed objects.
        /// </summary>
        /// <returns></returns>
        public virtual string EleflexGetObjectKey()
        {
            return _eleflexObjectKey;
        }

        /// <summary>
        /// Get the underlying dymamic storage mechanism.
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> EleflexGetStorage()
        {
            if (_dynamicStorage == null)
                EleflexInitialize();
            return _dynamicStorage;
        }

        /// <summary>
        /// Set the underlying dymamic storage mechanism.
        /// </summary>
        /// <returns></returns>
        public virtual void EleflexSetStorage(Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> storage)
        {
            _dynamicStorage = storage;
        }

        /// <summary>
        /// Get a list properties.
        /// </summary>
        /// <returns></returns>
        public virtual List<IEleflexProperty> EleflexGetProperties()
        {
            if (_dynamicStorage == null)
                EleflexInitialize();
            List<IEleflexProperty> list = new List<IEleflexProperty>();
            foreach (string key in _dynamicStorage.Keys)
            {
                Tuple<IEleflexDataType, IEleflexProperty> tuple = _dynamicStorage[key];
                if (tuple != null && tuple.Item2 != null)
                    list.Add(tuple.Item2);
            }
            return list;
        }

        /// <summary>
        /// Get a list property names.
        /// </summary>
        /// <returns></returns>
        public virtual List<string> EleflexGetPropertyNames()
        {
            return new List<string>(_dynamicStorage.Keys);
        }

        /// <summary>
        /// Get a value from the managed object.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool EleflexGetPropertyValue(string property, out IEleflexDataType value)
        {
            //If the object was serialized, storage will not be created. Initialize instance.
            if (_dynamicStorage == null)
                EleflexInitialize();

            if (!_dynamicStorage.ContainsKey(property))
            {
                value = null;
                return false;
            }
            value = _dynamicStorage[property].Item1;
            return true;
        }

        /// <summary>
        /// Set a value to the managed object.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool EleflexSetPropertyValue(string propertyName, IEleflexDataType value)
        {
            return EleflexSetPropertyValue(propertyName, value, null);
        }

        /// <summary>
        /// Set a value to the managed object.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <returns></returns>
        public virtual bool EleflexSetPropertyValue(string propertyName, IEleflexDataType value, IEleflexProperty property)
        {
            //If the object was serialized, storage will not be created. Initialize instance.
            if (_dynamicStorage == null)
                EleflexInitialize();

            if (_dynamicStorage.ContainsKey(propertyName))
            {   
                Tuple<IEleflexDataType, IEleflexProperty> cur = _dynamicStorage[propertyName];

                //If property is null, caller is not indending to define a new property definition, 
                //make sure incoming value matching existing property defintion
                if (property == null)
                {                    
                    IEleflexDataType convertedVal;

                    //Current data null, use incoming
                    if(cur.Item1 == null)
                        convertedVal = value;
                    else
                    {
                        //Property and value are both null, bad
                        if(value == null)
                            return false;

                        //Data types match, simply replace existing object
                        if (string.Compare(cur.Item1.DataTypeName, value.DataTypeName, true) == 0)
                            convertedVal = value;
                        else
                        {
                            //Try to convert incoming data to existing property definition
                            if (!cur.Item1.Convert(value.ToString(), out convertedVal))
                                return false;
                        }
                    }
                    _dynamicStorage[propertyName] = new Tuple<IEleflexDataType, IEleflexProperty>(convertedVal, cur.Item2);
                }
                else
                    _dynamicStorage[propertyName] = new Tuple<IEleflexDataType, IEleflexProperty>(value, property);
            }
            else
                _dynamicStorage.Add(propertyName, new Tuple<IEleflexDataType, IEleflexProperty>(value, property));

            return true;
        }

        /// <summary>
        /// Set a value to the managed object.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual bool EleflexSetPropertyValue(string propertyName, string value)
        {
            IEleflexDataType foundValue;
            IEleflexDataType convertedValue = null;
            if (EleflexGetPropertyValue(propertyName, out foundValue))
            {
                if(foundValue == null)
                {
                    //try to get value from property datatype definition (in case property initialized with null datatypevalue)
                    if(_dynamicStorage.ContainsKey(propertyName))
                    {
                        Tuple<IEleflexDataType, IEleflexProperty> def = _dynamicStorage[propertyName];
                        if(def != null && def.Item2 != null)
                            foundValue = EleflexDataTypeFactory.Create(def.Item2.DataTypeName);
                    }
                }
                    
                if (foundValue != null && !foundValue.Convert(value, out convertedValue))
                    return false;
                return EleflexSetPropertyValue(propertyName, convertedValue);
            }
            return EleflexSetPropertyValue(propertyName, new ValueStringNull(value));
        }

    }
}

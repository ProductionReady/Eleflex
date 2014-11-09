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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// This class allows database objects to be "inherited" into one parent class by resolving 
    /// getting and setting values and default relationships between linked EPOs in the chain.
    /// </summary>
    public abstract partial class EDOInheritance : EleflexDatabaseObject
    {

        /// <summary>
        /// Internal InheritedObjects.
        /// </summary>
        protected List<IEleflexDatabaseObject> _inheritedObjects = new List<IEleflexDatabaseObject>();      


        /// <summary>
        /// The list of EPOS comprising this object.
        /// </summary>
        public virtual List<IEleflexDatabaseObject> InheritedObjects
        {
            get { return _inheritedObjects; }
            set { _inheritedObjects = value; }
        }


        /// <summary>
        /// Get constraints.
        /// </summary>
        /// <returns></returns>
        public override List<IDatabaseConstraint> EDOGetConstraints()
        {
            return _inheritedObjects[0].EDOGetConstraints();
        }

        /// <summary>
        /// Get foreign keys.
        /// </summary>
        /// <returns></returns>
        public override List<IDatabaseForeignKey> EDOGetForeignKeys()
        {
            return _inheritedObjects[0].EDOGetForeignKeys();
        }

        /// <summary>
        /// Get indexes.
        /// </summary>
        /// <returns></returns>
        public override List<IDatabaseIndex> EDOGetIndexes()
        {
            return _inheritedObjects[0].EDOGetIndexes();
        }

        /// <summary>
        /// Get schema.
        /// </summary>
        /// <returns></returns>
        public override IDatabaseSchema EDOGetSchema()
        {
            return _inheritedObjects[0].EDOGetSchema();
        }

        /// <summary>
        /// Get the catalog name from the base object.
        /// </summary>
        /// <returns></returns>
        public override string EPOGetCatalogName()
        {
            if (_inheritedObjects == null || _inheritedObjects.Count == 0)
                return string.Empty;
            return _inheritedObjects[0].EPOGetCatalogName();
        }

        /// <summary>
        /// Get the database name of the base object.
        /// </summary>
        /// <returns></returns>
        public override string EDOGetName()
        {
            if (_inheritedObjects == null || _inheritedObjects.Count == 0)
                return string.Empty;
            return _inheritedObjects[0].EDOGetName();
        }
        

        /// <summary>
        /// Get the type of EPO.
        /// </summary>
        /// <returns></returns>
        public override DatabaseObjectType EDOGetType()
        {
            if (_inheritedObjects == null || _inheritedObjects.Count == 0)
                return DatabaseObjectType.Table;
            return _inheritedObjects[0].EDOGetType();
        }

        /// <summary>
        /// Get the list of properties for all the objects.
        /// </summary>
        /// <returns></returns>
        public override List<string> EleflexGetPropertyNames()
        {
            List<string> list = new List<string>();
            List<string> temp = null;

            for (int i = 0; i < _inheritedObjects.Count; i++)
            {
                temp = _inheritedObjects[i].EleflexGetPropertyNames();
                if (temp != null)
                    list.AddRange(temp);
            }
            return list;
        }

        /// <summary>
        /// Get a list of properties for all the objects.
        /// </summary>
        /// <returns></returns>
        public override List<IEleflexProperty> EleflexGetProperties()
        {
            List<IEleflexProperty> list = new List<IEleflexProperty>();
            List<IEleflexProperty> temp = null;

            for (int i = 0; i < _inheritedObjects.Count; i++)
            {
                temp = _inheritedObjects[i].EleflexGetProperties();
                if (temp != null)
                    list.AddRange(temp);
            }
            return list;
        }

        /// <summary>
        /// Get a list of default relations applied to queries.
        /// </summary>
        /// <returns></returns>
        public override List<IPersistenceRelation> EPOGetRelations()
        {
            List<IPersistenceRelation> list = new List<IPersistenceRelation>();
            List<IPersistenceRelation> temp = null;

            for (int i = 0; i < _inheritedObjects.Count; i++)
            {
                temp = _inheritedObjects[i].EPOGetRelations();
                if (temp != null)
                    list.AddRange(temp);
            }
            return list;
        }

        /// <summary>
        /// Get the underlying dymamic storage mechanism.
        /// </summary>
        /// <returns></returns>
        public override Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> EleflexGetStorage()
        {
            Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> combinedStorage = base.EleflexGetStorage();            
            for (int i = 0; i < _inheritedObjects.Count; i++)
            {
                Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> temp = _inheritedObjects[i].EleflexGetStorage();
                foreach (string key in temp.Keys)
                {
                    if(!combinedStorage.ContainsKey(key))
                        combinedStorage.Add(key, temp[key]);
                }
            }
            return combinedStorage;
        }

        /// <summary>
        /// Set the underlying dymamic storage mechanism.
        /// </summary>
        /// <param name="storage"></param>
        public override void EleflexSetStorage(Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> storage)
        {
            foreach (string key in storage.Keys)
            {
                bool found = false;
                for (int i = 0; i < _inheritedObjects.Count; i++)
                {
                    Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> tempStorage = _inheritedObjects[i].EleflexGetStorage();
                    if (tempStorage.ContainsKey(key))
                    {
                        found = true;
                        tempStorage[key] = storage[key];
                        break;
                    }
                }
                if (!found)
                {
                    if (_dynamicStorage == null)
                        EleflexInitialize();

                    if (_dynamicStorage.ContainsKey(key))
                        _dynamicStorage[key] = storage[key];
                    else
                        _dynamicStorage.Add(key, storage[key]);
                }
            }
        }

        /// <summary>
        /// Get a value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool EleflexGetPropertyValue(string propertyName, out IEleflexDataType value)
        {
            for (int i = 0; i < _inheritedObjects.Count; i++)
            {
                IEleflexDatabaseObject obj = _inheritedObjects[i];
                if(obj.EleflexGetPropertyNames().Contains(propertyName))
                    return obj.EleflexGetPropertyValue(propertyName, out value);                    
            }
            return base.EleflexGetPropertyValue(propertyName, out value);
        }

        /// <summary>
        /// Set a value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool EleflexSetPropertyValue(string propertyName, IEleflexDataType value)
        {
            for (int i = 0; i < _inheritedObjects.Count; i++)
            {
                IEleflexDatabaseObject obj = _inheritedObjects[i];
                if (obj.EleflexGetPropertyNames().Contains(propertyName))
                    return obj.EleflexSetPropertyValue(propertyName, value);
            }
            return base.EleflexSetPropertyValue(propertyName, value);
        }

    }
}

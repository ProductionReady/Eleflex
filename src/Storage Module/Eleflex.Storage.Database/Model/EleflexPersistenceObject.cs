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
using Eleflex.Storage.Database.Filters;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Persistence.
    /// </summary>
    public partial class EleflexPersistenceObject : EleflexSecurityObject, IEleflexPersistenceObject
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.Persistence.EleflexPersistenceObject" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Internal EPOCatalogName.
        /// </summary>
        protected string _epoCatalogName;

        /// <summary>
        /// Internal relations.
        /// </summary>
        protected List<IPersistenceRelation> _relations = new List<IPersistenceRelation>();


        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexPersistenceObject() : base() 
        {
            _eleflexObjectKey = EleflexObjectKey;
        }


        /// <summary>
        /// Create an instance of the implementing object to eliminate reflection usage.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new EleflexPersistenceObject();
        }

        /// <summary>
        /// Initialize an object with defaults.
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();            
        }        

        /// <summary>
        /// Get the catalog this EPO is associated to.
        /// </summary>
        /// <returns></returns>
        public virtual string EPOGetCatalogName()
        {
            return _epoCatalogName;
        }

        /// <summary>
        /// Get a list of relationships for this object.
        /// </summary>
        /// <returns></returns>
        public virtual List<IPersistenceRelation> EPOGetRelations()
        {
            return _relations;
        }

        /// <summary>
        /// Get a list of default relationships that are automatically used with this object.
        /// </summary>
        /// <returns></returns>
        public virtual List<string> EPOGetDefaultRelations()
        {
            return new List<string>();
        }

        /// <summary>
        /// Get the list of property definitions for this object.
        /// </summary>
        /// <returns>A list of property definitions defining this EPO.</returns>
        public virtual List<IEleflexProperty> EPOGetProperties()
        {
            List<IEleflexProperty> list = new List<IEleflexProperty>();
            Dictionary<string, Tuple<IEleflexDataType, IEleflexProperty>> storage = EleflexGetStorage();
            foreach (string key in storage.Keys)
            {
                Tuple<IEleflexDataType, IEleflexProperty> definition = storage[key];
                if(definition.Item2 is IEPOProperty)
                    list.Add(definition.Item2 as IEleflexProperty);
            }
            return list;
        }


        /// <summary>
        /// Set a property to be updated.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="property"></param>
        protected virtual void EPOUpdateColumn(IPersistenceRequest request, IEleflexProperty property)
        {
            if (request.Filters == null)
                return;

            foreach (IPersistenceFilter filter in request.Filters)
            {
                if (filter.FilterEnum == FilterEnum.Update && filter.Properties != null && filter.Properties.Count > 0)
                {
                    foreach (IEleflexProperty prop in filter.Properties)
                    {
                        if (prop.Name == property.Name)
                            return;
                    }
                }
            }
            request.Filters.Add(new FilterUpdate(property));
        }

        /// <summary>
        /// Pre get trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPreGet(IPersistenceRequest request) { }

        /// <summary>
        /// Post get trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPostGet(IPersistenceRequest request, List<IEleflexPersistenceObject> result) { }

        /// <summary>
        /// Pre delete trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPreDelete(IPersistenceRequest request) { }

        /// <summary>
        /// Post delete trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPostDelete(IPersistenceRequest request, int result) { }

        /// <summary>
        /// Pre update trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPreUpdate(IPersistenceRequest request) { }

        /// <summary>
        /// Post update trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPostUpdate(IPersistenceRequest request) { }

        /// <summary>
        /// Pre insert trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPreInsert(IPersistenceRequest request) { } 

        /// <summary>
        /// Post insert trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPostInsert(IPersistenceRequest request) { }

        /// <summary>
        /// Pre get aggregate trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPreGetAggregate(IPersistenceRequest request) { }

        /// <summary>
        /// Post get aggregate trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPostGetAggregate(IPersistenceRequest request, double result) { }

        /// <summary>
        /// Pre bulk update trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPreBulkUpdate(IPersistenceRequest request) { }

        /// <summary>
        /// Post bulk update trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPostBulkUpdate(IPersistenceRequest request) { }

        /// <summary>
        /// Pre bulk delete trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPreBulkDelete(IPersistenceRequest request) { }

        /// <summary>
        /// Post bulk delete trigger.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual void EPOTriggerPostBulkDelete(IPersistenceRequest request) { }

    }
}

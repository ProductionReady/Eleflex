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
using Eleflex.Storage.Database.Filters;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines the methods a supported provider must implement to be used by the framework.
    /// </summary>
    public abstract partial class PersistenceProviderBase : IPersistenceProvider, IDisposable
    {

        /// <summary>
        /// Constant used for logging.
        /// </summary>
        public const string CLASSNAME = "PR.Eleflex.Persistence.PersistenceProvider" + EleflexProperty.Field_Seperator;

        /// <summary>
        /// Disposal.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Execute an update statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IPersistenceResponseItem<IEleflexPersistenceObject> Update(IPersistenceRequest request)
        {
            //Validate Input
            if (request == null || request.Context == null)
                return null;

            if (request.EPO == null)
            {
                const string methodName = CLASSNAME + "Update";
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }

            AddDefaultUpdateProperties(request);
            return null;
        }

        /// <summary>
        /// Execute an insert statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IPersistenceResponseItem<IEleflexPersistenceObject> Insert(IPersistenceRequest request)
        {
            //Validate Input
            if (request == null || request.Context == null)
                return null;
            if (request.EPO == null)
            {
                const string methodName = CLASSNAME + "Insert";
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }
            return null;
        }
        
        /// <summary>
        /// Execute a get statement to return one item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IPersistenceResponseItem<IEleflexPersistenceObject> GetItem(IPersistenceRequest request)
        {
            //Validate Input
            if (request == null || request.Context == null)
                return null;
            if (request.EPO == null)
            {
                const string methodName = CLASSNAME + "GetItem";
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }
            AddDefaultSelectProperties(request);

            return null;
        }

        /// <summary>
        /// Execute a get statement to return a list of items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IPersistenceResponseItems<IEleflexPersistenceObject> GetItems(IPersistenceRequest request)
        {
            //Validate Input
            if (request == null || request.Context == null)
                return null;
            if (request.EPO == null)
            {
                const string methodName = CLASSNAME + "GetItems";
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }
            AddDefaultSelectProperties(request);
            return null;
        }

        /// <summary>
        /// Execute a statement to return an aggregated number.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IPersistenceResponseItem<double> GetAggregate(IPersistenceRequest request)
        {
            //Validate Input
            if (request == null || request.Context == null)
                return null;
            if (request.EPO == null)
            {
                const string methodName = CLASSNAME + "GetAggregate";
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }
            return null;
        }

        /// <summary>
        /// Execute a delete statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IPersistenceResponseItem<int> Delete(IPersistenceRequest request)
        {
            //Validate Input
            if (request == null || request.Context == null)
                return null;
            if (request.EPO == null)
            {
                const string methodName = CLASSNAME + "Delete";
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }
            return null;
        }

        /// <summary>
        /// Execute a bulk update statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IPersistenceResponseItem<int> BulkUpdate(IPersistenceRequest request)
        {
            //Validate Input
            if (request == null || request.Context == null)
                return null;
            if (request.EPO == null)
            {
                const string methodName = CLASSNAME + "BulkUpdate";
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }

            AddDefaultUpdateProperties(request);
            return null;
        }

        /// <summary>
        /// Execute a bulk delete statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public virtual IPersistenceResponseItem<int> BulkDelete(IPersistenceRequest request)
        {
            //Validate Input
            if (request == null || request.Context == null)
                return null;
            if (request.EPO == null)
            {
                const string methodName = CLASSNAME + "BulkDelete";
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_EPONull));
                return null;
            }
            return null;
        }

        /// <summary>
        /// Begin a transaction. Inheritors provide functionality.
        /// </summary>
        /// <returns></returns>
        public virtual IPersistenceTransaction BeginTransaction()
        {
            return null;
        }

        /// <summary>
        /// Add the default list of select properties if none defined.
        /// </summary>
        /// <param name="request"></param>
        public virtual void AddDefaultSelectProperties(IPersistenceRequest request)
        {
            //If no select properties are specified, we assume to select all
            bool addDefault = true;
            if (request.Filters == null || request.Filters.Count == 0)
                request.Filters = new List<IPersistenceFilter>();

            foreach (IPersistenceFilter filter in request.Filters)
            {
                if (filter.IsSelectClause && !filter.IsExpression)
                {
                    addDefault = false;
                    break;
                }
            }
            if (addDefault)
            {
                List<IEleflexProperty> props = request.EPO.EleflexGetProperties();
                foreach (IEleflexProperty prop in props)
                {
                    if(prop is IEPOProperty)
                        request.Filters.Add(new FilterSelect(prop));
                }
            }
        }

        /// <summary>
        /// Add the default list of update properties if none defined.
        /// </summary>
        /// <param name="request"></param>
        public virtual void AddDefaultUpdateProperties(IPersistenceRequest request)
        {
            //If no update properties are specified in the filter, find the default ones
            bool addDefault = true;
            if (request.Filters == null || request.Filters.Count == 0)
                request.Filters = new List<IPersistenceFilter>();

            //Find all update filters
            foreach (IPersistenceFilter filter in request.Filters)
            {
                if (filter.IsUpdateClause)
                {
                    addDefault = false;
                    break;
                }
            }
            if (!addDefault)
                return;

            List<IEleflexProperty> validUpdateProperties = new List<IEleflexProperty>();
            List<IEleflexProperty> props = request.EPO.EleflexGetProperties();
            foreach (IEleflexProperty prop in props)
            {
                if (prop is IEBOProperty)
                {
                    IEBOProperty bprop = prop as IEBOProperty;
                    //We do support primary key updating, but not for a default list of columns.
                    if (bprop.IsComputed || bprop.IsKey)
                    {
                        //do not add these
                    }
                    else
                        validUpdateProperties.Add(prop);
                }
                else
                    validUpdateProperties.Add(prop);
            }

            //Find changed properties
            IEBOPropertyChange changedEPO = request.EPO as IEBOPropertyChange;
            if (changedEPO != null)
            {
                //Find the changed properties
                bool found = false;
                List<string> changedProps = changedEPO.EBOPropertyChangedGet();
                foreach (string changedProp in changedProps)
                {
                    foreach (IEleflexProperty prop in validUpdateProperties)
                    {
                        if (prop.Name == changedProp)
                        {
                            found = true;
                            if (prop is IEPOProperty)
                                request.Filters.Add(new FilterUpdate(prop));
                            break;
                        }
                    }
                }                    
                if (found)
                    return;

                //If we didn't find any modified properties, use the default
            }

            //Add all possible properties
            foreach (IEleflexProperty prop in validUpdateProperties)
            {
                if (prop is IEPOProperty)
                    request.Filters.Add(new FilterUpdate(prop));
            }
        }

    }
}

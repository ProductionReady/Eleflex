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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Entry point helper class used to easily access data from storage providers.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static partial class DataAccess<T> where T : IEleflexPersistenceObject, new()
    {

        /// <summary>
        /// Constant used for logging purposes.
        /// </summary>
        private const string CLASSNAME = "PR.Eleflex.Persistence.DataAccess" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// The Data Transfer Object associated with this typed class.
        /// </summary>
        private static IEleflexPersistenceObject _epo = new T();



        /// <summary>
        /// Create request.
        /// </summary>
        /// <returns></returns>
        public static IPersistenceRequest CreateRequest()
        {
            return new PersistenceRequest(_epo);
        }


        
        public static IPersistenceRequest CreateRequest(ISecurityContext context)
        {
            return new PersistenceRequest(context, _epo, null);
        }

        /// <summary>
        /// Create request.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static IPersistenceRequest CreateRequest(IEleflexPersistenceObject dto)
        {
            return new PersistenceRequest(dto);
        }

        /// <summary>
        /// Create request.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public static IPersistenceRequest CreateRequest(ISecurityContext context, IEleflexPersistenceObject dto)
        {            
            return new PersistenceRequest(context, dto, null);
        }

        /// <summary>
        /// Create request.
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IPersistenceRequest CreateRequest(IPersistenceTransaction transaction)
        {
            return new PersistenceRequest(null,_epo,transaction);
        }

        /// <summary>
        /// Create request.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IPersistenceRequest CreateRequest(ISecurityContext context, IPersistenceTransaction transaction)
        {
            return new PersistenceRequest(context, _epo, transaction);
        }

        /// <summary>
        /// Create request.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IPersistenceRequest CreateRequest(IEleflexPersistenceObject dto, IPersistenceTransaction transaction)
        {
            return new PersistenceRequest(null,dto, transaction);
        }

        /// <summary>
        /// Create request.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static IPersistenceRequest CreateRequest(ISecurityContext context, IEleflexPersistenceObject dto, IPersistenceTransaction transaction)
        {
            return new PersistenceRequest(context, dto, transaction);
        }

        /// <summary>
        /// Begin a new transaction.
        /// </summary>
        /// <returns></returns>
        public static IPersistenceTransaction BeginTransaction()
        {
            IPersistenceProvider provider = InterfaceCache<IPersistenceProvider>.Get(_epo.EPOGetCatalogName());
            if (provider == null)
                return null;

            return provider.BeginTransaction();
        }

        /// <summary>
        /// Execute an aggregate statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static double GetAggregate(IPersistenceRequest request)
        {
            if (request.Context.IsError)
                return 0;

            IPersistenceProvider provider = GetPersistenceProvider(request);
            if (request.Context.IsError)
                return 0;

            IPersistenceResponseItem<double> respGet = provider.GetAggregate(request);
            if (request.Context.IsError)
                return 0;

            return respGet.Item;
        }

        /// <summary>
        /// Execute a get statement returning one item.
        /// </summary>
        /// <param name="epo"></param>
        /// <returns></returns>
        public static T GetItem(IEleflexPersistenceObject epo)
        {
            IPersistenceRequest request = CreateRequest(epo);
            T item = GetItem(request);
            if (request.Context.IsError)
                throw new PersistenceException(request); //Throw exception if not using context
            return item;
        }

        /// <summary>
        /// Execute a get statement returning one item.
        /// </summary>
        /// <param name="epo"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static T GetItem(IEleflexPersistenceObject epo, IPersistenceTransaction trans)
        {
            IPersistenceRequest request = CreateRequest(epo, trans);
            T item = GetItem(request);
            if (request.Context.IsError)
                throw new PersistenceException(request); //Throw exception if not using context
            return item;
        }

        /// <summary>
        /// Execute a get statement returning one item.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static T GetItem(IPersistenceRequest request)
        {
            if (request.Context.IsError)
                return default(T);

            IPersistenceProvider provider = GetPersistenceProvider(request);
            if (request.Context.IsError)
                return default(T);

            IPersistenceResponseItem<IEleflexPersistenceObject> respGet = provider.GetItem(request);
            if (request.Context.IsError)
                return default(T);

            if (respGet.Item != null)
                return (T)respGet.Item;
            return default(T);
        }

        /// <summary>
        /// Execute a get statement returning a list of typed items.
        /// </summary>
        /// <returns></returns>
        public static List<T> GetItems()
        {
            IPersistenceRequest request = CreateRequest();
            List<T> list = GetItems(request);
            if (request.Context.IsError)
                throw new PersistenceException(request); //Throw exception if not using context
            return list;
        }

        /// <summary>
        /// Execute a get statement returning a list of typed items.
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static List<T> GetItems(IPersistenceTransaction trans)
        {
            IPersistenceRequest request = CreateRequest(trans);
            List<T> list = GetItems(request);
            if (request.Context.IsError)
                throw new PersistenceException(request); //Throw exception if not using context
            return list;
        }

        /// <summary>
        /// Execute a get statement returning a list of typed items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static List<T> GetItems(IPersistenceRequest request)
        {
            List<IEleflexPersistenceObject> list = GetItemsManaged(request);
            if (request.Context.IsError)
                return null;
            
            List<T> resp = new List<T>();
            for(int i=0;i<list.Count;i++)
            {
                resp.Add((T)list[i]);
            }
            return resp;
        }

        /// <summary>
        /// Execute a get statement returning a list of typed items.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static List<IEleflexPersistenceObject> GetItemsManaged(IPersistenceRequest request)
        {
            IPersistenceProvider provider = GetPersistenceProvider(request);
            if (request.Context.IsError)
                return null;

            IPersistenceResponseItems<IEleflexPersistenceObject> respGet = provider.GetItems(request);
            if (request.Context.IsError)
                return null;

            return respGet.Items;
        }

        /// <summary>
        /// Execute an update statement.
        /// </summary>
        /// <param name="epo"></param>
        /// <returns></returns>
        public static T Update(IEleflexPersistenceObject epo)
        {
            IPersistenceRequest request = CreateRequest(epo);
            T item = Update(request);
            if (request.Context.IsError)
                throw new PersistenceException(request); //Throw exception if not using context
            return item;
        }

        /// <summary>
        /// Execute an update statement.
        /// </summary>
        /// <param name="epo"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static T Update(IEleflexPersistenceObject epo, IPersistenceTransaction trans)
        {
            IPersistenceRequest request = CreateRequest(epo, trans);
            T item = Update(request);
            if (request.Context.IsError)
                throw new PersistenceException(request); //Throw exception if not using context
            return item;
        }

        /// <summary>
        /// Execute an update statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static T Update(IPersistenceRequest request)
        {
            IPersistenceProvider provider = GetPersistenceProvider(request);
            if (request.Context.IsError)
                return default(T);

            IPersistenceResponseItem<IEleflexPersistenceObject> respUpdate = provider.Update(request);
            if (request.Context.IsError)
                return default(T);

            if (respUpdate.Item != null)
                return (T)respUpdate.Item;
            return default(T);
        }

        /// <summary>
        /// Execute a bulk update statement.
        /// </summary>
        /// <param name="request"></param>
        public static int BulkUpdate(IPersistenceRequest request)
        {
            IPersistenceProvider provider = GetPersistenceProvider(request);
            if (request.Context.IsError)
                return 0;

            IPersistenceResponseItem<int> respUpdate = provider.BulkUpdate(request);
            if (request.Context.IsError)
                return 0;
            return respUpdate.Item;
        }

        /// <summary>
        /// Execute an insert statement.
        /// </summary>
        /// <param name="epo"></param>
        /// <returns></returns>
        public static T Insert(IEleflexPersistenceObject epo)
        {
            IPersistenceRequest request = CreateRequest(epo);
            T item = Insert(request);
            if (request.Context.IsError)
                throw new PersistenceException(request); //Throw exception if not using context
            return item;
        }

        /// <summary>
        /// Execute an insert statement.
        /// </summary>
        /// <param name="epo"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static T Insert(IEleflexPersistenceObject epo, IPersistenceTransaction trans)
        {
            IPersistenceRequest request = CreateRequest(epo, trans);
            T item = Insert(request);
            if (request.Context.IsError)
                throw new PersistenceException(request); //Throw exception if not using context
            return item;
        }

        /// <summary>
        /// Execute an insert statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static T Insert(IPersistenceRequest request)
        {
            IPersistenceProvider provider = GetPersistenceProvider(request);
            if (request.Context.IsError)
                return default(T);

            IPersistenceResponseItem<IEleflexPersistenceObject> respInsert = provider.Insert(request);
            if (request.Context.IsError)
                return default(T);

            if (respInsert.Item != null)
                return (T)respInsert.Item;
            return default(T);
        }

        /// <summary>
        /// Execute a delete statement.
        /// </summary>
        /// <param name="epo"></param>
        /// <returns></returns>
        public static int Delete(IEleflexPersistenceObject epo)
        {
            IPersistenceRequest request = CreateRequest(epo);
            int item = Delete(request);
            if (request.Context.IsError)
                throw new PersistenceException(request); //Throw exception if not using context
            return item;
        }

        /// <summary>
        /// Execute a delete statement.
        /// </summary>
        /// <param name="epo"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static int Delete(IEleflexPersistenceObject epo, IPersistenceTransaction trans)
        {
            IPersistenceRequest request = CreateRequest(epo, trans);
            int item = Delete(request);
            if (request.Context.IsError)
                throw new PersistenceException(request); //Throw exception if not using context
            return item;
        }

        /// <summary>
        /// Execute a delete statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static int Delete(IPersistenceRequest request)
        {
            IPersistenceProvider provider = GetPersistenceProvider(request);
            if (request.Context.IsError)
                return 0;

            IPersistenceResponseItem<int> respDelete = provider.Delete(request);
            if (request.Context.IsError)
                return 0;
            return respDelete.Item;
        }

        /// <summary>
        /// Execute a bulk delete statement.
        /// </summary>
        /// <param name="request"></param>
        public static int BulkDelete(IPersistenceRequest request)
        {
            IPersistenceProvider provider = GetPersistenceProvider(request);
            if (request.Context.IsError)
                return 0;

            IPersistenceResponseItem<int> respDelete = provider.BulkDelete(request);
            if (request.Context.IsError)
                return 0;
            return respDelete.Item;
        }

        /// <summary>
        /// Get the storage provider.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static IPersistenceProvider GetPersistenceProvider(IPersistenceRequest request)
        {
            string catalog = _epo.EPOGetCatalogName();
            IPersistenceProvider provider = InterfaceCache<IPersistenceProvider>.Get(catalog);
            if (provider == null)
            {
                const string methodName = CLASSNAME + "GetPersistenceProvider";
                request.Context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_StorageProviderNotFound1, catalog));
                return null;
            }
            return provider;
        }

    }
}

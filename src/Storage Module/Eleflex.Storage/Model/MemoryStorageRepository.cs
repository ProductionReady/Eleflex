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
using System.Data;
using System.Linq;
using Eleflex;
using Eleflex.Storage;

namespace Eleflex.Storage
{
    /// <summary>
    /// Memory storage repository pattern for an entity.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TId"></typeparam>
    public class MemoryStorageRepository<TDomain> : IStorageRepository<TDomain, string>
        where TDomain : class, IMemoryStorageObject
    {

        protected readonly static Dictionary<string, Dictionary<string, TDomain>> _cache = new Dictionary<string, Dictionary<string, TDomain>>();
        protected readonly static object _lock = new object();
        protected Type _type;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="storageProvider"></param>
        protected MemoryStorageRepository()
        {
            _type = typeof(TDomain);
        }

        /// <summary>
        /// GetStorageProvider the domain cache.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Dictionary<string, TDomain> GetDomainCache(string name)
        {
            Dictionary<string, TDomain> container = null;
            if (_cache.ContainsKey(name))
                return _cache[name];

            container = new Dictionary<string, TDomain>();
            _cache.Add(name, container);
            return container;

        }

        /// <summary>
        /// Insert and Entity.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public virtual TDomain Insert(TDomain domain)
        {
            lock (_lock)
            {
                var domainCache = GetDomainCache(_type.Name);
                domain.EleflexKey = Guid.NewGuid().ToString();
                domainCache.Add(domain.EleflexKey, domain);
            }
            return domain;
        }

        /// <summary>
        /// GetStorageProvider an entity by it's key.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TDomain Get(string id)
        {
            lock (_lock)
            {
                var domainCache = GetDomainCache(_type.Name);
                if (domainCache.ContainsKey(id))
                    return domainCache[id];
            }
            return null;
        }

        /// <summary>
        /// Update and entity.
        /// </summary>
        /// <param name="domain"></param>
        /// <returns></returns>
        public virtual TDomain Update(TDomain domain)
        {
            lock (_lock)
            {
                var domainCache = GetDomainCache(_type.Name);
                if (domainCache.ContainsKey(domain.EleflexKey))
                    domainCache[_type.Name] = domain;
            }
            return domain;
        }

        /// <summary>
        /// Delete an entity.
        /// </summary>
        /// <param name="key"></param>
        public virtual void Delete(string key)
        {
            lock (_lock)
            {
                var domainCache = GetDomainCache(_type.Name);
                if (domainCache.ContainsKey(key))
                    domainCache.Remove(key);
            }
        }

        /// <summary>
        /// Query entities.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual IList<TDomain> Query(IStorageQuery query)
        {
            lock (_lock)
            {
                AutoMapper.Mapper.CreateMap<TDomain, TDomain>();
                var domainCache = GetDomainCache(_type.Name);
                List<TDomain> list = new List<TDomain>();
                foreach (TDomain item in domainCache.Values)
                    list.Add(AutoMapper.Mapper.Map<TDomain>(item));                
                return list;
            }
        }

        /// <summary>
        /// Query entities.
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        public virtual double QueryAggregate(IStorageQuery storageQuery)
        {
            lock (_lock)
            {
                AutoMapper.Mapper.CreateMap<TDomain, TDomain>();
                var domainCache = GetDomainCache(_type.Name);
                List<TDomain> list = new List<TDomain>();
                foreach (TDomain item in domainCache.Values)
                    list.Add(AutoMapper.Mapper.Map<TDomain>(item));
                return list.Count;
            }
        }

        /// <summary>
        /// Commit.
        /// </summary>
        public void Commit()
        {            
        }

        /// <summary>
        /// Rollback.
        /// </summary>
        public void Rollback()
        {            
        }
    }
}
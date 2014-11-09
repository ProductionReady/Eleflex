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
using System.Linq;
using System.Data.Entity;
using Eleflex.Lookups;
using Eleflex.Storage.EntityFramework;
using DomainModel = Eleflex.Lookups;
using StorageModel = Eleflex.Lookups.Storage.SqlServer.Model;

namespace Eleflex.Lookups.Storage.SqlServer
{
    /// <summary>
    /// Repository for a log.
    /// </summary>
    public class LookupRepository : EntityStorageRepository<DomainModel.Lookup, StorageModel.Lookup, ILookupsStorageProvider, string>, ILookupRepository
    {
        public LookupRepository(ILookupsStorageProvider sessionProvider) : base(sessionProvider) { }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override DomainModel.Lookup Get(string id)
        {
            var session = _storageProvider.GetSession().Session as DbContext;            
            StorageModel.Lookup entity = session.Set<StorageModel.Lookup>().Find(int.Parse(id)); //ID is different type in DB
            return AutoMapper.Mapper.Map<DomainModel.Lookup>(entity);
        }

        /// <summary>
        /// Get a list of categories.
        /// </summary>
        /// <returns></returns>
        public virtual IList<DomainModel.Lookup> GetCategories()
        {
            var session = _storageProvider.GetSession().Session as DbContext;
            var list = session.Set<StorageModel.Lookup>().Where(x => x.CategoryKey == null).OrderBy(x => x.SortOrder).ToList();
            return AutoMapper.Mapper.Map<List<DomainModel.Lookup>>(list);
        }

        /// <summary>
        /// Get a list of lookups for the specified code.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public virtual IList<DomainModel.Lookup> GetLookupsForCategoryCode(Guid code)
        {
            List<DomainModel.Lookup> outputList = new List<DomainModel.Lookup>();
            var session = _storageProvider.GetSession().Session as StorageModel.LookupsDB;
            var list = (from l in session.Lookups
                        join lp in session.Lookups on l.CategoryKey equals lp.LookupKey
                        where lp.Code == code
                        select l).OrderBy(x=>x.SortOrder).ToList();
            foreach (var item in list)
            {
                DomainModel.Lookup lookup = AutoMapper.Mapper.Map<DomainModel.Lookup>(item);
                lookup.Category = AutoMapper.Mapper.Map<DomainModel.Lookup>(item.Parent);
                outputList.Add(lookup);
            }
            return outputList;
        }

        /// <summary>
        /// Get a list of lookups for the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual IList<DomainModel.Lookup> GetLookupsForCategoryName(string name)
        {
            List<DomainModel.Lookup> outputList = new List<DomainModel.Lookup>();
            var session = _storageProvider.GetSession().Session as DbContext;
            var list = (from l in session.Set<StorageModel.Lookup>()
                        join lp in session.Set<StorageModel.Lookup>() on l.CategoryKey equals lp.LookupKey
                        where lp.Name == name
                        select l).OrderBy(x => x.SortOrder).ToList();
            foreach (var item in list)
            {
                DomainModel.Lookup lookup = AutoMapper.Mapper.Map<DomainModel.Lookup>(item);
                lookup.Category = AutoMapper.Mapper.Map<DomainModel.Lookup>(item.Parent);
                outputList.Add(lookup);
            }
            return outputList;
        }

    }
}

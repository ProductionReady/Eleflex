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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Eleflex;
using Eleflex.Versioning;
using Eleflex.Storage;
using Eleflex.Storage.EntityFramework;
using DomainModel = Eleflex.Versioning;
using StorageModel = Eleflex.Versioning.Storage.Azure.Model;

namespace Eleflex.Versioning.Storage.SqlServer
{
    /// <summary>
    /// Repository for a ModuleVersion.
    /// </summary>
    public class ModuleVersionEntityRepository : EntityStorageRepository<DomainModel.ModuleVersion, StorageModel.ModuleVersion, IVersioningStorageProvider, Guid>, IModuleVersionRepository
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="sessionProvider"></param>
        public ModuleVersionEntityRepository(IVersioningStorageProvider sessionProvider) : base(sessionProvider) { }

        /// <summary>
        /// Determine if is installed.
        /// </summary>
        /// <returns></returns>
        public bool IsInstalled()
        {
            return this.GetAll().Count > 0;
        }

        /// <summary>
        /// Get all.
        /// </summary>
        /// <returns></returns>
        public List<ModuleVersion> GetAll()
        {
            return this.Query(new StorageQuery()).ToList();
        }
    }
}

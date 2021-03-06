﻿#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2015 Production Ready, LLC. All Rights Reserved.
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
using Eleflex.Storage;
using Eleflex.Lookups;

namespace Eleflex.Lookups
{
    /// <summary>
    /// Inteface for a repository accessing the Lookup domain object.
    /// </summary>
    public interface ILookupsRepository : IStorageRepository<Lookup, Guid>
    {
        /// <summary>
        /// Get a list of categories.
        /// </summary>
        /// <returns></returns>
        IList<Lookup> GetCategories();

        /// <summary>
        /// Get a list of lookups for the specified code.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IList<Lookup> GetLookupsForCategoryKey(Guid key);

        /// <summary>
        /// Get a list of lookups for the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IList<Lookup> GetLookupsForCategoryName(string name);
    }
}

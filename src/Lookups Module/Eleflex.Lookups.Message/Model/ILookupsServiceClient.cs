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
using Eleflex;
using Eleflex.Services;
using Eleflex.Lookups.Message;
using Eleflex.Lookups.Message.LookupCommand;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Lookups.Message
{
    /// <summary>
    /// Service client for the Lookups.
    /// </summary>
    public interface ILookupsServiceClient : IServiceCommandRepository<Lookup, Guid>
    {
        /// <summary>
        /// Get a list of categories.
        /// </summary>
        /// <returns></returns>
        IServiceCommandResponseItems<Lookup> GetCategories();

        /// <summary>
        /// Get a list of lookups for the specified category code.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IServiceCommandResponseItems<Lookup> GetLookupsForCategoryKey(Guid key);

        /// <summary>
        /// Get a list of lookups for the specified category name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IServiceCommandResponseItems<Lookup> GetLookupsForCategoryName(string name);
    }
}

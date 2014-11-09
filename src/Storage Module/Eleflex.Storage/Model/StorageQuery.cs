﻿#region PRODUCTION READY® ELEFLEX® Software License. Copyright © 2014 Production Ready, LLC. All Rights Reserved.
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

namespace Eleflex.Storage
{
    /// <summary>
    /// Defined a storage query.
    /// </summary>
    public class StorageQuery : IStorageQuery
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public StorageQuery()
        {
            Filters = new List<StorageQueryFilter>();
        }

        /// <summary>
        /// List of filters
        /// </summary>
        public virtual List<StorageQueryFilter> Filters { get; set; }

        /// <summary>
        /// The start page.
        /// </summary>
        public virtual int StartPage { get; set; }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public virtual int NumberPerPage { get; set; }

    }
}
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
using System.Data.Entity;
using System.Linq;
using Eleflex.Storage;

namespace Eleflex.Storage
{
    /// <summary>
    /// Memory storage provider.
    /// </summary>
    public class MemoryStorageProvider : StorageProvider
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="connectionStringKey"></param>
        public MemoryStorageProvider(string key, string connectionStringKey)
            : base(key, connectionStringKey)
        {
        }

        /// <summary>
        /// Create a session.
        /// </summary>
        /// <returns></returns>
        public override IStorageSession CreateNonManagedSession()
        {
            return new MemoryStorageSession();
        }

    }
}

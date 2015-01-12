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
    /// Interface defining an index.
    /// </summary>
    public interface IDatabaseIndex : IEleflexSecurityObject
    {

        /// <summary>
        /// The parent object this index is associated.
        /// </summary>
        IEleflexDatabaseObject Parent { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        string Name { get; set; }
       
        /// <summary>
        /// Description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Determine if the index is clustered.
        /// </summary>
        bool IsClustered { get; set; }

        /// <summary>
        /// Determine if this is unique.
        /// </summary>
        bool IsUnique { get; set; }

        /// <summary>
        /// The list of associated columns.
        /// </summary>
        List<IEDOProperty> Columns { get; set; }

    }
}

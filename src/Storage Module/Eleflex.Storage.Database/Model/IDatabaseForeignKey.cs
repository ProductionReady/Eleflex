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
    /// Interface defining a foreign key.
    /// </summary>
    public interface IDatabaseForeignKey : IPersistenceRelation, IEleflexSecurityObject
    {

        /// <summary>
        /// The name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The primary database object.
        /// </summary>
        IEleflexDatabaseObject Primary { get; set; }

        /// <summary>
        /// The list of primary columns.
        /// </summary>
        List<IEDOProperty> PrimaryColumns { get; set; }

        /// <summary>
        /// The foreign object.
        /// </summary>
        IEleflexDatabaseObject Foreign { get; set; }

        /// <summary>
        /// The list of foreign columns.
        /// </summary>
        List<IEDOProperty> ForeignColumns { get; set; }

        /// <summary>
        /// The action to be invoked when deleting.
        /// </summary>
        DatabaseForeignKeyAction DeleteAction { get; set; }

        /// <summary>
        /// The action to be invoked when updating.
        /// </summary>
        DatabaseForeignKeyAction UpdateAction { get; set; }
    }
}

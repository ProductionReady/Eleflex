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

namespace Eleflex.Lookups
{
    /// <summary>
    /// Interface for a modifier of an item and is used for denoting multiple types in the system. (a lookup table)
    /// </summary>
    public interface ILookup
    {
        /// <summary>
        /// The identifier.
        /// </summary>
        Guid Key { get; set; }

        /// <summary>
        /// Determine if inactive.
        /// </summary>
        bool Inactive { get; set; }        

        /// <summary>
        /// Category this lookup belongs to.
        /// </summary>
        Lookup Category { get; set; }

        /// <summary>
        /// Sort order.
        /// </summary>
        int? SortOrder { get; set; }

        /// <summary>
        /// The abbreviation.
        /// </summary>
        string Abbreviation { get; set; }

        /// <summary>
        /// The name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The description.
        /// </summary>
        string Description { get; set; }
    }
}

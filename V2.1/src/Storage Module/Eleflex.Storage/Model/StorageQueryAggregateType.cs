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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Storage
{
    /// <summary>
    /// Enumberation for aggregate types.
    /// </summary>
    public enum StorageQueryAggregateType
    {
        /// <summary>
        /// Average.
        /// </summary>
        Average,
        /// <summary>
        /// Binary Checksum.
        /// </summary>
        BinaryChecksum,
        /// <summary>
        /// Checksum.
        /// </summary>
        Checksum,
        /// <summary>
        /// Count.
        /// </summary>
        Count,
        /// <summary>
        /// Minimum.
        /// </summary>
        Minimum,
        /// <summary>
        /// Maximum.
        /// </summary>
        Maximum,
        /// <summary>
        /// Sum.
        /// </summary>
        Sum
    }
}

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
using System.Runtime.Serialization;
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.Filters
{

    /// <summary>
    /// Types of supported equality.
    /// </summary>
    public enum ComparisonEnum
    {

        /// <summary>
        /// Equality.
        /// </summary>
        EQUAL,
        /// <summary>
        /// InEquality.
        /// </summary>
        NOT_EQUAL,
        /// <summary>
        /// Greater than.
        /// </summary>
        GREATER_THAN,
        /// <summary>
        /// Less than.
        /// </summary>
        LESS_THAN,
        /// <summary>
        /// Greater than or equal to.
        /// </summary>
        GREATER_THAN_EQUAL,
        /// <summary>
        /// Less than or equal to.
        /// </summary>
        LESS_THAN_EQUAL,
        /// <summary>
        /// Not supported.
        /// </summary>
        NOT_SUPPORTED
    }
}

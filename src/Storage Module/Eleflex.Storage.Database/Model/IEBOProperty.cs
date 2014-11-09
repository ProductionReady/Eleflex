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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines the elements of a managed property.
    /// </summary>
    public partial interface IEBOProperty : IEleflexProperty
    {

        /// <summary>
        /// Determines if the property is used to uniquely identify the object.
        /// </summary>
        bool IsKey { get; set; }

        /// <summary>
        /// Determines if the value is computed.
        /// </summary>
        bool IsComputed { get; set; }        

        /// <summary>
        /// The maximum length of the data type.
        /// </summary>
        int MaxLength { get; set; }

        /// <summary>
        /// The precision of the data type.
        /// </summary>
        int Precision { get; set; }

        /// <summary>
        /// The scale of the data type.
        /// </summary>
        int Scale { get; set; }

        /// <summary>
        /// The size of the data type.
        /// </summary>
        int Size { get; set; }

        /// <summary>
        /// Determine if is for concurrency.
        /// </summary>
        bool IsConcurrency { get; set; }

        /// <summary>
        /// Determine if is for creation.
        /// </summary>
        bool IsAuditCreate { get; set; }

        /// <summary>
        /// Determine if is for modifications.
        /// </summary>
        bool IsAuditUpdate { get; set; }


    }
}

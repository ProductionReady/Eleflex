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
    /// Defines a managed system message used within the framework.
    /// </summary>
    public partial interface IEleflexMessage : IEleflexBusinessObject, IEleflexDataKey
    {

        /// <summary>
        /// Computer's current date message was created (UTC).
        /// </summary>
        DateTime CreateDate { get; set; }

        /// <summary>
        /// Determines if the message is an error or informational.
        /// </summary>
        bool IsError { get; set; }

        /// <summary>
        /// Determines if the message is for the system or user.
        /// </summary>
        bool IsSystem { get; set; }

        /// <summary>
        /// Source/method occuring from for detailed tracking.
        /// </summary>
        string Source { get; set; }

        /// <summary>
        /// Any detail relating to the message.
        /// </summary>
        string Detail { get; set; }

        /// <summary>
        /// Properties context message referring to.
        /// </summary>
        List<string> PropertyContext { get; set; }

        /// <summary>
        /// Get the final formatted message.
        /// </summary>
        /// <returns></returns>
        string GetFormattedDetail();

    }
}

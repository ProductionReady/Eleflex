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
using Eleflex.Storage.Database.Filters;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a Persistence request.
    /// </summary>
    public partial interface IPersistenceRequest : ISecurityRequest<ISecurityContext>
    {

        /// <summary>
        /// The Data Transfer Object associated with the request.
        /// </summary>
        IEleflexPersistenceObject EPO { get; set; }

        /// <summary>
        /// Builder
        /// </summary>
        FilterBuilder Builder { get; set; }

        /// <summary>
        /// The list of Persistence filters associated with the request.
        /// </summary>
        List<IPersistenceFilter> Filters { get; set; }

        /// <summary>
        /// The transaction associated with the request.
        /// </summary>
        IPersistenceTransaction Transaction { get; set; }

        /// <summary>
        /// The timout value for the request (in seconds).
        /// </summary>
        int TimeoutSecs { get; set; }

        /// <summary>
        /// The start page.
        /// </summary>
        int StartPage { get; set; }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        int NumberPerPage { get; set; }
        
    }
}

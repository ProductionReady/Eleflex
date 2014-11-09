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
    /// Defines a generator producing a compliant SQL statement based on its
    /// implemented language.
    /// </summary>
    public partial interface IDatabaseFilterToSQLGenerator
    {

        /// <summary>
        /// Get a select statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IDatabaseSQLResponse GetSelectStatement(IPersistenceRequest request);

        /// <summary>
        /// Get an update statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IDatabaseSQLResponse GetUpdateStatement(IPersistenceRequest request);

        /// <summary>
        /// Get an insert statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IDatabaseSQLResponse GetInsertStatement(IPersistenceRequest request);

        /// <summary>
        /// Get a delete statement.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        IDatabaseSQLResponse GetDeleteStatement(IPersistenceRequest request);

    }
}

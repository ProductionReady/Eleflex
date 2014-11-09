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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a DTO object used with databases.
    /// </summary>
    public partial interface IEleflexDatabaseObject : IEleflexPersistenceObject
    {

        /// <summary>
        /// Get the name of the database object.
        /// </summary>
        /// <returns></returns>
        string EDOGetName();

        /// <summary>
        /// Get the type of database object.
        /// </summary>
        /// <returns></returns>
        DatabaseObjectType EDOGetType();        

        /// <summary>
        /// Get a list of database properties.
        /// </summary>
        /// <returns></returns>
        List<IEDOProperty> EDOGetProperties();

        /// <summary>
        /// Get schema.
        /// </summary>
        IDatabaseSchema EDOGetSchema();

        /// <summary>
        /// The list of assocated foreign keys for the object.
        /// </summary>
        List<IDatabaseForeignKey> EDOGetForeignKeys();

        /// <summary>
        /// The list of associated Indexes for the object.
        /// </summary>
        List<IDatabaseIndex> EDOGetIndexes();

        /// <summary>
        /// The list of associated Indexes for the object.
        /// </summary>
        List<IDatabaseConstraint> EDOGetConstraints();

        /// <summary>
        /// Set extended attributes of the object.
        /// </summary>
        /// <param name="constraints"></param>
        /// <param name="foreignKeys"></param>
        /// <param name="indexes"></param>
        void EDOSetExtendedAttrbutes(List<IDatabaseConstraint> constraints, List<IDatabaseIndex> indexes, List<IPersistenceRelation> relations);

    }
}

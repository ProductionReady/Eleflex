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
    /// Defines a database catalog.
    /// </summary>
    public partial class DatabaseCatalog : EleflexSecurityObject, IDatabaseCatalog
    {
        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.Persistence.Database.DatabaseCatalog" + EleflexProperty.Field_Seperator;


        public DatabaseCatalog()
            : base()
        {
            _eleflexObjectKey = EleflexObjectKey;
        }

        /// <summary>
        /// Create an instance of this type.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new DatabaseCatalog();
        }

        /// <summary>
        /// Initialize an object with defaults.
        /// </summary>
        public override void EleflexInitialize()
        {
            base.EleflexInitialize();            
        } 

        /// <summary>
        /// Name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Connection string.
        /// </summary>
        public virtual string ConnectionString { get; set; }

        /// <summary>
        /// Database provider used to access.
        /// </summary>
        public virtual IDatabaseProvider DatabaseProvider { get; set; }

        /// <summary>
        /// List of all data types in the database.
        /// </summary>
        public virtual List<IEDOProperty> DataTypes { get; set; }

        /// <summary>
        /// List of database objects in the catalog.
        /// </summary>
        public virtual List<IEleflexDatabaseObject> DatabaseObjects { get; set; }
        
        /// <summary>
        /// List of schemas in the the catalog.
        /// </summary>
        public virtual List<IDatabaseSchema> Schemas { get; set; }

    }
}

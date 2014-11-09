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
    /// Defines a database foreign key.
    /// </summary>
    public partial class DatabaseForeignKey : EleflexSecurityObject, IDatabaseForeignKey
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public DatabaseForeignKey()
        {
            PrimaryColumns = new List<IEDOProperty>();
            ForeignColumns = new List<IEDOProperty>();
        }


        /// <summary>
        /// Name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Name.
        /// </summary>
        public virtual string DatabaseName { get; set; }

        /// <summary>
        /// Description.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Primary.
        /// </summary>
        public virtual IEleflexDatabaseObject Primary { get; set; }

        /// <summary>
        /// Primary Columnss.
        /// </summary>
        public virtual List<IEDOProperty> PrimaryColumns { get; set; }

        /// <summary>
        /// Foreign table.
        /// </summary>
        public virtual IEleflexDatabaseObject Foreign { get; set; }

        /// <summary>
        /// Foreign Columns.
        /// </summary>
        public virtual List<IEDOProperty> ForeignColumns { get; set; }

        /// <summary>
        /// Delete Action.
        /// </summary>
        public virtual DatabaseForeignKeyAction DeleteAction { get; set; }

        /// <summary>
        /// Update action.
        /// </summary>
        public virtual DatabaseForeignKeyAction UpdateAction { get; set; }


        /// <summary>
        /// Get the name.
        /// </summary>
        /// <returns></returns>
        public string EPRGetName()
        {
            return Name;
        }

        /// <summary>
        /// Get the parent properties.
        /// </summary>
        /// <returns></returns>
        public List<IEleflexProperty> EPRGetParentProperties()
        {
            List<IEleflexProperty> list = new List<IEleflexProperty>();
            List<IEDOProperty> props = PrimaryColumns;
            foreach (IEDOProperty prop in props)
                list.Add(prop);
            return list;
        }

        /// <summary>
        /// Get the foreign properties.
        /// </summary>
        /// <returns></returns>
        public List<IEleflexProperty> EPRGetForeignProperties()
        {
            List<IEleflexProperty> list = new List<IEleflexProperty>();
            List<IEDOProperty> props = ForeignColumns;
            foreach (IEDOProperty prop in props)
                list.Add(prop);
            return list;
        }
    }
}

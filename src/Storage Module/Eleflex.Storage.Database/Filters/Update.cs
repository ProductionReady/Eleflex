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
using System.Text;
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.Filters
{

    /// <summary>
    /// The TSQL update filter item.
    /// </summary>
    public partial class Update : FilterUpdate, IDatabaseFilter
    {

        /// <summary>
        /// Constant used for logging.
        /// </summary>
        public const string CLASSNAME = "PR.Eleflex.Persistence.Database.TSQL.Update" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Constructor.
        /// </summary>
        public Update() :
            base()
        { }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="property"></param>
        public Update(IEleflexProperty property) :
            base(property)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filter"></param>
        public Update(FilterUpdate filter) :
            base(filter.Properties[0])
        { }


        /// <summary>
        /// Get the TSQL fragment.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="propertyReplacementNames"></param>
        /// <param name="valueReplacementNames"></param>
        /// <returns></returns>
        public virtual string GetSQLString(
            IEleflexContext context,
            List<string> propertyReplacementNames,
            List<string> valueReplacementNames)
        {
            const string methodName = CLASSNAME + "GetSQLString";

            if (!this.IsValid)
            {
                context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterInvalid));
                return null;
            }
            if (propertyReplacementNames == null || propertyReplacementNames.Count != _properties.Count)
            {
                context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterPropertyMismatch));
                return null;
            }
            if (valueReplacementNames == null || valueReplacementNames.Count != _properties.Count)
            {
                context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterValueMismatch));
                return null;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append(propertyReplacementNames[0]);
            sql.Append(" = ");
            sql.Append(valueReplacementNames[0]);
            return sql.ToString();
        }

    }
}

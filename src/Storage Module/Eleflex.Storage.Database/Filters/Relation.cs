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
    /// The TSQL relation filter item.
    /// </summary>
    public partial class Relation : FilterRelation, IDatabaseFilter
    {

        /// <summary>
        /// Constant used for logging.
        /// </summary>
        public const string CLASSNAME = "PR.Eleflex.Persistence.Database.TSQL.Relation" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Constructor.
        /// </summary>
        public Relation() :
            base()
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="relation"></param>
        public Relation(IPersistenceRelation relation) :
            base(relation)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filter"></param>
        public Relation(FilterRelation filter) :
            base(filter.Relationship)
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

            StringBuilder sql = new StringBuilder();
            
            bool isNullable = false;
            List<IEleflexProperty> props = _relation.EPRGetParentProperties();
            List<IEleflexProperty> foreignProps = _relation.EPRGetForeignProperties();
            for(int i=0;i<props.Count;i++)
            {
                if(props[i].IsNullable)
                {
                    isNullable = true;
                    break;
                }
            }

            IEleflexDatabaseObject dto = props[0].ParentObject as IEleflexDatabaseObject;
            if(dto==null)
            {
                context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterValueMismatch));
                return null;
            }

            if(isNullable)
                sql.Append(" LEFT JOIN ");
            else
                sql.Append(" INNER JOIN ");

            sql.Append("[");
            sql.Append(dto.EDOGetSchema().Name);
            sql.Append("].[");
            sql.Append(dto.EDOGetName());
            sql.Append("] AS [");
            sql.Append(dto.EDOGetSchema().Name);
            sql.Append(dto.EDOGetName());
            sql.Append("] ON ");

            int number = propertyReplacementNames.Count / 2;
            for (int i = 0; i < number; i++)
            {
                sql.Append(propertyReplacementNames[i]);
                sql.Append(" = ");
                sql.Append(propertyReplacementNames[number + i]);
            }

            return sql.ToString();
        }

    }
}

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
    /// The TSQL Aggregate filter item.
    /// </summary>
    public partial class Aggregate : FilterAggregate, IDatabaseFilter
    {

        /// <summary>
        /// Constant used for logging.
        /// </summary>
        public const string CLASSNAME = "PR.Eleflex.Persistence.Database.TSQL.Aggregate" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Constructor.
        /// </summary>
        public Aggregate() :
            base()
        { }        

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="property"></param>
        /// <param name="aggregateType"></param>
        public Aggregate(IEleflexProperty property, AggregateEnum aggregateType) :
            base(property, aggregateType)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filter"></param>
        public Aggregate(FilterAggregate filter) :
            base(filter.Properties[0], filter.AggregateEnum)
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

            StringBuilder sb = new StringBuilder();
            sb.Append(GetAggregateType(_aggregateType));
            sb.Append("(");
            sb.Append(propertyReplacementNames[0]);
            sb.Append(") AS ");
            sb.Append(valueReplacementNames[0]);
            string sql = sb.ToString();
            sb = null;
            return sql;
        }

        /// <summary>
        /// Get the type of aggregate.
        /// </summary>
        /// <param name="aggregateType"></param>
        /// <returns></returns>
        public static string GetAggregateType(AggregateEnum aggregateType)
        {
            switch (aggregateType)
            {
                default:
                case AggregateEnum.COUNT:
                    return "COUNT";
                case AggregateEnum.AVERAGE:
                    return "AVG";
                case AggregateEnum.BINARY_CHECKSUM:
                    return "BINARY_CHECKSUM";
                case AggregateEnum.CHECKSUM:
                    return "CHECKSUM";
                case AggregateEnum.MAXIMUM:
                    return "MAX";
                case AggregateEnum.MINIMUM:
                    return "MIN";
                case AggregateEnum.SUM:
                    return "SUM";
            }
        }

    }
}

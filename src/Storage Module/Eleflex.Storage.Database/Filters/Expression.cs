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
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.Filters
{

    /// <summary>
    /// The TSQL expression filter item.
    /// </summary>
    public partial class Expression : FilterExpression, IDatabaseFilter
    {

        /// <summary>
        /// Constant used for logging.
        /// </summary>
        public const string CLASSNAME = "PR.Eleflex.Persistence.Database.TSQL.Expression" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Constructor.
        /// </summary>
        public Expression() :
            base()
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="filter"></param>
        public Expression(FilterExpression filter) :
            base(filter.ExpressionEnum)
        { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="expressionType"></param>
        public Expression(ExpressionEnum expressionType) :
            base(expressionType)
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
            if (!this.IsValid)
            {
                const string methodName = CLASSNAME + "GetSQLString";
                context.AddMessage(new EleflexMessage(true, true, methodName, PersistenceConstants.SystemMessage_FilterInvalid));
                return null;
            }

            return GetExpressionType(_expressionType);
        }

        /// <summary>
        /// Get the expression type.
        /// </summary>
        /// <param name="expressionType"></param>
        /// <returns></returns>
        public static string GetExpressionType(ExpressionEnum expressionType)
        {
            switch (expressionType)
            {
                default:
                case ExpressionEnum.AND:
                    return " AND ";
                case ExpressionEnum.BEGIN_EXPRESSION:
                    return " ( ";
                case ExpressionEnum.END_EXPRESSION:
                    return " ) ";
                case ExpressionEnum.OR:
                    return " OR ";
            }
        }

    }
}

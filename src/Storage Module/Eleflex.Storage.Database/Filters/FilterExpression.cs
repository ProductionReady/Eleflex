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
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.Filters
{

    /// <summary>
    /// Defines a expression filter item.
    /// </summary>
    public partial class FilterExpression : FilterBase
    {

        /// <summary>
        /// Internal ExpressionType.
        /// </summary>
        protected ExpressionEnum _expressionType;


        /// <summary>
        /// Constructor.
        /// </summary>
        public FilterExpression() { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="expressionType"></param>
        public FilterExpression(ExpressionEnum expressionType)
        {
            _expressionType = expressionType;
        }


        /// <summary>
        /// The enumeration type of the storage filter.
        /// </summary>
        public override FilterEnum FilterEnum
        {
            get { return FilterEnum.Expression; }
        }

        /// <summary>
        /// The expression type of the filter.
        /// </summary>
        public virtual ExpressionEnum ExpressionEnum
        {
            get { return _expressionType; }
            set { _expressionType = value; }
        }

        /// <summary>
        /// Determines if used in the where clause.
        /// </summary>
        public override bool IsWhereClause
        {
            get { return true; }
        }

        /// <summary>
        /// Determines if this expression is an and statement.
        /// </summary>
        public override bool IsAnd
        {
            get 
            {
                switch (_expressionType)
                {
                    default:
                        return false;
                    case ExpressionEnum.AND:
                        return true; 
                }
                
            }
        }

        /// <summary>
        /// Determines if used as a grouping start expression.
        /// </summary>
        public override bool IsGroupingStartExpression
        {
            get
            {
                switch (_expressionType)
                {
                    default:
                        return false;
                    case ExpressionEnum.BEGIN_EXPRESSION:
                        return true;
                }
            }
        }

        /// <summary>
        /// Determinies if is an expression.
        /// </summary>
        public override bool IsExpression
        {
            get { return true; }
        }

        /// <summary>
        /// Determines if used to group expressions.
        /// </summary>
        public override bool IsGroupingExpression
        {
            get
            {
                switch (_expressionType)
                {
                    default:
                        return false;
                    case ExpressionEnum.BEGIN_EXPRESSION:
                    case ExpressionEnum.END_EXPRESSION:
                        return true;
                }
            }
        }

        /// <summary>
        /// Determines if us used to join an expression.
        /// </summary>
        public override bool IsJoinExpression
        {
            get
            {
                switch (_expressionType)
                {
                    default:
                        return false;
                    case ExpressionEnum.AND:
                    case ExpressionEnum.OR:
                        return true;
                }
            }
        }

        /// <summary>
        /// Determines if the storage filter is valid.
        /// </summary>
        public override bool IsValid
        {
            get { return true; }
        }
        
    }
}

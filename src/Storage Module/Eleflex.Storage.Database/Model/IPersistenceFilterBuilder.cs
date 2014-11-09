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
using Eleflex.Storage.Database.Filters;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Interface defining implementation to to assemble criteria filters for querying and modifying via a storage request.
    /// </summary>
    public partial interface IPersistenceFilterBuilder
    {

        /// <summary>
        /// Get the current set of filters built.
        /// </summary>
        /// <returns></returns>
        List<IPersistenceFilter> GetFilters();

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        IPersistenceFilterBuilder Expression(ExpressionEnum expressionType);

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        IPersistenceFilterBuilder BeginExpression();

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        IPersistenceFilterBuilder EndExpression();

        /// <summary>
        /// Inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        IPersistenceFilterBuilder And();

        /// <summary>
        /// Seperate inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        IPersistenceFilterBuilder Or();

        /// <summary>
        /// Add properties to be selected.
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Select(params string[] propertyNames);

        /// <summary>
        /// Add distinct modifier.
        /// </summary>
        /// <returns></returns>
        IPersistenceFilterBuilder Distinct();

        /// <summary>
        /// Add properties to be updated.
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Update(params string[] propertyNames);

        /// <summary>
        /// Add a between expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lowValue"></param>
        /// <param name="highValue"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Between(string propertyName, string lowValue, string highValue);

        /// <summary>
        /// A an equality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsEqual(string propertyName, string value);

        /// <summary>
        /// Add an inequality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsNotEqual(string propertyName, string value);

        /// <summary>
        /// Add a greater than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsGreaterThan(string propertyName, string value);

        /// <summary>
        /// Add a greater than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsGreaterThanOrEqual(string propertyName, string value);

        /// <summary>
        /// Add a less than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsLessThan(string propertyName, string value);

        /// <summary>
        /// Add a less than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsLessThanOrEqual(string propertyName, string value);

        /// <summary>
        /// Add a comparison expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="comparisonType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Compare(string propertyName, ComparisonEnum comparisonType, string value);

        /// <summary>
        /// Add a property comparison expression.
        /// </summary>
        /// <param name="propertyNameLeft"></param>
        /// <param name="comparisonType"></param>
        /// <param name="propertyNameRight"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder PropertyCompare(string propertyNameLeft, ComparisonEnum comparisonType, string propertyNameRight);

        /// <summary>
        /// Add a is like expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsLike(string propertyName, string value);

        /// <summary>
        /// Add a like expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isLike"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Like(string propertyName, bool isLike, string value);

        /// <summary>
        /// Add a not like expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsNotLike(string propertyName, string value);

        /// <summary>
        /// Add a is null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsNull(string propertyName);

        /// <summary>
        /// Add a null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isNull"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Null(string propertyName, bool isNull);

        /// <summary>
        /// Add a is not null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsNotNull(string propertyName);

        /// <summary>
        /// Add a in set expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsInSet(string propertyName, params string[] values);

        /// <summary>
        /// Add a set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="inSet"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Set(string propertyName, bool inSet, params string[] values);

        /// <summary>
        /// Add a not in set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder IsNotInSet(string propertyName, params string[] values);

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Sort(string propertyName);

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Sort(string propertyName, bool sortAscending);

        /// <summary>
        /// Add an averate aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Average(string propertyName);

        /// <summary>
        /// Add a binary checksum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder BinaryChecksum(string propertyName);

        /// <summary>
        /// Add a checksum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Checksum(string propertyName);

        /// <summary>
        /// Add a maximum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Maximum(string propertyName);

        /// <summary>
        /// Add a minumum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Minimum(string propertyName);

        /// <summary>
        /// Add a sum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Sum(string propertyName);

        /// <summary>
        /// Add an aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="aggregateType"></param>
        /// <returns></returns>
        IPersistenceFilterBuilder Aggregate(string propertyName, AggregateEnum aggregateType);

    }
}

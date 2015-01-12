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

namespace Eleflex.Storage
{
    /// <summary>
    /// Interface defining implementation to to assemble criteria filters for querying and modifying via a storage request.
    /// </summary>
    public partial interface IStorageQueryBuilder
    {

        /// <summary>
        /// Get storage query with the configured filters.
        /// </summary>
        /// <returns></returns>
        StorageQuery GetStorageQuery();

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        IStorageQueryBuilder Expression(StorageQueryExpressionType expressionType);

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        IStorageQueryBuilder BeginExpression();

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        IStorageQueryBuilder EndExpression();

        /// <summary>
        /// Inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        IStorageQueryBuilder And();

        /// <summary>
        /// Seperate inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        IStorageQueryBuilder Or();

        /// <summary>
        /// Add properties to be selected.
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        IStorageQueryBuilder Select(params string[] propertyNames);

        /// <summary>
        /// Add distinct modifier.
        /// </summary>
        /// <returns></returns>
        IStorageQueryBuilder Distinct();

        /// <summary>
        /// Add a between expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lowValue"></param>
        /// <param name="highValue"></param>
        /// <returns></returns>
        IStorageQueryBuilder Between(string propertyName, string lowValue, string highValue);

        /// <summary>
        /// A string comparison expression that contains the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IStorageQueryBuilder Contains(string propertyName, string value);

        /// <summary>
        /// A string comparison expression that starts with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IStorageQueryBuilder StartsWith(string propertyName, string value);

        /// <summary>
        /// A string comparison expression that ends with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IStorageQueryBuilder EndsWith(string propertyName, string value);

        /// <summary>
        /// A an equality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IStorageQueryBuilder IsEqual(string propertyName, string value);

        /// <summary>
        /// Add an inequality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IStorageQueryBuilder IsNotEqual(string propertyName, string value);

        /// <summary>
        /// Add a greater than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IStorageQueryBuilder IsGreaterThan(string propertyName, string value);

        /// <summary>
        /// Add a greater than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IStorageQueryBuilder IsGreaterThanOrEqual(string propertyName, string value);

        /// <summary>
        /// Add a less than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IStorageQueryBuilder IsLessThan(string propertyName, string value);

        /// <summary>
        /// Add a less than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IStorageQueryBuilder IsLessThanOrEqual(string propertyName, string value);

        /// <summary>
        /// Add a comparison expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="comparisonType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IStorageQueryBuilder Compare(string propertyName, StorageQueryComparisonType comparisonType, string value);

        /// <summary>
        /// Add a property comparison expression.
        /// </summary>
        /// <param name="propertyNameLeft"></param>
        /// <param name="comparisonType"></param>
        /// <param name="propertyNameRight"></param>
        /// <returns></returns>
        IStorageQueryBuilder PropertyCompare(string propertyNameLeft, StorageQueryComparisonType comparisonType, string propertyNameRight);

        /// <summary>
        /// Add a is null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IStorageQueryBuilder IsNull(string propertyName);

        /// <summary>
        /// Add a null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isNull"></param>
        /// <returns></returns>
        IStorageQueryBuilder Null(string propertyName, bool isNull);

        /// <summary>
        /// Add a is not null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IStorageQueryBuilder IsNotNull(string propertyName);

        /// <summary>
        /// Add a in set expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IStorageQueryBuilder IsInSet(string propertyName, params string[] values);

        /// <summary>
        /// Add a set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="inSet"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IStorageQueryBuilder Set(string propertyName, bool inSet, params string[] values);

        /// <summary>
        /// Add a not in set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        IStorageQueryBuilder IsNotInSet(string propertyName, params string[] values);

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IStorageQueryBuilder Sort(string propertyName);

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        IStorageQueryBuilder Sort(string propertyName, bool sortAscending);

        /// <summary>
        /// Add an averate aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IStorageQueryBuilder Average(string propertyName);

        /// <summary>
        /// Add a binary checksum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IStorageQueryBuilder BinaryChecksum(string propertyName);

        /// <summary>
        /// Add a checksum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IStorageQueryBuilder Checksum(string propertyName);

        /// <summary>
        /// Add a maximum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IStorageQueryBuilder Maximum(string propertyName);

        /// <summary>
        /// Add a minumum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IStorageQueryBuilder Minimum(string propertyName);

        /// <summary>
        /// Add a sum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        IStorageQueryBuilder Sum(string propertyName);

        /// <summary>
        /// Add an aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="aggregateType"></param>
        /// <returns></returns>
        IStorageQueryBuilder Aggregate(string propertyName, StorageQueryAggregateType aggregateType);

        /// <summary>
        /// Setup paging.
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="numberPerPage"></param>
        /// <returns></returns>
        IStorageQueryBuilder Paging(int startPage, int numberPerPage);
    }
}

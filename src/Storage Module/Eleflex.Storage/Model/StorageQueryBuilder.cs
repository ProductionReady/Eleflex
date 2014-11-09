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

namespace Eleflex.Storage
{
    /// <summary>
    /// Helper class used to assemble filters for storage querys.
    /// </summary>
    public partial class StorageQueryBuilder : IStorageQueryBuilder
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public StorageQueryBuilder()
        {
            Filters = new List<StorageQueryFilter>();
        }

        /// <summary>
        /// Paging start page.
        /// </summary>
        public virtual int StartPage { get; set; }

        /// <summary>
        /// Number of items per page.
        /// </summary>
        public virtual int NumberPerPage { get; set; }

        /// <summary>
        /// The list of filters.
        /// </summary>
        public virtual List<StorageQueryFilter> Filters { get; set; }

        /// <summary>
        /// Get the storage query.
        /// </summary>
        /// <returns></returns>
        public virtual StorageQuery GetStorageQuery()
        {
            return new StorageQuery() { Filters = this.Filters, NumberPerPage = this.NumberPerPage, StartPage = this.StartPage };
        }
        

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Expression(StorageQueryExpressionType expressionType)
        {
            StorageQueryFilter filter = new StorageQueryFilter();
            filter.FilterType = StorageQueryFilterType.Expression;
            filter.Expression = expressionType;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IStorageQueryBuilder BeginExpression()
        {
            return Expression(StorageQueryExpressionType.Begin);
        }

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IStorageQueryBuilder EndExpression()
        {
            return Expression(StorageQueryExpressionType.End);
        }

        /// <summary>
        /// Inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IStorageQueryBuilder And()
        {
            return Expression(StorageQueryExpressionType.And);
        }

        /// <summary>
        /// Seperate inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Or()
        {
            return Expression(StorageQueryExpressionType.Or);
        }


        /// <summary>
        /// Add properties to be selected.
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Select(params string[] propertyNames)
        {
            StorageQueryFilter filter = new StorageQueryFilter();
            filter.FilterType = StorageQueryFilterType.Select;
            filter.Columns.AddRange(propertyNames);
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add distinct modifier.
        /// </summary>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Distinct()
        {
            StorageQueryFilter filter = new StorageQueryFilter();
            filter.FilterType = StorageQueryFilterType.Distinct;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a between expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lowValue"></param>
        /// <param name="highValue"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Between(string propertyName, string lowValue, string highValue)
        {
            return IsGreaterThanOrEqual(propertyName, lowValue).And().IsLessThanOrEqual(propertyName, highValue);
        }

        /// <summary>
        /// A string comparison expression that contains the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Contains(string propertyName, string value)
        {
            return Compare(propertyName, StorageQueryComparisonType.Contains, value);
        }

        /// <summary>
        /// A string comparison expression that starts with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder StartsWith(string propertyName, string value)
        {
            return Compare(propertyName, StorageQueryComparisonType.StartsWith, value);
        }

        /// <summary>
        /// A string comparison expression that ends with the value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder EndsWith(string propertyName, string value)
        {
            return Compare(propertyName, StorageQueryComparisonType.EndsWith, value);
        }

        /// <summary>
        /// An equality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder IsEqual(string propertyName, string value)
        {
            return Compare(propertyName, StorageQueryComparisonType.Equal, value);
        }

        /// <summary>
        /// Add an inequality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder IsNotEqual(string propertyName, string value)
        {
            return Compare(propertyName, StorageQueryComparisonType.NotEqual, value);
        }


        /// <summary>
        /// Add a greater than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder IsGreaterThan(string propertyName, string value)
        {
            return Compare(propertyName, StorageQueryComparisonType.GreaterThan, value);
        }

        /// <summary>
        /// Add a greater than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder IsGreaterThanOrEqual(string propertyName, string value)
        {
            return Compare(propertyName, StorageQueryComparisonType.GreaterThanEqual, value);
        }

        /// <summary>
        /// Add a less than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder IsLessThan(string propertyName, string value)
        {
            return Compare(propertyName, StorageQueryComparisonType.LessThan, value);
        }


        /// <summary>
        /// Add a less than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder IsLessThanOrEqual(string propertyName, string value)
        {
            return Compare(propertyName, StorageQueryComparisonType.LessThanEqual, value);
        }


        /// <summary>
        /// Add a comparison expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="comparisonType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Compare(string propertyName, StorageQueryComparisonType comparisonType, string value)
        {
            StorageQueryFilter filter = new StorageQueryFilter();
            filter.FilterType = StorageQueryFilterType.Compare;
            filter.Columns.Add(propertyName);
            filter.Comparison = comparisonType;
            filter.Values.Add(value);
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a property comparison expression.
        /// </summary>
        /// <param name="propertyNameLeft"></param>
        /// <param name="comparisonType"></param>
        /// <param name="propertyNameRight"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder PropertyCompare(string propertyNameLeft, StorageQueryComparisonType comparisonType, string propertyNameRight)
        {
            StorageQueryFilter filter = new StorageQueryFilter();
            filter.FilterType = StorageQueryFilterType.PropertyCompare;
            filter.Columns.Add(propertyNameLeft);
            filter.Columns.Add(propertyNameRight);
            filter.Comparison = comparisonType;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a is null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder IsNull(string propertyName)
        {
            return Null(propertyName, true);
        }

        /// <summary>
        /// Add a null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isNull"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Null(string propertyName, bool isNull)
        {
            StorageQueryFilter filter = new StorageQueryFilter();
            filter.FilterType = StorageQueryFilterType.Null;
            filter.Columns.Add(propertyName);
            if (isNull)
                filter.InclusionType = StorageQueryInclusionType.Include;
            else
                filter.InclusionType = StorageQueryInclusionType.NotInclude;
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a is not null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder IsNotNull(string propertyName)
        {
            return Null(propertyName, false);
        }

        /// <summary>
        /// Add a in set expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder IsInSet(string propertyName, params string[] values)
        {
            return Set(propertyName, true, values);
        }


        /// <summary>
        /// Add a set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="inSet"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Set(string propertyName, bool inSet, params string[] values)
        {
            StorageQueryFilter filter = new StorageQueryFilter();
            filter.FilterType = StorageQueryFilterType.Set;
            filter.Columns.Add(propertyName);
            if (inSet)
                filter.InclusionType = StorageQueryInclusionType.Include;
            else
                filter.InclusionType = StorageQueryInclusionType.NotInclude;
            foreach (string value in values)
                filter.Values.Add(value);
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add a not in set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder IsNotInSet(string propertyName, params string[] values)
        {
            return Set(propertyName, false, values);
        }

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Sort(string propertyName)
        {
            return Sort(propertyName, true);
        }

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Sort(string propertyName, bool sortAscending)
        {
            StorageQueryFilter filter = new StorageQueryFilter();
            filter.FilterType = StorageQueryFilterType.Sort;
            filter.Columns.Add(propertyName);
            if (sortAscending)
                filter.InclusionType = StorageQueryInclusionType.Include;
            else
                filter.InclusionType = StorageQueryInclusionType.NotInclude;            
            Filters.Add(filter);
            return this;
        }

        /// <summary>
        /// Add an averate aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Average(string propertyName)
        {
            return Aggregate(propertyName, StorageQueryAggregateType.Average);
        }

        /// <summary>
        /// Add a binary checksum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder BinaryChecksum(string propertyName)
        {
            return Aggregate(propertyName, StorageQueryAggregateType.BinaryChecksum);
        }

        /// <summary>
        /// Add a checksum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Checksum(string propertyName)
        {
            return Aggregate(propertyName, StorageQueryAggregateType.Checksum);
        }

        /// <summary>
        /// Add a maximum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Maximum(string propertyName)
        {
            return Aggregate(propertyName, StorageQueryAggregateType.Maximum);
        }

        /// <summary>
        /// Add a minumum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Minimum(string propertyName)
        {
            return Aggregate(propertyName, StorageQueryAggregateType.Minimum);
        }

        /// <summary>
        /// Add a sum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Sum(string propertyName)
        {
            return Aggregate(propertyName, StorageQueryAggregateType.Sum);
        }

        /// <summary>
        /// Add an aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="aggregateType"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Aggregate(string propertyName, StorageQueryAggregateType aggregateType)
        {
            StorageQueryFilter filter = new StorageQueryFilter();
            filter.FilterType = StorageQueryFilterType.Aggregate;
            filter.Columns.Add(propertyName);
            filter.Aggregate = aggregateType;
            Filters.Add(filter);
            return this;
        }


        /// <summary>
        /// Setup paging.
        /// </summary>
        /// <param name="startPage"></param>
        /// <param name="numberPerPage"></param>
        /// <returns></returns>
        public virtual IStorageQueryBuilder Paging(int startPage, int numberPerPage)
        {
            StartPage = startPage;
            NumberPerPage = numberPerPage;
            return this;
        }

    }
}

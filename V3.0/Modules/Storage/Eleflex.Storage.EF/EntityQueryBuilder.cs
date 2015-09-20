using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using LinqKit;

namespace Eleflex.Storage.EF
{
    /// <summary>
    /// Static class used to construct a dynamic query given an IQueryable object, IStorageQuery parameters and property name mappings.
    /// </summary>
    public static partial class EntityQueryBuilder
    {

        /// <summary>
        /// Dynamically build a linq query base on the given type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="storageQuery"></param>
        /// <param name="propertyNameMappings"></param>
        /// <returns></returns>
        public static IOrderedQueryable<T> Query<T>(IQueryable<T> query, IStorageQuery storageQuery, Dictionary<string, string> propertyNameMappings = null)
        {
            //Get the filters back in grouped sets           
            FilterSet filterSet = GetFilterSet(storageQuery);

            //Get type and property info of the base type
            Type entityType = typeof(T);
            var properties = entityType.GetProperties();

            //Get column name mappings (in case storage object name is different than mapped object name)
            Dictionary<string, string> mappings = propertyNameMappings;
            if (mappings == null || mappings.Count == 0)
            {
                //Use same property name (will be case-insensitive)
                mappings = new Dictionary<string, string>();
                properties.ToList().ForEach(x => mappings.Add(x.Name, x.Name));
            }

            //Set query as expandable for use with Linqkit and predicate building
            query = query.AsExpandable();

            //Build "where" clause
            query = BuildWhere<T>(query, filterSet, entityType, properties, mappings);

            //Build "select" clause
            query = BuildSelect<T>(query, filterSet, entityType, properties, mappings);

            //Add any "order by" clause
            return BuildOrderBy<T>(query, filterSet, entityType, properties, mappings);
        }

        /// <summary>
        /// Build the where clause.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="filterSet"></param>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        private static IQueryable<T> BuildWhere<T>(IQueryable<T> query, FilterSet filterSet, Type entityType, PropertyInfo[] properties, Dictionary<string, string> mappings)
        {
            Expression<Func<T, bool>> lastSet = null;
            bool nextSetExpressionAnd = true;
            Expression<Func<T, bool>> lastExpression = null;
            for (int allSets = 0; allSets < filterSet.WhereFilters.Count; allSets++)
            {
                //Iterate through where filters
                List<IStorageQueryFilter> filters = filterSet.WhereFilters[allSets];
                bool nextExpressionAnd = true;
                for (int curItem = 0; curItem < filters.Count; curItem++)
                {
                    IStorageQueryFilter filter = filters[curItem];

                    //Get expression join type
                    if (filter.FilterType == StorageQueryFilterType.Expression)
                    {
                        if (filter.Expression == StorageQueryExpressionType.And)
                        {
                            nextExpressionAnd = true;
                            if (filters.Count == 1)
                                nextSetExpressionAnd = true;
                        }
                        else
                        {
                            nextExpressionAnd = false;
                            if (filters.Count == 1)
                                nextSetExpressionAnd = false;
                        }
                        continue;
                    }
                    //Build new expression
                    Expression<Func<T, bool>> expr = BuildExpresion<T>(filter, entityType, properties, mappings);
                    if (expr == null)
                        continue;
                    if (lastExpression == null)
                    {
                        lastExpression = expr;
                        continue;
                    }

                    //Add to last expression based on expression join type
                    if (nextExpressionAnd)
                        lastExpression = lastExpression.And(expr.Expand());
                    else
                        lastExpression = lastExpression.Or(expr.Expand());
                }
                if (lastExpression == null)
                    continue;
                //Set last set to last expression
                if (lastSet == null)
                {
                    lastSet = lastExpression;
                    lastExpression = null;
                    continue;
                }
                //Build on last set with last expression
                if (nextSetExpressionAnd)
                    lastSet = lastSet.And(lastExpression.Expand());
                else
                    lastSet = lastSet.Or(lastExpression.Expand());
                lastExpression = null;
            }
            if (lastSet != null)
                query = query.Where(lastSet);
            return query;
        }

        /// <summary>
        /// Build the select clause.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="filterSet"></param>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        private static IQueryable<T> BuildSelect<T>(IQueryable<T> query, FilterSet filterSet, Type entityType, PropertyInfo[] properties, Dictionary<string, string> mappings)
        {
            bool addDistinct = false;
            List<string> selectList = new List<string>();
            for (int filterCount = 0; filterCount < filterSet.SelectFilters.Count; filterCount++)
            {
                //Iterate through where filters
                IStorageQueryFilter filter = filterSet.SelectFilters[filterCount];
                if (filter.FilterType == StorageQueryFilterType.Distinct)
                {
                    addDistinct = true;
                    continue;
                }
                if (filter.FilterType == StorageQueryFilterType.Select)
                {
                    if (filter.Properties == null || filter.Properties.Count == 0)
                        throw new EleflexException("Select filter requires at least 1 field");
                    selectList.AddRange(filter.Properties);
                }
            }

            //Add Distinct (if needed)
            if (addDistinct)
                query = query.Distinct();

            //Add only selected columns (if specified)
            if (selectList.Count > 0)
            {
                NewExpression newExp = Expression.New(typeof(T));
                var typeParam = Expression.Parameter(entityType, "x");
                List<MemberBinding> bindings = new List<MemberBinding>();
                foreach (string item in selectList)
                {
                    PropertyInfo p = GetProperty(properties, mappings, item);
                    MemberExpression memberExpression = Expression.Property(typeParam, p);
                    bindings.Add(Expression.Bind(p, memberExpression));
                }

                var init = Expression.MemberInit(newExp, bindings);
                var lambda = Expression.Lambda<Func<T, T>>(init, typeParam);
                query = query.Select(lambda);
            }
            return query;
        }
        /// <summary>
        /// Build an expression based on the filter
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="t"></param>
        /// <param name="props"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> BuildExpresion<T>(IStorageQueryFilter filter, Type t, PropertyInfo[] props, Dictionary<string, string> mappings)
        {
            switch (filter.FilterType)
            {
                case StorageQueryFilterType.Aggregate:
                    break; //TODO
                case StorageQueryFilterType.Between:
                    return GetBetweenExpression<T>(filter, t, props, mappings);
                case StorageQueryFilterType.Compare:
                    return GetCompareExpression<T>(filter, t, props, mappings);
                case StorageQueryFilterType.Distinct:
                    break; //No expression (handled in buildselect)
                case StorageQueryFilterType.Expression:
                    break; //No expression (handled in buildwhere)
                case StorageQueryFilterType.Null:
                    return GetNullExpression<T>(filter, t, props, mappings);
                case StorageQueryFilterType.PropertyCompare:
                    break; //TODO
                case StorageQueryFilterType.Select:
                    break; //No expression (handled in buildselect)
                case StorageQueryFilterType.Set:
                    return GetSetExpression<T>(filter, t, props, mappings);
                case StorageQueryFilterType.Sort:
                    break; //No expression (handled by buildorderby)
            }
            return null;
        }

        /// <summary>
        /// Get a property.
        /// </summary>
        /// <param name="props"></param>
        /// <param name="mappings"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static PropertyInfo GetProperty(PropertyInfo[] props, Dictionary<string, string> mappings, string name)
        {
            if (!mappings.ContainsKey(name))
                throw new EleflexException("Property mapping not found for " + name);
            var p = props.Where(x => string.Compare(x.Name, mappings[name], true) == 0).FirstOrDefault();
            if (p == null)
                throw new EleflexException("Property not found for " + name);
            return p;
        }

        /// <summary>
        /// Get set expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="t"></param>
        /// <param name="props"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> GetSetExpression<T>(IStorageQueryFilter filter, Type t, PropertyInfo[] props, Dictionary<string, string> mappings)
        {
            if (filter.Properties == null || filter.Properties.Count != 1)
                throw new EleflexException("Set requires 1 field");
            if (filter.Values == null || filter.Values.Count == 0)
                throw new EleflexException("Set requires at least 1 value");

            //Use the comparison expression to build the null expression.
            StorageQueryBuilder builder = new StorageQueryBuilder();
            foreach (string val in filter.Values)
            {
                if (filter.InclusionType == StorageQueryInclusionType.Include)
                    builder.IsEqual(filter.Properties[0], val);
                else
                    builder.IsNotEqual(filter.Properties[0], val);
            }

            Expression<Func<T, bool>> exp = null;
            foreach (StorageQueryFilter valFilter in builder.Filters)
            {
                Expression<Func<T, bool>> tempExp = GetCompareExpression<T>(valFilter, t, props, mappings);
                if (exp == null)
                    exp = tempExp;
                else
                    exp = exp.Or(tempExp);
            }
            return exp;
        }


        /// <summary>
        /// Get null expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="t"></param>
        /// <param name="props"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> GetNullExpression<T>(IStorageQueryFilter filter, Type t, PropertyInfo[] props, Dictionary<string, string> mappings)
        {
            if (filter.Properties == null || filter.Properties.Count != 1)
                throw new EleflexException("Null requires 1 field");

            PropertyInfo p = null;
            MemberExpression member = null;
            Expression[] constant = null;
            p = GetProperty(props, mappings, filter.Properties[0]);
            var typeParam = Expression.Parameter(t, "x");

            member = Expression.MakeMemberAccess(typeParam, p);
            var memberHasValue = Expression.PropertyOrField(member, "HasValue");
            //var memberHasValue = MemberExpression.Property(member, "HasValue");

            if (filter.InclusionType == StorageQueryInclusionType.Include)
                constant = new Expression[] { Expression.Constant(false) };
            else
                constant = new Expression[] { Expression.Constant(true) };

            var equalsExp = Expression.Equal(memberHasValue, constant[0]);
            return Expression.Lambda(equalsExp, typeParam) as Expression<Func<T, bool>>;

        }

        /// <summary>
        /// Get between expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="t"></param>
        /// <param name="props"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> GetBetweenExpression<T>(IStorageQueryFilter filter, Type t, PropertyInfo[] props, Dictionary<string, string> mappings)
        {
            if (filter.Properties == null || filter.Properties.Count != 1)
                throw new EleflexException("Between comparison requires 1 field");
            if (filter.Values == null || filter.Values.Count != 2)
                throw new EleflexException("Between comparison requires 2 values");

            //Use the comparison expression to build the between expression.
            StorageQueryBuilder builder = new StorageQueryBuilder();
            if (filter.InclusionType == StorageQueryInclusionType.Include)
            {
                builder.IsGreaterThanOrEqual(filter.Properties[0], filter.Values[0]);
                builder.IsLessThanOrEqual(filter.Properties[0], filter.Values[1]);
            }
            else
            {
                builder.IsLessThan(filter.Properties[0], filter.Values[0]);
                builder.IsGreaterThan(filter.Properties[0], filter.Values[1]);
            }

            Expression<Func<T, bool>> first = GetCompareExpression<T>(builder.Filters[0], t, props, mappings);
            Expression<Func<T, bool>> second = GetCompareExpression<T>(builder.Filters[1], t, props, mappings);
            Expression<Func<T, bool>> exp = first.And(second);
            return exp;
        }

        /// <summary>
        /// Static method for strings
        /// </summary>
        private static readonly MethodInfo StringBeginsWithMethod = typeof(string).GetMethod(@"BeginsWith", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(string) }, null);
        private static readonly MethodInfo StringContainsMethod = typeof(string).GetMethod(@"Contains", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(string) }, null);
        private static readonly MethodInfo StringEndsWithMethod = typeof(string).GetMethod(@"EndsWith", BindingFlags.Instance | BindingFlags.Public, null, new[] { typeof(string) }, null);

        /// <summary>
        /// Get a comparison expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filter"></param>
        /// <param name="t"></param>
        /// <param name="props"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> GetCompareExpression<T>(IStorageQueryFilter filter, Type t, PropertyInfo[] props, Dictionary<string, string> mappings)
        {
            var typeParam = Expression.Parameter(t, "x");
            PropertyInfo p = null;
            MemberExpression member = null;
            Expression[] constant = null;
            switch (filter.Comparison)
            {
                case StorageQueryComparisonType.StartsWith:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new EleflexException("BeginsWith comparison requires 1 field");
                    p = GetProperty(props, mappings, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new EleflexException("BeginsWith comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(filter.Values[0]) };
                    var beginsWithCall = Expression.Call(member, StringBeginsWithMethod, constant);
                    return Expression.Lambda(beginsWithCall, typeParam) as Expression<Func<T, bool>>;
                case StorageQueryComparisonType.Contains:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new EleflexException("Contains comparison requires 1 field");
                    p = GetProperty(props, mappings, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new EleflexException("Contains comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(filter.Values[0]) };
                    var containsCall = Expression.Call(member, StringContainsMethod, constant);
                    return Expression.Lambda(containsCall, typeParam) as Expression<Func<T, bool>>;
                case StorageQueryComparisonType.EndsWith:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new EleflexException("EndsWith comparison requires 1 field");
                    p = GetProperty(props, mappings, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new EleflexException("EndsWith comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(filter.Values[0]) };
                    var endsWithCall = Expression.Call(member, StringEndsWithMethod, constant);
                    return Expression.Lambda(endsWithCall, typeParam) as Expression<Func<T, bool>>;
                case StorageQueryComparisonType.Equal:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new EleflexException("Equal comparison requires 1 field");
                    p = GetProperty(props, mappings, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new EleflexException("Equal comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var equalsExp = Expression.Equal(member, constant[0]);
                    return Expression.Lambda(equalsExp, typeParam) as Expression<Func<T, bool>>;
                case StorageQueryComparisonType.GreaterThan:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new EleflexException("GreaterThan comparison requires 1 field");
                    p = GetProperty(props, mappings, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new EleflexException("GreaterThan comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var greaterThanExp = Expression.GreaterThan(member, constant[0]);
                    return Expression.Lambda(greaterThanExp, typeParam) as Expression<Func<T, bool>>;
                case StorageQueryComparisonType.GreaterThanEqual:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new EleflexException("GreaterThanEqual comparison requires 1 field");
                    p = GetProperty(props, mappings, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new EleflexException("GreaterThanEqual comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var greaterThanEqualExp = Expression.GreaterThanOrEqual(member, constant[0]);
                    return Expression.Lambda(greaterThanEqualExp, typeParam) as Expression<Func<T, bool>>;
                case StorageQueryComparisonType.LessThan:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new EleflexException("LessThan comparison requires 1 field");
                    p = GetProperty(props, mappings, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new EleflexException("LessThan comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var lessThanExp = Expression.LessThan(member, constant[0]);
                    return Expression.Lambda(lessThanExp, typeParam) as Expression<Func<T, bool>>;
                case StorageQueryComparisonType.LessThanEqual:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new EleflexException("LessThanEqual comparison requires 1 field");
                    p = GetProperty(props, mappings, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new EleflexException("LessThanEqual comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var lessThanEqualExp = Expression.LessThanOrEqual(member, constant[0]);
                    return Expression.Lambda(lessThanEqualExp, typeParam) as Expression<Func<T, bool>>;
                case StorageQueryComparisonType.NotEqual:
                    if (filter.Properties == null || filter.Properties.Count != 1)
                        throw new EleflexException("NotEqual comparison requires 1 field");
                    p = GetProperty(props, mappings, filter.Properties[0]);
                    if (filter.Values == null || filter.Values.Count != 1)
                        throw new EleflexException("NotEqual comparison requires 1 value");
                    member = Expression.MakeMemberAccess(typeParam, p);
                    constant = new Expression[] { Expression.Constant(GetParsedPropertyType(p, filter.Values[0]), p.PropertyType) };
                    var noteEqualsExp = Expression.NotEqual(member, constant[0]);
                    return Expression.Lambda(noteEqualsExp, typeParam) as Expression<Func<T, bool>>;
            }
            return null;
        }

        /// <summary>
        /// Build the order by clause.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="filterSet"></param>
        /// <param name="entityType"></param>
        /// <param name="properties"></param>
        /// <param name="mappings"></param>
        /// <returns></returns>
        private static IOrderedQueryable<T> BuildOrderBy<T>(this IQueryable<T> query, FilterSet filterSet, Type entityType, PropertyInfo[] properties, Dictionary<string, string> mappings)
        {
            var param = Expression.Parameter(entityType, "x");
            IOrderedQueryable<T> oquery = query as IOrderedQueryable<T>;
            if (filterSet.SortFilters.Count == 0)
                return oquery;

            for (int i = 0; i < filterSet.SortFilters.Count; i++)
            {
                foreach (string column in filterSet.SortFilters[i].Properties)
                {
                    var p = GetProperty(properties, mappings, column);
                    if (p.PropertyType == typeof(bool))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, bool>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, bool>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(byte))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, byte>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, byte>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(char))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, char>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, char>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(DateTime))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, DateTime>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, DateTime>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(DateTimeOffset))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, DateTimeOffset>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, DateTimeOffset>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(decimal))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, decimal>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, decimal>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(double))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, double>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, double>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(Guid))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, Guid>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, Guid>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(short))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, short>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, short>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(int))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, int>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, int>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(long))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, long>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, long>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(sbyte))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, sbyte>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, sbyte>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(Single))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, Single>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, Single>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(string))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, string>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, string>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(TimeSpan))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, TimeSpan>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, TimeSpan>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(UInt16))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, UInt16>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, UInt16>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(UInt32))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, UInt32>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, UInt32>>(Expression.Property(param, p), param));
                    }
                    if (p.PropertyType == typeof(UInt64))
                    {
                        if (filterSet.SortFilters[i].SortAsc)
                            oquery = oquery.OrderBy(Expression.Lambda<Func<T, UInt64>>(Expression.Property(param, p), param));
                        else
                            oquery = oquery.OrderByDescending(Expression.Lambda<Func<T, UInt64>>(Expression.Property(param, p), param));
                    }
                }
            }
            return oquery;
        }

        /// <summary>
        /// Processed filters.
        /// </summary>
        internal class FilterSet
        {
            public FilterSet()
            {
                SelectFilters = new List<IStorageQueryFilter>();
                WhereFilters = new List<List<IStorageQueryFilter>>();
                SortFilters = new List<IStorageQueryFilter>();
            }
            public StorageQueryFilterType FilterSetType { get; set; }
            public List<IStorageQueryFilter> SelectFilters { get; set; }
            public List<List<IStorageQueryFilter>> WhereFilters { get; set; }
            public List<IStorageQueryFilter> SortFilters { get; set; }
        }


        /// <summary>
        /// Pre-processing to group filters into sets.
        /// </summary>
        /// <param name="storageQuery"></param>
        /// <returns></returns>
        private static FilterSet GetFilterSet(IStorageQuery storageQuery)
        {
            FilterSet filterSet = new FilterSet();
            List<IStorageQueryFilter> currentWhereFilters = new List<IStorageQueryFilter>();
            bool nestedExpression = false;
            for (int i = 0; i < storageQuery.StorageQueryFilters.Count; i++)
            {
                IStorageQueryFilter filter = storageQuery.StorageQueryFilters[i];
                switch (filter.FilterType)
                {
                    case StorageQueryFilterType.Between:
                        currentWhereFilters.Add(filter);
                        break;
                    case StorageQueryFilterType.Compare:
                        currentWhereFilters.Add(filter);
                        break;
                    case StorageQueryFilterType.Distinct:
                        filterSet.SelectFilters.Add(filter);
                        break;
                    case StorageQueryFilterType.Expression:
                        if (filter.Expression == StorageQueryExpressionType.Begin)
                        {
                            nestedExpression = true;
                            if (currentWhereFilters.Count > 0)
                            {
                                filterSet.WhereFilters.Add(currentWhereFilters);
                                currentWhereFilters = new List<IStorageQueryFilter>();
                            }
                            continue;
                        }
                        if (filter.Expression == StorageQueryExpressionType.End)
                        {
                            nestedExpression = false;
                            if (currentWhereFilters.Count > 0)
                            {
                                filterSet.WhereFilters.Add(currentWhereFilters);
                                currentWhereFilters = new List<IStorageQueryFilter>();
                            }
                            continue;
                        }
                        if (nestedExpression)
                            currentWhereFilters.Add(filter);
                        else
                        {
                            if (currentWhereFilters.Count > 0)
                            {
                                filterSet.WhereFilters.Add(currentWhereFilters);
                                currentWhereFilters = new List<IStorageQueryFilter>();

                            }
                            currentWhereFilters.Add(filter);
                            filterSet.WhereFilters.Add(currentWhereFilters);
                            currentWhereFilters = new List<IStorageQueryFilter>();
                        }
                        break;
                    case StorageQueryFilterType.Null:
                        currentWhereFilters.Add(filter);
                        break;
                    case StorageQueryFilterType.PropertyCompare:
                        currentWhereFilters.Add(filter);
                        break;
                    case StorageQueryFilterType.Select:
                        filterSet.SelectFilters.Add(filter);
                        break;
                    case StorageQueryFilterType.Set:
                        currentWhereFilters.Add(filter);
                        break;
                    case StorageQueryFilterType.Sort:
                        filterSet.SortFilters.Add(filter);
                        break;
                }
            }
            if (currentWhereFilters.Count > 0)
                filterSet.WhereFilters.Add(currentWhereFilters);

            return filterSet;
        }

        /// <summary>
        /// Convert a string value into the datatype of the property.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object GetParsedPropertyType(PropertyInfo prop, string value)
        {
            if (prop.PropertyType == typeof(bool))
                return bool.Parse(value);
            if (prop.PropertyType == typeof(bool?))
            {
                if (string.IsNullOrEmpty(value))
                    return new bool?();
                return new bool?(bool.Parse(value));
            }
            if (prop.PropertyType == typeof(byte))
                return byte.Parse(value);
            if (prop.PropertyType == typeof(byte?))
            {
                if (string.IsNullOrEmpty(value))
                    return new byte?();
                return new byte?(byte.Parse(value));
            }
            if (prop.PropertyType == typeof(char))
                return char.Parse(value);
            if (prop.PropertyType == typeof(char?))
            {
                if (string.IsNullOrEmpty(value))
                    return new char?();
                return new char?(char.Parse(value));
            }
            if (prop.PropertyType == typeof(DateTime))
                return DateTime.Parse(value);
            if (prop.PropertyType == typeof(DateTime?))
            {
                if (string.IsNullOrEmpty(value))
                    return new DateTime?();
                return new DateTime?(DateTime.Parse(value));
            }
            if (prop.PropertyType == typeof(DateTimeOffset))
                return DateTimeOffset.Parse(value);
            if (prop.PropertyType == typeof(DateTimeOffset?))
            {
                if (string.IsNullOrEmpty(value))
                    return new DateTimeOffset?();
                return new DateTimeOffset?(DateTimeOffset.Parse(value));
            }
            if (prop.PropertyType == typeof(decimal))
                return decimal.Parse(value);
            if (prop.PropertyType == typeof(decimal?))
            {
                if (string.IsNullOrEmpty(value))
                    return new decimal?();
                return new decimal?(decimal.Parse(value));
            }
            if (prop.PropertyType == typeof(double))
                return double.Parse(value);
            if (prop.PropertyType == typeof(double?))
            {
                if (string.IsNullOrEmpty(value))
                    return new double?();
                return new double?(double.Parse(value));
            }
            if (prop.PropertyType == typeof(Guid))
                return Guid.Parse(value);
            if (prop.PropertyType == typeof(Guid?))
            {
                if (string.IsNullOrEmpty(value))
                    return new Guid?();
                return new Guid?(Guid.Parse(value));
            }
            if (prop.PropertyType == typeof(short))
                return short.Parse(value);
            if (prop.PropertyType == typeof(short?))
            {
                if (string.IsNullOrEmpty(value))
                    return new short?();
                return new short?(short.Parse(value));
            }
            if (prop.PropertyType == typeof(int))
                return int.Parse(value);
            if (prop.PropertyType == typeof(int?))
            {
                if (string.IsNullOrEmpty(value))
                    return new int?();
                return new int?(int.Parse(value));
            }
            if (prop.PropertyType == typeof(long))
                return long.Parse(value);
            if (prop.PropertyType == typeof(long?))
            {
                if (string.IsNullOrEmpty(value))
                    return new long?();
                return new long?(long.Parse(value));
            }
            if (prop.PropertyType == typeof(sbyte))
                return sbyte.Parse(value);
            if (prop.PropertyType == typeof(sbyte?))
            {
                if (string.IsNullOrEmpty(value))
                    return new sbyte?();
                return new sbyte?(sbyte.Parse(value));
            }
            if (prop.PropertyType == typeof(Single))
                return Single.Parse(value);
            if (prop.PropertyType == typeof(Single?))
            {
                if (string.IsNullOrEmpty(value))
                    return new Single?();
                return new Single?(Single.Parse(value));
            }
            if (prop.PropertyType == typeof(string))
                return value;
            if (prop.PropertyType == typeof(TimeSpan))
                return TimeSpan.Parse(value);
            if (prop.PropertyType == typeof(TimeSpan?))
            {
                if (string.IsNullOrEmpty(value))
                    return new TimeSpan?();
                return new TimeSpan?(TimeSpan.Parse(value));
            }
            if (prop.PropertyType == typeof(UInt16))
                return UInt16.Parse(value);
            if (prop.PropertyType == typeof(UInt16?))
            {
                if (string.IsNullOrEmpty(value))
                    return new UInt16?();
                return new UInt16?(UInt16.Parse(value));
            }
            if (prop.PropertyType == typeof(UInt32))
                return UInt32.Parse(value);
            if (prop.PropertyType == typeof(UInt32?))
            {
                if (string.IsNullOrEmpty(value))
                    return new UInt32?();
                return new UInt32?(UInt32.Parse(value));
            }
            if (prop.PropertyType == typeof(UInt64))
                return UInt64.Parse(value);
            if (prop.PropertyType == typeof(UInt64?))
            {
                if (string.IsNullOrEmpty(value))
                    return new UInt64?();
                return new UInt64?(UInt64.Parse(value));
            }
            return value;
        }
    }
}

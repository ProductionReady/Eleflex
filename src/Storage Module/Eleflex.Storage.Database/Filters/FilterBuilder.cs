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
using Eleflex.Storage.Database;

namespace Eleflex.Storage.Database.Filters
{

    /// <summary>
    /// Helper class used to assemble filters for the storage request.
    /// </summary>
    public partial class FilterBuilder : IPersistenceFilterBuilder
    {

        /// <summary>
        /// Constant used for logging purposes.
        /// </summary>
        public const string CLASSNAME = "PR.Eleflex.Persistence.FilterBuilder" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// The list of storage filters.
        /// </summary>
        protected List<IPersistenceFilter> _storageFilters = new List<IPersistenceFilter>();
        /// <summary>
        /// The context used with the request.
        /// </summary>
        protected IEleflexContext _context;
        /// <summary>
        /// The associated Data Transfer Object.
        /// </summary>
        protected IEleflexPersistenceObject _epo;
        /// <summary>
        /// The list of associated relationships.
        /// </summary>
        protected List<IPersistenceRelation> _relations = new List<IPersistenceRelation>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dto"></param>
        public FilterBuilder(IEleflexContext context, IEleflexPersistenceObject dto)
        {
            if (context == null)
                _context = new EleflexContext();
            else
                _context = context;
            _epo = dto;

            //Add default relations
            List<string> defaultRelations = _epo.EPOGetDefaultRelations();
            if (defaultRelations == null || defaultRelations.Count==0)
                return;
            this.Relation(defaultRelations.ToArray());
        }

        /// <summary>
        /// Get the context associated with this builder.
        /// </summary>
        public virtual IEleflexContext Context
        {
            get { return _context; }
            set { _context = value; }
        }

        /// <summary>
        /// The list of filters.
        /// </summary>
        public virtual List<IPersistenceFilter> Filters
        {
            get { return _storageFilters; }
            set { _storageFilters = value; }
        }

        /// <summary>
        /// Get the current set of filters built.
        /// </summary>
        /// <returns></returns>
        public virtual List<IPersistenceFilter> GetFilters()
        {
            return Filters;
        }

        /// <summary>
        /// Clear existing filters.
        /// </summary>
        public virtual void ClearFilters()
        {
            _storageFilters = new List<IPersistenceFilter>();
        }

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Expression(ExpressionEnum expressionType)
        {
            _storageFilters.Add(
                new FilterExpression(expressionType));
            return this;
        }

        /// <summary>
        /// Begin a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder BeginExpression()
        {
            _storageFilters.Add(
                new FilterExpression(ExpressionEnum.BEGIN_EXPRESSION));
            return this;
        }

        /// <summary>
        /// End a grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder EndExpression()
        {
            _storageFilters.Add(
                new FilterExpression(ExpressionEnum.END_EXPRESSION));
            return this;
        }

        /// <summary>
        /// Inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder And()
        {
            _storageFilters.Add(
                new FilterExpression(ExpressionEnum.AND));
            return this;
        }

        /// <summary>
        /// Seperate inclusive grouping expression.
        /// </summary>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Or()
        {
            _storageFilters.Add(
                new FilterExpression(ExpressionEnum.OR));
            return this;
        }
        
        /// <summary>
        /// Add a relationship (parent to child).
        /// </summary>
        /// <param name="relationNames"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Relation(params string[] relationNames)
        {
            IEleflexPersistenceObject lastObject = _epo;
            if (_relations.Count > 0)
                lastObject = _relations[_relations.Count - 1].EPRGetForeignProperties()[0].ParentObject as IEleflexPersistenceObject;

            foreach (string relationName in relationNames)
            {
                IPersistenceRelation relation = FindRelationship(relationName, lastObject);
                if (relation == null)
                {
                    const string methodName = CLASSNAME + "Relation";
                    _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_RelationNotFound1, relationName));
                    return this;
                }
                lastObject = relation.EPRGetForeignProperties()[0].ParentObject as IEleflexPersistenceObject;
                _relations.Add(relation);
                _storageFilters.Add(new FilterRelation(relation));
            }
            return this;
        }

        /// <summary>
        /// Add an inverted relationship (child to parent).
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="relationName"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder RelationInverse(IEleflexPersistenceObject dto, string relationName)
        {            
            List<IPersistenceRelation> relations = dto.EPOGetRelations();

            const string methodName = CLASSNAME + "RelationInverse";

            if (relations == null)
            {                
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_RelationNotFound1, relationName));
                return this;
            }
            IPersistenceRelation curRelation = null;
            foreach (IPersistenceRelation tempRelation in relations)
            {
                if (tempRelation.EPRGetName() == relationName)
                {
                    curRelation = tempRelation;
                    break;
                }
            }
            if (curRelation == null)
            {
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_RelationNotFound1, relationName));
                return this;
            }

            //Swap the relationship
            IPersistenceRelation newRelation = new PersistenceRelation(
                relationName,
                curRelation.EPRGetForeignProperties(),
                curRelation.EPRGetParentProperties());

            _relations.Add(newRelation);
            _storageFilters.Add(new FilterRelation(newRelation));

            return this;
        }

        /// <summary>
        /// Add properties to be selected.
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Select(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                IEleflexProperty property = FindProperty(propertyName);
                if (property == null)
                {
                    const string methodName = CLASSNAME + "Select";
                    _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyName));
                    return this;
                }
                _storageFilters.Add(new FilterSelect(property));
            }
            return this;
        }

        /// <summary>
        /// Add distinct expression.
        /// </summary>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Distinct()
        {
            _storageFilters.Add(new FilterDistinct());
            return this;
        }

        /// <summary>
        /// Add properties to be updated.
        /// </summary>
        /// <param name="propertyNames"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Update(params string[] propertyNames)
        {
            foreach (string propertyName in propertyNames)
            {
                IEleflexProperty property = FindProperty(propertyName);
                if (property == null)
                {
                    const string methodName = CLASSNAME + "Update";
                    _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyName));
                    return this;
                }
                _storageFilters.Add(new FilterUpdate(property));
            }
            return this;
        }

        /// <summary>
        /// Add a between expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lowValue"></param>
        /// <param name="highValue"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsBetween(string propertyName, IEleflexDataType lowValue, IEleflexDataType highValue)
        {
            IEleflexProperty property = FindProperty(propertyName);
            if (property == null)
            {
                const string methodName = CLASSNAME + "IsBetween";
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyName));
                return this;
            }

            _storageFilters.Add(
                new FilterBetween(property, lowValue, highValue));
            return this;
        }
        /// <summary>
        /// Add a between expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lowValue"></param>
        /// <param name="highValue"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Between(string propertyName, IEleflexDataType lowValue, IEleflexDataType highValue)
        {
            IEleflexProperty property = FindProperty(propertyName);
            if (property == null)
            {
                const string methodName = CLASSNAME + "Between";
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyName));
                return this;
            }

            _storageFilters.Add(
                new FilterBetween(property, lowValue, highValue));
            return this;
        }

        /// <summary>
        /// Add a between expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lowValue"></param>
        /// <param name="highValue"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Between(string propertyName, object lowValue, object highValue)
        {
            IEleflexProperty property = null;
            IEleflexDataType lowData = null;

            const string methodName = CLASSNAME + "Between";

            if (!GetManagedValue(propertyName, lowValue, out property, out lowData))
            {
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_DataConversionLowValue1, propertyName));                
                return this;
            }
            IEleflexDataType highData = null;
            if (!GetManagedValue(propertyName, highValue, out property, out highData))
            {
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_DataConversionHighValue1, propertyName));
                return this;
            }

            _storageFilters.Add(
                new FilterBetween(property, lowData, highData));
            return this;
        }

        /// <summary>
        /// Add a between expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lowValue"></param>
        /// <param name="highValue"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Between(string propertyName, string lowValue, string highValue)
        {
            IEleflexProperty property = null;
            IEleflexDataType lowData = null;

            const string methodName = CLASSNAME + "Between";

            if (!GetManagedValue(propertyName, lowValue, out property, out lowData))
            {
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_DataConversionLowValue1, propertyName));
                return this;
            }
            IEleflexDataType highData = null;
            if (!GetManagedValue(propertyName, highValue, out property, out highData))
            {
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_DataConversionHighValue1, propertyName));
                return this;
            }

            _storageFilters.Add(
                new FilterBetween(property, lowData, highData));
            return this;
        }

        /// <summary>
        /// An an equality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsEqual(string propertyName, IEleflexDataType value)
        {
            return Compare(propertyName, ComparisonEnum.EQUAL, value);
        }

        /// <summary>
        /// A an equality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsEqual(string propertyName, object value)
        {
            return Compare(propertyName, ComparisonEnum.EQUAL, value);
        }

        /// <summary>
        /// A an equality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsEqual(string propertyName, string value)
        {
            return Compare(propertyName, ComparisonEnum.EQUAL, value);
        }

        /// <summary>
        /// Add an inequality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsNotEqual(string propertyName, IEleflexDataType value)
        {
            return Compare(propertyName, ComparisonEnum.NOT_EQUAL, value);
        }

        /// <summary>
        /// Add an inequality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsNotEqual(string propertyName, object value)
        {
            return Compare(propertyName, ComparisonEnum.NOT_EQUAL, value);
        }

        /// <summary>
        /// Add an inequality expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsNotEqual(string propertyName, string value)
        {
            return Compare(propertyName, ComparisonEnum.NOT_EQUAL, value);
        }

        /// <summary>
        /// Add a greater than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsGreaterThan(string propertyName, IEleflexDataType value)
        {
            return Compare(propertyName, ComparisonEnum.GREATER_THAN, value);
        }

        /// <summary>
        /// Add a greater than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsGreaterThan(string propertyName, object value)
        {
            return Compare(propertyName, ComparisonEnum.GREATER_THAN, value);
        }

        /// <summary>
        /// Add a greater than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsGreaterThan(string propertyName, string value)
        {
            return Compare(propertyName, ComparisonEnum.GREATER_THAN, value);
        }

        /// <summary>
        /// Add a greater than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsGreaterThanOrEqual(string propertyName, IEleflexDataType value)
        {
            return Compare(propertyName, ComparisonEnum.GREATER_THAN_EQUAL, value);
        }

        /// <summary>
        /// Add a greater than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsGreaterThanOrEqual(string propertyName, object value)
        {
            return Compare(propertyName, ComparisonEnum.GREATER_THAN_EQUAL, value);
        }

        /// <summary>
        /// Add a greater than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsGreaterThanOrEqual(string propertyName, string value)
        {
            return Compare(propertyName, ComparisonEnum.GREATER_THAN_EQUAL, value);
        }

        /// <summary>
        /// Add a less than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsLessThan(string propertyName, IEleflexDataType value)
        {
            return Compare(propertyName, ComparisonEnum.LESS_THAN, value);
        }

        /// <summary>
        /// Add a less than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsLessThan(string propertyName, object value)
        {
            return Compare(propertyName, ComparisonEnum.LESS_THAN, value);
        }

        /// <summary>
        /// Add a less than expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsLessThan(string propertyName, string value)
        {
            return Compare(propertyName, ComparisonEnum.LESS_THAN, value);
        }

        /// <summary>
        /// Add a less than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsLessThanOrEqual(string propertyName, IEleflexDataType value)
        {
            return Compare(propertyName, ComparisonEnum.LESS_THAN_EQUAL, value);
        }

        /// <summary>
        /// Add a less than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsLessThanOrEqual(string propertyName, object value)
        {
            return Compare(propertyName, ComparisonEnum.LESS_THAN_EQUAL, value);
        }

        /// <summary>
        /// Add a less than or equal to expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IPersistenceFilterBuilder IsLessThanOrEqual(string propertyName, string value)
        {
            return Compare(propertyName, ComparisonEnum.LESS_THAN_EQUAL, value);
        }

        /// <summary>
        /// Add a comparison expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="ComparisonEnum"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Compare(string propertyName, ComparisonEnum ComparisonEnum, IEleflexDataType value)
        {
            if (value == null)
                return Compare(propertyName, ComparisonEnum, (object)null);

            IEleflexProperty property = FindProperty(propertyName);
            if (property == null)
            {
                const string methodName = CLASSNAME + "Compare";
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyName));
                return this;
            }

            _storageFilters.Add(
                new FilterCompare(property, ComparisonEnum, value));
            return this;
        }
       

        /// <summary>
        /// Add a comparison expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="ComparisonEnum"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Compare(string propertyName, ComparisonEnum ComparisonEnum, object value)
        {
            IEleflexProperty property = null;
            IEleflexDataType data = null;
            if (!GetManagedValue(propertyName, value, out property, out data))
                return this;

            _storageFilters.Add(
                new FilterCompare(property, ComparisonEnum, data));
            return this;
        }

        /// <summary>
        /// Add a comparison expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="comparisonType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IPersistenceFilterBuilder Compare(string propertyName, ComparisonEnum comparisonType, string value)
        {
            IEleflexProperty property = null;
            IEleflexDataType data = null;
            if (!GetManagedValue(propertyName, value, out property, out data))
                return this;

            _storageFilters.Add(
                new FilterCompare(property, comparisonType, data));
            return this;
        }

        /// <summary>
        /// Add a property comparison expression.
        /// </summary>
        /// <param name="propertyNameLeft"></param>
        /// <param name="ComparisonEnum"></param>
        /// <param name="propertyNameRight"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder PropertyCompare(string propertyNameLeft, ComparisonEnum ComparisonEnum, string propertyNameRight)
        {
            IEleflexProperty propertyLeft = FindProperty(propertyNameLeft);

            const string methodName = CLASSNAME + "PropertyCompare";

            if (propertyLeft == null)
            {
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyNameLeft));
                return this;
            }
            IEleflexProperty propertyRight = FindProperty(propertyNameRight);
            if (propertyLeft == null)
            {
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyNameRight));
                return this;
            }

            _storageFilters.Add(
                new FilterPropertyCompare(propertyLeft, ComparisonEnum, propertyRight));
            return this;
        }

        /// <summary>
        /// Add a is like expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsLike(string propertyName, IEleflexDataType value)
        {
            return Like(propertyName, true, value);
        }

        /// <summary>
        /// Add a is like expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IPersistenceFilterBuilder IsLike(string propertyName, string value)
        {
            return Like(propertyName, true, value);
        }
        
        /// <summary>
        /// Add a like expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isLike"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Like(string propertyName, bool isLike, IEleflexDataType value)
        {
            IEleflexProperty property = FindProperty(propertyName);
            if (property == null)
            {
                const string methodName = CLASSNAME + "Like";
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyName));
                return this;
            }

            _storageFilters.Add(
                new FilterLike(property, isLike, value));
            return this;
        }

        /// <summary>
        /// Add a like expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isLike"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Like(string propertyName, bool isLike, object value)
        {
            IEleflexProperty property = null;
            IEleflexDataType data = null;
            if (!GetManagedValue(propertyName, value, out property, out data))
                return this;

            _storageFilters.Add(
                new FilterLike(property, isLike, data));
            return this;
        }

        /// <summary>
        /// Add a like expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isLike"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IPersistenceFilterBuilder Like(string propertyName, bool isLike, string value)
        {
            IEleflexProperty property = null;
            IEleflexDataType data = null;
            if (!GetManagedValue(propertyName, value, out property, out data))
                return this;

            _storageFilters.Add(
                new FilterLike(property, isLike, data));
            return this;
        }

        /// <summary>
        /// Add a not like expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsNotLike(string propertyName, IEleflexDataType value)
        {
            return Like(propertyName, false, value);
        }

        /// <summary>
        /// Add a not like expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IPersistenceFilterBuilder IsNotLike(string propertyName, string value)
        {
            return Like(propertyName, false, value);
        }

        /// <summary>
        /// Add a is null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsNull(string propertyName)
        {
            return Null(propertyName, true);
        }
        
        /// <summary>
        /// Add a null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="isNull"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Null(string propertyName, bool isNull)
        {
            IEleflexProperty property = FindProperty(propertyName);
            if (property == null)
            {
                const string methodName = CLASSNAME + "Null";
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyName));
                return this;
            }

            _storageFilters.Add(
                new FilterNull(property, isNull));
            return this;
        }

        /// <summary>
        /// Add a is not null expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsNotNull(string propertyName)
        {
            return Null(propertyName, false);
        }

        /// <summary>
        /// Add a in set expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsInSet(string propertyName, params IEleflexDataType[] values)
        {
            return Set(propertyName, true, values);
        }

        /// <summary>
        /// Add a in set expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public IPersistenceFilterBuilder IsInSet(string propertyName, params string[] values)
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
        public virtual IPersistenceFilterBuilder Set(string propertyName, bool inSet, params IEleflexDataType[] values)
        {
            IEleflexProperty property = FindProperty(propertyName);
            if (property == null)
            {
                const string methodName = CLASSNAME + "Set";
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyName));
                return this;
            }

            _storageFilters.Add(
                new FilterSet(property, inSet, values));
            return this;
        }

        /// <summary>
        /// Add a set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="inSet"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Set(string propertyName, bool inSet, params object[] values)
        {
            IEleflexProperty property = null;
            IEleflexDataType data = null;
            List<IEleflexDataType> inputValues = new List<IEleflexDataType>();
            if (values == null)
                return this;
            foreach (object value in values)
            {
                if (!GetManagedValue(propertyName, value, out property, out data))
                    return this;

                inputValues.Add(data);
            }

            _storageFilters.Add(
                new FilterSet(property, inSet, inputValues.ToArray()));
            return this;
        }

        /// <summary>
        /// Add a set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="inSet"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public IPersistenceFilterBuilder Set(string propertyName, bool inSet, params string[] values)
        {
            IEleflexProperty property = null;
            IEleflexDataType data = null;
            List<IEleflexDataType> inputValues = new List<IEleflexDataType>();
            if (values == null)
                return this;
            foreach (object value in values)
            {
                if (!GetManagedValue(propertyName, value, out property, out data))
                    return this;

                inputValues.Add(data);
            }

            _storageFilters.Add(
                new FilterSet(property, inSet, inputValues.ToArray()));
            return this;
        }

        /// <summary>
        /// Add a not in set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder IsNotInSet(string propertyName, params IEleflexDataType[] values)
        {
            return Set(propertyName, false, values);
        }

        /// <summary>
        /// Add a not in set operation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public IPersistenceFilterBuilder IsNotInSet(string propertyName, params string[] values)
        {
            return Set(propertyName, false, values);
        }

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Sort(string propertyName)
        {
            return Sort(propertyName, true);
        }

        /// <summary>
        /// Add a sort expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="sortAscending"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Sort(string propertyName, bool sortAscending)
        {
            IEleflexProperty property = FindProperty(propertyName);
            if (property == null)
            {
                const string methodName = CLASSNAME + "Sort";
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyName));
                return this;
            }
            _storageFilters.Add(
                new FilterSort(property, sortAscending));
            return this;
        }

        /// <summary>
        /// Add an averate aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Average(string propertyName)
        {
            return Aggregate(propertyName, AggregateEnum.AVERAGE);
        }

        /// <summary>
        /// Add a binary checksum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder BinaryChecksum(string propertyName)
        {
            return Aggregate(propertyName, AggregateEnum.BINARY_CHECKSUM);
        }

        /// <summary>
        /// Add a checksum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Checksum(string propertyName)
        {
            return Aggregate(propertyName, AggregateEnum.CHECKSUM);
        }

        /// <summary>
        /// Add a maximum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Maximum(string propertyName)
        {
            return Aggregate(propertyName, AggregateEnum.MAXIMUM);
        }

        /// <summary>
        /// Add a minumum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Minimum(string propertyName)
        {
            return Aggregate(propertyName, AggregateEnum.MINIMUM);
        }

        /// <summary>
        /// Add a sum aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Sum(string propertyName)
        {
            return Aggregate(propertyName, AggregateEnum.SUM);
        }

        /// <summary>
        /// Add an aggregate expression.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="AggregateEnum"></param>
        /// <returns></returns>
        public virtual IPersistenceFilterBuilder Aggregate(string propertyName, AggregateEnum AggregateEnum)
        {
            IEleflexProperty property = FindProperty(propertyName);
            if (property == null)
            {
                const string methodName = CLASSNAME + "Aggregate";
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyName));
                return this;
            }
            _storageFilters.Add(new FilterAggregate(property, AggregateEnum));
            return this;
        }

        /// <summary>
        /// Find a relationship.
        /// </summary>
        /// <param name="relationKey"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        public IPersistenceRelation FindRelationship(string relationKey, IEleflexPersistenceObject dto)
        {
            List<IPersistenceRelation> relations = dto.EPOGetRelations();
            if (relations == null)
                return null;

            for (int i = 0; i < relations.Count; i++)
            {
                if (relations[i].EPRGetName() == relationKey)
                    return relations[i];
            }
            return null;
        }

        /// <summary>
        /// Find a property.
        /// </summary>
        /// <param name="propertyKey"></param>
        /// <returns></returns>
        public IEleflexProperty FindProperty(string propertyKey)
        {
            List<IEleflexProperty> properties = _epo.EleflexGetProperties();
            if (properties == null)
                return null;

            for (int pCount = 0; pCount < properties.Count; pCount++)
            {
                if (properties[pCount].Name == propertyKey)
                    return properties[pCount];
            }

            //Not found in base object, look in relationships
            for (int i = 0; i < _relations.Count; i++)
            {
                IEleflexObject tempEPO = _relations[i].EPRGetForeignProperties()[0].ParentObject;
                properties = tempEPO.EleflexGetProperties();
                for (int pCount = 0; pCount < properties.Count; pCount++)
                {
                    if (properties[pCount].Name == propertyKey)
                        return properties[pCount];
                }
                //try inverted relationship
                //tempEPO = _relations[i].EPRGetParentProperties()[0].ParentEPO;
                //properties = tempEPO.EPOGetProperties();
                //for (int pCount = 0; pCount < properties.Count; pCount++)
                //{
                //    if (properties[pCount].Name == propertyKey)
                //        return properties[pCount];
                //}
            }
            return null;
        }

        /// <summary>
        /// Get a managed value given a property name and a value
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <param name="property"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool GetManagedValue(string propertyName, object value, out IEleflexProperty property, out IEleflexDataType data)
        {
            data = null;

            property = FindProperty(propertyName);

            const string methodName = CLASSNAME + "GetManagedValue";
            
            if (property == null)
            {                
                _context.AddMessage(new EleflexMessage(true, false, methodName, PersistenceConstants.UserMessage_PropertyNotFound1, propertyName));
                return false;
            }

            IEleflexDataType dataType = null;
            if (!property.ParentObject.EleflexGetPropertyValue(propertyName, out dataType))
            {
                _context.AddMessage(new EleflexMessage(true, true, methodName, EleflexConstants.SystemMessage_PropertyGet1, propertyName));
                return false;
            }

            string stringValue = null;
            if (value != null)
                stringValue = value.ToString();

            return dataType.Convert(stringValue, out data);
        }

    }
}

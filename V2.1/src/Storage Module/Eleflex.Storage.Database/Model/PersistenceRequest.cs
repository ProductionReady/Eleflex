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
using Eleflex.Storage.Database.Filters;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a persistence request.
    /// </summary>
    public partial class PersistenceRequest : SecurityRequest, IPersistenceRequest
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.Persistence.PersistenceRequest" + EleflexProperty.Field_Seperator;
        /// <summary>
        /// The default timeout value for requests (in seconds).
        /// </summary>
        private const int DEFAULT_TIMOUT_SECS = 60;

        /// <summary>
        /// The default maximum number of records to return.
        /// </summary>
        private const int DEFAULT_MAX_NUMBER_RECORDS = int.MaxValue;

        /// <summary>
        /// Internal EPO.
        /// </summary>
        protected IEleflexPersistenceObject _epo;
        /// <summary>
        /// Internal Builder.
        /// </summary>
        protected FilterBuilder _builder;
        /// <summary>
        /// Internal StartPage.
        /// </summary>
        protected int _startPage;
        /// <summary>
        /// Internal NumberPerPage.
        /// </summary>
        protected int _numberPerPage = DEFAULT_MAX_NUMBER_RECORDS;
        /// <summary>
        /// Internal Transaction.
        /// </summary>
        protected IPersistenceTransaction _transaction;
        /// <summary>
        /// Internal Timeout.
        /// </summary>
        protected int _timeout = DEFAULT_TIMOUT_SECS;


        /// <summary>
        /// Constructor.
        /// </summary>
        public PersistenceRequest() : base() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dto"></param>
        public PersistenceRequest(IEleflexPersistenceObject dto) :
            base()
        {
            _epo = dto;
            _builder = new FilterBuilder(Context, _epo);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        public PersistenceRequest(ISecurityContext context) :
            base()
        {
            if (context != null)
                Context = context;
            _builder = new FilterBuilder(Context, _epo);
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="dto"></param>
        /// <param name="transaction"></param>
        public PersistenceRequest(ISecurityContext context, IEleflexPersistenceObject dto, IPersistenceTransaction transaction) :
            base()
        {
            if (context != null)
                Context = context;
            _epo = dto;
            _builder = new FilterBuilder(Context, _epo);
            _transaction = transaction;
        }

        /// <summary>
        /// Create an instance of the implementing object to eliminate reflection usage.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new PersistenceRequest();
        }

        /// <summary>
        /// Context.
        /// </summary>
        public override ISecurityContext Context
        {
            get { return base.Context; }
            set
            {
                base.Context = value;
                if (_builder != null)
                    _builder.Context = this.Context;
            }
        }

        /// <summary>
        /// Build a list of storage filters to be used with the request.
        /// </summary>
        public virtual FilterBuilder Builder
        {
            get 
            {
                if(_builder == null)
                    _builder = new FilterBuilder(Context, _epo);
                return _builder; 
            }
            set { _builder = value; }
        }

        /// <summary>
        /// The DataTransferObject associated to this request.
        /// </summary>
        public virtual IEleflexPersistenceObject EPO
        {
            get { return _epo; }
            set { _epo = value; }
        }

        /// <summary>
        /// The list of storage filters associated with the request.
        /// </summary>
        public virtual List<IPersistenceFilter> Filters
        {
            get { return _builder.Filters; }
            set { _builder.Filters = value; }
        }

        /// <summary>
        /// The start page.
        /// </summary>
        public virtual int StartPage
        {
            get { return _startPage; }
            set { _startPage = value; }
        }

        /// <summary>
        /// The number of items per page.
        /// </summary>
        public virtual int NumberPerPage
        {
            get { return _numberPerPage; }
            set { _numberPerPage = value; }
        }

        /// <summary>
        /// The timout value for the request (in seconds).
        /// </summary>
        public virtual int TimeoutSecs
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// The transaction associated with the request.
        /// </summary>
        public virtual IPersistenceTransaction Transaction
        {
            get
            {
                if (_transaction != null)
                {
                    if (!_transaction.IsAlive)
                        _transaction = null;
                }
                return _transaction;
            }
            set { _transaction = value; }
        }

    }
}

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
using System.Data;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a parameter used with a database command.
    /// </summary>
    public partial class DatabaseCommandParameter : IDatabaseCommandParameter
    {

        /// <summary>
        /// Internal DBType.
        /// </summary>
        protected DbType _dbType;
        /// <summary>
        /// Internal Name.
        /// </summary>
        protected string _name;        
        /// <summary>
        /// Internal Size.
        /// </summary>
        protected int _size;
        /// <summary>
        /// Internal Value.
        /// </summary>
        protected IEleflexDataType _value;
        /// <summary>
        /// Internal StorageProperty.
        /// </summary>
        private IEleflexProperty _iProperty;


        /// <summary>
        /// Constructor.
        /// </summary>
        public DatabaseCommandParameter() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="value"></param>
        public DatabaseCommandParameter(string name, DbType dbType, IEleflexDataType value)
        {
            _name = name;
            _dbType = dbType;
            _value = value;
        }


        /// <summary>
        /// The database data type.
        /// </summary>
        public virtual DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        /// <summary>
        /// The name of the parameter.
        /// </summary>
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// The size of the parameter.
        /// </summary>
        public virtual int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        /// <summary>
        /// The value of the paramter.
        /// </summary>
        public virtual IEleflexDataType Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// The property associated with the parameter.
        /// </summary>
        public virtual IEleflexProperty IProperty
        {
            get { return _iProperty; }
            set { _iProperty = value; }
        }

    }
}

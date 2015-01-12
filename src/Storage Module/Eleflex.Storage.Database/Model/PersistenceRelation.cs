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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a relationship between objects in the framework.
    /// </summary>
    public partial class PersistenceRelation : IPersistenceRelation
    {

        /// <summary>
        /// Internal Properties.
        /// </summary>
        protected List<IEleflexProperty> _properties;
        /// <summary>
        /// Internal ForeignProperties.
        /// </summary>
        protected List<IEleflexProperty> _foreignProperties;
        /// <summary>
        /// Internal Name.
        /// </summary>
        protected string _name;


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="properties"></param>
        /// <param name="foreignProperties"></param>
        public PersistenceRelation(string name, List<IEleflexProperty> properties, List<IEleflexProperty> foreignProperties)
        {
            _name = name;
            _properties = properties;
            _foreignProperties = foreignProperties;
        }


        /// <summary>
        /// Get the name of the relationship.
        /// </summary>
        /// <returns></returns>
        public virtual string EPRGetName()
        {
            return _name;
        }

        /// <summary>
        /// Get a list of properties of parent object defined for the relationship.
        /// </summary>
        /// <returns></returns>
        public virtual List<IEleflexProperty> EPRGetParentProperties()
        {
            return _properties;
        }

        /// <summary>
        /// Get a list of properties on the foreign object defined for the relationship.
        /// </summary>
        /// <returns></returns>
        public virtual List<IEleflexProperty> EPRGetForeignProperties()
        {
            return _foreignProperties;
        }

    }
}

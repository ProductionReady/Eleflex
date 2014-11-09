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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines a DTO object used with databases. We are purposefully not exposing definitions as properties to provide a base type that
    /// inheritors can use for basic inherited database objects.
    /// </summary>
    public partial class EleflexDatabaseObject : EleflexPersistenceObject, IEleflexDatabaseObject
    {

        /// <summary>
        /// The unique key used to distinguish this object from others.
        /// </summary>
        public new const string EleflexObjectKey = "PR.Eleflex.Persistence.EleflexDatabaseObject" + EleflexProperty.Field_Seperator;


        /// <summary>
        /// Internal DatabaseObjectName.
        /// </summary>
        protected string _databaseObjectName;
        /// <summary>
        /// Internal DTOType.
        /// </summary>
        protected DatabaseObjectType _databaseObjectType;
        /// <summary>
        /// Internal schema.
        /// </summary>
        protected IDatabaseSchema _databaseSchema;
        /// <summary>
        /// Internal indexes.
        /// </summary>
        protected List<IDatabaseIndex> _databaseIndexes;
        /// <summary>
        /// Internal constraints.
        /// </summary>
        protected List<IDatabaseConstraint> _databaseConstraints;


        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexDatabaseObject() : base() 
        {
            _eleflexObjectKey = EleflexObjectKey;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="catalog"></param>
        /// <param name="schema"></param>
        /// <param name="name"></param>
        /// <param name="type"></param>
        public EleflexDatabaseObject(string catalog, IDatabaseSchema schema, string name, DatabaseObjectType type)
            : base() 
        {
            _eleflexObjectKey = catalog + EleflexProperty.Field_Seperator + schema.Name + EleflexProperty.Field_Seperator + name + EleflexProperty.Field_Seperator;
            _epoCatalogName = catalog;
            _databaseSchema = schema;
            _databaseObjectName = name;
            _databaseObjectType = type;            
        }

        /// <summary>
        /// Create an instance of the implementing object to eliminate reflection usage.
        /// </summary>
        /// <returns></returns>
        public override IEleflexObject EleflexCreate()
        {
            return new EleflexDatabaseObject();
        }

        //Initialize not needed        

        /// <summary>
        /// Get the name of the database object.
        /// </summary>
        /// <returns></returns>
        public virtual string EDOGetName()
        {
            return _databaseObjectName;
        }

        /// <summary>
        /// Get the type of database object.
        /// </summary>
        /// <returns></returns>
        public virtual DatabaseObjectType EDOGetType()
        {
            return _databaseObjectType;
        }

        /// <summary>
        /// Get a list of database properties.
        /// </summary>
        /// <returns></returns>
        public virtual List<IEDOProperty> EDOGetProperties()
        {
            List<IEDOProperty> list = new List<IEDOProperty>();
            List<IEleflexProperty> dtoProperties = EleflexGetProperties();
            foreach (IEleflexProperty prop in dtoProperties)
            {
                if (prop is IEDOProperty)
                    list.Add(prop as IEDOProperty);
            }
            return list;
        }

        /// <summary>
        /// Get schema.
        /// </summary>
        public virtual IDatabaseSchema EDOGetSchema()
        {
            return _databaseSchema;
        }

        /// <summary>
        /// The list of assocated foreign keys for the object.
        /// </summary>
        public virtual List<IDatabaseForeignKey> EDOGetForeignKeys()
        {
            List<IDatabaseForeignKey> list = new List<IDatabaseForeignKey>();
            List<IPersistenceRelation> relations = EPOGetRelations();
            foreach (IPersistenceRelation relation in relations)
            {
                if (relation is IDatabaseForeignKey)
                    list.Add(relation as IDatabaseForeignKey);
            }
            return list;
        }

        /// <summary>
        /// The list of associated Indexes for the object.
        /// </summary>
        public virtual List<IDatabaseIndex> EDOGetIndexes()
        {
            return _databaseIndexes;
        }

        /// <summary>
        /// The list of associated Indexes for the object.
        /// </summary>
        public virtual List<IDatabaseConstraint> EDOGetConstraints()
        {
            return _databaseConstraints;
        }


        /// <summary>
        /// Set extended attributes of the object.
        /// </summary>
        /// <param name="constraints"></param>
        /// <param name="foreignKeys"></param>
        /// <param name="indexes"></param>
        public virtual void EDOSetExtendedAttrbutes(List<IDatabaseConstraint> constraints, List<IDatabaseIndex> indexes, List<IPersistenceRelation> relations)
        {
            _databaseConstraints = constraints;
            _databaseIndexes = indexes;
            _relations = relations;
        }

    }
}

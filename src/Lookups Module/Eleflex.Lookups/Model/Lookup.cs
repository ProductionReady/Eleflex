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

namespace Eleflex.Lookups
{
    /// <summary>
    /// Lookup is a modifier for an item and is used for denoting multiple types in the system. (a lookup table).
    /// </summary>
    public class Lookup : ILookup
    {
        /// <summary>
        /// The key for the object.
        /// </summary>
        public virtual string Key { get; set; }

        /// <summary>
        /// Determine if inactive.
        /// </summary>
        public virtual bool Inactive { get; set; }

        /// <summary>
        /// Code is used to unqiuely identify a lookup value in source code as the Id value may change depending on storage implementation.
        /// </summary>
        public virtual Guid Code { get; set; }

        /// <summary>
        /// Category this lookup belongs to.
        /// </summary>
        public virtual Lookup Category { get; set; }        

        /// <summary>
        /// Sort order.
        /// </summary>
        public virtual int? SortOrder { get; set; }

        /// <summary>
        /// The abbreviation.
        /// </summary>
        public virtual string Abbreviation { get; set; }

        /// <summary>
        /// The name.
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// The description.
        /// </summary>
        public virtual string Description { get; set; }




        public virtual void ChangeLookupKey(string val)
        {
            Key = val;
        }
        public virtual void ChangeLookupInactive(bool val)
        {
            Inactive = val;
        }
        public virtual void ChangeLookupCode (Guid val)
        {
            Code = val;
        }
        public virtual void ChangeLookupCategory (Lookup val)
        {
            Category = val;
        }
        public virtual void ChangeLookupSort (int? val)
        {
            SortOrder = val;
        }
        public virtual void ChangeLookupAbbreviation (string val)
        {
            Abbreviation = val;
        }
        public virtual void ChangeLookupName (string val)
        {
            Name = val;
        }
        public virtual void ChangeLookupDescription(string val)
        {
            Description = val;
        }

    }
}

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

namespace Eleflex.Versioning.Message
{
    /// <summary>
    /// Defines a version used in the patching process.
    /// </summary>
    public class Version
    {
        /// <summary>
        /// Major.
        /// </summary>
        public virtual int Major { get; set; }
        /// <summary>
        /// Minor.
        /// </summary>
        public virtual int Minor { get; set; }
        /// <summary>
        /// Build.
        /// </summary>
        public virtual int Build { get; set; }
        /// <summary>
        /// Revision.
        /// </summary>
        public virtual int Revision { get; set; }

        /// <summary>
        /// Override to string to display the version number.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Major.ToString() + "." + Minor.ToString() + "." + Build.ToString() + "." + Revision.ToString();
        }
    }
}

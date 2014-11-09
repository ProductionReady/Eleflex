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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Versioning
{
    /// <summary>
    /// Defines a version in the patching process.
    /// </summary>
    public class Version
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Version() { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="major"></param>
        /// <param name="minor"></param>
        /// <param name="build"></param>
        /// <param name="revision"></param>
        public Version(int major, int minor, int build, int revision)
        {
            Major = major;
            Minor = minor;
            Build = build;
            Revision = revision;
        }

        /// <summary>
        /// Major. This is the major release of Eleflex. Currently V2.
        /// </summary>
        public virtual Nullable<int> Major { get; protected set; }
        /// <summary>
        /// Minor. This number increases when a storage change is required.
        /// </summary>
        public virtual Nullable<int> Minor { get; protected set; }
        /// <summary>
        /// Build. This number increases when an integrated component changes.
        /// </summary>
        public virtual Nullable<int> Build { get; protected set; }
        /// <summary>
        /// Revision. This number change when only a code change is required.
        /// </summary>
        public virtual Nullable<int> Revision { get; protected set; }

        /// <summary>
        /// Change major.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeMajor (int val)
        {
            Major = val;
        }
        /// <summary>
        /// Change minor.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeMinor(int val)
        {
            Minor = val;
        }
        /// <summary>
        /// Change build.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeBuild (int  val)
        {
            Build = val;
        }
        /// <summary>
        /// CHange revision.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeRevision(int val)
        {
            Revision = val;
        }

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

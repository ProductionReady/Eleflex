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
using Eleflex.Storage;

namespace Eleflex.Versioning
{
    /// <summary>
    /// Domain model for a module version. This will track which modules are currently installed and their respective version.
    /// This way when modules are updated the code can determine which which version to update from.
    /// </summary>
    public class ModuleVersion : IModuleVersion, IStorageExtraData
    {
        /// <summary>
        /// Unique module key.
        /// </summary>
        public virtual Guid ModuleKey { get; protected set; }
        /// <summary>
        /// Module name.
        /// </summary>
        public virtual string ModuleName { get; protected set; }
        /// <summary>
        /// Updated date.
        /// </summary>
        public virtual Nullable<System.DateTimeOffset> UpdateDate { get; set; }
        /// <summary>
        /// Version number.
        /// </summary>
        public virtual Version Version {get;protected set;}
        /// <summary>
        /// Property providing an extension for customized information.
        /// </summary>
        public virtual string ExtraData { get; set; }
        
        /// <summary>
        /// Change module key.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeModuleKey(Guid val)
        {
            ModuleKey = val;
        }

        /// <summary>
        /// Change module name.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeModuleName(string val)
        {
            ModuleName = val;
        }
        /// <summary>
        /// Change update date.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeUpdateDate(DateTimeOffset? val)
        {
            UpdateDate = val;
        }
        /// <summary>
        /// Change version number.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeVersion(Version val)
        {
            Version = val;
        }
        /// <summary>
        /// Change custom data.
        /// </summary>
        /// <param name="val"></param>
        public virtual void ChangeExtraData(string val)
        {
            ExtraData = val;
        }
    }
}

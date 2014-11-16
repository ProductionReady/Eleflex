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
using Eleflex;
using Microsoft.Practices.ServiceLocation;

namespace Eleflex.Versioning
{
    /// <summary>
    /// Patch for a module in the framework.
    /// </summary>
    public abstract class ModulePatch : ModuleVersion, IModulePatch
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="moduleKey"></param>
        /// <param name="moduleName"></param>
        public ModulePatch(Guid moduleKey, string moduleName)
        {
            ModuleKey = moduleKey;
            ModuleName = moduleName;
        }

        /// <summary>
        /// Dependent modules that must be executed prior to this version.
        /// </summary>
        public abstract List<Guid> DependentModules { get; }

        /// <summary>
        /// The versions prior to this version for which the patching process can be used.
        /// </summary>
        public abstract List<IModulePatch> PriorVersions { get; }
        
        /// <summary>
        /// Custom logic to update the patch.
        /// </summary>
        /// <returns></returns>
        public abstract void Update();

        /// <summary>
        /// Return patch name and version information quickly.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string output = ModuleName + " " + ModuleKey.ToString();
            if (Version != null)
                output += " " + Version.ToString();
            return output;
        }

    }
}

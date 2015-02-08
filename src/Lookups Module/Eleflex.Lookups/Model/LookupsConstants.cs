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

namespace Eleflex.Lookups
{
    /// <summary>
    /// Constants for use with the lookup module.
    /// </summary>
    public class LookupsConstants
    {        

        /// <summary>
        /// The module key for lookups.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("E3E389AE-5CEF-40D8-B271-13EFCCD30425");

        /// <summary>
        /// The module name for lookups.
        /// </summary>
        public const string MODULE_NAME = "Eleflex Lookups Module";

        /// <summary>
        /// The module key for lookups.
        /// </summary>
        public static Guid STORAGE_PROVIDER_MODULE_KEY = Guid.Parse("C51F23A4-2BDC-4971-9D3D-DF7C1355B436");

        /// <summary>
        /// The name used to distinguish the lookups storage provider from others.
        /// </summary>
        public const string STORAGE_PROVIDER_NAME = "EleflexLookupsStorageProvider";

    }
}

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

namespace Eleflex.Versioning
{
    /// <summary>
    /// Constants for use with the versioning module.
    /// </summary>
    public class VersioningConstants
    {
        /// <summary>
        /// The name used to distinguish the versioning storage provider from others.
        /// </summary>
        public const string STORAGE_PROVIDER_NAME = "EleflexVersioningStorageProvider";

        /// <summary>
        /// The module key for versioning.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("ECC9BCD6-5C0D-47B6-AF0D-72149664BB39");

        /// <summary>
        /// The module name for versioning.
        /// </summary>
        public const string MODULE_NAME = "Eleflex Versioning Module";
        
    }
}

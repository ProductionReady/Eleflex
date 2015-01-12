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

namespace Eleflex.Logging
{
    /// <summary>
    /// Constants for use with the logging module.
    /// </summary>
    public class LoggingConstants
    {
        /// <summary>
        /// The name used to distinguish the logging storage provider from others.
        /// </summary>
        public const string STORAGE_PROVIDER_NAME = "EleflexLoggingStorageProvider";

        /// <summary>
        /// The module key for logging.
        /// </summary>
        public static Guid MODULE_KEY = Guid.Parse("CD1B63E1-D585-40ED-A05E-EC88B75A7B6D");

        /// <summary>
        /// The module name for logging.
        /// </summary>
        public const string MODULE_NAME = "Eleflex Logging Module";

    }
}

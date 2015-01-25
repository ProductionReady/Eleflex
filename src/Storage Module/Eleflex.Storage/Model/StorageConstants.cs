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

namespace Eleflex.Storage
{
    /// <summary>
    /// Constants for use wih the Storage Module.
    /// </summary>
    public class StorageConstants
    {

        /// <summary>
        /// Connection string key for the default eleflex database instance.
        /// </summary>
        public const string CONNECTION_STRING_KEY_DEFAULT = "EleflexDefault";

        /// <summary>
        /// The default maximum number of records.
        /// </summary>
        public const int MAX_RETURNED_RECORDS_DEFAULT = 100;

    }
}

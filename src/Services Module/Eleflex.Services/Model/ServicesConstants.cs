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

namespace Eleflex.Services
{
    /// <summary>
    /// Message constants used with processing service commands.
    /// </summary>
    public class ServicesConstants
    {
        /// <summary>
        /// THe name of the service enpoint (in app/web.config) for the default Eleflex command service.
        /// </summary>
        public const string SERVICE_ENDPOINT_NAME_DEFAULT = "EleflexDefault";

        public const string ERROR_SYSTEM_GENERAL = "A general system error has occured";
        public const string ERROR_SYSTEM_GENERAL_CODE = "ERROR_SYSTEM_GENERAL";

        public const string ERROR_SERVICE = "The service may be busy or is offline.";
        public const string ERROR_SERVICE_CODE = "ERROR_SERVICE";

        public const string ERROR_SERVICE_REQUEST_INVALD = "The request is invalid.";
        public const string ERROR_SERVICE_REQUEST_INVALD_CODE = "ERROR_SERVICE_REQUEST_INVALD";

    }
}

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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines constants used for the service.
    /// </summary>
    public partial class EleflexConstants
    {

        /// <summary>
        /// Default datacontract namespace for exposed objects.
        /// </summary>
        public const string DataContractNamespace = "http://Schemas.ProductionReady.com/Frameworks/Eleflex/v2013";

        /// <summary>
        /// Default service endpoint.
        /// </summary>
        public const string ServiceEndpointName = "EleflexServiceEndpoint";


        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_Exception1 = "Exception: {0}";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_DataTypeFactoryCreation1 = "Could not create datatype {0}";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_PropertyGet1 = "Could not get property {0}";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_PropertySet1 = "Could not set property {0}";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_InterfaceCache1 = "InterfaceCache could not find key {0}";



        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_InternalSystemError = "Internal system error";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_RequestNull = "Request is null";


    }
}

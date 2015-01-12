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
    public partial class PersistenceConstants
    {

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_EPONull = "EPO is null";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_PropertyNull = "EleflexProperty is null";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_FilterNull = "Filter is null";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_StorageProviderNotFound1 = "storage provider not found {0}";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_FilterInvalid = "Filter invalid";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_FilterPropertyMismatch = "Filter property mismatch";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_FilterValueMismatch = "Filter value mismatch";

        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_FilterConversion1 = "Filter conversion error for {0}";
        
        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_PropertyInvalid1 = "EleflexProperty invalid for {0}";




        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_RelationNotFound1 = "Relation not found {0}";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_PropertyNotFound1 = "EleflexProperty not found {0}";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_DataConversionLowValue1 = MappingConstants.UserMessage_DataConversion1 + " of low value";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_DataConversionHighValue1 = MappingConstants.UserMessage_DataConversion1 + " of high value";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_FilterSelectPropertiesNotSpecified = "no select properties specified";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_FilterUpdatePropertiesNotSpecified = "no update properties specified";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_FilterInsertPropertiesNotSpecified = "no insert properties specified";
            

        

        ///// <summary>
        ///// Error.
        ///// </summary>
        //public const string Error_ObjectNotDBTO = "object is not a DBTO";

        ///// <summary>
        ///// Error.
        ///// </summary>
        //public const string Error_PropertyNotDBP = "object is not a DBP";        

        ///// <summary>
        ///// Error.
        ///// </summary>
        //public const string Error_RelatioNotFound = "relation not found";

    }
}

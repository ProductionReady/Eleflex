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
    public partial class MappingConstants
    {


        /// <summary>
        /// System message.
        /// </summary>
        public const string SystemMessage_MapRuleNull = "Mapping rule is null";



        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_DataNull1 = "{0} cannot be null";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_DataConversion1 = "{0} conversion error";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_RangeValidation3 = "{0} must be greater than or equal to {1} and less than or equal to {2}";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_DataFormat1 = "{0} is not in a valid format";

        /// <summary>
        /// User message.
        /// </summary>
        public const string UserMessage_DataExceedsLength2 = "{0} exceeds length of {1}";


    }
}

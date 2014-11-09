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

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Defines the execution context of the request.
    /// </summary>
    public partial interface IEleflexContext : IEleflexObject
    {

        /// <summary>
        /// Determines if an overall error occured processing the request.
        /// </summary>
        bool IsError { get; set; }
        

        /// <summary>
        /// List of messages
        /// </summary>
        List<IEleflexMessage> GetMessages();

        /// <summary>
        /// Add a message to the context.
        /// </summary>
        /// <param name="message"></param>
        void AddMessage(IEleflexMessage message);

        /// <summary>
        /// Add context information to this instance.
        /// </summary>
        /// <param name="context"></param>
        void AddContext(IEleflexContext context);

    }
}

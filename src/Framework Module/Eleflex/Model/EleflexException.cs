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
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// The base exception for all managed exceptions in the application.
    /// </summary>
    public class EleflexException : Exception, IEleflexException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public EleflexException() : base() 
        {
            ValidationMessages = new List<EleflexValidationMessage>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message"></param>
        public EleflexException(string message) : base(message) 
        {
            ValidationMessages = new List<EleflexValidationMessage>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public EleflexException(string message, Exception innerException) : base(message, innerException) 
        {
            ValidationMessages = new List<EleflexValidationMessage>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="messages"></param>
        public EleflexException(List<EleflexValidationMessage> messages)
            : base()
        {
            ValidationMessages = new List<EleflexValidationMessage>();
            if(messages != null)
                ValidationMessages.AddRange(messages);
            
        }

        /// <summary>
        /// List of validation messages.
        /// </summary>
        public List<EleflexValidationMessage> ValidationMessages { get; set; }
    }
}

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

namespace Eleflex.Services
{
    /// <summary>
    /// Contains information returned from a service command, such as error and messages.
    /// </summary>
    public class ServiceCommandResponseStatus
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ServiceCommandResponseStatus()
        {
            Messages = new List<EleflexValidationMessage>();
        }

        /// <summary>
        /// Determine is the result of the call was an error.
        /// </summary>
        public virtual bool IsError
        {
            get
            {
                if (Messages == null || Messages.Count == 0)
                    return false;
                foreach (EleflexValidationMessage message in Messages)
                {
                    if (message.IsError)
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// The list of message.
        /// </summary>
        public virtual List<EleflexValidationMessage> Messages { get; set; }

        /// <summary>
        /// Add an error message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageCode"></param>
        /// <param name="field"></param>
        public virtual void AddError(string message, string messageCode = null, string field = null)
        {
            EleflexValidationMessage item = new EleflexValidationMessage();
            item.IsError = true;
            item.Field = field;
            item.Message = message;
            item.MessageCode = messageCode;
            Messages.Add(item);
        }

        /// <summary>
        /// Add an informational message.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageCode"></param>
        /// <param name="field"></param>
        public virtual void AddMessage(string message, string messageCode = null, string field = null)
        {
            EleflexValidationMessage item = new EleflexValidationMessage();
            item.IsError = false;
            item.Field = field;
            item.Message = message;
            item.MessageCode = messageCode;
            Messages.Add(item);
        }
    }
}
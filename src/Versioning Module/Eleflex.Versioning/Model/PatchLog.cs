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
using Common.Logging;

namespace Eleflex.Versioning
{
    /// <summary>
    /// a log message used during the patching process.
    /// </summary>
    public class PatchLog
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="source"></param>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public PatchLog(LogLevel level, string source, string message, Exception exception)
        {
            CreateDate = DateTimeOffset.UtcNow;
            Level = level;
            Source = source;
            Message = message;
            Exception = exception;
        }

        /// <summary>
        /// Date created/
        /// </summary>
        public DateTimeOffset CreateDate { get; set; }
        /// <summary>
        /// Level.
        /// </summary>
        public LogLevel Level { get; set; }
        /// <summary>
        /// Message/
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Source.
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Exception.
        /// </summary>
        public Exception Exception { get; set; }
    }
}

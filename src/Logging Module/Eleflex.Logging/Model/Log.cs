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
    /// Domain model for a Log message.
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Key.
        /// </summary>
        public long LogKey	{get;set;}
        /// <summary>
        /// Created.
        /// </summary>
        public DateTimeOffset CreateDate {get;set;}
        /// <summary>
        /// Server name.
        /// </summary>
        public string Server	{get;set;}
        /// <summary>
        /// Application name.
        /// </summary>
        public string Application	{get;set;}
        /// <summary>
        /// IsError.
        /// </summary>
        public bool IsError	{get;set;}
        /// <summary>
        /// Severity.
        /// </summary>
        public string Severity	{get;set;}
        /// <summary>
        /// Message.
        /// </summary>
        public string Message	{get;set;}
        /// <summary>
        /// Message.
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// Message.
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// change key.
        /// </summary>
        /// <param name="val"></param>
        public void ChangeLogKey(long val)
        {
            LogKey = val;
        }
        /// <summary>
        /// Change create date.
        /// </summary>
        /// <param name="val"></param>
        public void ChangeCreateDate(DateTimeOffset val)
        {
            CreateDate = val;
        }
        /// <summary>
        /// Change server.
        /// </summary>
        /// <param name="val"></param>
        public void ChangeServer (string val)
        {
            Server = val;
        }
        /// <summary>
        /// Chage application.
        /// </summary>
        /// <param name="val"></param>
        public void ChangeApplication(string val)
        {
            Application = val;
        }
        /// <summary>
        /// CHange iserror.
        /// </summary>
        /// <param name="val"></param>
        public void ChangeIsError (bool val)
        {
            IsError = val;
        }
        /// <summary>
        /// Change severity.
        /// </summary>
        /// <param name="val"></param>
        public void ChangeSeverity (string val)
        {
            Severity = val;
        }
        /// <summary>
        /// CHange message.
        /// </summary>
        /// <param name="val"></param>
        public void ChangeMessage(string val)
        {
            Message = val;
        }
        /// <summary>
        /// Change source.
        /// </summary>
        /// <param name="val"></param>
        public void ChangeSource(string val)
        {
            Source = val;
        }
        /// <summary>
        /// CHange exception.
        /// </summary>
        /// <param name="val"></param>
        public void ChangeException(string val)
        {
            Exception = val;
        }
    }
}

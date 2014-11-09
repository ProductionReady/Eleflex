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
using System.Runtime.Serialization;

namespace Eleflex.Storage.Database
{

    /// <summary>
    /// Credentials for the request.
    /// </summary>
    public partial class SecurityCredentials : ISecurityCredentials
    {


        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityCredentials() : this(null, null, null, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="token"></param>
        public SecurityCredentials(string token) : this(null, null, null, token) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public SecurityCredentials(string username, string password) : this(null, username, password, null) { }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public SecurityCredentials(string domain, string username, string password) : this(domain, username, password, null) { }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        public SecurityCredentials(string domain, string username, string password, string token)
        {
            Domain = domain;
            Username = username;
            Password = password;
            Token = token;
        }
        
        /// <summary>
        /// Username
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Token.
        /// </summary>
        public string Token { get; set; }

    }
}

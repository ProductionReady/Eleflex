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
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Threading;

namespace Eleflex.Web
{

    /// <summary>
    /// This class temporarily set user impersonation for service calls
    /// </summary>
    public class ImpersonateUser : IDisposable
    {

        /// <summary>
        /// Key used to denote user impersonation username.
        /// </summary>
        public const string KEY_IMPERSONATE_USER_USERNAME = "EleflexImpersonateUserUsername";
        /// <summary>
        /// Key used to denote user impersonation password.
        /// </summary>
        public const string KEY_IMPERSONATE_USER_PASSWORD = "EleflexImpersonateUserPassword";


        /// <summary>
        /// Thread-local storage (if no httpcontext available)
        /// </summary>
        private static readonly ThreadLocal<string> _impersonateUsername = new ThreadLocal<string>(() => null);
        /// <summary>
        /// Thread-local storage (if no httpcontext available)
        /// </summary>
        private static readonly ThreadLocal<string> _impersonatePassword = new ThreadLocal<string>(() => null);
        

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImpersonateUser(string username, string password)
        {
            if (HttpContext.Current != null)
            {
                if (!HttpContext.Current.Items.Contains(KEY_IMPERSONATE_USER_USERNAME))
                {
                    HttpContext.Current.Items.Add(KEY_IMPERSONATE_USER_USERNAME, username);
                }
                else
                {
                    HttpContext.Current.Items[KEY_IMPERSONATE_USER_USERNAME] = username;
                }
                if (!HttpContext.Current.Items.Contains(KEY_IMPERSONATE_USER_PASSWORD))
                {
                    HttpContext.Current.Items.Add(KEY_IMPERSONATE_USER_PASSWORD, password);
                }
                else
                {
                    HttpContext.Current.Items[KEY_IMPERSONATE_USER_PASSWORD] = password;
                }
            }
            else
            {
                _impersonateUsername.Value = username;
                _impersonatePassword.Value = password;
            }
        }

        /// <summary>
        /// Disposal
        /// </summary>
        public void Dispose()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(KEY_IMPERSONATE_USER_USERNAME))
                {
                    HttpContext.Current.Items[KEY_IMPERSONATE_USER_USERNAME] = null;
                }
                if (HttpContext.Current.Items.Contains(KEY_IMPERSONATE_USER_PASSWORD))
                {
                    HttpContext.Current.Items[KEY_IMPERSONATE_USER_PASSWORD] = null;
                }
            }
            else
            {
                _impersonatePassword.Value = null;
                _impersonateUsername.Value = null;
            }

        }


        /// <summary>
        /// Determine if impersonating
        /// </summary>
        public static bool IsImpersonateUser
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items.Contains(KEY_IMPERSONATE_USER_USERNAME))
                    {
                        return HttpContext.Current.Items[KEY_IMPERSONATE_USER_USERNAME] != null;
                    }
                    return false;
                }
                else
                {
                    return _impersonateUsername.Value != null;
                }
            }
        }

        public static string GetUsername
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items.Contains(KEY_IMPERSONATE_USER_USERNAME))
                        return HttpContext.Current.Items[KEY_IMPERSONATE_USER_USERNAME] as string;
                    return null;
                }
                else
                {
                    return _impersonateUsername.Value;
                }
            }
        }

        public static string GetPassword
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items.Contains(KEY_IMPERSONATE_USER_PASSWORD))
                        return HttpContext.Current.Items[KEY_IMPERSONATE_USER_PASSWORD] as string;
                    return null;
                }
                else
                {
                    return _impersonatePassword.Value;
                }
            }
        }

    }
}

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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Threading;

namespace Eleflex.Web
{

    /// <summary>
    /// This class temporarily set system admin impersonation for service calls
    /// </summary>
    public class ImpersonateSystem : IDisposable
    {

        /// <summary>
        /// Key used to denote admin impersonation.
        /// </summary>
        public const string KEY_IMPERSONATE_SYSTEM = "EleflexImpersonateSystem";

        /// <summary>
        /// Key used to denote admin impersonation.
        /// </summary>
        public const string KEY_IMPERSONATE_SYSTEM_TOKEN = "EleflexImpersonateSystemToken";
        


        /// <summary>
        /// Thread-local storage (if no httpcontext available)
        /// </summary>
        private static readonly ThreadLocal<int> _impersonateAdmin = new ThreadLocal<int>(() => 0);
        /// <summary>
        /// Thread-local storage (if no httpcontext available)
        /// </summary>
        private static readonly ThreadLocal<string> _impersonateAdminToken = new ThreadLocal<string>(() => null);


        public ImpersonateSystem() : this(System.Configuration.ConfigurationManager.AppSettings[ImpersonateSystem.KEY_IMPERSONATE_SYSTEM_TOKEN]) { }
        /// <summary>
        /// Constructor.
        /// </summary>
        public ImpersonateSystem(string token)
        {
            if(HttpContext.Current!=null)
            {
                if (!HttpContext.Current.Items.Contains(KEY_IMPERSONATE_SYSTEM))
                {
                    HttpContext.Current.Items.Add(KEY_IMPERSONATE_SYSTEM, 1);
                }
                else
                {
                    int val = (int)HttpContext.Current.Items[KEY_IMPERSONATE_SYSTEM];
                    val++;
                    HttpContext.Current.Items[KEY_IMPERSONATE_SYSTEM] = val;
                }

                if (!HttpContext.Current.Items.Contains(KEY_IMPERSONATE_SYSTEM_TOKEN))
                {
                    HttpContext.Current.Items.Add(KEY_IMPERSONATE_SYSTEM_TOKEN, token);
                }
                else
                {
                    HttpContext.Current.Items[KEY_IMPERSONATE_SYSTEM_TOKEN] = token;
                }
            }
            else
            {
                _impersonateAdmin.Value = _impersonateAdmin.Value + 1;
                _impersonateAdminToken.Value = token;
            }
        }

        /// <summary>
        /// Disposal
        /// </summary>
        public void Dispose()
        {
            if (HttpContext.Current != null)
            {
                if (HttpContext.Current.Items.Contains(KEY_IMPERSONATE_SYSTEM))
                {
                    int val = (int)HttpContext.Current.Items[KEY_IMPERSONATE_SYSTEM];
                    val--;
                    HttpContext.Current.Items[KEY_IMPERSONATE_SYSTEM] = val;
                }
            }
            else
            {
                _impersonateAdmin.Value = _impersonateAdmin.Value - 1;
            }
            
        }


        /// <summary>
        /// Determine if impersonating
        /// </summary>
        public static bool IsImpersonateSystem
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items.Contains(KEY_IMPERSONATE_SYSTEM))
                    {
                        int val = (int)HttpContext.Current.Items[KEY_IMPERSONATE_SYSTEM];
                        return val > 0;
                    }
                    return false;
                }
                else
                {
                    return _impersonateAdmin.Value > 0;
                }
            }
        }

        public static string GetToken
        {
            get
            {
                if(HttpContext.Current != null)
                {
                    if (HttpContext.Current.Items.Contains(KEY_IMPERSONATE_SYSTEM_TOKEN))
                        return HttpContext.Current.Items[KEY_IMPERSONATE_SYSTEM_TOKEN] as string;
                    return null;
                }
                else
                {
                    return _impersonateAdminToken.Value;
                }
            }
        }

    }
}

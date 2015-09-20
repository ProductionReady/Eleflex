using System;
using System.Web;
using System.Threading;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// This class temporarily set system admin impersonation for service calls
    /// </summary>
    public partial class ImpersonateSystem : IDisposable
    {

        /// <summary>
        /// Key used to denote admin impersonation.
        /// </summary>
        public const string KEY_IMPERSONATE_SYSTEM = "EleflexImpersonateSystem";

        /// <summary>
        /// Key used to denote admin impersonation using a token.
        /// </summary>
        public const string KEY_IMPERSONATE_SYSTEM_TOKEN = "EleflexImpersonateSystemToken";
        


        /// <summary>
        /// Thread-local storage (if no httpcontext available)
        /// </summary>
        protected static readonly ThreadLocal<int> _impersonateAdmin = new ThreadLocal<int>(() => 0);
        /// <summary>
        /// Thread-local storage (if no httpcontext available)
        /// </summary>
        protected static readonly ThreadLocal<string> _impersonateAdminToken = new ThreadLocal<string>(() => null);

        /// <summary>
        /// Constructor.
        /// </summary>
        public ImpersonateSystem() : this(System.Configuration.ConfigurationManager.AppSettings[ImpersonateSystem.KEY_IMPERSONATE_SYSTEM_TOKEN]) { }
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="token"></param>
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
        /// Disposal.
        /// </summary>
        public virtual void Dispose()
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

        /// <summary>
        /// Get the token.
        /// </summary>
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

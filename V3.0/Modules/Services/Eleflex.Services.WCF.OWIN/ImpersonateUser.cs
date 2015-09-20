using System;
using System.Web;
using System.Threading;

namespace Eleflex.Services.WCF.OWIN
{
    /// <summary>
    /// This class temporarily set user impersonation for service calls
    /// </summary>
    public partial class ImpersonateUser : IDisposable
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
        protected static readonly ThreadLocal<string> _impersonateUsername = new ThreadLocal<string>(() => null);
        /// <summary>
        /// Thread-local storage (if no httpcontext available)
        /// </summary>
        protected static readonly ThreadLocal<string> _impersonatePassword = new ThreadLocal<string>(() => null);
        

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
        /// Disposal.
        /// </summary>
        public virtual void Dispose()
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

        /// <summary>
        /// Get the user name.
        /// </summary>
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

        /// <summary>
        /// Get the password.
        /// </summary>
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

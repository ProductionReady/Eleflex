using System;

namespace Eleflex
{
    /// <summary>
    /// Static class containing constants for the versioning module.
    /// </summary>
    public static partial class SecurityConstants
    {

        /// <summary>
        /// RoleKey for a user.
        /// </summary>
        public static Guid ROLE_USERKEY = new Guid("0C3623CB-5643-4FCD-8B8C-949D66C51AF2");
        /// <summary>
        /// Role for a user.
        /// </summary>
        public const string ROLE_USER = "User";
        /// <summary>
        /// RoleKey for an admin.
        /// </summary>
        public static Guid ROLE_ADMINKEY = new Guid("202BBB57-1411-4FC5-BE1A-832520AB78E3");
        /// <summary>
        /// Role for admin.
        /// </summary>
        public const string ROLE_ADMIN = "Admin";
        /// <summary>
        /// UserKey for the system admin account.
        /// </summary>
        public static Guid USERKEY_SYSTEM_ADMIN = new Guid("00000000-0000-0000-0000-000000000000");       

    }
}


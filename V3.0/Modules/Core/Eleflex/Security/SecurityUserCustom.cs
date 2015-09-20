using System;
using System.Collections.Generic;

namespace Eleflex
{
    /// <summary>
    /// Represents an object containg user information.
    /// </summary>
    public partial class SecurityUser
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public SecurityUser()
        {
            SecurityUserKey = Guid.NewGuid();
            Active = true;
            CreateDate = DateTime.UtcNow;
            PasswordLastChangeDate = CreateDate.Value;
        }
    }
}
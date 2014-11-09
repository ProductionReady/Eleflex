using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Security.Model
{
    /// <summary>
    /// Service exposing security related information.
    /// </summary>
    public interface ISecurityService
    {

        User GetUser(string usernameOrEmail);

        UserDetail GetUserDetail(Guid userKey);

        void LockoutUser(Guid userKey, DateTimeOffset? reinstateDate);

        void ResetPasswordByUsername(string username);
        
        void ResetPasswordByEmail(string username);

        void DeactivateUser(Guid userKey);

        void ChangePassword(Guid userKey, string oldPassword, string newPassword);

    }
}

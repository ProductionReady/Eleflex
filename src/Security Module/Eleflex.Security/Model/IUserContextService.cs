using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eleflex.Security.Model
{
    /// <summary>
    /// Interace providing the current user's information.
    /// </summary>
    public interface IUserContextService
    {
        /// <summary>
        /// Get the current user's detailed information.
        /// </summary>
        /// <returns></returns>
        UserDetail GetUserDetail();

    }
}

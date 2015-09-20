using ServiceModel = Eleflex;

namespace Eleflex.Security.Web.Admin.Models.Users
{
    public class UserRoleViewModel : ServiceModel.SecurityUserRole
    {

        public string RoleName { get; set; }
    }
}
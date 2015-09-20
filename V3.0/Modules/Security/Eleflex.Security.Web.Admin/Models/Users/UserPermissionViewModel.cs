using ServiceModel = Eleflex;

namespace Eleflex.Security.Web.Admin.Models.Users
{
    public class UserPermissionViewModel : ServiceModel.SecurityUserPermission
    {

        public string PermissionName { get; set; }
    }
}
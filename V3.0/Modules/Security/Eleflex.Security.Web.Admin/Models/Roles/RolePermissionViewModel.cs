using ServiceModel = Eleflex;

namespace Eleflex.Security.Web.Admin.Models.Roles
{
    public class RolePermissionViewModel : ServiceModel.SecurityRolePermission
    {

        public string PermissionName { get; set; }
    }
}
using AutoMapper;
using ServiceModel = Eleflex;
using WebModel = Eleflex.Security.Web.Admin.Models;
using Eleflex.Security.ASPNetIdentity;

namespace Eleflex.Security.Web.Admin
{
    [MappingRegistrationTask]
    public partial class AutoMapperRegistrationTask : RegistrationTask
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public AutoMapperRegistrationTask()
        {
            Description = "This task registers all mapping between the web model with the service and identity models.";
        }

        public override bool Register(ITaskOptions taskOptions)
        {


            //Users
            Mapper.CreateMap<WebModel.Users.EditViewModel, IdentityUser>();
            Mapper.CreateMap<IdentityUser, WebModel.Users.EditViewModel>();

            Mapper.CreateMap<WebModel.Users.EditViewModel, IdentityUser>();
            Mapper.CreateMap<IdentityUser, WebModel.Users.EditViewModel>();

            Mapper.CreateMap<WebModel.Users.UserRoleViewModel, ServiceModel.SecurityUserRole>();
            Mapper.CreateMap<ServiceModel.SecurityUserRole, WebModel.Users.UserRoleViewModel>();

            Mapper.CreateMap<WebModel.Users.EditRoleViewModel, ServiceModel.SecurityUserRole>();
            Mapper.CreateMap<ServiceModel.SecurityUserRole, WebModel.Users.EditRoleViewModel>();

            Mapper.CreateMap<WebModel.Roles.EditViewModel, IdentityRole>();
            Mapper.CreateMap<IdentityRole, WebModel.Roles.EditViewModel>();

            Mapper.CreateMap<WebModel.Roles.EditViewModel, IdentityRole>();
            Mapper.CreateMap<IdentityRole, WebModel.Roles.EditViewModel>();

            Mapper.CreateMap<WebModel.Users.UserPermissionViewModel, ServiceModel.SecurityUserPermission>();
            Mapper.CreateMap<ServiceModel.SecurityUserPermission, WebModel.Users.UserPermissionViewModel>();

            Mapper.CreateMap<WebModel.Users.EditPermissionViewModel, ServiceModel.SecurityUserPermission>();
            Mapper.CreateMap<ServiceModel.SecurityUserPermission, WebModel.Users.EditPermissionViewModel>();

            Mapper.CreateMap<WebModel.Users.UserClaimViewModel, ServiceModel.SecurityUserClaim>();
            Mapper.CreateMap<ServiceModel.SecurityUserClaim, WebModel.Users.UserClaimViewModel>();

            Mapper.CreateMap<WebModel.Users.EditClaimViewModel, ServiceModel.SecurityUserClaim>();
            Mapper.CreateMap<ServiceModel.SecurityUserClaim, WebModel.Users.EditClaimViewModel>();


            //Roles
            Mapper.CreateMap<WebModel.Roles.RoleRoleViewModel, ServiceModel.SecurityRoleRole>();
            Mapper.CreateMap<ServiceModel.SecurityRoleRole, WebModel.Roles.RoleRoleViewModel>();

            Mapper.CreateMap<WebModel.Roles.EditRoleViewModel, ServiceModel.SecurityRoleRole>();
            Mapper.CreateMap<ServiceModel.SecurityRoleRole, WebModel.Roles.EditRoleViewModel>();

            Mapper.CreateMap<WebModel.Roles.EditViewModel, IdentityRole>();
            Mapper.CreateMap<IdentityRole, WebModel.Roles.EditViewModel>();

            Mapper.CreateMap<WebModel.Roles.EditViewModel, IdentityRole>();
            Mapper.CreateMap<IdentityRole, WebModel.Roles.EditViewModel>();

            Mapper.CreateMap<WebModel.Roles.RolePermissionViewModel, ServiceModel.SecurityRolePermission>();
            Mapper.CreateMap<ServiceModel.SecurityRolePermission, WebModel.Roles.RolePermissionViewModel>();

            Mapper.CreateMap<WebModel.Roles.EditPermissionViewModel, ServiceModel.SecurityRolePermission>();
            Mapper.CreateMap<ServiceModel.SecurityRolePermission, WebModel.Roles.EditPermissionViewModel>();

            return base.Register(taskOptions);
        }
    }
}

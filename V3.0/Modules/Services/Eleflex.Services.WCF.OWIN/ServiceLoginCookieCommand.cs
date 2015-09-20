//using System.Web;
//using Microsoft.AspNet.Identity.Owin;

//namespace Eleflex.Services.WCF.OWIN
//{
//    /// <summary>
//    /// Service command to login a user and return a cookie response.
//    /// </summary>
//    [WCFCommandRegistrationTask(typeof(ServiceLoginCookieLogin))]
//    public class ServiceLoginCookieLogin : WCFCommand<ServiceLoginCookieRequest, ServiceLoginCookieResponse>
//    {

//        protected readonly IUserRepository _userRepository;

//        /// <summary>
//        /// Constructor.
//        /// </summary>
//        /// <param name="userRepository"></param>
//        public ServiceLoginCookieLogin(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        /// <summary>
//        /// Execute.
//        /// </summary>
//        /// <param name="request"></param>
//        /// <param name="response"></param>
//        public override void Execute(ServiceLoginCookieRequest request, ServiceLoginCookieResponse response)
//        {
//            ApplicationSignInManager signinManager = HttpContext.Current.GetOwinContext().Get<IdentitySignInManager>();
//            SignInStatus status = signinManager.PasswordSignIn(request.Username, request.Password, false, true);
//            switch (status)
//            {
//                case SignInStatus.Failure:
//                    response.ResponseStatus.AddError(Services.ServicesConstants.ERROR_SYSTEM_SECURITY, Services.ServicesConstants.ERROR_SYSTEM_SECURITY_CODE);
//                    break;
//                case SignInStatus.LockedOut:
//                    response.ResponseStatus.AddError(Services.ServicesConstants.ERROR_SYSTEM_SECURITY, Services.ServicesConstants.ERROR_SYSTEM_SECURITY_CODE);
//                    break;
//                case SignInStatus.RequiresVerification:
//                    response.ResponseStatus.AddError(Services.ServicesConstants.ERROR_SYSTEM_SECURITY, Services.ServicesConstants.ERROR_SYSTEM_SECURITY_CODE);
//                    break;
//                case SignInStatus.Success:
//                    response.Item = HttpContext.Current.Response.Headers["Set-Cookie"];
//                    break;
//            }

//        }
//    }
//}

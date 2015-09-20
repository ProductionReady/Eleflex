using System;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Extensions;
using Eleflex.Security.ASPNetIdentity;
using Eleflex.Services.WCF.OWIN;
using Eleflex;

[assembly: OwinStartupAttribute(typeof(WebServer.App_Start.Eleflex_Start.OWINStartup))]
namespace WebServer.App_Start.Eleflex_Start
{
    /// <summary>
    /// Startup class defineing OWIN security configurations.
    /// </summary>
    public class OWINStartup
    {

        /// <summary>
        /// Configure security.
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            //Configure methods for OWIN to construct needed objects
            app.CreatePerOwinContext<IdentityUserManager>(OWINStartup.CreateUserManager);
            app.CreatePerOwinContext<IdentitySignInManager>(OWINStartup.CreateSignInManager);
            app.CreatePerOwinContext<IdentityRoleManager>(OWINStartup.CreateRoleManager);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<IdentityUserManager, IdentityUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => manager.GenerateUserIdentityAsync(user))
                }
            });

            //Configure ASP.Net Identity to run part of the IIS pipeline (used for WCF service authentication/authorization)
            app.UseStageMarker(PipelineStage.Authenticate);

            //Use external cookie
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

        /// <summary>
        /// OWIN calls this method to create a signin manager.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IdentitySignInManager CreateSignInManager(IdentityFactoryOptions<IdentitySignInManager> options, IOwinContext context)
        {
            return new IdentitySignInManager(context.GetUserManager<IdentityUserManager>(), context.Authentication);
        }

        /// <summary>
        /// OWIN calls this method to create a user manager.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IdentityUserManager CreateUserManager(IdentityFactoryOptions<IdentityUserManager> options, IOwinContext context)
        {
            var userStore = ObjectLocator.Current.GetInstance<IUserStore<IdentityUser>>();
            var manager = new IdentityUserManager(userStore);

            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<IdentityUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            //manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<IdentityUser>
            //{
            //    MessageFormat = "Your security code is {0}"
            //});
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<IdentityUser>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = ObjectLocator.Current.GetInstance<IEmailIdentityMessageService>();
            manager.SmsService = new NoFunctionalityIdentityMessageService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<IdentityUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    TokenLifespan = TimeSpan.FromHours(3)
                };
            }
            return manager;
        }

        /// <summary>
        /// OWIN calls this method to create a role manager.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static IdentityRoleManager CreateRoleManager(IdentityFactoryOptions<IdentityRoleManager> options, IOwinContext context)
        {
            var roleStore = ObjectLocator.Current.GetInstance<IRoleStore<IdentityRole>>();
            var manager = new IdentityRoleManager(roleStore);
            return manager;
        }

    }
}
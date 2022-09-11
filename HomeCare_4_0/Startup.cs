using Microsoft.Owin;
using Owin;
using System;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;

[assembly: OwinStartupAttribute(typeof(HomeCare_4_0.Startup))]
namespace HomeCare_4_0
{
    public partial class Startup
    {
        string clientId = System.Configuration.ConfigurationManager.AppSettings["ClientId"];
        string redirectUri = System.Configuration.ConfigurationManager.AppSettings["RedirectUri"];
        static string tenant = System.Configuration.ConfigurationManager.AppSettings["Tenant"];
        // Authority is the URL for authority, composed by Microsoft identity platform endpoint and the tenant name (e.g. https://login.microsoftonline.com/contoso.onmicrosoft.com/v2.0)
        string authority = String.Format(System.Globalization.CultureInfo.InvariantCulture, System.Configuration.ConfigurationManager.AppSettings["Authority"], tenant);
        public void Configuration(IAppBuilder app)
        {
            // ConfigureAuth(app);
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                SlidingExpiration = true,
                CookieName = "AuthenAD",
                ExpireTimeSpan = TimeSpan.FromHours(6),

            });          

            app.UseOpenIdConnectAuthentication(
            new OpenIdConnectAuthenticationOptions
            {
                // Sets the ClientId, authority, RedirectUri as obtained from web.config
                ClientId = clientId,
                Authority = authority,
                RedirectUri = redirectUri,
                PostLogoutRedirectUri = redirectUri,
                Scope = OpenIdConnectScope.OpenIdProfile,
                ResponseType = OpenIdConnectResponseType.IdToken,
                TokenValidationParameters = new TokenValidationParameters()
                {
                    NameClaimType = "access_token",
                    SaveSigninToken = true //important to save the token in boostrapcontext                     
                },
                // OpenIdConnectAuthenticationNotifications configures OWIN to send notification of failed authentications to OnAuthenticationFailed method
                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthorizationCodeReceived = (context) =>
                    {
                        context.AuthenticationTicket.Properties.ExpiresUtc = DateTime.UtcNow.AddHours(6);
                        context.AuthenticationTicket.Properties.AllowRefresh = true;
                        context.AuthenticationTicket.Properties.IsPersistent = true;
                        return Task.FromResult(0);
                    },
                    AuthenticationFailed = OnAuthenticationFailed
                }
            });

            app.Use((context, next) =>
            {
                var httpContext = context.Get<System.Web.HttpContextBase>(typeof(HttpContextBase).FullName);
                httpContext.SetSessionStateBehavior(SessionStateBehavior.Required);
                return next();
            });

            // To make sure the above `Use` is in the correct position:
            app.UseStageMarker(PipelineStage.MapHandler);
        }

        private Task OnAuthenticationFailed(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> context)
        {
            context.HandleResponse();
            context.Response.Redirect("/?errormessage=" + context.Exception.Message);
            return Task.FromResult(0);
        }
    }
}

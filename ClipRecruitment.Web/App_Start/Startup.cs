using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity;
using Owin;
using System;
using ClipRecruitment.Web.Providers;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Cors;
using System.Web.Cors;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(ClipRecruitment.Web.App_Start.Startup))]
namespace ClipRecruitment.Web.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
                Provider = new OAuthBearerAuthenticationProvider()
                {
                    OnRequestToken = context =>
                    {
                        if (context.Request.Path.Value.Contains("signalr"))
                        {
                            var token = context.Request.Query.Get("Bearer_Token");
                            if (!string.IsNullOrEmpty(token))
                            {
                                context.Token = token;
                            }
                        }
                        return Task.FromResult(context);
                    }
                }
            });


            app.UseCors(CorsOptions.AllowAll);            
            app.MapSignalR(new HubConfiguration { EnableJSONP = true});
            app.CreatePerOwinContext(ApplicationIdentityContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider("self"),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            });

                            
        }
    }

    public static class Pagination
    {
        public static int Size = 10;

    }
}
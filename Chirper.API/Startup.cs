using Chirper.API.Infrastructure;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Chirper.API.Startup))]
namespace Chirper.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            ConfigureOAuth(app);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            // configure authentication
            var authenticationOptions = new OAuthBearerAuthenticationOptions();
            app.UseOAuthBearerAuthentication(authenticationOptions);

            //configure authorization
            var authorizationOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new ChirperAuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(authorizationOptions);
        }
    }
}
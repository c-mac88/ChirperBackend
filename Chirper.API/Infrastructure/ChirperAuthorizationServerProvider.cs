using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security;

namespace Chirper.API.Infrastructure
{
    public class ChirperAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            using (var authRepository = new AuthorizationRepository())
            {
                var user = await authRepository.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "username or password is incorrect");
                }
                else
                {
                    var props = new AuthenticationProperties(new Dictionary<string, string>
                    {
                            { "username", user.DisplayName },
                            { "firstname", user.FirstName },
                            { "lastname", user.LastName }
                    });
                    var token = new ClaimsIdentity(context.Options.AuthenticationType);
                    token.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                    token.AddClaim(new Claim(ClaimTypes.Role, "user"));
                    var ticket = new AuthenticationTicket(token, props);
                    context.Validated(ticket);
                }
            }
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }
    }
}
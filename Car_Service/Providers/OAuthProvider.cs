using Car_Service.BLL.Interfaces;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Car_Service.Providers
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        private IUserService _db;
        public OAuthProvider(IUserService db)
        {
            _db = db;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            var claims = await _db.Authenticate(context.UserName, context.Password);
            if (claims != null)
            {
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "role", claims.Claims.FirstOrDefault(s=>s.Type==ClaimTypes.Role).Value
                        
                    },

                });
                context.Validated(new AuthenticationTicket(claims, props));
            }
            else
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }
    }
}
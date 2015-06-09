using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MPERP2015.MP
{
    public class MPTokenAuthorizationProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            //if (context.UserName != "test" )
            if (isValidUser(context.UserName, context.Password))
                {
                    context.SetError("invalid_grant", string.Format( "驗證失敗({0})", this.GetType().Name));
                    return;
                }
            
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }

        bool isValidUser(string userName, string password)
        {
            bool result = false;

            MembershipModelContainer db = new MembershipModelContainer();
            var user = db.Users.Find(userName);
            if ((user != null) && (user.Password == password))
            {
                result = true;
            }
            return result;
        }
    }
}
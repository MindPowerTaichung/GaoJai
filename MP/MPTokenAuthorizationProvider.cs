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

            User user;
            if (!ValidUser(context.UserName, context.Password,out user))
                {
                    context.SetError("invalid_grant", string.Format( "驗證失敗({0})", this.GetType().Name));
                    return;
                }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", user.UserName));
            identity.AddClaim(new Claim("roleId", user.Role_Id==null ? "":user.Role_Id.ToString()));

            context.Validated(identity);

        }

        bool ValidUser(string userName, string password, out User user)
        {
            bool result = false;

            MembershipModelContainer db = new MembershipModelContainer();
            user = db.Users.Find(userName);
            if ((user != null) && (user.Password == password))
            {
                result = true;
            }
            return result;
        }
    }
}
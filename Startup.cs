using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using Microsoft.Owin.Infrastructure;
using System.Web.Http;
using MPERP2015.MP;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;

[assembly: OwinStartup(typeof(MPERP2015.Startup))]
namespace MPERP2015
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //var oauthProvider = new OAuthAuthorizationServerProvider
            //{
            //    OnGrantResourceOwnerCredentials = async context =>
            //    {
            //        //if (context.UserName == "test" && context.Password == "123")
            //        if (isValidUser(context.UserName,context.Password))
            //        {

            //            var claimsIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            //            claimsIdentity.AddClaim(new Claim("user", context.UserName));
            //            context.Validated(claimsIdentity);
            //            return;

            //        }
            //        context.Rejected();
            //    },
            //    OnValidateClientAuthentication = async context =>
            //    {
            //        context.Validated();
            //    }                 
            //};
            var oauthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1), //登入後閒置1Day,Token逾期
                Provider =new MPTokenAuthorizationProvider() //oauthProvider             
            };
            app.UseOAuthAuthorizationServer(oauthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

        
    }  
}

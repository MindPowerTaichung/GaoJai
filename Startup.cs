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
            var oauthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30), //登入後閒置30min,Token逾期
                Provider =new MPTokenAuthorizationProvider() //oauthProvider             
            };
            app.UseOAuthAuthorizationServer(oauthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }

        
    }  
}

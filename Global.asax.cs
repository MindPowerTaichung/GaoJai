using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace MPERP2015
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //RegisterRoutes(RouteTable.Routes);
        }

        //public static void RegisterRoutes(RouteCollection routes)
        //{
        //    routes.MapPageRoute("首頁",
        //                                    "Default.aspx",
        //                                    "~/View_KendoUI/login.aspx");
        //}

    }
}

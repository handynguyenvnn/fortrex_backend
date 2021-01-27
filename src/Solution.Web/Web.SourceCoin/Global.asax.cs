using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Web.SourceCoin
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly string syncConnect = ConfigurationManager.ConnectionStrings["Web.SourceCoin.Sync"].ConnectionString;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IocConfigurator.ConfiguratorIocUnityContainer();

            //SqlDependency.Start(syncConnect);
        }
        //Added code
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        //    {
        //        HttpContext.Current.Response.Flush();
        //    }
        //}
        protected void Application_End()
        {
            //SqlDependency.Stop(syncConnect);
        }

        protected void LogException(Exception exc)
        {
            //SqlDependency.Stop(syncConnect);
            // throw exc;
            // HttpContext.Current.Response.Redirect("~/");
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            //SqlDependency.Stop(syncConnect);
            // HttpContext.Current.Response.Redirect("~/");
        }
    }
}

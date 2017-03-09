using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ViedaSlimnicaProject.Controllers;

namespace ViedaSlimnicaProject
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AreaRegistration.RegisterAllAreas();
            RouteConfig2.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig2.RegisterBundles(BundleTable.Bundles);
            var configuration = new Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
            //    SDatabase.SetInitializer<SmartHospitalDatabaseContext>(null);
        }

        protected void Application_Error()
        {
            Exception exeception = Server.GetLastError();
            Response.Clear();
            HttpException httpexeception = exeception as HttpException;
            RouteData route = new RouteData();

            route.Values.Add("controller","Error");
            if (httpexeception != null)
            {
                switch (httpexeception.GetHttpCode())
                {
                    case 404:
                        route.Values.Add("action","Http404");
                        break;
                    case 500:
                        route.Values.Add("action", "Http500");
                        break;
                    default:
                        route.Values.Add("action", "General");
                        break;
                }
                Server.ClearError();
                Response.TrySkipIisCustomErrors = true;
                Response.Headers.Add("Content-Type", "text/html");
            }
            IController errorcontroller = new ErrorController();
            errorcontroller.Execute(new RequestContext(new HttpContextWrapper(Context), route));
        }
    }
}
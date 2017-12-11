using DTcms.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;

namespace DTcms.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BLL.dt_area_code bll = new BLL.dt_area_code();
            ManagePage.arealist = bll.GetModelList(" parent_code='330300' order by code asc ");
        }
        public override void Init()
        {
            this.PostAuthenticateRequest += (sender,e) => HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            base.Init();
        }
        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
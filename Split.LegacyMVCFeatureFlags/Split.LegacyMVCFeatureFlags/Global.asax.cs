using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Split.LegacyMVCFeatureFlags.Services;
using Splitio.Services.Client.Classes;
using Splitio.Services.Client.Interfaces;

namespace Split.LegacyMVCFeatureFlags
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var builder = new ContainerBuilder();

            builder.RegisterInstance(new SplitFactory("Your Api Key")).As<ISplitFactory>();
            builder.RegisterType<SplitFeatureService>();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}

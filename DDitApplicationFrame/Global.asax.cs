using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using System.Reflection;
using DDit.Core.Data.IRepositories;
using DDit.Core.Data.Repositories;
using DDitApplicationFrame.Common;
using DDitApplicationFrame.Service;
using DDit.Core.Data;
using StackExchange.Profiling;
using DDit.Component.Tools;



namespace DDitApplicationFrame
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {

            StackExchange.Profiling.EntityFramework6.MiniProfilerEF6.Initialize();
            var builder = new ContainerBuilder();

            SetupResolveRules(builder);
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            var container = builder.Build();

            BaseController._container = container;

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            log4net.Config.XmlConfigurator.Configure();

            ModelBinders.Binders.Clear();
            ModelBinders.Binders.Add(typeof(PropertyModelBinder), new PropertyModelBinder());

            HttpConfiguration config = GlobalConfiguration.Configuration;

            config.Formatters.JsonFormatter
                        .SerializerSettings
                        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        }

        protected void Application_BeginRequest()
        {
            if (Request.IsLocal)//这里是允许本地访问启动监控,可不写
            {
                MiniProfiler.Start();
            }
        }

        protected void Application_EndRequest()
        {
            MiniProfiler.Stop();
        }
        
        private void SetupResolveRules(ContainerBuilder builder)
        {
            var Inter = new List<Type>();
            Assembly assembly = Assembly.Load("DDit.Core.Data");
            foreach (Type aaa in assembly.GetTypes())
            {
                if (aaa.Name.StartsWith("I") && aaa.Name.EndsWith("Repository"))
                {
                    Inter.Add(aaa);
                }
            }

            var entity = new List<Type>();
            Assembly entassembly = Assembly.Load("DDit.Core.Data.Repository");
            foreach (Type aaa in entassembly.GetTypes())
            {
                if (aaa.Name.EndsWith("Repository"))
                {
                    entity.Add(aaa);
                }
            }

            foreach (var item in entity)
            {
                Inter.ForEach(a =>
                {
                    if (a.Name.Contains(item.Name)) {
                        builder.RegisterType(item).As(a);
                    }
                });    
            }

        }

    }
}
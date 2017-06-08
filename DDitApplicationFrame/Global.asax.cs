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
using DDitApplicationFrame.Service.Imp;
using DDit.Core.Data.Repository;
using System.Data.Entity;
using System.Web.Compilation;



namespace DDitApplicationFrame
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //开启MiniProfilerEF6监控 
            //StackExchange.Profiling.EntityFramework6.MiniProfilerEF6.Initialize();

            #region autofac IOC容器配置
            var builder = new ContainerBuilder();

            //注册所有的controller
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            //注册所有模块module
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            var assemblys = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();

            //注册所有继承IDependency接口的类
            builder.RegisterAssemblyTypes(assemblys)
            .Where(type => typeof(IDependency).IsAssignableFrom(type) && !type.IsAbstract);

            //注册服务，所有IxxxxRepository=>xxxxRepository
            builder.RegisterAssemblyTypes(assemblys).Where(t => t.Name.EndsWith("Repository") && !t.Name.StartsWith("I")).AsImplementedInterfaces();
          
            var container = builder.Build();

            BaseInfo._container = container;

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            #endregion

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
        
    }
}
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
using DDitApplicationFrame.Common;
using DDitApplicationFrame.Service;
using DDit.Core.Data;
using StackExchange.Profiling;
using DDit.Component.Tools;
using DDitApplicationFrame.Service.Imp;
using DDit.Core.Data.Repository;
using System.Data.Entity;
using System.Web.Compilation;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Net.Http;



namespace DDitApplicationFrame
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //开启MiniProfilerEF6监控，需要时取消注释
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

            //log4日志组件注册
            log4net.Config.XmlConfigurator.Configure();

            //注册自定义的模型绑定
            ModelBinders.Binders.Clear();
            ModelBinders.Binders.Add(typeof(PropertyModelBinder), new PropertyModelBinder());

            //.net 内置json序列化的时候阻止深循环
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter
                        .SerializerSettings
                        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
           
           // EF预热，手动在内存中加载mapping view,调试如果觉得启动慢可以注释这段代码，发布程序很有用

            using (var unitofwork = container.Resolve<UnitOfWork>())
            {
                var objectContext = ((IObjectContextAdapter)unitofwork.context).ObjectContext;
                var mappingCollection = (StorageMappingItemCollection)objectContext.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
                mappingCollection.GenerateViews(new List<EdmSchemaError>());
            }
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
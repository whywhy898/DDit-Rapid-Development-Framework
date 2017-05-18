using Autofac;
using DDit.Core.Data.IRepositories;
using DDit.Core.Data.Repositories;
using DDitApplicationFrame.Service;
using DDitApplicationFrame.Service.Imp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDitApplicationFrame.Common
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoginService>().As<ILoginService>().PropertiesAutowired();

            builder.RegisterType<UserService>().As<IUserService>().PropertiesAutowired();

        }
    }
}
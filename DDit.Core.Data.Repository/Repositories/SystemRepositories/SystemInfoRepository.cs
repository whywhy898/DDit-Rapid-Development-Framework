using DDit.Core.Data.IRepositories;
using DDit.Core.Data.SystemEntity.Entity;
using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDit.Component.Tools;

namespace DDit.Core.Data.Repository.Repositories
{
    class SystemInfoRepository : ISystemInfoRepository
    {

        public SystemInfo GetSystemInfo()
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                return dal.GetRepository<SystemInfo>().Get().FirstOrDefault();
           }
        }
    }
}

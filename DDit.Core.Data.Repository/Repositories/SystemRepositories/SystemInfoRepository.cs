using DDit.Core.Data.IRepositories;
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Repositories
{
    class SystemInfoRepository : ISystemInfoRepository
    {

        public SystemInfo GetSystemInfo()
        {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {

                return dal.GetRepository<SystemInfo>().Get().FirstOrDefault();

           }
        }
    }
}

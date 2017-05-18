using DDit.Core.Data.IRepositories.ISystemRepositories;
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace DDit.Core.Data.Repository.Repositories.SystemRepositories
{
    class TestRepository : ITestRepository
    {
        public void Delete()
        {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext())) {
                dal.GetRepository<Test>().Get().Delete();
                dal.Save();
            }
        }

        public Test GetSingle(int ID)
        {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {
                return dal.GetRepository<Test>().Get(a => a.ID == ID, includeProperties: "Account").FirstOrDefault<Test>();
            }
        }
    }
}

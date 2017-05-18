using DDit.Core.Data.IRepositories;
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Repositories
{
    class RoleAndBtnRepository : IRoleAndBtnRepository
    {

        public void AddRB(List<RoleMappingButton> modelList)
        {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {
                dal.GetRepository<RoleMappingButton>().Insert(modelList);
                dal.Save();
            }
        }
    }
}

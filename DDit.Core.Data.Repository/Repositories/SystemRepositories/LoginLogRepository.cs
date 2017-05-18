using DDit.Core.Data.IRepositories;
using DDit.Core.Data.SystemEntity.Entity;
using DDit.Component.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DDit.Core.Data.Repository.Repositories
{
    class LoginLogRepository : ILoginLogRepository
    {

        public void AddLoginlog(LoginLog model)
        {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {
                dal.GetRepository<LoginLog>().Insert(model);
                dal.Save();
            }
        }

        public Tuple<int, List<LoginLog>> GetLoginlog(LoginLog model)
        {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {
                var loginRepository = dal.GetRepository<LoginLog>();
                var conditions = ExpandHelper.True<LoginLog>();
                if (!string.IsNullOrEmpty(model.LoginName))
                    conditions = conditions.And(a => a.LoginName.Contains(model.LoginName));

                if (!string.IsNullOrEmpty(model.LoginNicker))
                    conditions = conditions.And(a => a.LoginNicker.Contains(model.LoginNicker));

                var logmodel = loginRepository.Get(filter: conditions);

                var count = logmodel.Count();

                if (model.order != null && model.order.Count() > 0)
                {
                    foreach (var item in model.order)
                    {
                        var column = model.columns.ElementAt(int.Parse(item.column));
                        logmodel = logmodel.OrderSort(column.data, item.dir);
                    }
                }

                var result = logmodel.PageBy(model.pageIndex,model.pageSize).ToList();

                return new Tuple<int, List<LoginLog>>(count, result);
            }
        }
    }
}

using DDit.Core.Data.IRepositories.ISystemRepositories;
using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using Autofac;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDit.Component.Tools;

namespace DDit.Core.Data.Repository.Repositories.SystemRepositories
{
    class DictionaryRepository : IDictionaryRepository
    {

        public Tuple<int, List<Dictionary>> GetDictionaryList(Dictionary model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {

                var dictionaryRepository = dal.GetRepository<Dictionary>();
                var templist = dictionaryRepository.Get(filter: a => a.DicCategoryID == model.DicCategoryID, includeProperties: "DicCategory");

                var count = templist.Count();

                if (model.order != null && model.order.Count() > 0)
                {
                    foreach (var item in model.order)
                    {
                        var column = model.columns.ElementAt(int.Parse(item.column));
                        templist = templist.OrderSort(column.data, item.dir);
                    }
                }

                var result = templist.PageBy(model.pageIndex,model.pageSize).ToList();

                return new Tuple<int, List<Dictionary>>(count, result);
            }
        }

        public void AddDic(Dictionary model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<Dictionary>().Insert(model);
                dal.Save();
            }
        }

        public void DisabledDic(int btnID)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var dictionaryRepository = dal.GetRepository<Dictionary>();
                var dicentitly = dictionaryRepository.GetByID(btnID);
                dicentitly.Enabled = false;
                dictionaryRepository.Update(dicentitly);
                dal.Save();
            }
        }

        public void ModifyDic(Dictionary model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<Dictionary>().UpdateSup(model, new List<string>() { "CreateTime" }, false);

                dal.Save();
            }
        }
    }
}

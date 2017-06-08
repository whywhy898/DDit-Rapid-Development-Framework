using DDit.Core.Data.Entity;
using DDit.Component.Tools;
using DDit.Core.Data.IRepositories;
using DDit.Core.Data.SystemEntity.Entity;
using System;
using Autofac;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Repositories
{
    class ButtonRepository : IButtonRepository
    {

        public Tuple<int, List<Button>> GetButtonList(Button model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var buttonRepository = dal.GetRepository<Button>();
                var conditions = ExpandHelper.True<Button>();
                if (!string.IsNullOrEmpty(model.ButtonName))
                {
                    conditions = conditions.And(a => a.ButtonName.Contains(model.ButtonName));
                }
                var templist = buttonRepository.Get(filter: conditions);

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

                return new Tuple<int, List<Button>>(count, result);
            }
        }

        public void AddBtn(Button model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<Button>().Insert(model);
                dal.Save();
            }
        }

        public void DeleteBtn(int btnID)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<Button>().Delete(btnID);
                dal.Save();
            }
        }

        public void ModifyBtn(Button model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<Button>().UpdateSup(model, new List<string>() { "CreateTime" }, false);
                dal.Save();
            }
        }

        public Button GetSingleBtnbyID(int id)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                return dal.GetRepository<Button>().GetByID(id);
            }
        }

        public List<Button> GetButtonList()
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                return dal.GetRepository<Button>().Get().ToList();
            }
        }

    }
}

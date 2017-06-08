using DDit.Core.Data.Entity;
using DDit.Core.Data.IRepositories;
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using Autofac;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDit.Component.Tools;

namespace DDit.Core.Data.Repository.Repositories
{
    class MenuAndBtnRepository : IMenuAndBtnRepository
    {

        public void MenuMapBtn(int MenuID,List<MenuMappingButton> mplist)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
               var menuRepository=dal.GetRepository<Menu>();
               var menuBtnRepository = dal.GetRepository<MenuMappingButton>();
               var menuModel = menuRepository.Get(filter: a => a.MenuID == MenuID, includeProperties: "mbList").FirstOrDefault();

               menuBtnRepository.Delete(menuModel.mbList.ToList());

               menuBtnRepository.Insert(mplist);

               dal.Save();
            }
        }

        public List<MenuMappingButton> GetMBList(MenuMappingButton model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var listMb = dal.GetRepository<MenuMappingButton>().Get(filter: a => a.ButtonID == model.ButtonID).ToList();
                return listMb;
            }
        }
    }
}

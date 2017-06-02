using DDit.Core.Data.Entity;
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
    class MenuAndBtnRepository : IMenuAndBtnRepository
    {

        public void MenuMapBtn(int MenuID,List<MenuMappingButton> mplist)
        {
            using (UnitOfWork dal = new UnitOfWork(ConnectDB.DataBase()))
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
            using (UnitOfWork dal = new UnitOfWork(ConnectDB.DataBase()))
            {
                var listMb = dal.GetRepository<MenuMappingButton>().Get(filter: a => a.ButtonID == model.ButtonID).ToList();
                return listMb;
            }
        }
    }
}

using DDit.Core.Data.Entity.SystemEntity.DoEntity;
using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.ISystemRepositories
{
  public interface IMenuRepository
    {
       Tuple<int, List<MenuDo>> GetMenuList(Menu model);

       List<Menu> GetParentMenu();

       Menu OrderAssignment(Menu model);

       void AddMenu(Menu model);

       void ModifyMenu(Menu model);

       void DeleteMenu(int Menuid);

       Menu GetSingleMenu(int Menuid);

    }
}

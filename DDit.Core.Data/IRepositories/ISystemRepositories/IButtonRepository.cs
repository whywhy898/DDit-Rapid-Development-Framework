using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.ISystemRepositories
{
   public interface IButtonRepository
    {
       List<Button> GetButtonList();

       Tuple<int, List<Button>> GetButtonList(Button model);

       void AddBtn(Button model);

       void DeleteBtn(int btnID);

       void ModifyBtn(Button model);

       Button GetSingleBtnbyID(int id);

    }
}

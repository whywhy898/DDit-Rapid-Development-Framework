using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.ISystemRepositories
{
   public interface IRoleRepository
    {
       Tuple<int, List<Role>> GetRoleList(Role model);

       List<Role> GetRoleItem();

       Role Validate(Role model);

       void AddRole(Role model);

       void ModifyRole(Role model);

       ResultEntity RemoveRole(int roleID);

       void AddMenuAndBtnOfRole(int roleID, List<int> Menu, List<RoleMappingButton> modelList);
    }
}

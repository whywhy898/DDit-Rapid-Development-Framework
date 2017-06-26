using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.ISystemRepositories
{
   public interface IRoleAndBtnRepository
    {

       void AddRB(List<RoleMappingButton> modelList);

       List<RoleMappingButton> GetbtnAuthByRole(int roleID);

    }
}

using DDit.Core.Data.Entity;
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDitApplicationFrame.Service
{
    public interface IUserService
    {
        List<Menu> GetMenuByUserID(int UserID);
    }
}
using DDit.Component.Data;
using DDit.Core.Data.Entity;
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories
{
    public interface IUserRepository 
    {
        Tuple<int,List<User>> GetList(User model);

        User GetSingle(User model);

        User GetbyID(int userID);

        void AddUser(User model);

        void ModifyUser(User model);

        void DeleteUser(User model);

        void SetUserInfoRole(int userID, List<int> roleIDList);

    }
}

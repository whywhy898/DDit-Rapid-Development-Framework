using DDit.Component.Data;
using DDit.Core.Data.IRepositories;
using DDit.Core.Data.Repository;
using DDit.Component.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDit.Core.Data.Repositories;
using Z.EntityFramework.Plus;
using System.Data.Entity;
using DDit.Core.Data.SystemEntity.Entity;
using System.Diagnostics;

namespace DDit.Core.Data.Repositories
{
   public class UserRepository : IUserRepository
    { 

       public Tuple<int, List<User>> GetList(User model)
        {
            using (UnitOfWork dal=new UnitOfWork(ConnectDB.DataBase()))
            {
                var SysUserRepository = dal.GetRepository<User>();

                var conditions = ExpandHelper.True<User>();
                if (!string.IsNullOrEmpty(model.UserName))
                    conditions = conditions.And(a => a.UserName.Contains(model.UserName));

                if (!string.IsNullOrEmpty(model.UserReallyname))
                    conditions = conditions.And(a => a.UserReallyname.Contains(model.UserReallyname));

                if (model.DepartmentID > 0)
                    conditions = conditions.And(a => a.DepartmentID == model.DepartmentID);

                var templist = SysUserRepository.Get(filter: conditions, includeProperties: "RoleList");
         
                var count = templist.Count();

                if (model.order != null&&model.order.Count()>0)
                {
                    foreach (var item in model.order)
                    {
                        var column = model.columns.ElementAt(int.Parse(item.column));
                        templist = templist.OrderSort(column.data, item.dir);
                    }
                  
                }
                var result = templist.PageBy(model.pageIndex, model.pageSize).ToList();

                return new Tuple<int, List<User>>(count, result);
            }
        }

        public User GetSingle(User model)
        {
            using(UnitOfWork dal=new UnitOfWork(ConnectDB.DataBase())){
                var result = dal.GetRepository<User>().Get(filter: a => a.UserName == model.UserName, includeProperties: "RoleList").FirstOrDefault();     
                return result;
            }
        }


        public User GetbyID(int userID)
        {
            using (UnitOfWork dal = new UnitOfWork(ConnectDB.DataBase()))
            {
               
                // var result = dal.GetRepository<User>().Get(filter: a => a.UserID == userID, includeProperties: "RoleList.MenuList,RoleList.rbList").AsNoTracking().FirstOrDefault();

                var result = dal.GetRepository<User>().Get(filter: a => a.UserID == userID,includeProperties: "RoleList").FirstOrDefault();

                foreach (var item in result.RoleList)
	            {
                    var role=dal.GetRepository<Role>().Get(a=>a.RoleID==item.RoleID,includeProperties:"MenuList,rbList").FirstOrDefault();
                    item.MenuList = role.MenuList;
                    item.rbList = role.rbList;
	            } 

                return result;
            }
        }

        public void AddUser(User model)
        {
            using (UnitOfWork dal = new UnitOfWork(ConnectDB.DataBase()))
            {

                dal.GetRepository<User>().Insert(model);
                dal.Save();
            }
        }

        public void ModifyUser(User model)
        {
            using (UnitOfWork dal = new UnitOfWork(ConnectDB.DataBase()))
            {
                dal.GetRepository<User>().UpdateSup(model, new List<string>() { "IsEnable", "CreateTime" }, false);
                dal.Save();
            }
        }

        public void DeleteUser(User model)
        {
            using (UnitOfWork dal = new UnitOfWork(ConnectDB.DataBase()))
            {
               var sysUserRepository= dal.GetRepository<User>();
               var Usermodel = sysUserRepository.GetByID(model.UserID);
               Usermodel.IsEnable=Usermodel.IsEnable?false:true;
               sysUserRepository.UpdateSup(Usermodel, new List<string>() { "IsEnable" });
               dal.Save();
            }
        }


        /// <summary>
        /// 添加用户角色信息,先删除原有数据,在添加到数据库
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="roleIDList"></param>
        /// <returns></returns>
        public void SetUserInfoRole(int userID, List<int> roleIDList)
        {
            using (UnitOfWork dal = new UnitOfWork(ConnectDB.DataBase()))
            {
                var sysUserRepository = dal.GetRepository<User>();
                var roleRepository = dal.GetRepository<Role>();
                var UserModel = GetbyID(userID);
                var roleList = UserModel.RoleList.ToList();

                roleList.ForEach(m =>
                {
                    var userModel = sysUserRepository.Get(filter: a => a.UserID == userID, includeProperties: "RoleList").FirstOrDefault();
                    var roleModel = roleRepository.GetByID(m.RoleID);
                    userModel.RoleList.Remove(roleModel);
                });

                roleIDList.ForEach(m =>
                {
                    var userModel = sysUserRepository.GetByID(userID);
                    var roleModel = roleRepository.GetByID(m);
                    userModel.RoleList.Add(roleModel);
                });

                dal.Save();
            }         
        }
    }
}

using DDit.Core.Data.Entity;
using DDit.Core.Data.IRepositories;
using DDit.Core.Data.SystemEntity.Entity;
using DDit.Component.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Repositories
{
    class RoleRepository : IRoleRepository
    {

        public Tuple<int, List<Role>> GetRoleList(Role model)
        {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {
                var conditions = ExpandHelper.True<Role>();
                if (!string.IsNullOrEmpty(model.RoleName))
                {
                    conditions = conditions.And(a => a.RoleName.Contains(model.RoleName));
                }
                var templist = dal.GetRepository<Role>().Get(filter: conditions, includeProperties: "MenuList.mbList");

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

                return new Tuple<int, List<Role>>(count,result);
            }
        }

        public void AddRole(Role model)
        {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {
               dal.GetRepository<Role>().Insert(model);
               dal.Save();
           }
        }

        public Role Validate(Role model) {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {
              var temp = dal.GetRepository<Role>().Get(filter: a => a.RoleName == model.RoleName);
              if (model.RoleID != 0) {
                  temp=temp.Where(a => a.RoleID == model.RoleID);
              }
              return temp.FirstOrDefault();
            }
        }

        public void ModifyRole(Role model)
        {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {
                dal.GetRepository<Role>().UpdateSup(model, new List<string>() { "CreateTime" }, false);

                dal.Save();
            }
        }

        public List<Role> GetRoleItem() {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {
                return dal.GetRepository<Role>().Get(orderBy: a => a.OrderByDescending(q => q.RoleID)).ToList();
            }
        }

        public void AddMenuAndBtnOfRole(int roleID, List<int> Menu, List<RoleMappingButton> modelList)
        {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {
                var roleRepository=dal.GetRepository<Role>();
                var roleBtnRepository = dal.GetRepository<RoleMappingButton>();
                var menuRepository = dal.GetRepository<Menu>();
                var roleModel = roleRepository.Get(filter: a => a.RoleID == roleID, includeProperties: "MenuList,rbList").FirstOrDefault();

                var menuList = new List<Menu>();

                Menu.ForEach(m =>
                {
                    menuList.Add(menuRepository.GetByID(m));
                });


                if (roleModel.MenuList.ToList().Except<Menu>(menuList).Count() == 0 && menuList.Except<Menu>(roleModel.MenuList.ToList()).Count() == 0)
                { }
                else {
                    //删除原有菜单
                    if (roleModel.MenuList.Count > 0)
                    {
                        roleModel.MenuList.ToList().ForEach(m =>
                        {
                            roleModel.MenuList.Remove(m);
                        });
                    }
                    //添加新菜单
                    roleModel.MenuList = menuList;
                    roleRepository.Update(roleModel);
                }

                if (modelList != null)
                {
                    //删除原有的按钮
                    if (roleModel.rbList.Count > 0)
                    {
                        roleBtnRepository.Delete(roleModel.rbList.ToList());
                    }
                    //添加新的按钮
                    roleBtnRepository.Insert(modelList);
                    dal.Save();
                }
            }
        }

        public ResultEntity RemoveRole(int roleID) {
            using (UnitOfWork dal = new UnitOfWork(new CoreDbContext()))
            {
               var roleRepository = dal.GetRepository<Role>();
               var rolemodel = roleRepository.Get(filter: a => a.RoleID == roleID, includeProperties: "UserList").FirstOrDefault();
               if (rolemodel.UserList.Count > 0)
               {
                   return new ResultEntity { result = false, message = "该角色正在被使用，无法删除信息！" };
               }
               else {
                   roleRepository.Delete(roleID);
                   dal.Save();
                   return new ResultEntity { result = true, message = "角色删除成功！" };
               }
           }
        }
    }
}

using DDit.Core.Data.Entity;
using DDit.Core.Data.IRepositories;
using DDit.Core.Data.SystemEntity.Entity;
using DDitApplicationFrame.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DDit.Component.Tools;


namespace DDitApplicationFrame.Service.Imp
{
    public class UserService:IUserService
    {
        public IUserRepository userRepository { get; set; }

        /// <summary>
        /// 得到用户角色的菜单列表
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public List<Menu> GetMenuByUserID(int UserID) {

            var rolelist = userRepository.GetbyID(UserID).RoleList;

            var menuList = new List<Menu>();

            foreach (var role in rolelist)
            {
                foreach (var menu in role.MenuList)
                {
                    menuList.Add(menu);
                }
            }

            var newMenu = menuList.DistinctBy(a => a.MenuID);

            return newMenu.ToList();
        }

    }
}
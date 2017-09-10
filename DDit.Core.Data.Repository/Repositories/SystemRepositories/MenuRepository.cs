using DDit.Core.Data.Entity;
using DDit.Core.Data.IRepositories.ISystemRepositories;
using DDit.Core.Data.Entity.SystemEntity;
using DDit.Component.Tools;
using System;
using System.Collections.Generic;
using Autofac;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DDit.Core.Data.Entity.SystemEntity.DoEntity;

namespace DDit.Core.Data.Repository.Repositories.SystemRepositories
{
    class MenuRepository : IMenuRepository
    {
        public Tuple<int, List<MenuDo>> GetMenuList(Menu model)
        {
            Mapper.Initialize(a =>
            {
                a.CreateMap<Menu, MenuDo>()
                    .ForMember(de => de.MenuParentName, op => { op.MapFrom(s => s.Father.MenuName); });
            });

            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var menuRepository = dal.GetRepository<Menu>();
                var conditions = ExpandHelper.True<Menu>();

                if (!string.IsNullOrEmpty(model.MenuName))
                    conditions = conditions.And(a => a.MenuName.Contains(model.MenuName));

                var templist = menuRepository.Get(filter: conditions, includeProperties: "mbList,Father").ProjectToQueryable<MenuDo>();

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

               //按钮排序
                result.ForEach(m =>
                {
                    if (m.mbList.Count > 0) {
                        m.mbList = m.mbList.OrderBy(a=>a.OrderBy).ToList();
                    }
                });

                return new Tuple<int, List<MenuDo>>(count, result);
           }
        }

        public List<Menu> GetParentMenu() {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
               // var result = dal.GetRepository<Menu>().Get(filter: p => p.MenuParentID == null, includeProperties: "Childs,Childs.mbList.ButtonModel").ToList();

                var result = dal.GetRepository<Menu>().Get(filter: p => p.MenuParentID == null, includeProperties: "Childs").ToList();
                
                return result;
            }
        }

        public Menu OrderAssignment(Menu model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var menuRepository = dal.GetRepository<Menu>();
                model.CreateTime = DateTime.Now;
                if (model.MenuOrder == 0)
                {
                    Menu maxModel = new Menu ();
                    if (model.MenuParentID == null)
                    {
                        maxModel = menuRepository.Get(filter: a => a.MenuParentID.Equals(model.MenuParentID), orderBy: p => p.OrderByDescending(s => s.MenuOrder)).FirstOrDefault();
                    }
                    else {
                        maxModel = menuRepository.Get(filter: a => a.MenuParentID == model.MenuParentID, orderBy: p => p.OrderByDescending(s => s.MenuOrder)).FirstOrDefault();
                    }
                    model.MenuOrder =maxModel != null?maxModel.MenuOrder + 1:1;
                    return model;
                }
                else {
                    return model;
                }
            }
        }

        public void AddMenu(Menu model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<Menu>().Insert(model);
                dal.Save();
            }
        }

        public void ModifyMenu(Menu model)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<Menu>().Update(model);
                dal.Save();
            }
        }

        public void DeleteMenu(int Menuid)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                dal.GetRepository<Menu>().Delete(Menuid);
                dal.Save();
            }
        }

        public Menu GetSingleMenu(int Menuid)
        {
            using (UnitOfWork dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                return dal.GetRepository<Menu>().Get(filter: a => a.MenuID == Menuid, includeProperties: "mbList").FirstOrDefault();            
            }
        }
    }
}

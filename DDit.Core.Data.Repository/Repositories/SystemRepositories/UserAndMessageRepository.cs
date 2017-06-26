using DDit.Component.Tools;
using DDit.Core.Data.IRepositories.ISystemRepositories;
using System;
using Autofac;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DDit.Core.Data.Entity.SystemEntity;

namespace DDit.Core.Data.Repository.Repositories.SystemRepositories
{
    class UserAndMessageRepository : IUserAndMessageRepository
    {
        public List<UserMappingMessage> GetMesByUser(int UserId,bool isAll)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var userMesR = dal.GetRepository<UserMappingMessage>();

                var conditions = ExpandHelper.True<UserMappingMessage>().And(a => a.UserID == UserId);

                if (!isAll)
                    conditions = conditions.And(a => a.IsRead==false);

                return userMesR.Get(conditions, includeProperties: "MessageInfo",orderBy:a=>a.OrderByDescending(b=>b.IsRead==true))
                               .ToList();

            }
        }


        public Tuple<int, List<UserMappingMessage>> GetMessagebyUser(UserMappingMessage model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                var um = dal.GetRepository<UserMappingMessage>();
                var conditions = ExpandHelper.True<UserMappingMessage>().And(a => a.UserID == model.UserID);


                var templist = um.Get(filter: conditions, includeProperties: "MessageInfo");

                var count = templist.Count();

                if (model.order != null && model.order.Count() > 0)
                {
                    foreach (var item in model.order)
                    {
                        var column = model.columns.ElementAt(int.Parse(item.column));
                        templist = templist.OrderSort(column.data, item.dir);
                    }
                }

                var result = templist.PageBy(model.pageIndex, model.pageSize).ToList();

                result.ForEach(a =>
                {
                    a.MessageInfo.SendUserInfo = new User()
                    {
                        UserName = dal.GetRepository<User>().Get(b => b.UserID == a.MessageInfo.SendUser).FirstOrDefault().UserReallyname
                    };
                });

                return new Tuple<int, List<UserMappingMessage>>(count, result); 
           }
        }


        public void SetMessageRead(UserMappingMessage model)
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                model.IsRead = true;

                var userMesR = dal.GetRepository<UserMappingMessage>();

                userMesR.UpdateSup(model, new List<string>() { "IsRead" });

                userMesR.Save();
            }
        }
    }
}

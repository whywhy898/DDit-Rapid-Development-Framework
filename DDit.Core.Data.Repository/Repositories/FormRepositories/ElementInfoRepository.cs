using DDit.Component.Tools;
using DDit.Core.Data.IRepositories.IFormRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using System.Threading.Tasks;
using DDit.Core.Data.Entity.FormEntity;

namespace DDit.Core.Data.Repository.Repositories.FormRepositories
{
    public class ElementInfoRepository : IElementInfoRepository
    {

        public List<Entity.FormEntity.ElementInfo> GetElementInfoList()
        {
            using (var dal = BaseInfo._container.Resolve<UnitOfWork>())
            {
                return dal.GetRepository<ElementInfo>().Get().ToList();
            }
        }
    }
}

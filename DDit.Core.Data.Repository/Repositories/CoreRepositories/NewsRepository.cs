
using DDit.Component.Tools;
using DDit.Core.Data.Entity.CoreEntity;
using DDit.Core.Data.IRepositories.ICoreRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Repositories.CoreRepositories
{
    class NewsRepository : INewsRepository
    {

        public Tuple<int, List<News>> GetNewsList(News model)
        {
            using(var dal=new UnitOfWork(new CoreDbContext())){

                var newsRepository = dal.GetRepository<News>();
                var conditions = ExpandHelper.True<News>();
                if (!string.IsNullOrEmpty(model.NewTitle))
                {
                    conditions = conditions.And(a => a.NewTitle.Contains(model.NewTitle));
                }

                var templist = newsRepository.Get(filter: conditions);

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

                return new Tuple<int, List<News>>(count, result);
            }
        }
    }
}

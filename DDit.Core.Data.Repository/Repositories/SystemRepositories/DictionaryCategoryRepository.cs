using DDit.Core.Data.IRepositories;
using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Repository.Repositories
{
    class DictionaryCategoryRepository : IDictionaryCategoryRepository
    {

        public List<DictionaryCategory> DiCategoryList()
        {
            using (UnitOfWork dal = new UnitOfWork(ConnectDB.DataBase()))
            {
                return dal.GetRepository<DictionaryCategory>().Get(filter: a => a.Enabled == true, includeProperties: "DicValueList").ToList();
            }
        }

        public void AddDic(DictionaryCategory model)
        {
            using (UnitOfWork dal = new UnitOfWork(ConnectDB.DataBase()))
            {
                dal.GetRepository<DictionaryCategory>().Insert(model);
                dal.Save();
            }
        }

        public void DisabledDic(int btnID)
        {
            using (UnitOfWork dal = new UnitOfWork(ConnectDB.DataBase()))
            {
                var dictionaryCategoryRepository = dal.GetRepository<DictionaryCategory>();
                var entity = dictionaryCategoryRepository.GetByID(btnID);
                    entity.Enabled = false;
                    dictionaryCategoryRepository.Update(entity);
                    dal.Save();
            }
        }

        public void ModifyDic(DictionaryCategory model)
        {
            using (UnitOfWork dal = new UnitOfWork(ConnectDB.DataBase()))
            {
                dal.GetRepository<DictionaryCategory>().UpdateSup(model, new List<string>() { "CreateTime", "Enabled" }, false);
                dal.Save();
            }
        }
    }
}

using DDit.Core.Data.SystemEntity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories
{
   public interface IDictionaryRepository
    {
       Tuple<int, List<Dictionary>> GetDictionaryList(Dictionary model);

       void AddDic(Dictionary model);

       void DisabledDic(int btnID);

       void ModifyDic(Dictionary model);
    }
}

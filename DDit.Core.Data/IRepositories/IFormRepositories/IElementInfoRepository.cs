using DDit.Core.Data.Entity.FormEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.IFormRepositories
{
   public interface IElementInfoRepository
    {
       List<ElementInfo> GetElementInfoList();
    }
}

using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.ISystemRepositories
{
  public interface IUserAndMessageRepository
    {
       List<UserMappingMessage> GetMesByUser(int UserId,bool isAll);

       Tuple<int, List<UserMappingMessage>> GetMessagebyUser(UserMappingMessage model);

       void SetMessageRead(UserMappingMessage model);
    }
}

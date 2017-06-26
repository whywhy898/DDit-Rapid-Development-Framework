using DDit.Core.Data.Entity.SystemEntity;
using DDit.Core.Data.Entity.SystemEntity.DoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.ISystemRepositories
{
   public interface IMessageRepository
    {
       Tuple<int, List<MessageDo>> GetMessageList(Message model);

       List<Dictionary> GetDepartmentInfo();

       void InsertMessage(Message model);

       void ModifyMessage(Message model);

       Message GetMessageSingle(int id);

       void RemoveMessage(int id);

       void DetectionMessage(int currentUserId);

       void SetSendState(Message model);
    }
}

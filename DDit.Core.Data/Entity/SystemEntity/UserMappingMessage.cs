using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.SystemEntity
{
   public class UserMappingMessage:BaseEntity
    {
       /// <summary>
       /// 主键标识
       /// </summary>
       public int ID { get; set; }

       /// <summary>
       /// 消息主键
       /// </summary>
       public int MessageID { get; set; }

       /// <summary>
       /// 用户主键
       /// </summary>
       public int UserID { get; set; }

       /// <summary>
       /// 是否已读
       /// </summary>
       public bool IsRead { get; set; }

       public User UserInfo { get; set; }

       public Message MessageInfo { get; set; }
    }
}

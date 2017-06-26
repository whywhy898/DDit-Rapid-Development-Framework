using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.SystemEntity
{
   public class Message:BaseEntity
    {

       /// <summary>
       /// 消息主键
       /// </summary>
       public int MessageID { get; set; }

       /// <summary>
       /// 消息标题
       /// </summary>
       public string MessageTitle { get; set; }

       /// <summary>
       /// 消息内容
       /// </summary>
       public string MessageText { get; set; }

       /// <summary>
       /// 消息发送人
       /// </summary>
       public int SendUser { get; set; }

       /// <summary>
       /// 消息接收人，用逗号分开
       /// </summary>
       public string RecUser { get; set; }

       /// <summary>
       /// 发送邮件状态 0 不发送 1发送中 2成功 3失败
       /// </summary>
       public int SendEmailState { get; set; }

       /// <summary>
       /// 消息发送时间
       /// </summary>
       public DateTime? SendTime { get; set; }

       /// <summary>
       /// 消息过期时间
       /// </summary>
       public DateTime? ExpirationTime { get; set; }

       /// <summary>
       /// 是否发送邮件
       /// </summary>
       public bool IsSendEmail { get; set; }

    //   public List<UserMappingMessage> UserList { get; set; }

       public User SendUserInfo { get; set; }
    }
}

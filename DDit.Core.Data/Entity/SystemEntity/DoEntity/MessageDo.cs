using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.SystemEntity.DoEntity
{
   public class MessageDo
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
        /// 消息发送人
        /// </summary>
        public string SendUserName { get; set; }

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
    }
}

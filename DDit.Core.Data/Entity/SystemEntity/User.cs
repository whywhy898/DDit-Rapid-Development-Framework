using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.SystemEntity
{
    public class User : BaseEntity
    {
       public User() {
           RoleList = new List<Role>();
           MessageList = new List<UserMappingMessage>();
       }
       public int UserID { get; set; }

       public string UserName { get; set; }

       public string UserPassword {get;set;}

       public string UserReallyname {get;set;}

       public string HeadPortrait { get; set; }

       public string MobilePhone { get; set; }

       public string Email { get; set; }

       public int DepartmentID {get;set;}
        
       public bool IsEnable {get;set;}

       public DateTime CreateTime {get;set;}
       
       public DateTime? UpdateTime {get;set;}

       public string Remark { get; set; }

       public ICollection<Role> RoleList { get; set; }

       //接收消息列表
       public ICollection<UserMappingMessage> MessageList { get; set; }

       public Dictionary Department { get; set; }

       //发送消息列表
       public List<Message> Messages { get; set; }

    }
}

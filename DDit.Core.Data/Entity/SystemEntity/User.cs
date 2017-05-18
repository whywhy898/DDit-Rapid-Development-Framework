using DDit.Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.SystemEntity.Entity
{
    public class User : BaseEntity
    {
       public User() {
           RoleList = new List<Role>();
       }
       public int UserID { get; set; }

       public string UserName { get; set; }

       public string UserPassword {get;set;}

       public string UserReallyname {get;set;}

       public string HeadPortrait { get; set; }

       public int DepartmentID {get;set;}
        
       public bool IsEnable {get;set;}

       public DateTime CreateTime {get;set;}
       
       public DateTime? UpdateTime {get;set;}

       public string Remark { get; set; }

       public ICollection<Role> RoleList { get; set; }

    }
}

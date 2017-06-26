using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.SystemEntity.DoEntity
{
   public class AutoUserDo
    {
       public int id { get; set; }

       public string text { get; set; }

       public string department { get; set; }

       public string HeadPortrait { get; set; }

       public List<roleinfo> RoleList { get; set; }
    }

   public class roleinfo{
       public string RoleName{get;set;}
    }
}

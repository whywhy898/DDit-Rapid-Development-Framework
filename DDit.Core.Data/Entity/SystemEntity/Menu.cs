using DDit.Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web.Mvc;

namespace DDit.Core.Data.SystemEntity.Entity
{
    public class Menu : BaseEntity
    {
 
       public int MenuID { get; set; }

       public string MenuName { get; set; }

       public string MenuUrl { get; set; }

       public int? MenuParentID { get; set; }

       public int MenuOrder { get; set; }

       public string MenuIcon { get; set; }

       public int IsVisible { get; set; }

       public DateTime CreateTime { get; set; }

       public DateTime? UpdateTime { get; set; }

       public Menu Father { get; set; }

       public ICollection<Menu> Childs { get; set; }

       [JsonIgnore]
       public ICollection<Role> RoleList { get; set; }

       public ICollection<MenuMappingButton> mbList { get; set; }

    }
}

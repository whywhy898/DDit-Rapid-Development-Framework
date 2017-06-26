using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.SystemEntity
{
   public class MenuMappingButton
    {
       public int ID { get; set; }

       public int MenuID { get; set; }

       public int ButtonID { get; set; }

       public int OrderBy { get; set; }

       public Menu MenuModel { get; set; }

       public Button ButtonModel { get; set; }

    }
}

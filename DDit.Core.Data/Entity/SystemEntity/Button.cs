using DDit.Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.SystemEntity.Entity
{
    public class Button:BaseEntity
    {
        public int ButtonID { get; set; }

        public string ButtonOpID { get; set; }

        public string ButtonName { get; set; }

        public string ButtonOperation { get; set; }

        public string ButtonMagic { get; set; }

        public string ButtonDescribe { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }

        public ICollection<MenuMappingButton> mbList { get; set; }

        public ICollection<RoleMappingButton> rbList { get; set; }

    }
}

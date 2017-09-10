using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.FormEntity
{
    public class ElementInfo : BaseEntity
    {
       public int ElementId { get; set; }

       public string ElementText { get; set; }

       public string ElementIoc { get; set; }

       public string ElementType { get; set; }
    }
}

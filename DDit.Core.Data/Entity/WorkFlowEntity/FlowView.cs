using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.WorkFlowEntity
{
   public class FlowView
    {

       public string title { get; set; }

       public dynamic nodes { get; set; }

       public dynamic lines { get; set; }

       public dynamic areas { get; set; }

       public int initNum { get; set; }

    }
}

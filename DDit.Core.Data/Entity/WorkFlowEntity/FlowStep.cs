using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.WorkFlowEntity
{
   public class FlowStep
    {
       public int StepId { get; set; }

       public int FlowId { get; set; }

       public string name { get; set; }

       public int left { get; set; }

       public int top { get; set; }

       public string type { get; set; }

       public int width { get; set; }

       public int height { get; set; }

       public bool alt { get; set; }

       public string stepUser { get; set; }

       public string stepName { get; set; }

       public string flowNodeName { get; set; }

       public string remark { get; set; }
    }
}

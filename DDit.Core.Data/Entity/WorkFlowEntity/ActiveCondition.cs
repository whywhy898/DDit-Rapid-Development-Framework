using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.WorkFlowEntity
{
   public class ActiveCondition
    {
       public int ConditionId { get; set; }

       public int FlowId { get; set; }

       public int ActiveId { get; set; }

       public int Index { get; set; }

       public string Field { get; set; }

       public string Compare { get; set; }

       public string CompareValue { get; set; }

       public string Logic { get; set; }

       public string Group { get; set; }
    }
}

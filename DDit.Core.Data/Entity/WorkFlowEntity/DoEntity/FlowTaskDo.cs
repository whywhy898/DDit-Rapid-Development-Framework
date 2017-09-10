using DDit.Component.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.WorkFlowEntity.DoEntity
{
  public class FlowTaskDo
    {
       public int FlowInfoId { get; set; }

       public string UserName { get; set; }

       public DateTime CreateTime { get; set; }

       public FlowState FlowInfoState { get; set; }

       public string FlowName { get; set; }

       public string FlowCategory { get; set; }

       public string FlowRemark { get; set; }

    }
}

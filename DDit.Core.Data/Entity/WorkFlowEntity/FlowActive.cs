using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.WorkFlowEntity
{
   public class FlowActive
    {  
       /// <summary>
       /// 流动作主键（线）
       /// </summary>
       public int ActiveId { get; set; }
       /// <summary>
       /// 流任务主键
       /// </summary>
       public int FlowId { get; set; }
       /// <summary>
       /// 动作类型(插件字段)
       /// </summary>
       public string type { get; set; }
       /// <summary>
       /// 起始步骤
       /// </summary>
       public string from { get; set; }
       /// <summary>
       /// 结束步骤
       /// </summary>
       public string to { get; set; }
       /// <summary>
       /// 名字
       /// </summary>
       public string name { get; set; }

       public bool alt { get; set; }

       public string FlowLineName { get; set; }

       public List<ActiveCondition> ConditionInfo { get; set; }

    }
}

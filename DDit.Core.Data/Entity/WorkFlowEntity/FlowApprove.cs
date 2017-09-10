using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.WorkFlowEntity
{
   public class FlowApprove:BaseEntity
    {
       /// <summary>
       /// 审批Id
       /// </summary>
       public int ApproveId { get; set; }
       /// <summary>
       /// 流信息Id
       /// </summary>
       public int FlowInfoId { get; set; }
       /// <summary>
       /// 审批人
       /// </summary>
       public string ApproveUser { get; set; }

       public int? ReallyApproveUser { get; set; } 
       /// <summary>
       /// 流步骤Id
       /// </summary>
       public int FlowStepId { get; set; }
       /// <summary>
       /// 审批时间
       /// </summary>
       public DateTime? ApproveTime { get; set; }
       /// <summary>
       /// 审批结果 0待处理 1通过 2退回
       /// </summary>
       public int ApproveResult { get; set; }

       /// <summary>
       /// 审批备注
       /// </summary>
       public string ApproveRemark { get; set; }

       public User ReallyApproveUserInfo { get; set; }

       public FlowStep FlowStepInfo { get; set; }

       public FlowInfo FlowTaskInfo { get; set; }
    }
}

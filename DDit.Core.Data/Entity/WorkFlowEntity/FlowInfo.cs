using DDit.Component.Tools;
using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.WorkFlowEntity
{
    public class FlowInfo : BaseEntity
    {
       /// <summary>
       /// 流任务Id
       /// </summary>
       public int FlowInfoId { get; set; }
       /// <summary>
       /// 工作流Id
       /// </summary>
       public int FlowId { get; set; }
       /// <summary>
       /// 表单Id
       /// </summary>
       public int FormId { get; set; }
       /// <summary>
       /// 表单信息Id
       /// </summary>
       public int FormInfoId { get; set; }
       /// <summary>
       /// 用户Id
       /// </summary>
       public int UserId { get; set; }
       /// <summary>
       /// 步骤Id
       /// </summary>
       public int FlowStepId { get; set; }

       /// <summary>
       /// 当前工作流进行状态
       /// 0:条件不通过 1:处理中 2:结束 3:驳回
       /// </summary>
       public FlowState FlowInfoState { get; set; }

       /// <summary>
       /// 创建时间
       /// </summary>
       public DateTime CreateTime { get; set; }

       public List<FlowApprove> Approves { get; set; }

       public WorkFlow WorkFlowInfo { get; set; }

       public User Userinfo { get; set; }

    }
}

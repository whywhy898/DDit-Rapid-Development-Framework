using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.WorkFlowEntity.DoEntity
{
   public class FlowApproveDo
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
        /// 流任务发起人姓名
        /// </summary>
        public string FlowName { get; set; }

        /// <summary>
        /// 流任务发起人姓名
        /// </summary>
        public string StartUserName { get; set; }

        /// <summary>
        /// 流任务发起时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 审批人昵称
        /// </summary>
        public string ApproveUserName { get; set; }

        /// <summary>
        /// 实际审批人
        /// </summary>
        public string ReallyApproveUserName { get; set; }

        /// <summary>
        /// 流步骤名称
        /// </summary>
        public string FlowStepName { get; set; }

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

    }
}

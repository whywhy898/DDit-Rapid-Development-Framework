using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.WorkFlowEntity.DoEntity
{
   public class FlowApproveInfoDo
    {
        /// <summary>
        /// 审批Id
        /// </summary>
        public int ApproveId { get; set; }

        /// <summary>
        /// 表单Id
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// 表单信息Id
        /// </summary>
        public int FormInfoId { get; set; }

        /// <summary>
        /// 流任务Id
        /// </summary>
        public int FlowInfoId { get; set; }

        /// <summary>
        /// 工作流名称
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
        /// 流步骤名称
        /// </summary>
        public string FlowStepName { get; set; }
    }
}

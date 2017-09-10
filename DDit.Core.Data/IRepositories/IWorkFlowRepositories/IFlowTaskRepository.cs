using DDit.Core.Data.Entity.WorkFlowEntity;
using DDit.Core.Data.Entity.WorkFlowEntity.DoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.IWorkFlowRepositories
{
   public interface IFlowTaskRepository
    {
       Tuple<int, List<FlowTaskDo>> GetFlowInfoBySelf(FlowInfo model);

       /// <summary>
       /// 待办审批调用
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       Tuple<int, List<FlowApproveInfoDo>> GetFlowApproveInfo(FlowApprove model);

       /// <summary>
       /// 查看我的流进度调用
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       Tuple<int, List<FlowApproveDo>> GetFlowApproveSelf(FlowApprove model);

       /// <summary>
       /// 执行审批
       /// </summary>
       /// <param name="model"></param>
       void ApproveActive(FlowApprove model);
    }
}

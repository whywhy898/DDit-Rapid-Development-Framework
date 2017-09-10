using DDit.Core.Data.Entity;
using DDit.Core.Data.Entity.WorkFlowEntity;
using DDit.Core.Data.Entity.WorkFlowEntity.DoEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.IRepositories.IWorkFlowRepositories
{
   public interface IWorkFlowRepository
    {
       Tuple<int, List<WorkFlowDo>> GetWorkFlowItem(WorkFlow model);

       void AddWorkFlow(WorkFlow model);

       void MoidfyWorkFlow(WorkFlow model);

       bool RemoveWorkFlow(WorkFlow model);

       void SaveFlowViewInfo(int flowid,string jsonstr);

       FlowView GetFlowViewInfo(int flowid);

       bool HasFlowView(int flowid);
    }
}

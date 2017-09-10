using DDit.Core.Data.Entity.FormEntity;
using DDit.Core.Data.Entity.SystemEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.WorkFlowEntity
{
   public class WorkFlow:BaseEntity
    {
       public int FlowID { get; set; }

       public int FormID { get; set; }

       public int FlowSort { get; set; }

       public string FlowName { get; set; }

       public string remark { get; set; }

       public int CreateUser { get; set; }

       public DateTime CreateTime { get; set; }

       public User CuserInfo { get; set; }

       public FormInfo forminfo { get; set; }

       public Dictionary SortInfo { get; set; }

       public List<FlowStep> flowSteps { get; set; }

       public List<FlowActive> flowActives { get; set; }

       public List<ActiveCondition> activeCondis { get; set; }

    }
}

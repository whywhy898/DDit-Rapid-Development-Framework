using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.WorkFlowEntity.DoEntity
{
   public class WorkFlowDo
    {
        public int FlowID { get; set; }

        public int FormID { get; set; }

        public int FlowSort { get; set; }

        public string FormName { get; set; }

        public string FlowSortName { get; set; }

        public string FlowName { get; set; }

        public string remark { get; set; }

        public string CreateUserName { get; set; }

        public bool IsPerfect { get; set; }

        public DateTime CreateTime { get; set; }

    }
}

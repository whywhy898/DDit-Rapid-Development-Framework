using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.SystemEntity.Entity
{
    public class Account
    {
        public int AccountID { get; set; }

        public int TestID { get; set; }

        public int Total { get; set; }

        public Test Student { get; set; }
    }
}

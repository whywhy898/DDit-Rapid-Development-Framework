using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.SystemEntity
{
    public class SystemInfo
    {
        public int SystemID { get; set; }

        public string SystemTitle { get; set; }

        public string SystemVersion { get; set; }

        public string SystemCopyright { get; set; }

        public bool IsValidCode { get; set; }

    }
}

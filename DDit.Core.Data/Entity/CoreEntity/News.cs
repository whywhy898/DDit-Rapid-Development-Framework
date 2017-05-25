using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.CoreEntity
{
    public class News : BaseEntity
    {
        public int NewId { get; set; }

        public string NewTitle { get; set; }

        public string NewContent { get; set; }

        public string NewAuthor { get; set; }

        public string CreateTime { get; set; }

    }
}

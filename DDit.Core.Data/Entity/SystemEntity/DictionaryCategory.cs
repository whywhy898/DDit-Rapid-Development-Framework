using DDit.Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.SystemEntity
{
    public class DictionaryCategory : BaseEntity
    {
       public int ID { get; set; }

       public string Category { get; set; }

       public bool Enabled { get; set; }

       public DateTime CreateTime { get; set; }

       public DateTime? UpdateTime { get; set; }

       public ICollection<Dictionary> DicValueList { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity
{
   public class ResultEntity
    {
       public bool result { get; set; }

       public string message { get; set; }

       public object Data { get; set; }

       //绑定表格-返回集合数
       public int recordsTotal { get; set; }
       
       //绑定表格-集合过滤数
       public int recordsFiltered { get; set; }

       //绑定表格-集合内容
       public object dataList { get; set; }

    }
}

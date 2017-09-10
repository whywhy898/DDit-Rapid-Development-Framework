using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.FormEntity
{
    public class FormInfo : BaseEntity
    {
       /// <summary>
       /// 主键Id
       /// </summary>
       public int FormId { get; set; }

       /// <summary>
       /// 表单名称
       /// </summary>
       public string FormName { get; set; }

       /// <summary>
       /// 数据库名称
       /// </summary>
       public string DBName { get; set; }

        /// <summary>
        /// 表主键
        /// </summary>
       public string FieldKey { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
       public string remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
       public DateTime CreatTime { get; set; }

       [NotMapped]
       public bool isConfiguration { get; set; }

       public List<FormElement> elementPropertys { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDit.Core.Data.Entity.FormEntity
{
    public class FormElement : BaseEntity
    {

        /// <summary>
        /// 关联主键
        /// </summary>
        public int FEId { get; set; }

        /// <summary>
        /// 元素主键ID
        /// </summary>
        public int ElementId { get; set; }

        /// <summary>
        /// 表单信息ID
        /// </summary>
        public int FormId { get; set; }

        /// <summary>
        /// 字段标识
        /// </summary>
        public string FieldIden { get; set; }

        /// <summary>
        /// 元素lable
        /// </summary>
        public string ElementLable { get; set; }

        /// <summary>
        /// 元素验证
        /// </summary>
        public string ElementValid { get; set; }

        /// <summary>
        /// 元素高度
        /// </summary>
        public string ElementHeight { get; set; }

        /// <summary>
        /// 元素默认值
        /// </summary>
        public string ElementDefValue { get; set; }

        /// <summary>
        /// 下拉框数据源字典
        /// </summary>
        public int DataDictionary { get; set; }

        /// <summary>
        /// 元素顺序
        /// </summary>
        public int ElementOrder { get; set; }

        /// <summary>
        /// 元素格式类型
        /// </summary>
        public string ElementFormatType { get; set; }

        [NotMapped]
        public string ElementValue { get; set; }

        public ElementInfo element { get; set; }

    }
}

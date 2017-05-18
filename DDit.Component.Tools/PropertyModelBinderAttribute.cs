using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace DDit.Component.Tools
{
    [AttributeUsage(ValidTargets, AllowMultiple = false, Inherited = false)]
    public class PropertyModelBinderAttribute : Attribute
    {
        /// <summary>
        /// 指定此属性可以应用特性的应用程序元素。
        /// </summary>
        internal const AttributeTargets ValidTargets = AttributeTargets.Field | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Parameter;
        /// <summary>
        /// 声明属性名称。
        /// </summary>
        private string _propertyName = string.Empty;

        /// <summary>
        /// 获取或设置属性别名。
        /// </summary>
        public string PropertyName
        {
            get { return _propertyName; }
        }

        /// <summary>
        /// 使用指定的属性别名。
        /// </summary>
        /// <param name="propertyName">指定的属性别名。</param>
        public PropertyModelBinderAttribute(string propertyName)
        {
            _propertyName = propertyName;
        }

        /// <summary>
        /// 检索关联的模型联编程序。。
        /// </summary>
        /// <returns>对实现 System.Web.Mvc.IModelBinder 接口的对象的引用。</returns>
        public IModelBinder GetBinder()
        {
            return new PropertyModelBinder();
        }
    }
}

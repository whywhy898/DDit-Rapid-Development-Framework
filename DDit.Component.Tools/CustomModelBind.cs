using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace DDit.Component.Tools
{
    public class CustomModelBind : System.Web.Mvc.IModelBinder
    {
        //model绑定适用于带 ？ 属性的实体
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object obj = Activator.CreateInstance(bindingContext.ModelType);
            foreach (PropertyInfo p in bindingContext.ModelType.GetProperties())
            {

               //ValueProviderResult vpResult=  bindingContext.ValueProvider.GetValue(p.Name);

               //忽略表单安全（不严重参数代码）
               var valueProvider = (bindingContext.ValueProvider as IUnvalidatedValueProvider);     
               ValueProviderResult vpResult = valueProvider.GetValue(p.Name, true);
               if (vpResult != null)
               {
                   object value = vpResult.ConvertTo(p.PropertyType);
                   p.SetValue(obj, value, null);
               }
            }
            return obj;
        }
    }
}
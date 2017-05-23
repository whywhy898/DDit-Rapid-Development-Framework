using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace DDit.Component.Tools
{
    /// <summary>
    /// 将浏览器请求映射到数据对象。
    /// </summary>
    public class PropertyModelBinder : DefaultModelBinder
    {

        /// <summary>
        /// 初始化 <see cref="PropertyModelBinder"/> 类的新实例。
        /// </summary>
        public PropertyModelBinder()
        {

        }

        /// <summary>
        /// 使用指定的控制器上下文和绑定上下文来绑定模型。
        /// </summary>
        /// <param name="controllerContext">运行控制器的上下文。</param>
        /// <param name="bindingContext">绑定模型的上下文。</param>
        /// <returns>已绑定的对象。</returns>
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var model = base.BindModel(controllerContext, bindingContext);
          //  if (model is BaseEntiryModel) ((BaseEntiryModel)model).BindModel(controllerContext, bindingContext);
            return model;
        }

        /// <summary>
        ///  使用指定的控制器上下文、绑定上下文、属性描述符和属性联编程序来返回属性值。
        /// </summary>
        /// <param name="controllerContext">运行控制器的上下文。</param>
        /// <param name="bindingContext">绑定模型的上下文。</param>
        /// <param name="propertyDescriptor">要访问的属性的描述符。</param>
        /// <param name="propertyBinder">一个对象，提供用于绑定属性的方式。</param>
        /// <returns>一个对象，表示属性值。</returns>
        protected override object GetPropertyValue(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor, IModelBinder propertyBinder)
        {
            var value = base.GetPropertyValue(controllerContext, bindingContext, propertyDescriptor, propertyBinder);

            return value;
        }

        /// <summary>
        /// 使用指定的控制器上下文、绑定上下文和指定的属性描述符来绑定指定的属性。
        /// </summary>
        /// <param name="controllerContext">运行控制器的上下文。</param>
        /// <param name="bindingContext">绑定模型的上下文。</param>
        /// <param name="propertyDescriptor">描述要绑定的属性。</param>
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor)
        {
            string fullPropertyKey = CreateSubPropertyName(bindingContext.ModelName, propertyDescriptor.Name);
            object propertyValue = null;

            if (propertyDescriptor.Attributes[typeof(PropertyModelBinderAttribute)] != null)
            {
                var attribute = (PropertyModelBinderAttribute)propertyDescriptor.Attributes[typeof(PropertyModelBinderAttribute)];
                string propertyName = attribute.PropertyName;
                var valueResult = bindingContext.ValueProvider.GetValue(propertyName);

                if (valueResult != null)
                    propertyValue = valueResult.AttemptedValue;
            }
            else
            {
                if (!bindingContext.ValueProvider.ContainsPrefix(fullPropertyKey))
                {
                    return;
                }
            }

            // call into the property's model binder
            IModelBinder propertyBinder = Binders.GetBinder(propertyDescriptor.PropertyType);
            object originalPropertyValue = propertyDescriptor.GetValue(bindingContext.Model);
            ModelMetadata propertyMetadata = bindingContext.PropertyMetadata[propertyDescriptor.Name];
            propertyMetadata.Model = originalPropertyValue;
            ModelBindingContext innerBindingContext = new ModelBindingContext()
            {
                ModelMetadata = propertyMetadata,
                ModelName = fullPropertyKey,
                ModelState = bindingContext.ModelState,
                ValueProvider = bindingContext.ValueProvider
            };
            object newPropertyValue = GetPropertyValue(controllerContext, innerBindingContext, propertyDescriptor, propertyBinder);
            if (newPropertyValue == null)
            {
                newPropertyValue = propertyValue;
            }

            propertyMetadata.Model = newPropertyValue;
            // validation
            ModelState modelState = bindingContext.ModelState[fullPropertyKey];

            if (modelState == null || modelState.Errors.Count == 0)
            {
                if (OnPropertyValidating(controllerContext, bindingContext, propertyDescriptor, newPropertyValue))
                {
                    SetProperty(controllerContext, bindingContext, propertyDescriptor, newPropertyValue);
                    OnPropertyValidated(controllerContext, bindingContext, propertyDescriptor, newPropertyValue);
                }
            }
            else
            {
                SetProperty(controllerContext, bindingContext, propertyDescriptor, newPropertyValue);

                // Convert FormatExceptions (type conversion failures) into InvalidValue messages
                foreach (ModelError error in modelState.Errors.Where(err => String.IsNullOrEmpty(err.ErrorMessage) && err.Exception != null).ToList())
                {
                    for (Exception exception = error.Exception; exception != null; exception = exception.InnerException)
                    {
                        // We only consider "known" type of exception and do not make too aggressive changes here
                        if (exception is FormatException || exception is OverflowException)
                        {
                            string displayName = propertyMetadata.GetDisplayName();
                            string errorMessageTemplate = GetValueInvalidResource(controllerContext);
                            string errorMessage = String.Format(CultureInfo.CurrentCulture, errorMessageTemplate, modelState.Value.AttemptedValue, displayName);
                            modelState.Errors.Remove(error);
                            modelState.Errors.Add(errorMessage);
                            break;
                        }
                    }
                }
            }
            //base.BindProperty(controllerContext, bindingContext, propertyDescriptor);
        }

        /// <summary>
        /// 使用指定的控制器上下文、绑定上下文和属性值来设置指定的属性。
        /// </summary>
        /// <param name="controllerContext">运行控制器的上下文。</param>
        /// <param name="bindingContext">绑定模型的上下文。</param>
        /// <param name="propertyDescriptor">描述要绑定的属性。</param>
        /// <param name="value">为属性设置的值。</param>
        protected override void SetProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, PropertyDescriptor propertyDescriptor, object value)
        {
            ModelMetadata propertyMetadata = bindingContext.PropertyMetadata[propertyDescriptor.Name];
            propertyMetadata.Model = value;
            string modelStateKey = CreateSubPropertyName(bindingContext.ModelName, propertyMetadata.PropertyName);

            if (value == null && bindingContext.ModelState.IsValidField(modelStateKey))
            {
                ModelValidator requiredValidator = ModelValidatorProviders.Providers.GetValidators(propertyMetadata, controllerContext).Where(v => v.IsRequired).FirstOrDefault();
                if (requiredValidator != null)
                {
                    foreach (ModelValidationResult validationResult in requiredValidator.Validate(bindingContext.Model))
                    {
                        bindingContext.ModelState.AddModelError(modelStateKey, validationResult.Message);
                    }
                }
            }

            bool isNullValueOnNonNullableType = value == null && !TypeAllowsNullValue(propertyDescriptor.PropertyType);

            if (!propertyDescriptor.IsReadOnly && !isNullValueOnNonNullableType)
            {
                try
                {
                    var typeValue = Convert.ChangeType(value, propertyDescriptor.PropertyType, CultureInfo.InvariantCulture);
                    propertyDescriptor.SetValue(bindingContext.Model, typeValue);
                }
                catch (Exception ex)
                {
                    if (bindingContext.ModelState.IsValidField(modelStateKey))
                    {
                        bindingContext.ModelState.AddModelError(modelStateKey, ex);
                    }
                }
            }

            if (isNullValueOnNonNullableType && bindingContext.ModelState.IsValidField(modelStateKey))
            {
                bindingContext.ModelState.AddModelError(modelStateKey, GetValueRequiredResource(controllerContext));
            }
        }



        /// <summary>
        /// 使用指定的控制器上下文和绑定上下文来返回模型的属性。
        /// </summary>
        /// <param name="controllerContext">运行控制器的上下文。</param>
        /// <param name="bindingContext">绑定模型的上下文。</param>
        /// <returns>属性描述符的集合。</returns>
        protected override PropertyDescriptorCollection GetModelProperties(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            bindingContext.PropertyFilter = new Predicate<string>(pred);
            var values = base.GetModelProperties(controllerContext, bindingContext);
            return values;
        }

        /// <summary>
        /// 获取属性筛选器的判定对象。
        /// </summary>
        /// <param name="target">属性筛选器的属性。</param>
        /// <returns>一个布尔值。</returns>
        protected bool pred(string target)
        {
            return true;
        }

        #region Private ...

        /// <summary>
        /// 类型允许空值。
        /// </summary>
        /// <param name="type">指定的类型。</param>
        /// <returns>若类型值为空，则返回 true，否则返回 false。</returns>
        private static bool TypeAllowsNullValue(Type type)
        {
            return (!type.IsValueType || IsNullableValueType(type));
        }

        /// <summary>
        /// 是可为空值类型。
        /// </summary>
        /// <param name="type">指定的类型。</param>
        /// <returns>若类型值为空，则返回 true，否则返回 false。</returns>
        private static bool IsNullableValueType(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// 获取价值无效的资源。
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        private static string GetValueInvalidResource(ControllerContext controllerContext)
        {
            return GetUserResourceString(controllerContext, "PropertyValueInvalid") ?? "The value '{0}' is not valid for {1}.";
        }

        /// <summary>
        /// 获取价值所需的资源。
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <returns></returns>
        private static string GetValueRequiredResource(ControllerContext controllerContext)
        {
            return GetUserResourceString(controllerContext, "PropertyValueRequired") ?? "A value is required.";
        }

        private static string GetUserResourceString(ControllerContext controllerContext, string resourceName)
        {
            string result = null;

            if (!String.IsNullOrEmpty(ResourceClassKey) && (controllerContext != null) && (controllerContext.HttpContext != null))
            {
                result = controllerContext.HttpContext.GetGlobalResourceObject(ResourceClassKey, resourceName, CultureInfo.CurrentUICulture) as string;
            }

            return result;
        }

        #endregion

    }
}

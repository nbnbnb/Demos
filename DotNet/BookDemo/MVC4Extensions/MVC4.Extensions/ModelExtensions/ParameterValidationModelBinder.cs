using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Extensions.ModelExtensions
{
    public class ParameterValidationModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object model = bindingContext.ModelMetadata.Model =
                base.BindModel(controllerContext, bindingContext);

            ModelMetadata modelMetadata = bindingContext.ModelMetadata;

            if (modelMetadata.IsComplexType || null == model)
            {
                return model;
            }

            // 下面开始针对简单类型的验证
            // ***********************
            Dictionary<string, bool> dictionary =
                new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

            IEnumerable<ModelValidationResult> res = ModelValidator
                .GetModelValidator(modelMetadata, controllerContext).Validate(model);

            foreach (ModelValidationResult result in res)
            {
                // 参数名
                string key = bindingContext.ModelName;

                // 首先判断这个缓存中是否存在指定的键
                if (!dictionary.ContainsKey(key))
                {
                    // 如果不存在这个键
                    // 再获取是否有这个键的 Error 对象
                    dictionary[key] = bindingContext.ModelState.IsValidField(key);
                }
                // 如果有这个键的 Error 对象
                if (dictionary[key])
                {
                    bindingContext.ModelState.AddModelError(key, result.Message);
                }
            }

            return model;
        }
    }
}
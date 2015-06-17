using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApp.Models
{
    public class CustomModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return this.GetModel(controllerContext, bindingContext.ModelType, bindingContext.ValueProvider, bindingContext.ModelName);
        }

        private object GetModel(ControllerContext controllerContext, Type modelType,
            IValueProvider valueProvider, string key)
        {
            if (!valueProvider.ContainsPrefix(key))
            {
                return null;
            }

            return valueProvider.GetValue(key).ConvertTo(modelType);
        }

        private object CreateObject(Type modelType)
        {
            Type type = modelType;
            if (modelType.IsGenericType)
            {

            }

            return Activator.CreateInstance(type);
        }
    }
}
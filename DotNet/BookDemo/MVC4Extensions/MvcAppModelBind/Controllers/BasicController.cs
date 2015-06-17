using MVC4.Extensions.ModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcAppModelBind.Controllers
{
    public abstract class BasicController : Controller
    {
        public IModelBinder ModelBinder { get; private set; }

        public BasicController()
        {
            this.ModelBinder = new CustomModelBinder();
            this.ValueProvider = this.CreateValueProvider();
        }

        protected abstract IValueProvider CreateValueProvider();

        protected ActionResult InvokeAction(string actionName)
        {
            ControllerDescriptor controllerDescription =
                new ReflectedControllerDescriptor(this.GetType());

            ActionDescriptor actionDescriptor = controllerDescription.FindAction(ControllerContext, actionName);

            // 方法的参数列表
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            foreach (ParameterDescriptor parameterDescriptor in actionDescriptor.GetParameters())
            {
                // 如果没有显式指定 BindAttribute，就使用参数的名称
                string modelName = parameterDescriptor.BindingInfo.Prefix
                    ?? parameterDescriptor.ParameterName;

                // 创建 ModelBindingContext，用于 BindModel 方法获取参数值
                ModelBindingContext bindingContext = new ModelBindingContext
                {
                    // 是否使用祛前缀比较
                    FallbackToEmptyPrefix =
                        parameterDescriptor.BindingInfo.Prefix == null,
                    ModelMetadata = ModelMetadataProviders.Current
                    .GetMetadataForType(null, parameterDescriptor.ParameterType),
                    ModelName = modelName,
                    ModelState = ModelState,
                    ValueProvider = this.ValueProvider
                };

                // 将获取的参数值添加到参数列表中
                parameters.Add(parameterDescriptor.ParameterName, this.ModelBinder
                    .BindModel(ControllerContext, bindingContext));
            }

            // 执行 Action
            return (ActionResult)actionDescriptor.Execute(ControllerContext, parameters);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Extensions.ModelExtensions
{
    public class ParameterValidationModelValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        private const string PK = "ParameterDescriptor";

        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            object descriptor;

            // ContainerType == null 表示
            // 针对最外层的容器类型，而不是容器类型的属性
            if (metadata.ContainerType == null && context.RouteData.DataTokens.TryGetValue(PK, out descriptor))
            {
                ParameterDescriptor parameterDescriptor = descriptor as ParameterDescriptor;

                // 这里还重定义 DisplayAttribute
                DisplayAttribute displayAttribute = parameterDescriptor
                    .GetCustomAttributes(true).OfType<DisplayAttribute>()
                    .FirstOrDefault()
                    ?? new DisplayAttribute { Name = parameterDescriptor.ParameterName };

                metadata.DisplayName = displayAttribute.Name;

                // 合并参数上定义的特性
                var addedAttributes = parameterDescriptor.GetCustomAttributes(true)
                    .OfType<Attribute>();

                return base.GetValidators(metadata, context, attributes.Union(addedAttributes));

            }

            return base.GetValidators(metadata, context, attributes);
        }
    }
}
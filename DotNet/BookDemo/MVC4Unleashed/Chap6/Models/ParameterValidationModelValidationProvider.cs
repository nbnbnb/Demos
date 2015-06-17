using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap6.Models
{
    public class ParameterValidationModelValidatorProvider
        : DataAnnotationsModelValidatorProvider
    {
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context,
            IEnumerable<Attribute> attributes)
        {
            object descriptor;
            if (metadata.ContainerType == null &&
                context.RouteData.DataTokens.TryGetValue("ParameterDescriptor", out descriptor))
            {
                ParameterDescriptor parameterDescriptor =
                    (ParameterDescriptor)descriptor;
                DisplayAttribute displayAttribute =
                    parameterDescriptor.GetCustomAttributes(true)
                    .OfType<DisplayAttribute>().FirstOrDefault()
                    ?? new DisplayAttribute { Name = parameterDescriptor.ParameterName };

                // ErrorMessage 将会使用此参数作为 FormatErrorMessage 的 name 属性
                metadata.DisplayName = displayAttribute.Name;

                var addedAttributes = parameterDescriptor.GetCustomAttributes(true)
                    .OfType<Attribute>();
                return base.GetValidators(metadata, context, attributes.Union(addedAttributes));
            }
            else
            {
                return base.GetValidators(metadata, context, attributes);
            }
        }
    }
}
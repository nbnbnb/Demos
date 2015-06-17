using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Extensions.ModelExtensions
{
    public class CustomCompositeModelValidator : ModelValidator
    {
        public CustomCompositeModelValidator(ModelMetadata modelMetadata, ControllerContext controllerContext)
            : base(modelMetadata, controllerContext)
        {

        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            bool isPropertiesValid = true;
            // 首先查找属性的元数据
            foreach (ModelMetadata propertyMetadata in Metadata.Properties)
            {
                // 然后迭代属性上的每一个验证器
                foreach (ModelValidator validator in propertyMetadata.GetValidators(this.ControllerContext))
                {
                    // 得到属性上的验证结果
                    IEnumerable<ModelValidationResult> results =
                        validator.Validate(propertyMetadata.Model);

                    // 如果结果不为空，则表示验证失败
                    if (results.Any())
                    {
                        // 设置属性验证失败
                        isPropertiesValid = false;
                    }

                    // 返回验证结果迭代器
                    foreach (ModelValidationResult result in results)
                    {
                        yield return new ModelValidationResult
                        {
                            // 如果对于容器对象的某个属性验证来说，属性名称作为 MemberName 属性
                            // 此处传递的 result.Member==""
                            MemberName = CustomModelBinder.CreateSubPropertyName(propertyMetadata.PropertyName, result.MemberName),
                            Message = result.Message
                        };
                    }
                }
            }

            // 如果属性验证通过了，才会对自身进行验证
            if (isPropertiesValid)
            {
                // Metadata 属性是类型上的元数据
                // 所以这个方法是获取的类型上的验证列表
                foreach (ModelValidator validator in Metadata.GetValidators(this.ControllerContext))
                {
                    IEnumerable<ModelValidationResult> results =
                        validator.Validate(Metadata.Model);

                    // 返回验证结果迭代器
                    foreach (ModelValidationResult result in results)
                    {
                        yield return result;
                    }
                }
            }
        }
    }
}
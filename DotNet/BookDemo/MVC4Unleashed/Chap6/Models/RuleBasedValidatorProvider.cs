using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap6.Models
{
    public class RuleBasedValidatorProvider :
        DataAnnotationsModelValidatorProvider
    {
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata,
            ControllerContext context, IEnumerable<Attribute> attributes)
        {
            object validationRuleName = string.Empty;
            context.RouteData.DataTokens.TryGetValue("ValidationRuleName", out validationRuleName);
            string ruleName = validationRuleName.ToString();
            attributes = this.FilterAttributes(attributes, ruleName);

            return base.GetValidators(metadata, context, attributes);
        }

        private IEnumerable<Attribute> FilterAttributes(IEnumerable<Attribute> attributes, string ruleName)
        {
            var validatorAttributes = attributes.OfType<ValidatorAttribute>();
            var nonValidatorAttributes = attributes.Except(validatorAttributes);

            List<ValidatorAttribute> validValidatorAttributes =
                new List<ValidatorAttribute>();

            // 如果当前的验证规则没有指定
            if (string.IsNullOrEmpty(ruleName))
            {
                // 则选择第一个没有指定验证规则的 ValidatorAttribute
                validValidatorAttributes
                    .Add(
                        validValidatorAttributes
                            .Where(v => string.IsNullOrEmpty(v.RuleName))
                            .FirstOrDefault());
            }
            else  // 如果当前的验证规则存在
            {
                var groups = from validator in validatorAttributes
                             group validator by validator.GetType();
                foreach (var group in groups)
                {
                    // 选择与之具有相同规则名称的第一个 ValidatorAttribute
                    ValidatorAttribute validatorAttribute =
                        group.Where(v => string.Compare(v.RuleName, ruleName, true) == 0)
                        .FirstOrDefault();
                    if (null != validatorAttribute)
                    {
                        validValidatorAttributes.Add(validatorAttribute);
                    }
                    else  // 如果没有找到
                    {
                        // 则选则第一个没有指定验证规则的 ValidatorAttribute
                        validatorAttribute = group.Where(v => string.IsNullOrEmpty(v.RuleName)).FirstOrDefault();
                        if (null != validatorAttribute)
                        {
                            validValidatorAttributes.Add(validatorAttribute);
                        }
                    }
                }
            }

            return nonValidatorAttributes.Union(validValidatorAttributes);
        }
    }
}
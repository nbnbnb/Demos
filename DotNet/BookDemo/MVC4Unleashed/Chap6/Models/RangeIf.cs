using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Chap6.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeIfAttribute : RangeAttribute
    {

        private object typeid;

        public override object TypeId
        {
            get
            {
                return typeid ?? (typeid = new object());
            }
        }

        public string Property { get; set; }

        public string Value { get; set; }

        public RangeIfAttribute(string property, string value,
            double minimum, double maximum)
            : base(minimum, maximum)
        {
            this.Property = property;
            this.Value = value ?? "";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property =
                validationContext.ObjectType.GetProperty(this.Property);

            // 获取验证对象实例中指定属性的值
            object propertyValue =
                property.GetValue(validationContext.ObjectInstance, null);
            propertyValue = propertyValue ?? "";

            // 如果与特性中设置的值相等，则表示需要进行验证
            // 否则验证通过
            // 返回 null 也可以
            if (propertyValue.ToString() != this.Value)
            {
                //return ValidationResult.Success;
                return null;    
            }

            return base.IsValid(value, validationContext);
        }
    }
}
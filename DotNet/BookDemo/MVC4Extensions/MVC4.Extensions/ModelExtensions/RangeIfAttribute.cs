using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MVC4.Extensions.ModelExtensions
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class RangeIfAttribute : RangeAttribute
    {
        private object _typeId = new object();

        public string PropertyName { get; set; }

        public string Value { get; set; }

        public RangeIfAttribute(string propertyName, string value,
            double minimum, double maximum)
            : base(minimum, maximum)
        {
            this.PropertyName = propertyName;
            this.Value = value;
        }

        public override object TypeId
        {
            get
            {
                return _typeId;
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo propertyInfo = validationContext.ObjectType.GetProperty(this.PropertyName);
            object propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

            propertyValue = propertyValue ?? "";

            if (propertyValue.ToString() != this.Value)
            {
                // 与标记的不符，不执行此次验证
                return ValidationResult.Success;
            }

            // 复合条件，使用基类的认证
            return base.IsValid(value, validationContext);
        }
    }
}
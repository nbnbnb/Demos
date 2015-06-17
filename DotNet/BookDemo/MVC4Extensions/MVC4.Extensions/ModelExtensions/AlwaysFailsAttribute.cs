using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC4.Extensions.ModelExtensions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class AlwaysFailsAttribute : ValidationAttribute
    {
        private object _typeId;

        public override bool IsValid(object value)
        {
            return false;
        }

        public override object TypeId
        {
            get
            {
                return _typeId ?? (_typeId = new object());
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chap6.Models
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ValidationRuleAttribute : Attribute
    {
        public string RuleName { get; private set; }

        public ValidationRuleAttribute(string ruleName)
        {
            RuleName = ruleName;
        }
    }
}
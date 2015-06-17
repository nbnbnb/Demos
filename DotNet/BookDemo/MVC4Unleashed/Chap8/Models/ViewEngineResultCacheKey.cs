using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chap8.Models
{
    internal sealed class ViewEngineResultCacheKey
    {
        public string ControllerName { get; set; }

        public string ViewName { get; set; }

        public ViewEngineResultCacheKey(string controllerName, string viewName)
        {
            this.ControllerName = controllerName;
            this.ViewName = viewName;
        }

        public override int GetHashCode()
        {
            return this.ControllerName.ToLower().GetHashCode()
                ^ this.ViewName.ToLower().GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ViewEngineResultCacheKey key = obj as ViewEngineResultCacheKey;
            if (null == key)
            {
                return false;
            }
            return key.GetHashCode() == this.GetHashCode();
        }
    }
}
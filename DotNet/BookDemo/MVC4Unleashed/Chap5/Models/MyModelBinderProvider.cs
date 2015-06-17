using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chap5.Models
{
    public class MyModelBinderProvider:IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            if (modelType == typeof(Foo))
            {
                return new FooModelBinder();
            }
            if (modelType == typeof(Bar))
            {
                return new BarModelBinder();
            }
            if (modelType == typeof(Baz))
            {
                return new BazModelBinder();
            }

            return null;
        }
    }
}
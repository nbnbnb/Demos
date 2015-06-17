using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Extensions.ModelExtensions
{
    public class ExtendedDataAnnotationsProvider : CachedDataAnnotationsModelMetadataProvider
    {
        /*  由于通过下面的方法，重写了缓存对象中的 DisplayName，所以这段代码可以注释掉
        protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
        {
            var modelMetadata = base.CreateMetadataFromPrototype(prototype, modelAccessor);

            // 通过下面的代码可以判断，在原型对象中，共用的是 PrototypeCache 属性
            // 由于 PrototypeCache 没有进行设置值
            // 所以此处需要在从原型中获取属性之后，在进行一次赋值操作
            // 同样，也可以在下面的原型操作中对  PrototypeCache 进行赋值，从而省略此方法
            var abc = typeof(CachedDataAnnotationsModelMetadata)
                .GetProperty("PrototypeCache", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(modelMetadata, null);
            var efg = typeof(CachedDataAnnotationsModelMetadata)
                .GetProperty("PrototypeCache", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(modelMetadata, null);

            bool xyz = object.ReferenceEquals(abc, efg);  // True

            modelMetadata.DisplayName = prototype.DisplayName;
            return modelMetadata;
        }
         * */

        protected override CachedDataAnnotationsModelMetadata CreateMetadataPrototype(IEnumerable<Attribute> attributes, Type containerType, Type modelType, string propertyName)
        {
            var modelMetadata = base.CreateMetadataPrototype(attributes, containerType, modelType, propertyName);
            if (String.IsNullOrEmpty(modelMetadata.DisplayName))
            {
                // 获取第一个自定义的特性
                DisplayTextAttribute displayTextAttribute = attributes.OfType<DisplayTextAttribute>().FirstOrDefault();

                if (null != displayTextAttribute)
                {
                    displayTextAttribute.SetDisplayName(modelMetadata);
                }
            }

            return modelMetadata;
        }
    }
}
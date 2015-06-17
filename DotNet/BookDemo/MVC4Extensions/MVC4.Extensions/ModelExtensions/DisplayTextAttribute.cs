using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Extensions.ModelExtensions
{
    /// <summary>
    /// 当 Model 元数据被创建出来后，会初始化一个 ModelMetadata 对象
    /// 当这个对象初始化完成之后，会获取应用在目标元素上所有实现了 IMetadataAware 的特性
    /// 将 ModelMetadata 对象作为参数调用 OnMetadataCreated 方法
    /// 所以，通过自定义该接口的特性，不仅仅可以添加一些额外的元数据属性
    /// 还可以修改已经通过相应的标准特性初始化的相关属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class DisplayTextAttribute : Attribute  // 测试 ExtendedDataAnnotationsProvider
    //public class DisplayTextAttribute : Attribute, IMetadataAware // 测试 IMetadataAware
    {
        private static Type _staticResourceType;

        public string DisplayName { get; set; }

        public Type ResourceType { get; set; }

        public DisplayTextAttribute()
        {
            this.ResourceType = _staticResourceType;
        }

        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.DisplayName = GetDisplayName(metadata);
        }

        private string GetDisplayName(ModelMetadata metadata)
        {
            this.DisplayName = this.DisplayName ??   // 首先检查是否指定了 DisplayName
                // 在检测是否有属性名称
                // 如果没有属性名称，则表示这是一个类型，所以获取类型的名称
                (metadata.PropertyName ?? metadata.ModelType.Name);

            // 如果没有设置资源类型
            // 则使用上面的逻辑获得的 DisplayName
            if (null == this.ResourceType)
            {
                return this.DisplayName;
            }

            // 获取通过 DisplayName 表示的资源键
            PropertyInfo property = this.ResourceType.GetProperty(this.DisplayName,
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

            // 最后重新设置 DisplayName
            return property.GetValue(null, null).ToString();
        }

        public static void SetResourceType(Type resourceType)
        {
            _staticResourceType = resourceType;
        }

        public void SetDisplayName(CachedDataAnnotationsModelMetadata modelMetadata)
        {
            modelMetadata.DisplayName = GetDisplayName(modelMetadata);

            // 设置缓存对象的值

            CachedDataAnnotationsMetadataAttributes cachedObject = typeof(CachedDataAnnotationsModelMetadata)
                       .GetProperty("PrototypeCache", BindingFlags.NonPublic | BindingFlags.Instance)
                       .GetValue(modelMetadata, null) as CachedDataAnnotationsMetadataAttributes;

            if (null != cachedObject)
            {
                // 由于 DisplayName 受保护，所以此处用反射
                typeof(CachedDataAnnotationsMetadataAttributes)
                    .GetProperty("DisplayName", BindingFlags.Instance | BindingFlags.Public)
                    .SetValue(cachedObject, new DisplayNameAttribute(modelMetadata.DisplayName), null);
            }
        }
    }
}
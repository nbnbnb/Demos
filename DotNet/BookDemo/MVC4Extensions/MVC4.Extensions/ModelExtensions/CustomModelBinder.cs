using MVC4.Extensions.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC4.Extensions.ModelExtensions
{
    public class CustomModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object model = this.GetModel(controllerContext, bindingContext.ModelType,
                bindingContext.ValueProvider, bindingContext.ModelName);

            // 如果无法获得对象，并且指定可以剔除前缀进行绑定
            // 传递一个空键，表示剔除前缀
            if (bindingContext.FallbackToEmptyPrefix && null == model)
            {
                model = this.GetModel(controllerContext, bindingContext.ModelType, bindingContext.ValueProvider, "");
            }

            return model;
        }

        /// <summary>
        /// 对于简单类型 key 表示参数名称或参数上应用的 BindAttribute 指定的前缀
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="modelType"></param>
        /// <param name="valueProvider"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetModel(ControllerContext controllerContext, Type modelType,
            IValueProvider valueProvider, string key)
        {
            if (!valueProvider.ContainsPrefix(key))
            {
                return null;
            }

            // 通过类型的元数据，判断是不是复合类$型
            ModelMetadata modelMetadata = ModelMetadataProviders.Current
                .GetMetadataForType(null, modelType);

            if (!modelMetadata.IsComplexType)
            {
                return valueProvider.GetValue(key).ConvertTo(modelType);
            }

            if (modelType.IsArray)
            {
                return GetArrayModel(controllerContext, modelType, valueProvider, key);
            }

            // 由于字典也实现了 IEnumerable<T>，所以下面的代码要放在 GetCollectionModel 之前
            Type dictionaryType = ExtractGenericInterface(modelType, typeof(IDictionary<,>));
            if (null != dictionaryType)
            {
                return GetDictionaryModel(controllerContext, modelType, valueProvider, key);
            }

            // 由于数组也实现了 IEnumerable<T> 接口，所以需要将下面的代码放在针对数组绑定的 GetArrayModel 之后
            Type enumerableType = ExtractGenericInterface(modelType, typeof(IEnumerable<>));
            if (null != enumerableType)
            {
                return GetCollectionModel(controllerContext, modelType, valueProvider, key);
            }

            return GetComplexModel(controllerContext, modelType, valueProvider, key);
        }

        private Type ExtractGenericInterface(Type queryType, Type interfaceType)
        {
            Func<Type, bool> predicate = t => t.IsGenericType && (t.GetGenericTypeDefinition() == interfaceType);
            if (!predicate(queryType))
            {
                // 再查询一次接口
                return queryType.GetInterfaces().FirstOrDefault<Type>(predicate);
            }
            return queryType;
        }

        // 获得文字索引
        private IEnumerable<string> GetIndexes(string prefix, IValueProvider valueProvider, out bool numericIndex)
        {
            string key = string.IsNullOrEmpty(prefix) ? "index" : prefix + "." + "index";
            ValueProviderResult result = valueProvider.GetValue(key);

            if (null != result)
            {
                // 进行索引转换
                string[] indexes = result.ConvertTo(typeof(string[])) as string[];
                if (null != indexes)
                {
                    numericIndex = false;
                    return indexes;
                }
            }

            // 0 基索引
            numericIndex = true;
            return GetZeroBasedIndexes();
        }

        private static IEnumerable<string> GetZeroBasedIndexes()
        {
            int iteratorVariable = 0;
            while (true)
            {
                yield return iteratorVariable.ToString();
                iteratorVariable++;
            }
        }

        private object GetArrayModel(ControllerContext controllerContext,
            Type modelType, IValueProvider valueProvider, string key)
        {
            List<object> list = GetListModel(controllerContext, modelType, modelType.GetElementType(), valueProvider, key);

            object[] array = (object[])Array.CreateInstance(modelType.GetElementType(), list.Count);
            list.CopyTo(array);

            return array;
        }

        private List<object> GetListModel(ControllerContext controllerContext, Type modelType,
            Type elementType, IValueProvider valueProvider, string key)
        {
            List<object> list = new List<object>();
            if (!string.IsNullOrEmpty(key) && valueProvider.ContainsPrefix(key))
            {
                ValueProviderResult result = valueProvider.GetValue(key);
                if (null != result)
                {
                    // 数组实现了这个接口
                    IEnumerable enumerable = result.ConvertTo(modelType) as IEnumerable;

                    foreach (var value in enumerable)
                    {
                        list.Add(value);
                    }
                }
            }

            bool numericIndex;
            IEnumerable<string> indexes = GetIndexes(key, valueProvider, out numericIndex);
            foreach (var index in indexes)
            {
                string indexPrefix = key + "[" + index + "]";

                // 这里的意思是
                // 当为文字索引时，是可以获得值的，不用 break
                // 只有当为 0 基索引，又无法获得值时，才 break
                if (!valueProvider.ContainsPrefix(indexPrefix) && numericIndex)
                {
                    break;
                }
                list.Add(GetModel(controllerContext, elementType, valueProvider, indexPrefix));
            }

            return list;
        }



        private object CreateModel(Type modelType)
        {
            Type type = modelType;

            // 如果是泛型的对象
            if (modelType.IsGenericType)
            {
                Type genericTypeDefinition = modelType.GetGenericTypeDefinition();
                // 如果是泛型字典接口 IDictionary<,>
                // 都创建为 Dictionary<>
                if (genericTypeDefinition == typeof(IDictionary<,>))
                {
                    type = typeof(Dictionary<,>).MakeGenericType(modelType.GetGenericArguments());
                }
                // 如果是泛型接口 IEnumerable ICollection IList
                // 都创建一个  List<>
                else if (genericTypeDefinition == typeof(IEnumerable<>) ||
                   genericTypeDefinition == typeof(ICollection<>) ||
                   genericTypeDefinition == typeof(IList<>))
                {
                    type = typeof(List<>).MakeGenericType(modelType.GetGenericArguments());
                }
            }

            return Activator.CreateInstance(type);
        }

        protected virtual object GetComplexModel(ControllerContext controllerContext,
            Type modelType, IValueProvider valueProvider, string prefix)
        {
            // 复合类型，首先创建实例
            object model = CreateModel(modelType);

            // 迭代其中的每一个属性
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(modelType))
            {
                // 如果属性只读，跳过赋值
                if (property.IsReadOnly)
                {
                    continue;
                }

                // 生成键
                string key = string.IsNullOrEmpty(prefix) ? property.Name :
                    prefix + "." + property.Name;

                // 对属性进行赋值
                // 里面调用了 GetModel 方法
                // 此处将会发送递归调用【GetModel 再调用 GetComplexModel】
                property.SetValue(model, GetModel(controllerContext, property.PropertyType, valueProvider, key));
            }

            // Model 的验证
            // 首先验证类型
            // 创建类型对象的元数据
            ModelMetadata metadata = ModelMetadataProviders.Current
                .GetMetadataForType(() => model, modelType);

            // 使用自定义的 ModelValidator 对类型对象进行验证
            CustomCompositeModelValidator validator = new CustomCompositeModelValidator(metadata, controllerContext);

            foreach (ModelValidationResult result in validator.Validate(model))
            {
                string key = CreateSubPropertyName(prefix, result.MemberName);
                controllerContext.Controller.ViewData.ModelState.AddModelError(key, result.Message);
            }

            return model;
        }

        protected virtual object GetCollectionModel(ControllerContext controllerContext, Type modelType,
            IValueProvider valueProvider, string prefix)
        {
            Type elementType = modelType.GetGenericArguments()[0];

            // 多个同名的数据项，同样也适合
            List<Object> list = GetListModel(controllerContext, modelType, elementType, valueProvider, prefix);

            // 创建集合类型
            object model = CreateModel(modelType);

            ReplaceHelper.ReplaceCollectionImpl(elementType, model, list);

            return model;
        }

        protected virtual object GetDictionaryModel(ControllerContext controllerContext,
            Type modelType, IValueProvider valueProvider, string prefix)
        {
            List<KeyValuePair<object, object>> list = new List<KeyValuePair<object, object>>();
            bool numericIndex;

            IEnumerable<string> indexes = GetIndexes(prefix, valueProvider, out numericIndex);

            Type[] genericArguments = modelType.GetGenericArguments();
            Type keyType = genericArguments[0];
            Type valueType = genericArguments[1];

            foreach (var index in indexes)
            {
                string indexPrefix = prefix + "[" + index + "]";
                if (!valueProvider.ContainsPrefix(indexPrefix) && numericIndex)
                {
                    break;
                }

                string keyPrefix = indexPrefix + ".Key";
                string valuePrefix = indexPrefix + ".Value";

                object key = GetModel(controllerContext, keyType, valueProvider, keyPrefix);
                object value = GetModel(controllerContext, valueType, valueProvider, valuePrefix);

                list.Add(new KeyValuePair<object, object>(key, value));
            }

            object model = CreateModel(modelType);
            ReplaceHelper.ReplaceDictionaryImpl(keyType, valueType, model, list);

            return model;
        }

        internal static string CreateSubPropertyName(string prefix, string propertyName)
        {
            prefix = prefix ?? "";
            propertyName = propertyName ?? "";
            return (prefix + "." + propertyName).Trim('.');
        }
    }
}
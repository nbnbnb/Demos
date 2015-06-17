using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Chap5.Models
{
    public class DefaultModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            object model = this.GetModel(controllerContext, bindingContext.ModelType,
                bindingContext.ValueProvider, bindingContext.ModelName);

            if (bindingContext.FallbackToEmptyPrefix && null == model)
            {
                model = this.GetModel(controllerContext, bindingContext.ModelType,
                    bindingContext.ValueProvider, "");
            }

            return model;
        }

        public object GetModel(ControllerContext controllerContext,
            Type modelType, IValueProvider valueProvider, string key)
        {
            if (!valueProvider.ContainsPrefix(key))
            {
                return null;
            }

            if (modelType.IsArray)
            {
                return GetArrayModel(controllerContext, modelType, valueProvider, key);
            }

            Type enumerableType = ExtractGenericInterface(modelType, typeof(IEnumerable<>));
            if (null != enumerableType)
            {
                return GetCollectionModel(controllerContext, modelType, valueProvider, key);
            }

            ModelMetadata modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, modelType);
            if (!modelMetadata.IsComplexType)
            {
                return valueProvider.GetValue(key).ConvertTo(modelType);
            }
            if (modelMetadata.IsComplexType)
            {
                return GetComplexModel(controllerContext, modelType, valueProvider, key);
            }

            return null;
        }

        private Type ExtractGenericInterface(Type queryType, Type interfaceType)
        {
            Func<Type, bool> predicate = t => t.IsGenericType && (t.GetGenericTypeDefinition() == interfaceType);

            if (!predicate(queryType))
            {
                return queryType.GetInterfaces().FirstOrDefault<Type>(predicate);
            }

            return queryType;
        }

        private object CreateModel(Type modelType)
        {
            Type type = modelType;
            if (modelType.IsGenericType)
            {
                Type genericTypeDefinition = modelType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(IDictionary<,>))
                {
                    type = typeof(Dictionary<,>).MakeGenericType(modelType.GetGenericArguments());
                }
                else if (genericTypeDefinition == typeof(IEnumerable<>)
                    || genericTypeDefinition == typeof(ICollection<>)
                    || genericTypeDefinition == typeof(IList<>))
                {
                    type = typeof(List<>).MakeGenericType(modelType.GetGenericArguments());
                }
            }

            return Activator.CreateInstance(type);
        }

        protected virtual object GetComplexModel(ControllerContext controllerContext, Type modelType,
            IValueProvider valueProvider, string prefix)
        {
            object model = CreateModel(modelType);
            foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(modelType))
            {
                if (property.IsReadOnly)
                {
                    continue;
                }
                string key = string.IsNullOrEmpty(prefix) ? property.Name : prefix + "." + property.Name;
                property.SetValue(model, GetModel(controllerContext, property.PropertyType, valueProvider, key));
            }

            return model;
        }

        protected virtual object GetArrayModel(
            ControllerContext controllerContext, Type modelType, IValueProvider valueProvider, string prefix)
        {
            List<object> list = GetListModel(controllerContext, modelType, modelType.GetElementType(), valueProvider, prefix);
            object[] array = (object[])Array.CreateInstance(modelType.GetElementType(), list.Count);
            list.CopyTo(array);
            return array;
        }

        private List<object> GetListModel(ControllerContext controllerContext, Type modelType, Type type, IValueProvider valueProvider, string prefix)
        {
            List<object> list = new List<object>();
            if (!String.IsNullOrEmpty(prefix) && valueProvider.ContainsPrefix(prefix))
            {
                ValueProviderResult result = valueProvider.GetValue(prefix);
                if (null != result)
                {
                    IEnumerable enumerable = result.ConvertTo(modelType) as IEnumerable;
                    foreach (var value in enumerable)
                    {
                        list.Add(value);
                    }
                }
            }

            bool numericIndex;
            IEnumerable<string> indexes = GetIndexes(prefix, valueProvider, out numericIndex);
            foreach (var index in indexes)
            {
                string indexPrefix = prefix + "[" + index + "]";
                if (!valueProvider.ContainsPrefix(indexPrefix) && numericIndex)
                {
                    break;
                }
                list.Add(GetModel(controllerContext, type, valueProvider, indexPrefix));
            }

            return list;
        }

        private IEnumerable<string> GetIndexes(string prefix, IValueProvider valueProvider, out bool numericIndex)
        {
            string key = string.IsNullOrEmpty(prefix) ? "index" : prefix + "." + "index";
            ValueProviderResult result = valueProvider.GetValue(key);
            if (null != result)
            {
                string[] indexes = result.ConvertTo(typeof(string[])) as string[];
                if (null != indexes)
                {
                    numericIndex = false;
                    return indexes;
                }
            }
            numericIndex = true;
            return GetZeroBasedIndexes();
        }

        private IEnumerable<string> GetZeroBasedIndexes()
        {
            int iteratorVariable = 0;
            while (true)
            {
                yield return iteratorVariable.ToString();
                iteratorVariable++;
            }
        }

        protected virtual object GetCollectionModel(
            ControllerContext controllerContext, Type modelType, IValueProvider valueProvider, string prefix)
        {
            Type elementType = modelType.GetGenericArguments()[0];
            List<object> list = GetListModel(controllerContext, modelType, elementType, valueProvider, prefix);
            object model = CreateModel(modelType);
            ReplaceHelper.ReplaceCollection(elementType, model, list);
            return model;
        }

        internal static class ReplaceHelper
        {
            private static MethodInfo replaceDictionaryMethod =
                typeof(ReplaceHelper).GetMethod("ReplaceDictionaryImpl", BindingFlags.Static | BindingFlags.NonPublic);

            private static MethodInfo replaceCollectionMethod =
                typeof(ReplaceHelper).GetMethod("ReplaceCollectionImpl", BindingFlags.Static | BindingFlags.NonPublic);

            public static void ReplaceCollection(Type elementType, object model, object list)
            {
                replaceCollectionMethod.MakeGenericMethod(new Type[] { elementType })
                    .Invoke(null, new object[] { model, list });
            }

            private static void ReplaceCollectioinImpl<T>(ICollection<T> model, IEnumerable list)
            {
                model.Clear();
                if (list != null)
                {
                    foreach (object obj2 in list)
                    {
                        T item = (obj2 is T) ? ((T)obj2) : default(T);
                        model.Add(item);
                    }
                }
            }

            public static void ReplaceDictionary(Type keyType, Type valueType, object dictionary, object newContents)
            {
                replaceDictionaryMethod.MakeGenericMethod(new Type[] { keyType, valueType })
                    .Invoke(null, new object[] { dictionary, newContents });
            }
            private static void ReplaceDictionary<TKey, TValue>(IDictionary<TKey, TValue> dictionary,
                IEnumerable<KeyValuePair<object, object>> newContants)
            {
                dictionary.Clear();
                foreach (KeyValuePair<object, object> pair in newContants)
                {
                    TKey key = (TKey)pair.Key;
                    TValue local2 = (TValue)((pair.Value is TValue)
                        ? pair.Value : default(TValue));
                    dictionary[key] = local2;
                }
            }
        }

        protected virtual object GetDictionaryModel(ControllerContext controllerContext,
            Type modelType,IValueProvider valueProvider,string prefix)
        {
            List<KeyValuePair<object, object>> list = new List<KeyValuePair<object, object>>();

            bool numericIndex;
            IEnumerable<string> indexes = GetIndexes(prefix, valueProvider, out numericIndex);
            Type[] genericArguments = modelType.GetGenericArguments();
            Type keyType = genericArguments[0];
            Type valueType = genericArguments[1];

            foreach (var index in indexes)
            {
                string indexPrefix = prefix = "[" + index + "]";
                if (!valueProvider.ContainsPrefix(indexPrefix) & numericIndex)
                {
                    break;
                }
                string keyPrefix = indexPrefix = ".Key";
                string valuePrefix = indexPrefix = ".Value";
                object key = GetModel(controllerContext, keyType, valueProvider, keyPrefix);
                object value = GetModel(controllerContext, valueType, valueProvider, valuePrefix);
                list.Add(new KeyValuePair<object, object>(key, value));
            }

            object model = CreateModel(modelType);
            ReplaceHelper.ReplaceDictionary(keyType, valueType, model, list);
            return modelType;
        }
    }


}
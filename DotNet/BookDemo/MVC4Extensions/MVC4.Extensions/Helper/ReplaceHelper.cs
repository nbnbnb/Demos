using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MVC4.Extensions.Helper
{
    /// <summary>
    /// 学习这个帮助方法中，是如何执行泛型方法的
    /// 1，保存一个通过反射创建的 MethodInfo 【泛型的】
    /// 2，在运行时通过这个方法生成真正的封闭方法，然后传入参数，动态的执行
    /// </summary>
    public static class ReplaceHelper
    {
        private static MethodInfo replaceCollectionMethod = typeof(ReplaceHelper)
            .GetMethod("ReplaceCollectionImpl", BindingFlags.Static | BindingFlags.NonPublic);

        private static MethodInfo replaceDictionaryImpl = typeof(ReplaceHelper)
            .GetMethod("ReplaceDictionaryImpl", BindingFlags.Static | BindingFlags.NonPublic);

        public static void ReplaceCollectionImpl(Type elementType, object model, object list)
        {
            replaceCollectionMethod
                .MakeGenericMethod(elementType)
                .Invoke(null, new[] { model, list });
        }

        private static void ReplaceCollectionImpl<T>(ICollection<T> model, IEnumerable list)
        {
            model.Clear();

            foreach (object obj in list)
            {
                T item = (obj is T) ? (T)obj : default(T);
                model.Add(item);
            }
        }

        public static void ReplaceDictionaryImpl(Type keyType, Type valueType, object dictionary,
            object list)
        {
            replaceDictionaryImpl
                .MakeGenericMethod(keyType, valueType)  // 创建泛型方法
                .Invoke(null, new[] { dictionary, list }); // 然后传入参数进行执行
        }

        private static void ReplaceDictionaryImpl<TKey, TValue>(
            IDictionary<TKey, TValue> dictionary,
            List<KeyValuePair<object, object>> list)
        {
            dictionary.Clear();
            foreach (KeyValuePair<object, object> pair in list)
            {
                TKey key = (TKey)pair.Key;
                TValue value = (pair.Value is TValue) ? (TValue)pair.Value : default(TValue);
                dictionary[key] = value;
            }
        }
    }
}
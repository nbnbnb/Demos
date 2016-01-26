using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ClientTest
{
    public class SerializeHelper
    {

        /// <summary>
        /// 将对象序列化成XML字符串
        /// </summary>
        /// <param name="obj">实体对象</param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            return Serialize(obj, obj.GetType(), Encoding.UTF8);
        }

        /// <summary>
        /// 将对象序列化成XML字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string Serialize(object obj, Type type)
        {
            return Serialize(obj, type, Encoding.UTF8);
        }

        /// <summary>
        /// 将对象序列化成XML字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static string Serialize(object obj, Type type, Encoding encode)
        {
            try
            {
                var serializer = new XmlSerializer(type);
                var namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                using (var stream = new MemoryStream())
                {
                    serializer.Serialize(stream, obj, namespaces);
                    var str = encode.GetString(stream.GetBuffer());
                    return str.TrimEnd(new char[1]);
                }
            }
            catch (Exception e)
            {
               
                return string.Empty;
            }
        }

       
      

        /// <summary>
        /// 将XML字符串序列化成实体
        /// </summary>
        /// <param name="xml">待转换字符串</param>
        /// <param name="type">对象类型</param>
        /// <returns></returns>
        public static object Deserialize(string xml, Type type)
        {
            try
            {
                var serializer = new XmlSerializer(type);
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                {
                    return serializer.Deserialize(stream);
                }
            }
            catch (Exception e)
            {
                
                return null;
            }
        }

        /// <summary>
        /// 将XML字符串序列化成实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="xml">待转换字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml) where T : class
        {
            return Deserialize<T>(xml, Encoding.UTF8, false);
        }

        /// <summary>
        /// 将XML字符串序列化成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml, Encoding encode) where T : class
        {
            return Deserialize<T>(xml, encode, false);
        }

        /// <summary>
        /// 将XML字符串序列化成实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xml"></param>
        /// <param name="encode"></param>
        /// <param name="isThrow"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string xml, Encoding encode, bool isThrow) where T : class
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var stream = new MemoryStream(encode.GetBytes(xml)))
                {
                    return serializer.Deserialize(stream) as T;
                }
            }
            catch (Exception e)
            {
                
                if (isThrow) throw;
                return null;
            }
        }
    }
}

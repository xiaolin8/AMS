using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DotNet.Utilities
{
    public partial class JsonHelper2
    {
        //private static string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        #region 单例构造
        private static JsonHelper2 instance;
        private static readonly object syncroot = new object();
        public static JsonHelper2 GetInstance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncroot)
                    {
                        if (instance == null)
                        {
                            instance = new JsonHelper2();
                        }
                    }
                }
                return instance;
            }
        }
        #endregion


        #region  方法和属性


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj_data"></param>
        /// <returns></returns>
        public string GetJsonStringByObject(object obj)
        {
            Newtonsoft.Json.JsonSerializerSettings setting = new Newtonsoft.Json.JsonSerializerSettings();
            setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //IsoDateTimeConverter dt = new IsoDateTimeConverter();
            //dt.DateTimeFormat = DateTimeFormat;
            //setting.Converters.Add(dt);

            //setting.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            return JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.None, setting);

        }



        /// <summary>
        /// Json格式字符串转为Object对象
        /// 
        /// </summary>
        /// <param name="as_jsonstring"></param>
        /// <returns></returns>
        public object GetObjectByJsonString(string _json, bool isToHashtable = false)
        {

            object obj_return = null;

            obj_return = Newtonsoft.Json.JsonConvert.DeserializeObject(_json);
            if (isToHashtable)
            {
                obj_return = toObject(obj_return);
            }


            return obj_return;

        }

        /// <summary>
        /// JObject 类型转HashTable == GetObjectByJsonString<Hashtable>(o)
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private static object toObject(object o)
        {
            if (o == null) return null;

            if (o is JObject)
            {
                JObject jo = o as JObject;

                Hashtable h = new Hashtable();

                foreach (KeyValuePair<string, JToken> entry in jo)
                {
                    h[entry.Key] = toObject(entry.Value);
                }

                o = h;
            }
            return o;
        }
        /// <summary>
        /// Json格式字符串转为Object对象
        /// 
        /// </summary>
        /// <param name="as_jsonstring"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public T GetObjectByJsonString<T>(string _json)
        {
            return JsonConvert.DeserializeObject<T>(_json);
        }


        // private static T DeserializeJsonToObject<T>(string json) where T : class
        //{
        //    JsonSerializer serializer = new JsonSerializer();
        //    StringReader sr = new StringReader(json);
        //    object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
        //    T t = o as T;
        //    return t;
        //}

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public List<T> GetListByJsonString<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public T GetAnonymousTypeByJsonString<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }
        #endregion
    }
}
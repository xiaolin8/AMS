using System;
using System.Reflection;
using System.Text;

namespace DotNet.Utilities
{
    public class ReflectionHelper
    {
        private static BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

        private ReflectionHelper()
        {
        }

        /**/
        /// <summary>
        /// 执行某个方法
        /// </summary>
        /// <param name="obj">指定的对象</param>
        /// <param name="methodName">对象方法名称</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object InvokeMethod(object obj, string methodName, object[] args)
        {
            object objResult = null;
            Type type = obj.GetType();
            objResult = type.InvokeMember(methodName, bindingFlags | BindingFlags.InvokeMethod, null, obj, args);
            return objResult;
        }

        public static object InvokeMethod(Type type, string methodName, object[] args)
        {
            object objResult = null;
            object obj = Activator.CreateInstance(type);
            objResult = type.InvokeMember(methodName, bindingFlags | BindingFlags.InvokeMethod, null, obj, args);
            return objResult;
        }

        /**/
        /// <summary>
        /// 设置对象字段的值
        /// </summary>
        public static void SetField(object obj, string name, object value)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(name, bindingFlags);
            object objValue = Convert.ChangeType(value, fieldInfo.FieldType);
            fieldInfo.SetValue(objValue, value);
        }

        /**/
        /// <summary>
        /// 获取对象字段的值
        /// </summary>
        public static object GetField(object obj, string name)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(name, bindingFlags);
            return fieldInfo.GetValue(obj);
        }

        /**/
        /// <summary>
        /// 设置对象属性的值
        /// </summary>
        public static void SetProperty(object obj, string name, object value)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(name, bindingFlags);
            object objValue = Convert.ChangeType(value, propertyInfo.PropertyType);
            propertyInfo.SetValue(obj, objValue, null);
        }

        /**/
        /// <summary>
        /// 获取对象属性的值
        /// </summary>
        public static object GetProperty(object obj, string name)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(name, bindingFlags);
            return propertyInfo.GetValue(obj, null);
        }

        /**/
        /// <summary>
        /// 获取对象属性信息（组装成字符串输出）
        /// </summary>
        public static string GetProperties(object obj)
        {
            StringBuilder strBuilder = new StringBuilder();
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bindingFlags);

            foreach (PropertyInfo property in propertyInfos)
            {
                strBuilder.Append(property.Name);
                strBuilder.Append(":");
                strBuilder.Append(property.GetValue(obj, null));
                strBuilder.Append("/r/n");
            }

            return strBuilder.ToString();
        }
    }
}

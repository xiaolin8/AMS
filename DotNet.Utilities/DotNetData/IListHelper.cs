﻿using System.Collections;
using System.Collections.Generic;

namespace AMS.Utilities
{
    /// <summary>
    /// IList 公共帮助类
    /// </summary>
    public class IListHelper
    {
        /// <summary>
        /// IList如何转成List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> IListToList<T>(IList list)
        {
            T[] array = new T[list.Count];
            list.CopyTo(array, 0);
            return new List<T>(array);
        }
    }
}

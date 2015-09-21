﻿using System;

namespace DotNet.Utilities
{
    /// <summary>
    /// 实体类扩展属性
    /// </summary>
    public class EntityAttribute
    {
    }
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public class KeyAttribute : Attribute
    {
        public KeyAttribute()
        {
        }

        public KeyAttribute(string name)
        {
            _name = name;
        }
        private string _name;
        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }
}

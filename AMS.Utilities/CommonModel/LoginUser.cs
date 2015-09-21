using System;

namespace DotNet.Utilities
{
    /// <summary>
    /// 保存 LoginUser对象(用户登录)
    /// </summary>
    [Serializable]
    public class LoginUser
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string UID { get; set; }

        /// <summary>
        /// 用户登录名
        /// </summary>
        public string UName { get; set; }

        /// <summary>
        /// 用户全称
        /// </summary>
        public string UFullName { get; set; }

        /// <summary>
        /// 用户密码
        /// </summary>
        public string PWD { get; set; }

        /// <summary>
        /// 所属用户组
        /// </summary>
        public string UGROUP { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string UDesc { get; set; }

        public string GROUPNAME { get; set; }
    }
}

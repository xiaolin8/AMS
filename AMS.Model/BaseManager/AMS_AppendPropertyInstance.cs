using System.ComponentModel;
using DotNet.Utilities;

namespace AMS.Entity
{
    /// <summary>
    /// 附加属性信息实例
    /// </summary>
    [Description("附加属性信息实例")]
    [Key("PropertyInstanceId")]
    public class AMS_AppendPropertyInstance
    {
        private string _PropertyInstanceId = null;
        /// <summary>
        /// 属性实例主键
        /// </summary>
        /// <returns></returns>
        [Description("属性实例主键")]
        public string PropertyInstanceId
        {
            get
            {
                return this._PropertyInstanceId;
            }
            set
            {
                this._PropertyInstanceId = value;
            }
        }
        private string _PropertyId = null;
        /// <summary>
        /// 附加属性信息主键
        /// </summary>
        /// <returns></returns>
        [Description("附加属性信息主键")]
        public string PropertyId
        {
            get
            {
                return this._PropertyId;
            }
            set
            {
                this._PropertyId = value;
            }
        }
        private string _PropertyInstance_Value = null;
        /// <summary>
        /// 对象属性值
        /// </summary>
        /// <returns></returns>
        [Description("对象属性值")]
        public string PropertyInstance_Value
        {
            get
            {
                return this._PropertyInstance_Value;
            }
            set
            {
                this._PropertyInstance_Value = value;
            }
        }
        private string _PropertyInstance_Key = null;
        /// <summary>
        /// 对象业务属性主键
        /// </summary>
        /// <returns></returns>
        [Description("对象业务属性主键")]
        public string PropertyInstance_Key
        {
            get
            {
                return this._PropertyInstance_Key;
            }
            set
            {
                this._PropertyInstance_Key = value;
            }
        }
    }
}
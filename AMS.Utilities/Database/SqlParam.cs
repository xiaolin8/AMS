using System.Data;

namespace DotNet.Utilities
{
    /// <summary>
    /// 存放参数【键，键值】
    /// </summary>
    public class SqlParam
    {
        /// <summary>
        /// 目标字段
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DataType { get; set; }
        /// <summary>
        ///值 
        /// </summary>
        public object FiledValue { get; set; }

        public SqlParam()
        {
        }

        public SqlParam(string _FieldName, object _FiledValue)
            : this(_FieldName, DbType.AnsiString, _FiledValue)
        {
        }
        public SqlParam(string _FieldName, DbType _DbType, object _FiledValue)
        {
            if (ConfigHelper.GetValue("ComponentDbType") == "Oracle")
            {
                _FieldName = _FieldName.Replace("@", ":");
            }
            this.FieldName = _FieldName;
            this.DataType = _DbType;
            this.FiledValue = _FiledValue;
        }
    }
}
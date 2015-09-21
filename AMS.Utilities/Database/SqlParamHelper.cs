using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MySql.Data.MySqlClient;

namespace DotNet.Utilities
{
    public class SqlParamHelper
    {
        #region 有关数据库关键字
        /// <summary>
        ///  获得Sql字符串相加符号
        ///             Oracle ||
        ///             SQLServer +
        ///             MySql +
        /// </summary>
        /// <returns>字符加</returns>
        public static string PlusSign { get; set; }
        /// <summary>
        /// 获得数据库日期时间
        ///             Oracle：SYSDATE()
        ///             SQLServer：GETDATE()
        ///             MySql：NOW()
        /// </summary>
        /// <returns>日期时间</returns>
        public static string GetDBNow { get; set; }
        /// <summary>
        /// 获得数据库参数化
        ///             Oracle :
        ///             SQLServer @
        ///             MySql ?
        /// </summary>
        public static string ParamKey { get; set; }
        #endregion
        
        #region 对象参数转换SqlParam
        /// <summary>
        /// Hashtable对象参数转换
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static SqlParam[] GetParameter(Hashtable ht)
        {
            SoftRegHelper Verify = new SoftRegHelper();
            DbType dbtype = new DbType();
            IList<SqlParam> sqlparam = new List<SqlParam>();
            foreach (string key in ht.Keys)
            {
                if (ht[key] != null)
                {
                    if (ht[key] is DateTime)
                        dbtype = DbType.DateTime;
                    else
                        dbtype = DbType.AnsiString;
                    sqlparam.Add(new SqlParam(ParamKey + key, dbtype, ht[key]));
                }
            }
            return sqlparam.ToArray();
        }
        /// <summary>
        /// 实体类对象参数转换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static SqlParam[] GetParameter<T>(T entity)
        {
            SoftRegHelper Verify = new SoftRegHelper();
            DbType dbtype = new DbType();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            IList<SqlParam> sqlparam = new List<SqlParam>();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    if (prop.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                        dbtype = DbType.DateTime;
                    else
                        dbtype = DbType.AnsiString;
                    sqlparam.Add(new SqlParam(ParamKey + prop.Name, dbtype, prop.GetValue(entity, null)));
                }
            }
            return sqlparam.ToArray();
        }
        #endregion

        #region 通过参数类构造键值
        /// <summary>
        /// 通过参数类构造键值,MYSQL
        /// </summary>
        /// <param name="cmd">SQL命令</param>
        /// <param name="_params">参数化</param>
        public static void MySqlAddInParameter(DbCommand dbCommand, Hashtable ht)
        {
            MySqlParameter parameters = new MySqlParameter();
            if (ht == null) return;
            foreach (string key in ht.Keys)
            {
                dbCommand.Parameters.Add(new MySqlParameter(ParamKey + key, ht[key]));
            }
        }
        /// <summary>
        /// 通过参数类构造键值,MYSQL
        /// </summary>
        /// <param name="cmd">SQL命令</param>
        /// <param name="_params">参数化</param>
        public static void MySqlAddInParameter(DbCommand dbCommand, SqlParam[] param)
        {
            MySqlParameter parameters = new MySqlParameter();
            if (param == null) return;
            foreach (SqlParam _param in param)
            {
                dbCommand.Parameters.Add(new MySqlParameter(_param.FieldName, _param.FiledValue));
            }
        }
        /// <summary>
        /// 通过参数类构造键值
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="cmd">SQL命令</param>
        /// <param name="_params">参数化</param>
        public static void AddInParameter(Database database, DbCommand cmd, SqlParam[] _params)
        {
            SoftRegHelper Verify = new SoftRegHelper();
            if (_params == null) return;
            DbType dbtype = new DbType();
            foreach (SqlParam _param in _params)
            {
                if (_param.FiledValue is DateTime)
                    dbtype = DbType.DateTime;
                else
                    dbtype = DbType.AnsiString;
                database.AddInParameter(cmd, _param.FieldName, dbtype, _param.FiledValue);
            }
        }
        /// <summary>
        /// 通过Hashtable对象构造键值
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="cmd">SQL命令</param>
        /// <param name="_params">参数化</param>
        public static void AddInParameter(Database database, DbCommand cmd, Hashtable ht)
        {
            SoftRegHelper Verify = new SoftRegHelper();
            if (ht == null) return;
            foreach (string key in ht.Keys)
            {
                if (key == "Msg")
                {
                    database.AddOutParameter(cmd, ParamKey + key, DbType.AnsiString, 1000);
                }
                else
                {
                    database.AddInParameter(cmd, ParamKey + key, DbType.AnsiString, ht[key]);
                }
            }
        }

        /// <summary>
        /// 通过Hashtable对象构造键值
        /// </summary>
        /// <param name="database">数据库</param>
        /// <param name="cmd">SQL命令</param>
        /// <param name="_params">参数化</param>
        public static void AddMoreParameter(Database database, DbCommand cmd, Hashtable ht)
        {
            SoftRegHelper Verify = new SoftRegHelper();
            if (ht == null) return;
            foreach (string key in ht.Keys)
            {
                if (key.StartsWith("OUT_"))
                {
                    string tmp = key.Remove(0, 4);
                    database.AddOutParameter(cmd, ParamKey + tmp, DbType.AnsiString, 1000);
                }
                else
                {
                    database.AddInParameter(cmd, ParamKey + key, DbType.AnsiString, ht[key]);
                }
            }
        }
        #endregion

        #region 拼接 新增 SQL语句
        /// <summary>
        /// 哈希表生成InsertSql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="ParamKey">
        ///             参数化
        ///             Oracle :
        ///             SQLServer @
        ///             MySql ?
        /// </param>
        /// <returns>int</returns>
        public static StringBuilder InsertSql(string tableName, Hashtable ht)
        {
            SoftRegHelper Verify = new SoftRegHelper();
            StringBuilder sb = new StringBuilder();
            sb.Append(" Insert Into ");
            sb.Append(tableName);
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (string key in ht.Keys)
            {
                if (ht[key] != null)
                {
                    sb_prame.Append("," + key);
                    sp.Append("," + ParamKey + "" + key);
                }
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb;
        }
        /// <summary>
        /// 泛型方法，反射生成InsertSql语句
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="ParamKey">
        ///             参数化
        ///             Oracle :
        ///             SQLServer @
        ///             MySql ?
        /// </param>
        /// <returns>int</returns>
        public static StringBuilder InsertSql<T>(T entity)
        {
            SoftRegHelper Verify = new SoftRegHelper();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append(" Insert Into ");
            sb.Append(type.Name);
            sb.Append("(");
            StringBuilder sp = new StringBuilder();
            StringBuilder sb_prame = new StringBuilder();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    sb_prame.Append("," + (prop.Name));
                    sp.Append("," + ParamKey + "" + (prop.Name));
                }
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb;
        }
        #endregion

        #region 拼接 修改 SQL语句
        /// <summary>
        /// 哈希表生成UpdateSql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">主键</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="ParamKey">
        ///             参数化
        ///             Oracle :
        ///             SQLServer @
        ///             MySql ?
        /// </param>
        /// <returns></returns>
        public static StringBuilder UpdateSql(string tableName, string pkName, Hashtable ht)
        {
            SoftRegHelper Verify = new SoftRegHelper();
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update ");
            sb.Append(tableName);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (string key in ht.Keys)
            {
                if (ht[key] != null)
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(key);
                        sb.Append("=");
                        sb.Append(ParamKey + key);
                    }
                    else
                    {
                        sb.Append("," + key);
                        sb.Append("=");
                        sb.Append(ParamKey + key);
                    }
                }
            }
            sb.Append(" Where ").Append(pkName).Append("=").Append(ParamKey + pkName);
            return sb;
        }
        /// <summary>
        /// 泛型方法，反射生成UpdateSql语句
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="pkName">主键</param>
        /// <param name="ParamKey">
        ///             参数化
        ///             Oracle :
        ///             SQLServer @
        ///             MySql ?
        /// </param>
        /// <returns>int</returns>
        public static StringBuilder UpdateSql<T>(T entity, string pkName)
        {
            SoftRegHelper Verify = new SoftRegHelper();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append(" Update ");
            sb.Append(type.Name);
            sb.Append(" Set ");
            bool isFirstValue = true;
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    if (isFirstValue)
                    {
                        isFirstValue = false;
                        sb.Append(prop.Name);
                        sb.Append("=");
                        sb.Append(ParamKey + prop.Name);
                    }
                    else
                    {
                        sb.Append("," + prop.Name);
                        sb.Append("=");
                        sb.Append(ParamKey + prop.Name);
                    }
                }
            }
            sb.Append(" Where ").Append(pkName).Append("=").Append(ParamKey + pkName);
            return sb;
        }
        #endregion

        #region 拼接 删除 SQL语句
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <returns></returns>
        public static StringBuilder DeleteSql(string tableName, string pkName)
        {
            SoftRegHelper Verify = new SoftRegHelper();
            StringBuilder sb = new StringBuilder("Delete From " + tableName + " Where " + pkName + " = " + ParamKey + pkName + "");
            return sb;
        }
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">多参数</param>
        /// <returns></returns>
        public static StringBuilder DeleteSql(string tableName, Hashtable ht)
        {
            SoftRegHelper Verify = new SoftRegHelper();
            StringBuilder sb = new StringBuilder("Delete From " + tableName + " Where 1=1");
            foreach (string key in ht.Keys)
            {
                sb.Append(" AND " + key + " = " + ParamKey + "" + key + "");
            }
            return sb;
        }
        #endregion
    }
}
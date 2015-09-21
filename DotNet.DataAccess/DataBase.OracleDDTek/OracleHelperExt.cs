﻿using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text;
using DotNet.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;

namespace DotNet.DbUtilities
{
    /// <summary>
    /// 有关数据库连接的方法
    /// </summary>
    public class OracleHelperExt : IDbHelper, IDisposable
    {
        #region 数据库连接必要条件参数
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected string connectionString = "";
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="connString">数据库连接字符串</param>
        public OracleHelperExt(string connString)
        {
            DbCommon.ParamKey = ":";
            DbCommon.PlusSign = "||";
            DbCommon.GetDBNow = "SYSDATA()";
            connectionString = connString;
            new SqlSourceType();
        }
        /// <summary>
        /// 当前数据库类型
        /// </summary>
        public SqlSourceType SqlSourceType
        {
            get
            {
                return SqlSourceType.Oracle;
            }
        }
        /// <summary>
        /// 对象用于锁
        /// </summary>
        private static readonly Object locker = new Object();
        private OracleDatabase db = null;
        /// <summary>
        /// 取得单身实例
        /// </summary>
        public OracleDatabase GetInstance()
        {
            //在并发时，使用单一对象
            if (db == null)
            {
                return db = new OracleDatabase(connectionString);
            }
            else
            {
                lock (locker)
                {
                    return db;
                }
            }
        }
        //DbCommand 命令
        private DbCommand dbCommand = null;
        /// <summary>
        /// 命令
        /// </summary>
        public DbCommand DbCommand
        {
            get
            {
                return this.dbCommand;
            }
            set
            {
                this.dbCommand = value;
            }
        }
        //事务 命令
        private DbTransaction dbTransaction = null;
        // 是否已在事务之中
        private bool inTransaction = false;
        /// <summary>
        /// 是否已采用事务
        /// </summary>
        public bool InTransaction
        {
            get
            {
                return this.inTransaction;
            }
            set
            {
                this.inTransaction = value;
            }
        }
        #endregion

        #region 根据 SQL 返回影响行数
        /// <summary>
        /// 根据SQL返回影响行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public object GetObjectValue(StringBuilder sql)
        {
            return this.GetObjectValue(sql, null);
        }
        /// <summary>
        /// 根据SQL返回影响行数,带参数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public object GetObjectValue(StringBuilder sql, SqlParam[] param)
        {
            try
            {
                dbCommand = this.GetInstance().GetSqlStringCommand(Replace(sql.ToString()));
                DbCommon.AddInParameter(db, dbCommand, param);
                return db.ExecuteScalar(dbCommand);
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
                return null;
            }
        }
        #endregion

        #region 根据 SQL 执行
        /// <summary>
        ///  根据SQL执行
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>object</returns>
        public int ExecuteBySql(StringBuilder sql)
        {
            return this.ExecuteBySql(sql, null);
        }
        /// <summary>
        ///  根据SQL执行,带参数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化</param>
        /// <returns>object</returns>
        public int ExecuteBySql(StringBuilder sql, SqlParam[] param)
        {
            int num = 0;
            try
            {
                dbCommand = this.GetInstance().GetSqlStringCommand(Replace(sql.ToString()));
                DbCommon.AddInParameter(db, dbCommand, param);
                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    dbTransaction = connection.BeginTransaction();
                    try
                    {
                        num = db.ExecuteNonQuery(dbCommand, dbTransaction);
                        dbTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        dbTransaction.Rollback();
                        num = -1;
                        DbLog.WriteException(e);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                        dbTransaction.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
            }
            return num;
        }
        /// <summary>
        ///  根据SQL执行,带参数,不带事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化</param>
        /// <returns>object</returns>
        public int ExecuteBySqlNotTran(StringBuilder sql, SqlParam[] param)
        {
            int num = 0;
            try
            {
                dbCommand = this.GetInstance().GetSqlStringCommand(Replace(sql.ToString()));
                DbCommon.AddInParameter(db, dbCommand, param);
                num = db.ExecuteNonQuery(dbCommand);
            }
            catch (Exception e)
            {
                num = -1;
                DbLog.WriteException(e);
            }
            return num;
        }
        /// <summary>
        /// 批量执行SQL语句
        /// </summary>
        /// <param name="sqls">sql语句</param>
        /// <param name="m_param">参数化</param>
        /// <returns></returns>
        public int BatchExecuteBySql(object[] sqls, object[] param)
        {
            int num = 0;
            try
            {
                using (DbConnection connection = this.GetInstance().CreateConnection())
                {
                    connection.Open();
                    dbTransaction = connection.BeginTransaction();
                    try
                    {
                        for (int i = 0; i < sqls.Length; i++)
                        {
                            StringBuilder builder = (StringBuilder)sqls[i];
                            if (builder != null)
                            {
                                SqlParam[] paramArray = (SqlParam[])param[i];
                                DbCommand sqlStringCommand = this.db.GetSqlStringCommand(builder.ToString().Replace("@", ":"));
                                DbCommon.AddInParameter(db, sqlStringCommand, paramArray);
                                this.db.ExecuteNonQuery(sqlStringCommand, dbTransaction);
                            }
                        }
                        dbTransaction.Commit();
                        num = 1;
                    }
                    catch (Exception e)
                    {
                        num = -1;
                        dbTransaction.Rollback();
                        DbLog.WriteException(e);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                        dbTransaction.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
            }
            return num;
        }
        #endregion

        #region 根据 SQL 返回 DataTable 数据集
        /// <summary>
        /// 根据 SQL 返回 DataTable 数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTableBySQL(StringBuilder sql)
        {
            return this.GetDataTableBySQL(sql, null);
        }
        /// <summary>
        /// 根据 SQL 返回 DataTable 数据集，带参数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化</param>
        /// <returns>DataTable</returns>
        public DataTable GetDataTableBySQL(StringBuilder sql, SqlParam[] param)
        {
            try
            {
                dbCommand = this.GetInstance().GetSqlStringCommand(Replace(sql.ToString()));
                DbCommon.AddInParameter(db, dbCommand, param);
                return DbReader.ReaderToDataTable(db.ExecuteReader(dbCommand));
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
                return null;
            }
        }
        #endregion

        #region 根据 储过程 返回 DataTable 数据集
        /// <summary>
        /// 摘要:
        ///     执行一存储过程DataTable
        /// 参数：
        ///     procName：存储过程名称
        ///     Hashtable：传入参数字段名
        /// </summary>
        public DataTable GetDataTableProc(string procName, Hashtable ht)
        {
            try
            {
                dbCommand = this.GetInstance().GetStoredProcCommand(procName);
                DbCommon.AddInParameter(db, dbCommand, ht);
                return DbReader.ReaderToDataTable(db.ExecuteReader(dbCommand));
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
                return null;
            }
        }
        /// <summary>
        /// 执行一存储过程返回数据表 返回多个值
        /// <param name="procName">存储过程名称</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="rs">Hashtable</param>
        public DataTable GetDataTableProcReturn(string procName, Hashtable ht, ref Hashtable rs)
        {
            try
            {
                dbCommand = this.GetInstance().GetStoredProcCommand(procName);
                DbCommon.AddMoreParameter(db, dbCommand, ht);
                DataTable dt = DbReader.ReaderToDataTable(db.ExecuteReader(dbCommand));
                rs = new Hashtable();
                foreach (string key in ht.Keys)
                {
                    if (key.StartsWith("OUT_"))
                    {
                        string tmp = key.Remove(0, 4);
                        object val = db.GetParameterValue(dbCommand, ":" + tmp);
                        rs[key] = val;
                    }
                }
                return dt;
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
                return null;
            }
        }
        #endregion

        #region 根据 SQL 返回 DataSet 数据集
        /// <summary>
        /// 根据 SQL 返回 DataSet 数据集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSetBySQL(StringBuilder sql)
        {
            return GetDataSetBySQL(sql, null);
        }
        /// <summary>
        /// 根据 SQL 返回 DataSet 数据集，带参数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数化</param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSetBySQL(StringBuilder sql, SqlParam[] param)
        {
            try
            {
                dbCommand = this.GetInstance().GetSqlStringCommand(Replace(sql.ToString()));
                DbCommon.AddInParameter(db, dbCommand, param);
                return db.ExecuteDataSet(dbCommand);
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
                return null;
            }
        }
        #endregion

        #region 根据 储过程 返回 DataSet 数据集
        /// <summary>
        /// 摘要:
        ///     执行一存储过程DataSet
        /// 参数：
        ///     procName：存储过程名称
        ///     Hashtable：传入参数字段名
        /// </summary>
        public DataSet GetDataSetProc(string procName, Hashtable ht)
        {
            try
            {
                dbCommand = this.GetInstance().GetStoredProcCommand(procName);
                DbCommon.AddInParameter(db, dbCommand, ht);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                return ds;
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
                return null;
            }
        }
        /// <summary>
        /// 执行一存储过程返回数据集 返回多个值
        /// <param name="procName">存储过程名称</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="rs">Hashtable</param>
        public DataSet GetDataSetProcReturn(string procName, Hashtable ht, ref Hashtable rs)
        {
            try
            {
                dbCommand = this.GetInstance().GetStoredProcCommand(procName);
                DbCommon.AddMoreParameter(db, dbCommand, ht);
                DataSet ds = db.ExecuteDataSet(dbCommand);
                rs = new Hashtable();
                foreach (string key in ht.Keys)
                {
                    if (key.StartsWith("OUT_"))
                    {
                        string tmp = key.Remove(0, 4);
                        object val = db.GetParameterValue(dbCommand, ":" + tmp);
                        rs[key] = val;
                    }
                }
                return ds;
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
                return null;
            }
        }
        #endregion

        #region 根据 SQL 返回 IList
        /// <summary>
        /// 根据 SQL 返回 IList
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="sql">语句</param>
        /// <returns></returns>
        public IList GetDataListBySQL<T>(StringBuilder sql)
        {
            return this.GetDataListBySQL<T>(sql, null);
        }
        /// <summary>
        /// 根据 SQL 返回 IList,带参数 (比DataSet效率高)
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="sql">Sql语句</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public IList GetDataListBySQL<T>(StringBuilder sql, SqlParam[] param)
        {
            dbCommand = this.GetInstance().GetSqlStringCommand(Replace(sql.ToString()));
            DbCommon.AddInParameter(db, dbCommand, param);
            return DbReader.ReaderToList<T>(db.ExecuteReader(dbCommand));
        }
        #endregion

        #region 根据 存储过程 执行
        /// <summary>
        /// 调用存储过程(带事务)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="ht">参数化</param>
        public int ExecuteByProc(string procName, Hashtable ht)
        {
            int num = 0;
            try
            {
                DbCommand storedProcCommand = this.GetInstance().GetStoredProcCommand(procName);
                DbCommon.AddInParameter(db, storedProcCommand, ht);
                using (DbConnection connection = this.db.CreateConnection())
                {
                    connection.Open();
                    dbTransaction = connection.BeginTransaction();
                    try
                    {
                        num = this.db.ExecuteNonQuery(storedProcCommand, dbTransaction);
                        dbTransaction.Commit();
                    }
                    catch (Exception e)
                    {
                        num = -1;
                        dbTransaction.Rollback();
                        DbLog.WriteException(e);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                        dbTransaction.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
            }
            return num;
        }
        /// <summary>
        ///调用存储过程 (不带事务)
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="ht">参数化</param>
        /// <returns></returns>
        public int ExecuteByProcNotTran(string procName, Hashtable ht)
        {
            int num = 0;
            try
            {
                DbCommand storedProcCommand = this.GetInstance().GetStoredProcCommand(procName);
                DbCommon.AddInParameter(db, storedProcCommand, ht);
                using (DbConnection connection = this.db.CreateConnection())
                {
                    connection.Open();
                    try
                    {
                        num = this.db.ExecuteNonQuery(storedProcCommand);
                    }
                    catch (Exception e)
                    {
                        DbLog.WriteException(e);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
            }
            return num;
        }
        /// <summary>
        /// 批量调用存储过程
        /// </summary>
        /// <param name="text"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public int BatchExecuteByProc(object[] text, object[] param)
        {
            int num = 0;
            try
            {
                using (DbConnection connection = this.GetInstance().CreateConnection())
                {
                    connection.Open();
                    dbTransaction = connection.BeginTransaction();
                    try
                    {
                        for (int i = 0; i < text.Length; i++)
                        {
                            string strtext = text[i].ToString().ToUpper().Replace("@", ":");
                            if (strtext != null)
                            {
                                SqlParam[] paramArray = (SqlParam[])param[i];
                                DbCommand command = null;
                                if (strtext.StartsWith("PROC_"))
                                {
                                    command = this.db.GetStoredProcCommand(strtext);
                                }
                                else
                                {
                                    command = this.db.GetSqlStringCommand(strtext);
                                }
                                DbCommon.AddInParameter(db, command, paramArray);
                                this.db.ExecuteNonQuery(command, dbTransaction);
                            }
                        }
                        dbTransaction.Commit();
                        num = 1;
                    }
                    catch (Exception e)
                    {
                        num = -1;
                        dbTransaction.Rollback();
                        DbLog.WriteException(e);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                        dbTransaction.Dispose();
                    }
                }
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
            }
            return num;
        }
        /// <summary>
        /// 调用存储过程返回指定消息
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="msg">OutPut Msg</param>
        public int ExecuteByProcReturnMsg(string procName, Hashtable ht, ref object msg)
        {
            int num = 0;
            try
            {
                DbCommand storedProcCommand = this.GetInstance().GetStoredProcCommand(procName);
                DbCommon.AddInParameter(db, storedProcCommand, ht);
                using (DbConnection connection = this.db.CreateConnection())
                {
                    connection.Open();
                    dbTransaction = connection.BeginTransaction();
                    try
                    {
                        num = this.db.ExecuteNonQuery(storedProcCommand, dbTransaction);
                        dbTransaction.Commit();
                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        num = -1;
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                        dbTransaction.Dispose();
                    }
                }
                msg = this.db.GetParameterValue(storedProcCommand, ":Msg");
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
            }
            return num;
        }
        /// <summary>
        /// 调用存储过程返回指定消息（不带事务）
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="msg">OutPut Msg</param>
        public int ExecuteByProcNotTranReturnMsg(string procName, Hashtable ht, ref object msg)
        {
            int num = 0;
            try
            {
                DbCommand storedProcCommand = this.GetInstance().GetStoredProcCommand(procName);
                DbCommon.AddInParameter(db, storedProcCommand, ht);
                using (DbConnection connection = this.db.CreateConnection())
                {
                    try
                    {
                        connection.Open();
                        num = this.db.ExecuteNonQuery(storedProcCommand);
                        num = 1;
                    }
                    catch (Exception e)
                    {
                        DbLog.WriteException(e);
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                }
                msg = this.db.GetParameterValue(storedProcCommand, ":Msg");
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
            }
            return num;
        }
        /// <summary>
        /// 调用存储过程返回指定消息
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="ht">Hashtable</param>
        /// <param name="msg">OutPut rs</param>
        public int ExecuteByProcReturn(string procName, Hashtable ht, ref Hashtable rs)
        {
            int num = 0;
            try
            {
                DbCommand storedProcCommand = this.GetInstance().GetStoredProcCommand(procName);
                DbCommon.AddMoreParameter(db, storedProcCommand, ht);
                using (DbConnection connection = this.db.CreateConnection())
                {
                    connection.Open();
                    dbTransaction = connection.BeginTransaction();
                    try
                    {
                        num = this.db.ExecuteNonQuery(storedProcCommand, dbTransaction);
                        dbTransaction.Commit();
                    }
                    catch
                    {
                        num = -1;
                        dbTransaction.Rollback();
                    }
                    finally
                    {
                        connection.Close();
                        connection.Dispose();
                        dbTransaction.Dispose();
                    }
                }
                rs = new Hashtable();
                foreach (string str in ht.Keys)
                {
                    if (str.StartsWith("OUT_"))
                    {
                        object parameterValue = this.db.GetParameterValue(storedProcCommand, ":" + str.Remove(0, 4));
                        rs[str] = parameterValue;
                    }
                }
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
            }
            return num;
        }
        #endregion

        #region 数据分页 返回 DataTable
        /// <summary>
        /// 摘要:
        ///     数据分页
        /// 参数：
        ///     sql：传入要执行sql语句
        ///     param：参数化
        ///     orderField：排序字段
        ///     orderType：排序类型
        ///     pageIndex：当前页
        ///     pageSize：页大小
        ///     count：返回查询条数
        /// </summary>
        public DataTable GetPageList(string sql, SqlParam[] param, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                count = DbCommon.GetInt(GetObjectValue(new StringBuilder("Select Count(1) From (" + sql + ")  t"), param));
                int num = (pageIndex - 1) * pageSize;
                int num2 = pageSize;
                int num3 = num + num2 + 1;
                builder.Append("select * from(select t.*,rownum rn from(" + sql + " order by " + orderField + " " + orderType + ") t where rownum<" + num3 + ") where rn>" + num + "");
                return GetDataTableBySQL(builder, param);
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
                return null; ;
            }
        }
        /// <summary>
        /// 摘要:
        ///     数据分页
        /// 参数：
        ///     sql：传入要执行sql语句
        ///     orderField：排序字段
        ///     orderType：排序类型
        ///     pageIndex：当前页
        ///     pageSize：页大小
        ///     count：返回查询条数
        /// </summary>
        public DataTable GetPageList(string sql, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            return GetPageList(sql, null, orderField, orderType, pageIndex, pageSize, ref  count);
        }
        #endregion

        #region 数据分页 返回 ILis
        /// <summary>
        /// 摘要:
        ///     数据分页
        /// 参数：
        ///     sql：传入要执行sql语句
        ///     param：参数化
        ///     orderField：排序字段
        ///     orderType：排序类型
        ///     pageIndex：当前页
        ///     pageSize：页大小
        ///     count：返回查询条数
        /// </summary>
        public IList GetPageList<T>(string sql, SqlParam[] param, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                count = DbCommon.GetInt(GetObjectValue(new StringBuilder("Select Count(1) From (" + sql + ")  t"), param));
                int num = (pageIndex - 1) * pageSize;
                int num2 = pageSize;
                int num3 = num + num2 + 1;
                builder.Append("select * from(select t.*,rownum rn from(" + sql + " order by " + orderField + " " + orderType + ") t where rownum<" + num3 + ") where rn>" + num + "");
                return GetDataListBySQL<T>(builder, param);
            }
            catch (Exception e)
            {
                DbLog.WriteException(e);
                return null; ;
            }
        }
        /// <summary>
        /// 摘要:
        ///     数据分页
        /// 参数：
        ///     sql：传入要执行sql语句
        ///     orderField：排序字段
        ///     orderType：排序类型
        ///     pageIndex：当前页
        ///     pageSize：页大小
        ///     count：返回查询条数
        /// </summary>
        public IList GetPageList<T>(string sql, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            return GetPageList<T>(sql, null, orderField, orderType, pageIndex, pageSize, ref  count);
        }
        #endregion

        #region SqlBulkCopy 批量数据处理
        /// <summary>
        ///大批量数据插入
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        public bool BulkInsert(DataTable dt)
        {
            DbHelperExpand copy = new DbHelperExpand();
            return copy.OracleBulkInsert(dt, connectionString);
        }
        #endregion

        #region IDisposable 成员
        /// <summary>
        /// 内存回收
        /// </summary>
        public void Dispose()
        {
            if (this.dbCommand != null)
            {
                this.dbCommand.Dispose();
            }
        }
        #endregion

        /// <summary>
        /// 替换字符
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Replace(string text)
        {
            text = text.Replace("@", ":");
            text = text.Replace("ISNULL", "nvl");
            text = text.Replace("GETDATE()", "(select sysdate from dual)");
            return text;
        }
    }
}

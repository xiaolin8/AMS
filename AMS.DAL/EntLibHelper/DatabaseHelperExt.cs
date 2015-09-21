using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using DotNet.Utilities;

namespace AMS.DAL
{
    public partial class DatabaseHelper
    {

        #region 对象参数转换SqlParam
        /// <summary>
        /// Hashtable对象参数转换,跟进Hashtable获得List
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public List<Parameter> GetParameter(Hashtable ht)
        {

            DbType dbtype = new DbType();
            List<Parameter> list = new List<Parameter>();
            foreach (string key in ht.Keys)
            {
                if (ht[key] != null)
                {
                    if (ht[key] is DateTime)
                    {
                        dbtype = DbType.DateTime;
                    }
                    else
                    {
                        dbtype = DbType.AnsiString;
                    }
                    list.Add(new Parameter(key, ht[key], dbtype));
                }
            }
            return list;
        }
        /// <summary>
        /// 实体类对象参数转换
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<Parameter> GetParameter<T>(T entity)
        {

            DbType dbtype = new DbType();
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            List<Parameter> list = new List<Parameter>();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    if (prop.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                    {
                        dbtype = DbType.DateTime;
                    }
                    else
                    {
                        dbtype = DbType.AnsiString;
                    }
                    list.Add(new Parameter(prop.Name, prop.GetValue(entity, null), dbtype));
                }
            }
            return list;
        }
        /// <summary>
        /// 字符串转换为参数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public List<Parameter> GetParameter(string text,string value)
        {
            List<Parameter> list = new List<Parameter>();
            list.Add(new Parameter(text, value, DbType.AnsiString));

            return list;
        }
        #endregion

        #region 拼接 查询
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string PrepareQuerySqlString<T>(T entity)
        {
            Type type = entity.GetType();
            PropertyInfo[] props = type.GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append(" select * from  ");
            sb.Append(type.Name);
            sb.Append("where ");
            StringBuilder sp = new StringBuilder();
           
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null)
                {
                    sp.Append("," + prop.Name + "=@" + prop.Name);
                }
            }
           
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb.ToString();
        }

        #endregion
        #region 拼接 新增 SQL语句
        /// <summary>
        /// 哈希表生成InsertSql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">Hashtable</param>
        /// <returns>int</returns>
        public string PrepareInsertSqlString(string tableName, Hashtable ht)
        {
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
                    sp.Append(",@" + key);
                }
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb.ToString();
        }
        /// <summary>
        /// 泛型方法，反射生成InsertSql语句
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns>int</returns>
        public string PrepareInsertSqlString<T>(T entity)
        {

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
                    sb_prame.Append("," + prop.Name);
                    sp.Append(",@" + (prop.Name));
                }
            }
            sb.Append(sb_prame.ToString().Substring(1, sb_prame.ToString().Length - 1) + ") Values (");
            sb.Append(sp.ToString().Substring(1, sp.ToString().Length - 1) + ")");
            return sb.ToString();
        }
        #endregion

        #region 拼接 修改 SQL语句
        /// <summary>
        /// 哈希表生成UpdateSql语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">主键</param>
        /// <param name="ht">Hashtable</param>
        /// <returns></returns>
        public string PrepareUpdateSqlString(string tableName, string pkName, Hashtable ht)
        {

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
                        sb.Append("@" + key);
                    }
                    else
                    {
                        sb.Append("," + key);
                        sb.Append("=");
                        sb.Append("@" + key);
                    }
                }
            }
            sb.Append(" Where ").Append(pkName).Append("=").Append("@" + pkName);
            return sb.ToString();
        }
        /// <summary>
        /// 泛型方法，反射生成UpdateSql语句
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="pkName">主键</param>
        /// <returns>int</returns>
        public string PrepareUpdateSqlString<T>(T entity, string pkName)
        {

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
                        sb.Append("@" + prop.Name);
                    }
                    else
                    {
                        sb.Append("," + prop.Name);
                        sb.Append("=");
                        sb.Append("@" + prop.Name);
                    }
                }
            }
            sb.Append(" Where ").Append(pkName).Append("=").Append("@" + pkName);

            return sb.ToString();
        }
        #endregion

        #region 拼接 删除 SQL语句
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="pkName"></param>
        /// <returns></returns>
        public string PrepareDeleteSqlString<T>(string tableName, string pkName)
        {

            StringBuilder sb = new StringBuilder("Delete From " + tableName + " Where " + pkName + " = @" + pkName + "");
            return sb.ToString();
        }
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <returns></returns>
        public string PrepareDeleteSqlString(string tableName, string pkName)
        {

            StringBuilder sb = new StringBuilder("Delete From " + tableName + " Where " + pkName + " = @" + pkName + "");
            return sb.ToString();
        }
        /// <summary>
        /// 拼接删除SQL语句
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">多参数</param>
        /// <returns></returns>
        public string PrepareDeleteSqlString(string tableName, Hashtable ht)
        {

            StringBuilder sb = new StringBuilder("Delete From " + tableName + " Where 1=1");
            foreach (string key in ht.Keys)
            {
                sb.Append(" AND " + key + " =@" + key + "");
            }
            return sb.ToString();
        }
        #endregion

        //#region GetHashtable  
        ///// <summary>
        ///// 根据唯一ID获取对象,返回Hashtable
        ///// </summary>
        ///// <param name="tableName">表名</param>
        ///// <param name="pkName">字段主键</param>
        ///// <param name="pkVal">字段值</param>
        ///// <returns>返回Hashtable</returns>
        //public Hashtable GetHashtableById(string tableName, string pkName, string pkVal)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("SELECT * FROM ").Append(tableName).Append(" Where ").Append(pkName).Append("=@ID");
        //    List<Parameter> list=new List<Parameter>();
        //    list.Add(new Parameter("ID",pkVal));
        //    return DataTableHelper.DataTableToHashtable(this.ExecuteDataTable(sb.ToString(),list));
        //}
        ///// <summary>
        ///// 根据参数获取对象,返回Hashtable
        ///// </summary>
        ///// <param name="tableName">表名</param>
        ///// <param name="ht">参数</param>
        ///// <returns>返回Hashtable</returns>
        //public Hashtable GetHashtableById(string tableName, Hashtable ht)
        //{
          
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT * FROM " + tableName + " WHERE 1=1");
        //    foreach (string key in ht.Keys)
        //    {
        //        strSql.Append(" AND " + key + " = @" + key + "");
        //    }
        //    return DataTableHelper.DataTableToHashtable(this.ExecuteDataTable(strSql.ToString(), this.GetParameter(ht)));
        //}
        ///// <summary>
        ///// 根据参数获取对象,返回Hashtable
        ///// </summary>
        ///// <param name="tableName">表名</param>
        ///// <param name="where">条件</param>
        ///// <param name="param">参数化</param>
        ///// <returns>返回Hashtable</returns>
        //public Hashtable GetHashtableById(string tableName, string where, List<Parameter> param)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT * FROM " + tableName + " WHERE 1=1");
        //    strSql.Append(where);
        //    return DataTableHelper.DataTableToHashtable(this.ExecuteDataTable(strSql.ToString(), param));
        //}
       
        //#endregion

        #region  T

        /// <summary>
        /// 根据唯一ID获取对象,返回实体，实体为数据表
        /// </summary>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkVal">字段值</param>
        /// <returns>返回实体类</returns>
        public T GetModelById<T>(string pkName, string pkVal)
        {
            if (string.IsNullOrEmpty(pkVal))
            {
                return default(T);
            }

            T model = Activator.CreateInstance<T>();
            Type type = model.GetType();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ").Append(type.Name).Append(" Where ").Append(pkName).Append("=@ID");
            List<Parameter> list = new List<Parameter>();
            list.Add(new Parameter("ID", pkVal));
            DataTable dt = this.ExecuteDataTable(sb.ToString(), list);
            if (dt.Rows.Count > 0)
            {
                return DataReaderHelper.ReaderToModel<T>(dt.Rows[0]);
            }
            return model;
        }
        /// <summary>
        /// 根据查询参数获取对象,返回实体,实体为数据表
        /// </summary>
        /// <param name="ht">参数</param>
        /// <returns>返回实体类</returns>
        public T GetModelById<T>(Hashtable ht)
        {

            T model = Activator.CreateInstance<T>();
            Type type = model.GetType();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM " + type.Name + " WHERE 1=1");
            foreach (string key in ht.Keys)
            {
                strSql.Append(" AND " + key + " =@" + key + "");
            }
            DataTable dt = this.ExecuteDataTable(strSql.ToString(), this.GetParameter(ht));
            if (dt.Rows.Count > 0)
            {
                return DataReaderHelper.ReaderToModel<T>(dt.Rows[0]);
            }
            return model;
        }
        /// <summary>
        /// 根据查询条件获取对象,返回实体，实体为数据表
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns>返回实体类</returns>
        public T GetModelById<T>(StringBuilder where, List<Parameter> param=null)
        {

            T model = Activator.CreateInstance<T>();
            Type type = model.GetType();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM " + type.Name + " WHERE 1=1");
            strSql.Append(where);
            DataTable dt = this.ExecuteDataTable(strSql.ToString(), param);
            if (dt.Rows.Count > 0)
            {
                return DataReaderHelper.ReaderToModel<T>(dt.Rows[0]);
            }
            return model;
        }

        /// <summary>
        /// 根据查询条件获取对象,返回实体，实体可为业务Model
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T GetModel<T>(string sql, List<Parameter> param=null)
        {

            T model = Activator.CreateInstance<T>();
            Type type = model.GetType();
            DataTable dt = this.ExecuteDataTable(sql, param);
            if (dt.Rows.Count > 0)
            {
                return DataReaderHelper.ReaderToModel<T>(dt.Rows[0]);
            }
            return model;
        }
        #endregion

        #region GetMax
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="fieldName">字段</param>
        /// <returns></returns>
        public object GetMax(string tableName, string fieldName)
        {
            string sqlString = "SELECT MAX(" + fieldName + ") FROM " + tableName;
            object obj = this.ExecuteScalar(sqlString);
            if (!string.IsNullOrEmpty(obj.ToString()))
            {
                return Convert.ToInt32(obj) + 1;
            }
            return 1;
        }
        #endregion

        #region GetRecordCount
        /// <summary>
        /// 影响行数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="pkName">字段主键</param>
        /// <param name="pkVal">字段值</param>
        /// <returns>返回数量</returns>
        public int GetRecordCount(string tableName, string pkName, string pkVal)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Count(1) FROM " + tableName + "");
            strSql.Append(" where " + pkName + " = '" + pkVal + "'");

            object obj = this.ExecuteScalar(strSql.ToString());

            if (obj != null)
            {
                int i;
                int.TryParse(obj.ToString(), out i);
                return i;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 影响行数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="ht">参数</param>
        /// <returns>返回数量</returns>
        public int GetRecordCount(string tableName, Hashtable ht)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Count(1) FROM " + tableName + " WHERE 1=1");
            foreach (string key in ht.Keys)
            {
                strSql.Append(" AND " + key + " = '" + ht[key] + "'");
            }


            object obj = this.ExecuteScalar(strSql.ToString());

            if (obj != null)
            {
                int i;
                int.TryParse(obj.ToString(), out i);
                return i;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 影响行数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="where">条件</param>
        /// <param name="list">参数化</param>
        /// <returns>返回数量</returns>
        public int GetRecordCount(string tableName, StringBuilder where, List<Parameter> list)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Count(1) FROM " + tableName + " WHERE 1=1");
            strSql.Append(where);
            object obj = this.ExecuteScalar(strSql.ToString(), list);
            if (obj != null)
            {
                int i;
                int.TryParse(obj.ToString(), out i);
                return i;
            }
            else
            {
                return 0;
            }
        }
        #endregion

        #region 根据 SQL 返回 IList
        /// <summary>
        /// 根据 SQL 返回 List
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="sql">语句</param>
        /// <returns></returns>
        public List<T> GetDataListBySQL<T>(string sql)
        {
            return this.GetDataListBySQL<T>(sql, null);
        }
        /// <summary>
        /// 根据 SQL 返回 List,带参数 (比DataSet效率高)
        /// </summary>
        /// <typeparam name="T">实体类</typeparam>
        /// <param name="sql">Sql语句</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public List<T> GetDataListBySQL<T>(string sql, List<Parameter> param)
        {
            
            return DataReaderHelper.ReaderToList<T>(this.ExecuteDataReader(sql,param));
        }
        #endregion

        #region 数据分页 返回 DataTable
        /// <summary>
        ///  执行Oracle的分页存储过程，返回查询结果集
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <param name="pageIndex">页号,从0开始</param>
        /// <param name="pageSize">每页多少行</param>
        /// <param name="recordCount">共有多少行</param>
        /// <returns></returns>
        private DataTable ExecutePaginationProcedureOracle(string sqlString, int pageIndex, int pageSize, ref int recordCount)
        {
            #region P_Pagination
            // create or replace procedure P_Pagination
            //(
            //  Pindex in number,       --页号  从0开始
            //  Psql in varchar2,       --查询语句
            //  Psize in number,        --每页分多少行
            //  Rcount out Sys_Refcursor,    --共有多少行
            //  Pcount out Sys_Refcursor,      --共有多少页
            //  v_cur out Sys_Refcursor --返回数据集
            // )
            // AS

            //  v_sql VARCHAR2(3000);
            //  v_RCount number;
            //  v_PCount number;
            //  v_Plow number;
            //  v_Phei number;
            // Begin
            // -----------取分页总数 -----------
            //  v_sql := 'select count(*) from (' || Psql || ')';
            //  execute immediate v_sql into v_RCount;
            //  v_PCount := ceil(v_RCount/Psize);
            // ---------------显示任意页内容 ---------------

            //  v_Phei := (Pindex+1) * Psize ;
            //  v_Plow := v_Phei - Psize + 1;
            //  v_sql := 'select * from (select rownum rn,t.* from (' || Psql || ')t) where rn between ' || v_Plow || ' and ' || v_Phei ;

            // open Rcount for select v_RCount from dual;
            // open Pcount for select v_PCount from dual;
            // open v_cur for v_sql;

            // End P_Pagination;
            #endregion

            DataTable oResult = null;

            object[] oParamArray = new object[6];
            oParamArray[0] = pageIndex;//Pindex in number,       --页号  从0开始
            oParamArray[1] = sqlString;//--查询语句
            oParamArray[2] = pageSize;//--每页分多少行
            oParamArray[3] = "";//--共有多少行
            oParamArray[4] = "";//--共有多少页
            oParamArray[5] = "";//--返回数据集

            DataSet dsResult = _db.ExecuteDataSet("P_Pagination", oParamArray);

            recordCount = Int32.Parse(dsResult.Tables[0].Rows[0][0].ToString());
            //int iPageCount=Int32.Parse(dsResult.Tables[1].Rows[0][0].ToString());//共有多少页
            oResult = dsResult.Tables[2].Copy();

            return oResult;
        }

        #region SqlServer
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="Parameters"></param>
        /// <param name="orderField"></param>
        /// <param name="orderType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private DataTable GetPageList(string sql, List<Parameter> Parameters, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder sb = new StringBuilder();

            int num = (pageIndex - 1) * pageSize;
            int num1 = (pageIndex) * pageSize;
            sb.Append("Select * From (Select ROW_NUMBER() Over (Order By " + orderField + " " + orderType + "");
            sb.Append(") As rowNum, * From (" + sql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");

            object oCount = this.ExecuteScalar(new StringBuilder("Select Count(1) From (" + sql + ")").ToString(), Parameters);
            if (oCount != null)
            {
                count = Convert.ToInt32(oCount.ToString());
            }
            return this.ExecuteDataTable(sb.ToString(), Parameters);
        }
#endregion
        #region Oracle
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
        public DataTable GetPageListOracle(string sql, List<Parameter> param, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                int num = (pageIndex - 1) * pageSize;
                int num2 = pageSize;
                int num3 = num + num2 + 1;
                builder.Append("select * from(select t.*,rownum rn from(" + sql + " order by " + orderField + " " + orderType + ") t where rownum<" + num3 + ") where rn>" + num + "");
                object oCount = this.ExecuteScalar(new StringBuilder("Select Count(1) From (" + sql + ")").ToString(), param);
                if (oCount != null)
                {
                    count = Convert.ToInt32(oCount.ToString());
                }
                return this.ExecuteDataTable(builder.ToString(), param);
            }
            catch (Exception e)
            {
               
                return null; ;
            }
        }
  
        #endregion


        /// <summary>
        /// 获得分页，Datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sortField"></param>
        /// <param name="sortType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public DataTable GetPageList(string sql, string sortField, string sortType, int pageIndex, int pageSize, ref int count, List<Parameter> Parameters = null)
        {
            DataTable dtResult = null;
            switch (this.DBType)
            {
                case "OracleDatabase":
                    dtResult = this.GetPageListOracle(sql, Parameters, sortField, sortType, pageIndex, pageSize, ref  count);
                    break;
                case "SqlDatabase":
                    dtResult = this.GetPageList(sql, Parameters, sortField, sortType, pageIndex, pageSize, ref  count);
                    break;
            }
            return dtResult;
        }
 


        #endregion

        #region 数据分页 返回 IList

        #region SqlServer
        
      
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
        private IList GetPageList<T>(string sql, List<Parameter> param, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            
            try
            {
                StringBuilder sb = new StringBuilder();

            int num = (pageIndex - 1) * pageSize;
            int num1 = (pageIndex) * pageSize;
            sb.Append("Select * From (Select ROW_NUMBER() Over (Order By " + orderField + " " + orderType + "");
            sb.Append(") As rowNum, * From (" + sql + ") As T ) As N Where rowNum > " + num + " And rowNum <= " + num1 + "");

            object oCount = this.ExecuteScalar(new StringBuilder("Select Count(1) From (" + sql + ") cntTmp").ToString(), param);
            if (oCount != null)
            {
                count = Convert.ToInt32(oCount.ToString());
            }
            return this.GetDataListBySQL<T>(sb.ToString(), param);
            }
            catch (Exception e)
            {
            
                return null; ;
            }
        }
        ///// <summary>
        ///// 摘要:
        /////     数据分页
        ///// 参数：
        /////     sql：传入要执行sql语句
        /////     orderField：排序字段
        /////     orderType：排序类型
        /////     pageIndex：当前页
        /////     pageSize：页大小
        /////     count：返回查询条数
        ///// </summary>
        //public IList GetPageList<T>(string sql, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        //{
        //    return GetPageList<T>(sql, null, orderField, orderType, pageIndex, pageSize, ref  count);
        //}
        #endregion
        #region Oracle
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
        private IList GetPageListOracle<T>(string sql, List<Parameter> param, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder builder = new StringBuilder();
            try
            {
                int num = (pageIndex - 1) * pageSize;
                int num2 = pageSize;
                int num3 = num + num2 + 1;
                builder.Append("select * from(select t.*,rownum rn from(" + sql + " order by " + orderField + " " + orderType + ") t where rownum<" + num3 + ") where rn>" + num + "");
                object oCount = this.ExecuteScalar(new StringBuilder("Select Count(1) From (" + sql + ")").ToString(), param);
                if (oCount != null)
                {
                    count = Convert.ToInt32(oCount.ToString());
                }
                return GetDataListBySQL<T>(builder.ToString(), param);
            }
            catch (Exception e)
            {
              
                return null; ;
            }
        }
        ///// <summary>
        ///// 摘要:
        /////     数据分页
        ///// 参数：
        /////     sql：传入要执行sql语句
        /////     orderField：排序字段
        /////     orderType：排序类型
        /////     pageIndex：当前页
        /////     pageSize：页大小
        /////     count：返回查询条数
        ///// </summary>
        //public IList GetPageListOracle<T>(string sql, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        //{
        //    return GetPageListOracle<T>(sql, null, orderField, orderType, pageIndex, pageSize, ref  count);
        //}
        #endregion



        /// <summary>
        ///  获得分页，IList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="sortField"></param>
        /// <param name="sortType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public IList GetPageList<T>(string sql, string sortField, string sortType, int pageIndex, int pageSize, ref int count, List<Parameter> Parameters = null)
        {
            IList result = null;
            switch (this.DBType)
            {
                case "OracleDatabase":
                    result = this.GetPageListOracle<T>(sql, Parameters, sortField, sortType, pageIndex, pageSize, ref  count);
                    break;
                default://sqlserver
                    result = this.GetPageList<T>(sql, Parameters, sortField, sortType, pageIndex, pageSize, ref  count);
                    break;
            }
            return result;
        }
        #endregion

    }
}

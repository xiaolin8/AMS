using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 系统日志
    /// </summary>
    public class AMS_SysLogBLL : ServiceBase
    {
        private static AMS_SysLogBLL item;
        public static AMS_SysLogBLL Instance
        {
            get
            {
                if (item == null)
                {
                    item = new AMS_SysLogBLL();
                }
                return item;
            }
        }

        public enum OperationType
        {
            Add = 1,
            Update = 2,
            Delete = 3,
            Other = 4,
        }

        //private readonly AMS_SysLogBLL dal = new AMS_SysLogBLL();

        #region 系统登录日志
        /// <summary>
        /// 添加登录日志
        /// </summary>
        /// <param name="Account">账户</param>
        /// <param name="Status">登录状态</param>
        /// <param name="IPAddress">IP地址</param>
        /// <param name="IPAddressName">IP所在城市地址</param>
        public void AddSysLoginLog(string Account, string Status, string IPAddress, string IPAddressName)
        {
            //dal.AddSysLoginLog(Account, Status, IPAddress, IPAddressName);

            Hashtable ht = new Hashtable();
            ht["SysLoginLogId"] = CommonHelper.GetGuid;
            ht["Account"] = Account;
            ht["Status"] = Status;
            ht["IPAddress"] = IPAddress;
            ht["IPAddressName"] = IPAddressName;
            DbUtils.Insert("AMS_SysLoginLog", ht);
        }
        /// <summary>
        /// 统计登录日志，柱状图
        /// </summary>
        /// <returns></returns>
        public DataTable GetHighchartLoginLog()
        {
            //return dal.GetHighchartLoginLog();
            /*
//SqlServer
SELECT  DAY(CreateDate) AS data ,
                        COUNT(1) AS count
                FROM    AMS_SysLoginLog
                WHERE   MONTH(CreateDate) = MONTH(GETDATE())
                GROUP BY DAY(CreateDate) 

//MySql
SELECT  DAY(CreateDate) AS data ,
                        COUNT(1) AS count
                FROM    AMS_SysLoginLog
                WHERE   MONTH(CreateDate) = MONTH(curdate())
                GROUP BY DAY(CreateDate) 

//Oracle
SELECT  to_char(CreateDate,'dd') AS data ,
                        COUNT(1) AS count
                FROM    AMS_SysLoginLog
                WHERE   to_char(CreateDate,'mm') = to_char(sysdate,'mm')
                GROUP BY to_char(CreateDate,'dd') 
*/

            StringBuilder strSql = new StringBuilder();
            //2014-05-08
            //Add By Joker
            #region 语句差异化处理
            if (DataFactory.DbType == SqlSourceType.Oracle)
            {

                strSql.Append(@"SELECT  to_char(CreateDate,'dd') AS data ,
                                    COUNT(1) AS count
                            FROM    AMS_SysLoginLog
                            WHERE   to_char(CreateDate,'mm') = to_char(sysdate,'mm')
                            GROUP BY to_char(CreateDate,'dd') ");
            }
            else if (DataFactory.DbType == SqlSourceType.SQLServer)
            {
                strSql.Append(@"SELECT  DAY(CreateDate) AS data ,
                                    COUNT(1) AS count
                            FROM    AMS_SysLoginLog
                            WHERE   MONTH(CreateDate) = MONTH(GETDATE())
                            GROUP BY DAY(CreateDate) ");
            }
            else if (DataFactory.DbType == SqlSourceType.MySql)
            {
                strSql.Append(@"SELECT  DAY(CreateDate) AS data ,
                                    COUNT(1) AS count
                            FROM    AMS_SysLoginLog
                            WHERE   MONTH(CreateDate) = MONTH(curdate())
                            GROUP BY DAY(CreateDate) ");
            }
            #endregion

            return DbHelper.GetDataTableBySQL(strSql);
        }
        /// <summary>
        /// 登录日志列表(带条件)
        /// </summary>
        /// <param name="Parm_Key_Value">搜索条件键值</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        public IList GetLoginLogList(Hashtable Parm_Key_Value, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder SqlWhere = new StringBuilder();
            List<SqlParam> ListParam = new List<SqlParam>();
            //账户
            if (Parm_Key_Value["Account"] != null && !string.IsNullOrEmpty(Parm_Key_Value["Account"].ToString()))
            {
                SqlWhere.Append(" AND Account like @Account");
                ListParam.Add(new SqlParam("@Account", '%' + Parm_Key_Value["Account"].ToString() + '%'));
            }
            //日期
            if (!string.IsNullOrEmpty(Parm_Key_Value["start_Date"].ToString()) || !string.IsNullOrEmpty(Parm_Key_Value["end_Date"].ToString()))
            {
                SqlWhere.Append(" AND CreateDate >= @Start_BillDate");
                SqlWhere.Append(" AND CreateDate <= @End_BillDate");
                ListParam.Add(new SqlParam("@Start_BillDate", CommonHelper.GetDateTime(Parm_Key_Value["start_Date"].ToString())));
                ListParam.Add(new SqlParam("@End_BillDate", CommonHelper.GetDateTime(Parm_Key_Value["end_Date"].ToString())));
            }

            //return dal.GetLoginLogList(SqlWhere, ListParam.ToArray(), orderField, orderType, pageIndex, pageSize, ref  count);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM AMS_SysLoginLog WHERE 1=1");
            strSql.Append(SqlWhere);
            return DbHelper.GetPageList<AMS_SysLoginLog>(strSql.ToString(), ListParam.ToArray(), CommonHelper.ToOrderField("CreateDate", orderField), orderType, pageIndex, pageSize, ref count);


        }
        /// <summary>
        /// 清空操作日志
        /// </summary>
        /// <param name="KeepTime">日志保留时间</param>
        /// <returns></returns>
        public bool EmptyLoginLog(string KeepTime)
        {
            DateTime CreateDate = DateTime.Now;
            if (KeepTime == "7")//保留近一周
            {
                CreateDate = DateTime.Now.AddDays(-7);
            }
            else if (KeepTime == "1")//保留近一个月
            {
                CreateDate = DateTime.Now.AddMonths(-1);
            }
            else if (KeepTime == "3")//保留近三个月
            {
                CreateDate = DateTime.Now.AddMonths(-3);
            }
            else if (KeepTime == "0")//不保留，全部删除
            {
                //return dal.RemoveAllLoginLog() >= 0 ? true : false;
                StringBuilder strSql2 = new StringBuilder("DELETE FROM AMS_SysLoginLog");
                int isOk = DbHelper.ExecuteBySql(strSql2);
                return isOk > 0 ? true : false;
            }
            //return dal.EmptyLoginLog(CreateDate) >= 0 ? true : false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"DELETE FROM AMS_SysLoginLog WHERE 1=1");
            strSql.Append(" AND CreateDate <= @CreateDate");
            SqlParam[] param = {
                                         new SqlParam("@CreateDate",CreateDate)};
            int isOk2 = DbHelper.ExecuteBySql(strSql, param);

            return isOk2 > 0 ? true : false;
        }
        #endregion

        #region 系统异常日志
        #endregion

        #region 操作日志

        /// <summary>
        /// 获取实体类主键字段
        /// </summary>
        /// <param name="obj">业务实体</param>
        /// <returns></returns>
        private object GetKeyField(Type t)
        {
            string _KeyField = "";
            KeyAttribute KeyField;
            var name = t.Name;
            foreach (Attribute attr in t.GetCustomAttributes(true))
            {
                KeyField = attr as KeyAttribute;
                if (KeyField != null)
                    _KeyField = KeyField.Name;
            }
            return _KeyField;
        }
        /// <summary>
        /// 获取实体类主键字段值
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public object GetKeyFieldValue<T>(T entity)
        {
            Type objTye = typeof(T);
            string _KeyField = "";
            KeyAttribute KeyField;
            var name = objTye.Name;
            foreach (Attribute attr in objTye.GetCustomAttributes(true))
            {
                KeyField = attr as KeyAttribute;
                if (KeyField != null)
                    _KeyField = KeyField.Name;
            }
            PropertyInfo property = objTye.GetProperty(_KeyField);
            return property.GetValue(entity, null).ToString();
        }
        /// <summary>
        /// 获取实体类中文名称
        /// </summary>
        /// <param name="obj">业务实体</param>
        /// <returns></returns>
        private string GetBusingessName<T>()
        {
            Type objTye = typeof(T);
            string entityName = "";
            var busingessNames = objTye.GetCustomAttributes(true).OfType<DescriptionAttribute>();
            var descriptionAttributes = busingessNames as DescriptionAttribute[] ?? busingessNames.ToArray();
            if (descriptionAttributes.Any())
                entityName = descriptionAttributes.ToList()[0].Description;
            else
            {
                entityName = objTye.Name;
            }
            return entityName;
        }
        /// <summary>
        /// 获取实体类 字段中文名称
        /// </summary>
        /// <param name="pi">字段属性信息</param>
        /// <returns></returns>
        private string GetFieldText(PropertyInfo pi)
        {
            DescriptionAttribute descAttr;
            string txt = "";
            var descAttrs = pi.GetCustomAttributes(typeof(DescriptionAttribute), true);
            if (descAttrs.Any())
            {
                descAttr = descAttrs[0] as DescriptionAttribute;
                txt = descAttr.Description;
            }
            else
            {
                txt = pi.Name;
            }
            return txt;
        }

        /// <summary>
        /// 添加操作作业日志
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="CreateUserId">创建用户主键</param>
        /// <param name="CreateUserName">创建用户</param>
        /// <param name="busingessName">业务名称</param>
        /// <returns></returns>
        public void AddTaskLog<T>(T entity, string CreateUserId, string CreateUserName, string busingessName = "")
        {
            //dal.AddTaskLog<T>(entity, CreateUserId, CreateUserName, busingessName);

            try
            {
                List<StringBuilder> ListSql = new List<StringBuilder>();
                List<object> ListParam = new List<object>();
                AMS_SysLogs SysLog = new AMS_SysLogs();
                SysLog.SyslogsId = CommonHelper.GetGuid;
                SysLog.BusinessName = busingessName;
                SysLog.OperationIp = RequestHelper.GetIPAddress();
                SysLog.CreateUserId = CreateUserId;
                SysLog.CreateUserName = CreateUserName;
                SysLog.OperationType = (int)OperationType.Other;
                ListSql.Add(SqlParamHelper.InsertSql(SysLog));
                ListParam.Add(SqlParamHelper.GetParameter(SysLog));
                //添加日志详细信息
                AMS_SysLogDetails SysLogDetails = new AMS_SysLogDetails();
                SysLogDetails.SysLogDetailsId = CommonHelper.GetGuid;
                SysLogDetails.SyslogsId = SysLog.SyslogsId;
                SysLogDetails.Remark = busingessName;
                ListSql.Add(SqlParamHelper.InsertSql(SysLogDetails));
                ListParam.Add(SqlParamHelper.GetParameter(SysLogDetails));
                DbHelper.BatchExecuteBySql(ListSql.ToArray(), ListParam.ToArray());
            }
            finally
            {

            }
        }
        /// <summary>
        /// 添加更新操作日志
        /// </summary>
        /// <param name="oldObj">旧实体对象</param>
        /// <param name="newObj">新实体对象</param>
        /// <param name="CreateUserId">创建用户主键</param>
        /// <param name="CreateUserName">创建用户</param>
        /// <param name="busingessName">业务名称</param>
        /// <returns></returns>
        public void UpdateTaskLog<T>(T oldObj, T newObj, string CreateUserId, string CreateUserName, string busingessName = "")
        {
            //dal.UpdateTaskLog<T>(oldObj, newObj, CreateUserId, CreateUserName, busingessName);
            // AddTaskLog<T>(T entity, string CreateUserId, string CreateUserName, string busingessName = "")

            try
            {
                List<StringBuilder> ListSql = new List<StringBuilder>();
                List<object> ListParam = new List<object>();
                Type objTye = typeof(T);
                AMS_SysLogs SysLog = new AMS_SysLogs();
                SysLog.SyslogsId = CommonHelper.GetGuid;
                SysLog.TableName = oldObj.ToString();
                if (busingessName == "")
                {
                    busingessName = GetBusingessName<T>();
                }
                SysLog.BusinessName = busingessName;
                SysLog.OperationIp = RequestHelper.GetIPAddress();
                SysLog.CreateUserId = CreateUserId;
                SysLog.CreateUserName = CreateUserName;
                SysLog.OperationType = (int)OperationType.Add;
                PropertyInfo property = objTye.GetProperty(GetKeyField(objTye).ToString());
                SysLog.Object_ID = property.GetValue(oldObj, null).ToString();
                ListSql.Add(SqlParamHelper.InsertSql(SysLog));
                ListParam.Add(SqlParamHelper.GetParameter(SysLog));
                //添加日志详细信息
                foreach (PropertyInfo pi in objTye.GetProperties())
                {
                    if (pi.GetValue(oldObj, null) != null)
                    {
                        AMS_SysLogDetails SysLogDetails = new AMS_SysLogDetails();
                        SysLogDetails.SysLogDetailsId = CommonHelper.GetGuid;
                        SysLogDetails.SyslogsId = SysLog.SyslogsId;
                        SysLogDetails.FieldName = GetFieldText(pi);
                        SysLogDetails.FieldText = pi.Name;
                        SysLogDetails.NewValue = "" + pi.GetValue(oldObj, null) + "";
                        ListSql.Add(SqlParamHelper.InsertSql(SysLogDetails));
                        ListParam.Add(SqlParamHelper.GetParameter(SysLogDetails));
                    }
                }
                DbHelper.BatchExecuteBySql(ListSql.ToArray(), ListParam.ToArray());
            }
            finally
            {

            }
        }
        /// <summary>
        /// 执行删除的操作日志
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <param name="CreateUserId">创建用户主键</param>
        /// <param name="CreateUserName">创建用户</param>
        /// <param name="busingessName">业务名称</param>
        /// <returns></returns>
        public void DeleteTaskLog<T>(T entity, string CreateUserId, string CreateUserName, string busingessName = "")
        {
            //dal.DeleteTaskLog<T>(entity, CreateUserId, CreateUserName, busingessName);
            //DeleteTaskLog<T>(T entity, string CreateUserId, string CreateUserName, string busingessName = "")

            try
            {
                List<StringBuilder> ListSql = new List<StringBuilder>();
                List<object> ListParam = new List<object>();
                Type objTye = typeof(T);
                AMS_SysLogs SysLog = new AMS_SysLogs();
                SysLog.SyslogsId = CommonHelper.GetGuid;
                SysLog.TableName = entity.ToString();
                if (busingessName == "")
                {
                    busingessName = GetBusingessName<T>();
                }
                SysLog.BusinessName = busingessName;
                SysLog.OperationIp = RequestHelper.GetIPAddress();
                SysLog.CreateUserId = CreateUserId;
                SysLog.CreateUserName = CreateUserName;
                SysLog.OperationType = (int)OperationType.Delete;
                PropertyInfo property = objTye.GetProperty(GetKeyField(objTye).ToString());
                SysLog.Object_ID = property.GetValue(entity, null).ToString();
                ListSql.Add(SqlParamHelper.InsertSql(SysLog));
                ListParam.Add(SqlParamHelper.GetParameter(SysLog));
                //添加日志详细信息
                foreach (PropertyInfo pi in objTye.GetProperties())
                {
                    if (pi.GetValue(entity, null) != null)
                    {
                        AMS_SysLogDetails SysLogDetails = new AMS_SysLogDetails();
                        SysLogDetails.SysLogDetailsId = CommonHelper.GetGuid;
                        SysLogDetails.SyslogsId = SysLog.SyslogsId;
                        SysLogDetails.FieldName = GetFieldText(pi);
                        SysLogDetails.FieldText = pi.Name;
                        SysLogDetails.OldValue = "" + pi.GetValue(entity, null) + "";
                        ListSql.Add(SqlParamHelper.InsertSql(SysLogDetails));
                        ListParam.Add(SqlParamHelper.GetParameter(SysLogDetails));
                    }
                }
                DbHelper.BatchExecuteBySql(ListSql.ToArray(), ListParam.ToArray());
            }
            finally
            {

            }
        }
        /// <summary>
        /// 操作日志列表(带条件)
        /// </summary>
        /// <param name="Parm_Key_Value">搜索条件键值</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        public IList GetLogList(Hashtable Parm_Key_Value, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder SqlWhere = new StringBuilder();
            List<SqlParam> ListParam = new List<SqlParam>();
            //操作用户
            if (Parm_Key_Value["CreateUserName"] != null && !string.IsNullOrEmpty(Parm_Key_Value["CreateUserName"].ToString()))
            {
                SqlWhere.Append(" AND CreateUserName like @CreateUserName");
                ListParam.Add(new SqlParam("@CreateUserName", '%' + Parm_Key_Value["CreateUserName"].ToString() + '%'));
            }
            //操作类型
            if (Parm_Key_Value["OperationType"] != null && !string.IsNullOrEmpty(Parm_Key_Value["OperationType"].ToString()))
            {
                SqlWhere.Append(" AND OperationType = @OperationType");
                ListParam.Add(new SqlParam("@OperationType", Parm_Key_Value["OperationType"].ToString()));
            }
            //操作模块
            if (Parm_Key_Value["BusinessName"] != null && !string.IsNullOrEmpty(Parm_Key_Value["BusinessName"].ToString()))
            {
                SqlWhere.Append(" AND BusinessName like @BusinessName");
                ListParam.Add(new SqlParam("@BusinessName", '%' + Parm_Key_Value["BusinessName"].ToString() + '%'));
            }
            //日期
            if (!string.IsNullOrEmpty(Parm_Key_Value["start_Date"].ToString()) || !string.IsNullOrEmpty(Parm_Key_Value["end_Date"].ToString()))
            {
                SqlWhere.Append(" AND CreateDate >= @Start_BillDate");
                SqlWhere.Append(" AND CreateDate <= @End_BillDate");
                ListParam.Add(new SqlParam("@Start_BillDate", CommonHelper.GetDateTime(Parm_Key_Value["start_Date"].ToString())));
                ListParam.Add(new SqlParam("@End_BillDate", CommonHelper.GetDateTime(Parm_Key_Value["end_Date"].ToString())));
            }

            //return dal.GetLogList(SqlWhere, ListParam.ToArray(), orderField, orderType, pageIndex, pageSize, ref  count);
            //GetLogList(StringBuilder where, SqlParam[] param, string orderField, string orderType, int pageIndex, int pageSize, ref int count)

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM AMS_SysLogs WHERE 1=1");
            strSql.Append(SqlWhere);
            return DbHelper.GetPageList<AMS_SysLogs>(strSql.ToString(), ListParam.ToArray(), CommonHelper.ToOrderField("CreateDate", orderField), orderType, pageIndex, pageSize, ref count);

        }
        /// <summary>
        /// 操作日志详细信息列表
        /// </summary>
        /// <param name="SyslogsId">日志主表ID</param>
        /// <returns></returns>
        public IList GetLogDetailList(string SyslogsId)
        {
            //return dal.GetLogDetailList(SyslogsId);
            //GetLogDetailList(string SyslogsId)
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM AMS_SysLogDetails WHERE 1=1");
            strSql.Append(" AND SyslogsId = @SyslogsId");
            SqlParam[] param = {
                                         new SqlParam("@SyslogsId",SyslogsId)};
            return DbHelper.GetDataListBySQL<AMS_SysLogDetails>(strSql, param);
        }
        /// <summary>
        /// 清空操作日志
        /// </summary>
        /// <param name="KeepTime">日志保留时间</param>
        /// <returns></returns>
        public bool EmptyLog(string KeepTime)
        {
            DateTime CreateDate = DateTime.Now;
            if (KeepTime == "7")//保留近一周
            {
                CreateDate = DateTime.Now.AddDays(-7);
            }
            else if (KeepTime == "1")//保留近一个月
            {
                CreateDate = DateTime.Now.AddMonths(-1);
            }
            else if (KeepTime == "3")//保留近三个月
            {
                CreateDate = DateTime.Now.AddMonths(-3);
            }
            else if (KeepTime == "0")//不保留，全部删除
            {
                //return dal.RemoveAllLog() >= 0 ? true : false;

                StringBuilder strSql2 = new StringBuilder("DELETE FROM AMS_SysLogs");
                int IsOK = DbHelper.ExecuteBySql(strSql2);
                return IsOK >= 0 ? true : false;
            }

            //return dal.EmptyLog(CreateDate) >= 0 ? true : false;

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"DELETE FROM AMS_SysLogs WHERE 1=1");
            strSql.Append(" AND CreateDate <= @CreateDate");
            SqlParam[] param = { 
                                   new SqlParam("@CreateDate", CreateDate) 
                               };
            int IsOK2 =  DbHelper.ExecuteBySql(strSql, param);
            return IsOK2 >= 0 ? true : false;
        }
        #endregion
    }
}

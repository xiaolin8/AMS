using System;
using System.Collections;
using System.Data;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// IP黑名单
    /// </summary>
    public class AMS_IPBlacklistBLL : ServiceBase
    {
        //private readonly AMS_IPBlacklistDAL dal = new AMS_IPBlacklistDAL();

        #region Method
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        public object GetMaxCode()
        {
            return DbUtils.GetMax("AMS_IPBlacklist", "SortCode");
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(AMS_IPBlacklist entity)
        {
            entity.SortCode = CommonHelper.GetInt(this.GetMaxCode());
            int IsOk = DbUtils.Insert(entity);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<AMS_IPBlacklist>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(AMS_IPBlacklist entity)
        {
            #region 获取旧值
            var oldEntity = this.GetEntity(AMS_SysLogBLL.Instance.GetKeyFieldValue<AMS_IPBlacklist>(entity).ToString());
            #endregion
            int IsOk = DbUtils.Update(entity, "IPBlacklistId");
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.UpdateTaskLog<AMS_IPBlacklist>(oldEntity, entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public bool Delete(string KeyValue)
        {
            #region 获取旧值
            var oldEntity = this.GetEntity(KeyValue);
            #endregion
            int IsOk = DbUtils.Delete("AMS_IPBlacklist", "IPBlacklistId", KeyValue);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.DeleteTaskLog<AMS_IPBlacklist>(oldEntity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public bool BatchDelete(string[] KeyValue)
        {
            int IsOk = DbUtils.BatchDelete("AMS_IPBlacklist", "IPBlacklistId", KeyValue);
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public AMS_IPBlacklist GetEntity(string KeyValue)
        {
            return DbUtils.GetModelById<AMS_IPBlacklist>("IPBlacklistId", KeyValue);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public IList GetList()
        {
            return this.GetListWhere(null, null);
        }
        /// <summary>
        /// 获得数据列表(带条件)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public IList GetListWhere(StringBuilder where, SqlParam[] param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AMS_IPBlacklist WHERE 1=1");
            strSql.Append(where);
            return DbHelper.GetDataListBySQL<AMS_IPBlacklist>(strSql, param);
        }
        #endregion

        /// <summary>
        /// 获得不允许登录IP范围列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns></returns>
        public IList GetIPBlacklist()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AMS_IPBlacklist WHERE 1=1");
            strSql.Append(" AND Enabled =1 ");
            strSql.Append(" AND Failuretime >= @Failuretime ");
            SqlParam[] param = {
                                         new SqlParam("@Failuretime",DbType.DateTime, DateTime.Now)};
            return DbHelper.GetDataListBySQL<AMS_IPBlacklist>(strSql, param);
        }

        /// <summary>
        /// 登录系统IP限制，是否允许登录该系统
        /// </summary>
        /// <returns></returns>
        public bool TheIpIsRange(string ip)
        {
            bool IsOk = false;
            if (CommonHelper.GetBool(ConfigHelper.GetValue("CheckIPAddress")))
            {
                foreach (AMS_IPBlacklist item in GetIPBlacklist())
                {
                    long start = IP2Long(item.StartIp);
                    long end = IP2Long(item.EndIp);
                    long ipAddress = IP2Long(ip);
                    IsOk = (ipAddress >= start && ipAddress <= end);
                    if (IsOk)
                    {
                        throw new Exception(MessageHelper.MSG0030);
                    }
                }
            }
            else
            {
                IsOk = true;
            }
            return IsOk;
        }
        private long IP2Long(string ip)
        {
            string[] ipBytes;
            double num = 0;
            if (!string.IsNullOrEmpty(ip))
            {
                ipBytes = ip.Split('.');
                for (int i = ipBytes.Length - 1; i >= 0; i--)
                {
                    num += ((int.Parse(ipBytes[i]) % 256) * Math.Pow(256, (3 - i)));
                }
            }
            return (long)num;
        }
    }
}
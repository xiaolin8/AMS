using System.Collections;
using System.Text;
using System.Collections.Generic;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 系统接口访问
    /// </summary>
    public class AMS_InterfaceAccessBLL : ServiceBase
    {
        //private readonly AMS_InterfaceAccessDAL dal = new AMS_InterfaceAccessDAL();

        #region Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(AMS_InterfaceAccess entity)
        {
            int IsOk = DbUtils.Insert(entity);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<AMS_InterfaceAccess>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(AMS_InterfaceAccess entity)
        {
            #region 获取旧值
            var oldEntity = this.GetEntity(AMS_SysLogBLL.Instance.GetKeyFieldValue<AMS_InterfaceAccess>(entity).ToString());
            #endregion
            int IsOk = DbUtils.Update(entity, "IAccessId");
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.UpdateTaskLog<AMS_InterfaceAccess>(oldEntity, entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.Delete("AMS_InterfaceAccess", "IAccessId", KeyValue);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.DeleteTaskLog<AMS_InterfaceAccess>(oldEntity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.BatchDelete("AMS_InterfaceAccess", "IAccessId", KeyValue);
            return IsOk >= 0 ? true : false;

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public AMS_InterfaceAccess GetEntity(string KeyValue)
        {
            return DbUtils.GetModelById<AMS_InterfaceAccess>("IAccessId", KeyValue);
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
        /// <param name="Parm_Key_Value">搜索条件键值</param>
        /// <returns></returns>
        public IList GetListWhere(Hashtable Parm_Key_Value)
        {
            StringBuilder SqlWhere = new StringBuilder();
            List<SqlParam> ListParam = new List<SqlParam>();
            //编号
            if (Parm_Key_Value["Code"] != null && !string.IsNullOrEmpty(Parm_Key_Value["Code"].ToString()))
            {
                SqlWhere.Append(" AND Code like @Code");
                ListParam.Add(new SqlParam("@Code", '%' + Parm_Key_Value["Code"].ToString() + '%'));
            }
            //名称
            if (Parm_Key_Value["AuthorizationUserName"] != null && !string.IsNullOrEmpty(Parm_Key_Value["AuthorizationUserName"].ToString()))
            {
                SqlWhere.Append(" AND AuthorizationUserName like @AuthorizationUserName");
                ListParam.Add(new SqlParam("@AuthorizationUserName", '%' + Parm_Key_Value["AuthorizationUserName"].ToString() + '%'));
            }
            return GetListWhere(SqlWhere, ListParam.ToArray());
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
            strSql.Append("SELECT * FROM AMS_InterfaceAccess WHERE 1=1");
            strSql.Append(where);
            return DbHelper.GetDataListBySQL<AMS_InterfaceAccess>(strSql, param);
        }
        #endregion


        /// <summary>
        /// 验证是否有效用户可以访问接口
        /// </summary>
        /// <param name="AuthorizationUserId">用户主键</param>
        /// <returns></returns>
        public bool IsEnabled(string AuthorizationUserId)
        {
            if (CommonHelper.GetBool(ConfigHelper.GetValue("CheckInterface")))
            {
                return true;
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  COUNT(1)
                            FROM    AMS_InterfaceAccess
                            WHERE   AuthorizationUserId = @AuthorizationUserId
                                    AND GETDATE() BETWEEN StartDate AND EndDate AND Enabled=1");
            SqlParam[] param = {
                                         new SqlParam("@AuthorizationUserId",AuthorizationUserId)};

            
            int IsOK = CommonHelper.GetInt(DbHelper.GetObjectValue(strSql, param));

            return IsOK > 0 ? true : false;
        }
    }
}
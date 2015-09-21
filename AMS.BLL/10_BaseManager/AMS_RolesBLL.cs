using System.Collections;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 系统角色
    /// </summary>
    public class AMS_RolesBLL : ServiceBase
    {
        //private readonly AMS_RolesDAL dal = new AMS_RolesDAL();

        #region Method
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        public object GetMaxCode()
        {
            return DbUtils.GetMax("AMS_Roles", "SortCode");
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        public int GetRecordCount(string KeyValue)
        {
            return DbUtils.RecordCount("AMS_Roles", "RoleId", KeyValue);
        }
        /// <summary>
        /// 获取记录总数（带条件）
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public int GetRecordCount(StringBuilder where, SqlParam[] param)
        {
            return DbUtils.RecordCount("AMS_Roles", where, param);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(AMS_Roles entity)
        {
            //return dal.Insert(entity) >= 0 ? true : false;
            entity.SortCode = CommonHelper.GetInt(this.GetMaxCode());
            int IsOk = DbUtils.Insert(entity);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<AMS_Roles>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(AMS_Roles entity)
        {
            //return dal.Update(entity) >= 0 ? true : false;
            #region 获取旧值
            var oldEntity = this.GetEntity(AMS_SysLogBLL.Instance.GetKeyFieldValue<AMS_Roles>(entity).ToString());
            #endregion
            int IsOk = DbUtils.Update(entity, "RoleId");
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.UpdateTaskLog<AMS_Roles>(oldEntity, entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            //return dal.Delete(KeyValue) >= 0 ? true : false;
            #region 获取旧值
            var oldEntity = this.GetEntity(KeyValue);
            #endregion
            int IsOk = DbUtils.Delete("AMS_Roles", "RoleId", KeyValue);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.DeleteTaskLog<AMS_Roles>(oldEntity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            //return dal.BatchDelete(KeyValue) >= 0 ? true : false;
            int IsOK = DbUtils.BatchDelete("AMS_Roles", "RoleId", KeyValue);
            return IsOK >= 0 ? true : false;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public AMS_Roles GetEntity(string KeyValue)
        {
            return DbUtils.GetModelById<AMS_Roles>("RoleId", KeyValue);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="OrganizationId">公司主键</param>
        /// <returns></returns>
        public IList GetList(string OrganizationId)
        {
            if (string.IsNullOrEmpty(OrganizationId)) return null;
            StringBuilder where = new StringBuilder();
            where.Append(" AND OrganizationId = @OrganizationId");
            SqlParam[] param = {
                                         new SqlParam("@OrganizationId",OrganizationId)};
            return GetListWhere(where, param);
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
            strSql.Append(@"SELECT  * FROM 
                            ( SELECT    R.RoleId ,
                                        R.Code ,
										R.OrganizationId,
                                        R.FullName ,
                                        I.ItemName AS Category ,
                                        R.Description ,
                                        R.AllowEdit ,
                                        R.AllowDelete ,
                                        R.Enabled
                              FROM      AMS_Roles R
                                        LEFT JOIN AMS_ItemDetails I ON R.Category = I.ItemCode
                              WHERE     I.ItemsId = '5fed1313-7355-4cc4-a7ec-73211a361fa6'
                            )A WHERE 1=1");
            strSql.Append(where);
            return DbHelper.GetDataListBySQL<AMS_Roles>(strSql, param);
        }
        #endregion
    }
}
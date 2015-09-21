using System.Collections;
using System.Data;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 数据权限存储表
    /// </summary>
    public class AMS_DataPermissionBLL : ServiceBase
    {
        //private readonly AMS_DataPermissionDAL dal = new AMS_DataPermissionDAL();

        #region Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(AMS_DataPermission entity)
        {
            int IsOk = DbUtils.Insert(entity);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<AMS_DataPermission>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(AMS_DataPermission entity)
        {
            #region 获取旧值
            var oldEntity = this.GetEntity(AMS_SysLogBLL.Instance.GetKeyFieldValue<AMS_DataPermission>(entity).ToString());
            #endregion
            int IsOk = DbUtils.Update(entity, "DataPermissionId");
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.UpdateTaskLog<AMS_DataPermission>(oldEntity, entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public AMS_DataPermission GetEntity(string KeyValue)
        {
            return DbUtils.GetModelById<AMS_DataPermission>("DataPermissionId", KeyValue);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="RoleId">角色主键</param>
        /// <param name="ResourceId">授权项目</param>
        /// <returns></returns>
        public AMS_DataPermission GetEntity(string RoleId, string ResourceId)
        {
            StringBuilder where = new StringBuilder();
            where.Append(" AND RoleId = @RoleId");
            where.Append(" AND ResourceId = @ResourceId");
            SqlParam[] para = {
                                        new SqlParam("@RoleId",RoleId),
                                        new SqlParam("@ResourceId", ResourceId)};
            return GetEntity(where, para);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public AMS_DataPermission GetEntity(StringBuilder where, SqlParam[] param)
        {
            AMS_DataPermission entity = new AMS_DataPermission();
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  * FROM AMS_DataPermission WHERE 1=1");
            strSql.Append(where);
            DataTable dt = DbHelper.GetDataTableBySQL(strSql, param);
            if (dt.Rows.Count > 0)
            {
                return DbReader.ReaderToModel<AMS_DataPermission>(dt.Rows[0]);
            }
            else
            {
                return entity;
            }
        }
        #endregion

        #region 授权项目
        /// <summary>
        /// 组织机构列表
        /// </summary>
        /// <returns></returns>
        public IList GetOrganizationList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AMS_Organization WHERE 1=1");
            strSql.Append(" Order BY SortCode");
            return DbHelper.GetDataListBySQL<AMS_Organization>(strSql);
        }
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public IList GetUserList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AMS_User WHERE 1=1");
            strSql.Append(" Order BY SortCode");
            return DbHelper.GetDataListBySQL<AMS_User>(strSql);
        }
        #endregion
    }
}
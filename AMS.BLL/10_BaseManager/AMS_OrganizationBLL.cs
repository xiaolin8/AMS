using System.Collections;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 组织机构、部门
    /// </summary>
    public class AMS_OrganizationBLL : ServiceBase
    {
        //private readonly AMS_OrganizationDAL dal = new AMS_OrganizationDAL();

        #region Method
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        public object GetMaxCode()
        {
            return DbUtils.GetMax("AMS_Organization", "SortCode");
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        public int GetRecordCount(string KeyValue)
        {
            //return dal.GetRecordCount(KeyValue);
            return DbUtils.RecordCount("AMS_Organization", "OrganizationId", KeyValue);

        }

        /// <summary>
        /// 获取记录总数（带条件）
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public int GetRecordCount(StringBuilder where, SqlParam[] param)
        {
            return DbUtils.RecordCount("AMS_Organization", where, param);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(AMS_Organization entity)
        {
            //return dal.Insert(entity) >= 0 ? true : false;
            entity.SortCode = CommonHelper.GetInt(this.GetMaxCode());
            int IsOk = DbUtils.Insert(entity);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<AMS_Organization>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(AMS_Organization entity)
        {
            //return dal.Update(entity) >= 0 ? true : false;
            #region 获取旧值
            var oldEntity = this.GetEntity(AMS_SysLogBLL.Instance.GetKeyFieldValue<AMS_Organization>(entity).ToString());
            #endregion
            int IsOk = DbUtils.Update(entity, "OrganizationId");
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.UpdateTaskLog<AMS_Organization>(oldEntity, entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.Delete("AMS_Organization", "OrganizationId", KeyValue);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.DeleteTaskLog<AMS_Organization>(oldEntity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.BatchDelete("AMS_Organization", "OrganizationId", KeyValue);
            return IsOk >= 0 ? true : false;

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public AMS_Organization GetEntity(string KeyValue)
        {
            //return dal.GetEntity(KeyValue);
            return DbUtils.GetModelById<AMS_Organization>("OrganizationId", KeyValue);

        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public IList GetList()
        {
            //return dal.GetListWhere(null, null);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AMS_Organization WHERE 1=1");
            strSql.Append(" Order BY SortCode");
            return DbHelper.GetDataListBySQL<AMS_Organization>(strSql);

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
            strSql.Append("SELECT * FROM AMS_Organization WHERE 1=1");
            strSql.Append(where);
            strSql.Append(DataPermission.Instance.Organization);
            strSql.Append(" Order BY SortCode");
            return DbHelper.GetDataListBySQL<AMS_Organization>(strSql, param);
        }
        #endregion

        /// <summary>
        /// 根据组织机构主键，判断是否有下级节点
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public bool IsBelowMenu(string KeyValue)
        {
            StringBuilder where = new StringBuilder();
            where.Append(" AND ParentId = @ParentId");
            SqlParam[] param = {
                                         new SqlParam("@ParentId",KeyValue)};
            return GetRecordCount(where, param) > 0 ? true : false;
        }
    }
}
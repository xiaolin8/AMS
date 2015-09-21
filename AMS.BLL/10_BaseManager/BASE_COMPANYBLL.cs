using System.Collections;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 组织机构、部门
    /// </summary>
    public class BASE_COMPANYBLL : ServiceBase
    {
        //private readonly BASE_COMPANYDAL dal = new BASE_COMPANYDAL();

        #region Method
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        public object GetMaxCode()
        {
            return DbUtils.GetMax("BASE_COMPANY", "SortCode");
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        public int GetRecordCount(string KeyValue)
        {
            //return dal.GetRecordCount(KeyValue);
            return DbUtils.RecordCount("BASE_COMPANY", "OrganizationId", KeyValue);
        }

        /// <summary>
        /// 获取记录总数（带条件）
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public int GetRecordCount(StringBuilder where, SqlParam[] param)
        {
            return DbUtils.RecordCount("BASE_COMPANY", where, param);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(BASE_COMPANY entity)
        {
            //return dal.Insert(entity) >= 0 ? true : false;
            entity.sortcode = CommonHelper.GetInt(this.GetMaxCode());
            int IsOk = DbUtils.Insert(entity);
            #region 写入操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<BASE_COMPANY>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false; ;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(BASE_COMPANY entity)
        {
            //return dal.Update(entity) >= 0 ? true : false;

            #region 获取旧值
            var oldEntity = this.GetEntity(AMS_SysLogBLL.Instance.GetKeyFieldValue<BASE_COMPANY>(entity).ToString());
            #endregion
            int IsOk = DbUtils.Update(entity, "OrganizationId");
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.UpdateTaskLog<BASE_COMPANY>(oldEntity, entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.Delete("BASE_COMPANY", "OrganizationId", KeyValue);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.DeleteTaskLog<BASE_COMPANY>(oldEntity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.BatchDelete("BASE_COMPANY", "OrganizationId", KeyValue);
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public BASE_COMPANY GetEntity(string KeyValue)
        {
            //return dal.GetEntity(KeyValue);
            return DbUtils.GetModelById<BASE_COMPANY>("OrganizationId", KeyValue);

        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public IList GetList()
        {
            //return dal.GetListWhere(null, null);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM BASE_COMPANY WHERE 1=1");
            strSql.Append(" Order BY SortCode");
            return DbHelper.GetDataListBySQL<BASE_COMPANY>(strSql);

        }
        /// <summary>
        /// 获得数据列表(带条件)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public IList GetListWhere(StringBuilder where, SqlParam[] param)
        {
            //return dal.GetListWhere(where, param);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM BASE_COMPANY WHERE 1=1");
            //strSql.Append(where);
            //strSql.Append(DataPermission.Instance.Organization);
            strSql.Append(" Order BY SortCode");
            return DbHelper.GetDataListBySQL<BASE_COMPANY>(strSql, param);

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
            //return dal.GetRecordCount(where, param) > 0 ? true : false;
            return GetRecordCount(where, param) > 0 ? true : false;
        }
    }
}
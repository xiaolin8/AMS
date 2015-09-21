using System.Collections;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 附加属性信息
    /// </summary>
    public class AMS_AppendPropertyBLL : ServiceBase
    {
        //private readonly AMS_AppendPropertyDAL dal = new AMS_AppendPropertyDAL();

        #region Method
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        public object GetMaxCode()
        {
            return DbUtils.GetMax("AMS_AppendProperty", "SortCode");
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(AMS_AppendProperty entity)
        {
            int IsOk = DbUtils.Insert(entity);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<AMS_AppendProperty>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(AMS_AppendProperty entity)
        {
            #region 获取旧值
            var oldEntity = this.GetEntity(AMS_SysLogBLL.Instance.GetKeyFieldValue<AMS_AppendProperty>(entity).ToString());
            #endregion
            int IsOk = DbUtils.Update(entity, "PropertyId");
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.UpdateTaskLog<AMS_AppendProperty>(oldEntity, entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.Delete("AMS_AppendProperty", "PropertyId", KeyValue);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.DeleteTaskLog<AMS_AppendProperty>(oldEntity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public AMS_AppendProperty GetEntity(string KeyValue)
        {
            return DbUtils.GetModelById<AMS_AppendProperty>("PropertyId", KeyValue);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public IList GetList(string ParentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AMS_AppendProperty WHERE 1=1");
            strSql.Append(" AND ParentId = @ParentId");
            SqlParam[] param = {
                                         new SqlParam("@ParentId",ParentId)};
            return DbHelper.GetDataListBySQL<AMS_AppendProperty>(strSql, param);

        }
        #endregion
    }
}
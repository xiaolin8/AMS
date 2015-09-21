using System.Collections;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 数据字典主表
    /// </summary>
    public class AMS_ItemsBLL : ServiceBase
    {
        //private readonly AMS_ItemsDAL dal = new AMS_ItemsDAL();
        //private readonly AMS_ItemDetailsDAL details_dal = new AMS_ItemDetailsDAL();

        #region Method
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        public object GetMaxCode()
        {
            return DbUtils.GetMax("AMS_Items", "SortCode");
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        public int GetRecordCount(string KeyValue)
        {
            return DbUtils.RecordCount("AMS_Items", "ItemsId", KeyValue);
        }
        /// <summary>
        /// 获取记录总数（带条件）
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public int GetRecordCount(StringBuilder where, SqlParam[] param)
        {
            return DbUtils.RecordCount("AMS_Items", where, param);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(AMS_Items entity)
        {
            //return dal.Insert(entity) >= 0 ? true : false;
            int IsOk = DbUtils.Insert(entity);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<AMS_Items>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(AMS_Items entity)
        {
            //return dal.Update(entity) >= 0 ? true : false;
            #region 获取旧值
            var oldEntity = this.GetEntity(AMS_SysLogBLL.Instance.GetKeyFieldValue<AMS_Items>(entity).ToString());
            #endregion
            int IsOk = DbUtils.Update(entity, "ItemsId");
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.UpdateTaskLog<AMS_Items>(oldEntity, entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.Delete("AMS_Items", "ItemsId", KeyValue);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.DeleteTaskLog<AMS_Items>(oldEntity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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

            int IsOk = DbUtils.BatchDelete("AMS_Items", "ItemsId", KeyValue);
            return IsOk >= 0 ? true : false;

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public AMS_Items GetEntity(string KeyValue)
        {
            //return dal.GetEntity(KeyValue);
            return DbUtils.GetModelById<AMS_Items>("ItemsId", KeyValue);

        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public IList GetList()
        {
            return GetListWhere(null, null);
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
            strSql.Append("SELECT * FROM AMS_Items WHERE 1=1");
            strSql.Append(where);
            strSql.Append(" Order BY SortCode");
            return DbHelper.GetDataListBySQL<AMS_Items>(strSql, param);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        public IList GetPageList(string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            return GetPageListWhere(null, null, orderField, orderType, pageIndex, pageSize, ref  count);
        }
        /// <summary>
        /// 分页获取数据列表(带条件)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        public IList GetPageListWhere(StringBuilder where, SqlParam[] param, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            //return dal.GetPageListWhere(where, param, orderField, orderType, pageIndex, pageSize, ref count);
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM AMS_Items WHERE 1=1");
            strSql.Append(where);
            return DbHelper.GetPageList<AMS_Items>(strSql.ToString(), param, CommonHelper.ToOrderField("SortCode", orderField), orderType, pageIndex, pageSize, ref count);

        }
        #endregion

        /// <summary>
        /// 根据主键，判断是否有明细数据
        /// </summary>
        /// <param name="KeyValue"></param>
        /// <returns></returns>
        public bool IsItemDetails(string KeyValue)
        {
            StringBuilder where = new StringBuilder();
            where.Append(" AND ItemsId = @ItemsId");
            SqlParam[] param = {
                                         new SqlParam("@ItemsId",KeyValue)};
            return GetRecordCount(where, param) > 0 ? true : false;
        }
    }
}
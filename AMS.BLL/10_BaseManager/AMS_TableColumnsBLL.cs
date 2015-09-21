using System.Collections;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 模块列表表头定义
    /// </summary>
    public class AMS_TableColumnsBLL : ServiceBase
    {
        //private readonly AMS_TableColumnsDAL dal = new AMS_TableColumnsDAL();

        #region Method
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        public object GetMaxCode()
        {
            //return dal.GetMaxCode();
            return DbUtils.GetMax("AMS_TableColumns", "SortCode");

        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        public int GetRecordCount(string KeyValue)
        {
            //return dal.GetRecordCount(KeyValue);
            return DbUtils.RecordCount("AMS_TableColumns", "TableColumnsId", KeyValue);

        }
        /// <summary>
        /// 获取记录总数（带条件）
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public int GetRecordCount(StringBuilder where, SqlParam[] param)
        {
            return DbUtils.RecordCount("AMS_TableColumns", where, param);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(AMS_TableColumns entity)
        {
            //return dal.Insert(entity) >= 0 ? true : false;
            entity.SortCode = CommonHelper.GetInt(this.GetMaxCode());
            int IsOk = DbUtils.Insert(entity);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<AMS_TableColumns>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(AMS_TableColumns entity)
        {
            //return dal.Update(entity) >= 0 ? true : false;
            #region 获取旧值
            var oldEntity = this.GetEntity(AMS_SysLogBLL.Instance.GetKeyFieldValue<AMS_TableColumns>(entity).ToString());
            #endregion
            int IsOk = DbUtils.Update(entity, "TableColumnsId");
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.UpdateTaskLog<AMS_TableColumns>(oldEntity, entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.Delete("AMS_TableColumns", "TableColumnsId", KeyValue);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.DeleteTaskLog<AMS_TableColumns>(oldEntity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.BatchDelete("AMS_TableColumns", "TableColumnsId", KeyValue);
            return IsOk >= 0 ? true : false;

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public AMS_TableColumns GetEntity(string KeyValue)
        {
            return DbUtils.GetModelById<AMS_TableColumns>("TableColumnsId", KeyValue);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="MenuId">模块菜单主键</param>
        /// <returns></returns>
        public IList GetList(string MenuId)
        {
            StringBuilder where = new StringBuilder();
            where.Append(" AND MenuId = @MenuId");
            SqlParam[] param = {
                                         new SqlParam("@MenuId",MenuId)};
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
            strSql.Append("SELECT * FROM AMS_TableColumns WHERE 1=1");
            strSql.Append(where);
            strSql.Append(" Order BY SortCode");
            return DbHelper.GetDataListBySQL<AMS_TableColumns>(strSql, param);
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
            //return dal.GetPageListWhere(null, null, orderField, orderType, pageIndex, pageSize, ref  count);
            return this.GetPageListWhere(null, null, orderField, orderType, pageIndex, pageSize, ref  count);

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
            strSql.Append("SELECT * FROM AMS_TableColumns WHERE 1=1");
            strSql.Append(where);
            return DbHelper.GetPageList<AMS_TableColumns>(strSql.ToString(), param, CommonHelper.ToOrderField("SortCode", orderField), orderType, pageIndex, pageSize, ref count);

        }
        #endregion

        /// <summary>
        /// 设置表头定义批量公开
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="MenuId">模块菜单主键</param>
        /// <returns></returns>
        public bool BatchIsPublic(string[] KeyValue, string MenuId)
        {
            //return dal.BatchIsPublic(KeyValue, MenuId) >= 0 ? true : false;
            StringBuilder[] sqls = new StringBuilder[KeyValue.Length + 1];
            object[] objs = new object[KeyValue.Length + 1];
            AMS_TableColumns entity = new AMS_TableColumns();
            entity.MenuId = MenuId;
            entity.IsPublic = 0;
            sqls[0] = SqlParamHelper.UpdateSql(entity, "MenuId");
            objs[0] = SqlParamHelper.GetParameter(entity);
            int index = 1;
            foreach (string item in KeyValue)
            {
                if (item.Length > 0)
                {
                    AMS_TableColumns entityitem = new AMS_TableColumns();
                    entityitem.TableColumnsId = item;
                    entityitem.IsPublic = 1;
                    entityitem.SortCode = index;
                    sqls[index] = SqlParamHelper.UpdateSql(entityitem, "TableColumnsId");
                    objs[index] = SqlParamHelper.GetParameter(entityitem);
                    index++;
                }
            }
            int IsOK = DbHelper.BatchExecuteBySql(sqls, objs);
            return IsOK >= 0 ? true : false;
        }
    }
}
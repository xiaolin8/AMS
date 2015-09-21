using System.Collections;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 模块（菜单导航）
    /// </summary>
    public class AMS_SysMenuBLL : ServiceBase
    {
        //private readonly AMS_SysMenuDAL dal = new AMS_SysMenuDAL();

        #region Method
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        public object GetMaxCode()
        {
            return DbUtils.GetMax("AMS_SysMenu", "SortCode");
        }
        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        public int GetRecordCount(string KeyValue)
        {
            return DbUtils.RecordCount("AMS_SysMenu", "MenuId", KeyValue);
        }
        /// <summary>
        /// 获取记录总数（带条件）
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public int GetRecordCount(StringBuilder where, SqlParam[] param)
        {
            return DbUtils.RecordCount("AMS_SysMenu", where, param);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(AMS_SysMenu entity)
        {
            //return dal.Insert(entity) >= 0 ? true : false;
            int IsOk = DbUtils.Insert(entity);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<AMS_SysMenu>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(AMS_SysMenu entity)
        {
            //return dal.Update(entity) >= 0 ? true : false;
            #region 获取旧值
            var oldEntity = this.GetEntity(AMS_SysLogBLL.Instance.GetKeyFieldValue<AMS_SysMenu>(entity).ToString());
            #endregion
            int IsOk = DbUtils.Update(entity, "MenuId");
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.UpdateTaskLog<AMS_SysMenu>(oldEntity, entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.Delete("AMS_SysMenu", "MenuId", KeyValue);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.DeleteTaskLog<AMS_SysMenu>(oldEntity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.BatchDelete("AMS_SysMenu", "MenuId", KeyValue);
            return IsOk >= 0 ? true : false;

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public AMS_SysMenu GetEntity(string KeyValue)
        {
            //return dal.GetEntity(KeyValue);
            return DbUtils.GetModelById<AMS_SysMenu>("MenuId", KeyValue);

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
            strSql.Append(@"SELECT  M.MenuId ,
                                M.ParentId ,
                                M.Code ,
                                M.FullName ,
                                M.FullName_EN,
                                M.Description ,
                                M.Img ,
                                M.NavigateUrl ,
                                M.FormName ,
                                M.Target ,
                                M.IsUnfold ,
                                M.Enabled ,
                                M.SortCode ,
                                MA.FullName AS Category
                        FROM    AMS_SysMenu M
                                LEFT JOIN AMS_SysMenu MA ON MA.MenuId = M.ParentId WHERE 1=1");
            strSql.Append(where);
            strSql.Append(" Order BY SortCode");
            return DbHelper.GetDataListBySQL<AMS_SysMenu>(strSql, param);
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
            strSql.Append("SELECT * FROM AMS_SysMenu WHERE 1=1");
            strSql.Append(where);
            return DbHelper.GetPageList<AMS_SysMenu>(strSql.ToString(), param, CommonHelper.ToOrderField("SortCode", orderField), orderType, pageIndex, pageSize, ref count);

        }
        #endregion

        /// <summary>
        /// 根据模块菜单主键，判断是否有下级菜单
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
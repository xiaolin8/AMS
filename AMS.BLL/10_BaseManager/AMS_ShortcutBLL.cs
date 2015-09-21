using System.Collections;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 首页快捷功能
    /// </summary>
    public class AMS_ShortcutBLL : ServiceBase
    {
        //private readonly AMS_ShortcutDAL dal = new AMS_ShortcutDAL();

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(AMS_Shortcut entity)
        {
            entity.ShortcutId = CommonHelper.GetGuid;
            //return dal.Insert(entity) >= 0 ? true : false;
            int IsOk = DbUtils.Insert(entity);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<AMS_Shortcut>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        public bool Delete(string KeyValue, string UserId)
        {
            //return dal.Delete(KeyValue, UserId) >= 0 ? true : false;
            #region 获取旧值
            var oldEntity = this.GetEntity(KeyValue);
            #endregion
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"DELETE FROM AMS_Shortcut WHERE 1=1");
            strSql.Append(" AND MenuId = @MenuId");
            strSql.Append(" AND UserId = @UserId");
            SqlParam[] param = {
                                         new SqlParam("@MenuId",KeyValue),
                                         new SqlParam("@UserId",UserId)};
            int IsOk = DbHelper.ExecuteBySql(strSql, param);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.DeleteTaskLog<AMS_Shortcut>(oldEntity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public AMS_Shortcut GetEntity(string KeyValue)
        {
            return DbUtils.GetModelById<AMS_Shortcut>("ShortcutId", KeyValue);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        public IList GetList(string UserId)
        {
            //return dal.GetList(UserId);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  M.MenuId ,
                                    M.Code ,
                                    M.FullName ,
                                    M.Description ,
                                    M.Img ,
                                    M.NavigateUrl
                            FROM    AMS_Shortcut S
                                    LEFT JOIN AMS_SysMenu M ON S.MenuId = M.MenuId
                            WHERE 1=1 AND UserId = @UserId");
            strSql.Append(" ORDER BY S.CreateDate");
            SqlParam[] param = {
                                         new SqlParam("@UserId",UserId)};
            return DbHelper.GetDataListBySQL<AMS_ModulePermission>(strSql, param);

        }
    }
}
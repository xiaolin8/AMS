using System.Data;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 用户菜单关系
    /// </summary>
    public class AMS_UserMenuBLL : ServiceBase
    {
        //private readonly AMS_UserMenuDAL dal = new AMS_UserMenuDAL();

        /// <summary>
        /// 获得数据列表(带条件)
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        public DataTable GetList(string UserId)
        {
            //return dal.GetList(UserId);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  
                            M.MenuId,
                            M.Code ,
                            M.FullName ,
                            M.Img ,
                            M.Category,
                            M.Description,
							M.SortCode,
                            M.ParentId,
                            UM.MenuId AS IsExist
                            FROM    AMS_SysMenu M
                            LEFT JOIN AMS_UserMenu UM ON M.MenuId = UM.MenuId AND 1=1");
            strSql.Append(" AND UserId = @UserId");
            strSql.Append(" ORDER BY M.SortCode");
            SqlParam[] param = {
                                         new SqlParam("@UserId",UserId)};
            return DbHelper.GetDataTableBySQL(strSql, param);
        }
        /// <summary>
        /// 分配角色模块菜单权限
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="UserId">用户主键</param>
        /// <param name="CreateUserId">操作用户主键</param>
        /// <param name="CreateUserName">操作用户</param>
        /// <returns></returns>
        public bool AddModulePermission(string[] KeyValue, string UserId, string CreateUserId, string CreateUserName)
        {
            //return dal.AddModulePermission(KeyValue, UserId, CreateUserId, CreateUserName) >= 0 ? true : false;
            StringBuilder[] sqls = new StringBuilder[KeyValue.Length + 1];
            object[] objs = new object[KeyValue.Length + 1];
            sqls[0] = SqlParamHelper.DeleteSql("AMS_UserMenu", "UserId");
            objs[0] = new SqlParam[] { new SqlParam("@UserId", UserId) };
            int index = 1;
            foreach (string item in KeyValue)
            {
                if (item.Length > 0)
                {
                    AMS_UserMenu entity = new AMS_UserMenu();
                    entity.UserMenuId = CommonHelper.GetGuid;
                    entity.UserId = UserId;
                    entity.MenuId = item;
                    entity.CreateUserId = CreateUserId;
                    entity.CreateUserName = CreateUserName;
                    sqls[index] = SqlParamHelper.InsertSql(entity);
                    objs[index] = SqlParamHelper.GetParameter(entity);
                    index++;
                }
            }
            int IsOK = DbHelper.BatchExecuteBySql(sqls, objs);
            return IsOK >= 0 ? true : false;
        }
    }
}
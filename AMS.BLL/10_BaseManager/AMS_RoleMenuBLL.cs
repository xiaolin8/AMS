using System.Data;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 系统角色菜单关系
    /// </summary>
    public class AMS_RoleMenuBLL : ServiceBase
    {
        //private readonly AMS_RoleMenuDAL dal = new AMS_RoleMenuDAL();

        /// <summary>
        /// 获得数据列表(带条件)
        /// </summary>
        /// <param name="RoleId">角色主键</param>
        /// <returns></returns>
        public DataTable GetList(string RoleId)
        {
            //return dal.GetList(RoleId);
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
                            R.MenuId AS IsExist
                            FROM    AMS_SysMenu M
                            LEFT JOIN AMS_RoleMenu R ON M.MenuId = R.MenuId AND 1=1");
            strSql.Append(" AND RoleId = @RoleId");
            strSql.Append(" ORDER BY M.SortCode");
            SqlParam[] param = {
                                         new SqlParam("@RoleId",RoleId)};
            return DbHelper.GetDataTableBySQL(strSql, param);
        }
        /// <summary>
        /// 分配角色模块菜单权限
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="RoleId">角色主键</param>
        /// <param name="CreateUserId">操作用户主键</param>
        /// <param name="CreateUserName">操作用户</param>
        /// <returns></returns>
        public bool AddModulePermission(string[] KeyValue, string RoleId, string CreateUserId, string CreateUserName)
        {
            //return dal.AddModulePermission(KeyValue, RoleId, CreateUserId, CreateUserName) >= 0 ? true : false;
            StringBuilder[] sqls = new StringBuilder[KeyValue.Length + 1];
            object[] objs = new object[KeyValue.Length + 1];
            sqls[0] = SqlParamHelper.DeleteSql("AMS_RoleMenu", "RoleId");
            objs[0] = new SqlParam[] { new SqlParam("@RoleId", RoleId) };
            int index = 1;
            foreach (string item in KeyValue)
            {
                if (item.Length > 0)
                {
                    AMS_RoleMenu entity = new AMS_RoleMenu();
                    entity.RoleMenuId = CommonHelper.GetGuid;
                    entity.RoleId = RoleId;
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
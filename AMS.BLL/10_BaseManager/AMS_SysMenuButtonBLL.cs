using System.Data;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 菜单导航操作按钮关系表
    /// </summary>
    public class AMS_SysMenuButtonBLL : ServiceBase
    {
        //private readonly AMS_SysMenuButtonDAL dal = new AMS_SysMenuButtonDAL();

        /// <summary>
        /// 获得数据列表(带条件)
        /// </summary>
        /// <param name="MenuId">模块菜单主键</param>
        /// <returns></returns>
        public DataTable GetListWhere(string MenuId)
        {
            StringBuilder where = new StringBuilder();
            where.Append(" AND S.MenuId = @MenuId");
            SqlParam[] param = {
                                         new SqlParam("@MenuId",MenuId)};
            //return dal.GetListWhere(where, param);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  
                            B.ButtonId ,
                            B.FullName ,
                            B.Img ,
                            B.Category,
                            B.Description,
                            ISNULL(S.CreateDate,GETDATE()+1)  AS IsExist
                            FROM    AMS_Button B
                            LEFT JOIN AMS_SysMenuButton S ON B.ButtonId = S.ButtonId AND 1=1");
            strSql.Append(where);
            strSql.Append(" ORDER BY IsExist,S.SortCode ,B.SortCode");
            return DbHelper.GetDataTableBySQL(strSql, param);
        }

        /// <summary>
        /// 设批量添加，菜单导航操作按钮关系
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="MenuId">模块菜单主键</param>
        /// <param name="CreateUserId">操作用户主键</param>
        /// <param name="CreateUserName">操作用户</param>
        /// <returns></returns>
        public bool BatchAddMenuButton(string[] KeyValue, string MenuId, string CreateUserId, string CreateUserName)
        {
            //return dal.BatchAddMenuButton(KeyValue, MenuId, CreateUserId, CreateUserName) >= 0 ? true : false;
            StringBuilder[] sqls = new StringBuilder[KeyValue.Length + 1];
            object[] objs = new object[KeyValue.Length + 1];
            sqls[0] = SqlParamHelper.DeleteSql("AMS_SysMenuButton", "MenuId");
            objs[0] = new SqlParam[] { new SqlParam("@MenuId", MenuId) };
            int index = 1;
            foreach (string item in KeyValue)
            {
                if (item.Length > 0)
                {
                    AMS_SysMenuButton entity = new AMS_SysMenuButton();
                    entity.SysMenuButtonId = CommonHelper.GetGuid;
                    entity.MenuId = MenuId;
                    entity.ButtonId = item;
                    entity.SortCode = index;
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
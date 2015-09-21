using System;
using System.Data;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 系统角色菜单按钮关系
    /// </summary>
    public class AMS_RoleMenuButtonBLL : ServiceBase
    {
        //private readonly AMS_RoleMenuButtonDAL dal = new AMS_RoleMenuButtonDAL();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="RoleId">角色主键</param>
        /// <returns></returns>
        public DataTable GetList(string RoleId)
        {
            //return dal.GetList(RoleId);

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  B.ButtonId ,
                                    B.FullName ,
                                    B.Img ,
                                    B.Description ,
                                    MB.MenuId ,
                                    B.Category ,
                                    RMB.RoleMenuButtonId AS IsExist
                            FROM    AMS_SysMenuButton MB
                                    LEFT JOIN AMS_Button B ON B.ButtonId = MB.ButtonId
                                    LEFT JOIN AMS_RoleMenuButton RMB ON RMB.ButtonId = B.ButtonId
                            AND RMB.MenuId = MB.MenuId AND 1=1
							AND RMB.RoleId = @RoleId ");
            strSql.Append(" ORDER BY MB.SortCode");
            SqlParam[] param = {
                                         new SqlParam("@RoleId",RoleId)};
            return DbHelper.GetDataTableBySQL(strSql, param);
        }
        /// <summary>
        /// 分配角色按钮权限
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="RoleId">角色主键</param>
        /// <param name="CreateUserId">操作用户主键</param>
        /// <param name="CreateUserName">操作用户</param>
        /// <returns></returns>
        public bool AddButtonPermission(string[] KeyValue, string RoleId, string CreateUserId, string CreateUserName)
        {
            //return dal.AddButtonPermission(KeyValue, RoleId, CreateUserId, CreateUserName) >= 0 ? true : false;

            StringBuilder[] sqls = new StringBuilder[KeyValue.Length + 1];
            object[] objs = new object[KeyValue.Length + 1];
            sqls[0] = SqlParamHelper.DeleteSql("AMS_RoleMenuButton", "RoleId");
            objs[0] = new SqlParam[] { new SqlParam("@RoleId", RoleId) };
            int index = 1;
            foreach (string item in KeyValue)
            {
                if (item.Length > 0)
                {
                    string[] stritem = item.Split('|');
                    AMS_RoleMenuButton entity = new AMS_RoleMenuButton();
                    entity.RoleMenuButtonId = CommonHelper.GetGuid;
                    entity.RoleId = RoleId;
                    entity.ButtonId = stritem[0];
                    entity.MenuId = stritem[1];
                    entity.CreateDate = DateTime.Now;
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
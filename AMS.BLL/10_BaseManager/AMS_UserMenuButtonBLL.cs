using System;
using System.Data;
using System.Text;
using System.Threading;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 用户菜单按钮关系
    /// </summary>
    public class AMS_UserMenuButtonBLL : ServiceBase
    {
        //private readonly AMS_UserMenuButtonDAL dal = new AMS_UserMenuButtonDAL();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        public DataTable GetList(string UserId)
        {
            //return dal.GetList(UserId);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  B.ButtonId ,
                                    B.FullName ,
                                    B.Img ,
                                    B.Description ,
                                    MB.MenuId ,
                                    B.Category ,
                                    UMB.UserMenuButtonId AS IsExist
                            FROM    AMS_SysMenuButton MB
                                    LEFT JOIN AMS_Button B ON B.ButtonId = MB.ButtonId
                                    LEFT JOIN AMS_UserMenuButton UMB ON UMB.ButtonId = B.ButtonId
                            AND UMB.MenuId = MB.MenuId AND 1=1
							AND UMB.UserId = @UserId ");
            strSql.Append(" ORDER BY B.SortCode");
            SqlParam[] param = {
                                         new SqlParam("@UserId",UserId)};
            return DbHelper.GetDataTableBySQL(strSql, param);
        }
        /// <summary>
        /// 分配用户按钮权限
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="UserId">用户主键</param>
        /// <param name="CreateUserId">操作用户主键</param>
        /// <param name="CreateUserName">操作用户</param>
        /// <returns></returns>
        public bool AddButtonPermission(string[] KeyValue, string UserId, string CreateUserId, string CreateUserName)
        {
            //return dal.AddButtonPermission(KeyValue, UserId, CreateUserId, CreateUserName) >= 0 ? true : false;
            StringBuilder[] sqls = new StringBuilder[KeyValue.Length + 1];
            object[] objs = new object[KeyValue.Length + 1];
            sqls[0] = SqlParamHelper.DeleteSql("AMS_UserMenuButton", "UserId");
            objs[0] = new SqlParam[] { new SqlParam("@UserId", UserId) };
            int index = 1;
            foreach (string item in KeyValue)
            {
                if (item.Length > 0)
                {
                    Thread.Sleep(10);////延迟10毫秒
                    string[] stritem = item.Split('|');
                    AMS_UserMenuButton entity = new AMS_UserMenuButton();
                    entity.UserMenuButtonId = CommonHelper.GetGuid;
                    entity.UserId = UserId;
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
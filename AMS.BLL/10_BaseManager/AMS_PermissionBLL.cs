using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 用户模块权限模块，角色模块权限，用户按钮权限，角色按钮权限，用户数据权限，角色数据权限
    /// </summary>
    public class AMS_PermissionBLL : ServiceBase
    {
        //private readonly AMS_PermissionDAL dal = new AMS_PermissionDAL();

        private static AMS_PermissionBLL item;
        public static AMS_PermissionBLL Instance
        {
            get
            {
                if (item == null)
                {
                    item = new AMS_PermissionBLL();
                }
                return item;
            }
        }

        /// <summary>
        /// 获取 角色，用户 模块菜单权限
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        public IList GetModulePermission(string UserId, string Lan, string strWhere)
        {
            string lan_field = "";
            if (Lan == "en-US")
            {
                lan_field = "FullName_EN";
            }
            else
            {
                lan_field = "FullName";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  UM.UserId ,
                                    UM.MenuId ,
                                    SM.ParentId ,
                                    SM." + lan_field + @" as  FullName ,
                                    SM.Description ,
                                    SM.Img ,
                                    SM.NavigateUrl ,
                                    SM.FormName ,
                                    SM.Target ,
                                    SM.IsUnfold
                            FROM    AMS_SysMenu SM
                                    INNER JOIN ( SELECT UR.UserId AS UserId ,
                                                        RM.MenuId AS MenuId
                                                 FROM   AMS_RoleMenu RM
                                                        INNER JOIN AMS_UserRole UR ON RM.RoleId = UR.RoleId
                                                 UNION
                                                 SELECT UserId ,
                                                        MenuId
                                                 FROM   AMS_UserMenu
                                                 UNION
                                                 SELECT U.UserId ,
                                                        RM.MenuId
                                                 FROM   AMS_User U
                                                        LEFT JOIN AMS_RoleMenu RM ON U.RoleId = RM.RoleId
                                                 WHERE  U.UserId = @UserId
                                               ) UM ON SM.MenuId = UM.MenuId
                            WHERE   UM.UserId = @UserId");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(@" and " + strWhere);
            }
            strSql.Append(@" ORDER BY SortCode");
            SqlParam[] param = {
                                         new SqlParam("@UserId",UserId)};
            return DbHelper.GetDataListBySQL<AMS_ModulePermission>(strSql, param);
        }

        public IList GetSubModulePermission(string UserId, string Lan, string parentId)
        {
            string lan_field = "";
            if (Lan == "en-US")
            {
                lan_field = "FullName_EN";
            }
            else
            {
                lan_field = "FullName";
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  
                                    UM.MenuId id,
                                    SM.ParentId parentnodes,
                                    SM." + lan_field + @"  text ,
                                    SM." + lan_field + @"  text ,
                                    SM.Img img,
                                    SM.navigateurl Location,
                                    'false' hasChildren
                            FROM    AMS_SysMenu SM
                                    INNER JOIN ( SELECT UR.UserId AS UserId ,
                                                        RM.MenuId AS MenuId
                                                 FROM   AMS_RoleMenu RM
                                                        INNER JOIN AMS_UserRole UR ON RM.RoleId = UR.RoleId
                                                 UNION
                                                 SELECT UserId ,
                                                        MenuId
                                                 FROM   AMS_UserMenu
                                                 UNION
                                                 SELECT U.UserId ,
                                                        RM.MenuId
                                                 FROM   AMS_User U
                                                        LEFT JOIN AMS_RoleMenu RM ON U.RoleId = RM.RoleId
                                                 WHERE  U.UserId = @UserId
                                               ) UM ON SM.MenuId = UM.MenuId
                            WHERE   UM.UserId = @UserId");
            if (!string.IsNullOrEmpty(parentId))
            {
                strSql.Append(@"  start with SM.parentid='" + parentId + "' connect by prior SM.menuid=SM.parentid");
            }
            strSql.Append(@" ORDER BY SortCode");
            SqlParam[] param = {
                                         new SqlParam("@UserId",UserId)};
            return DbHelper.GetDataListBySQL<AMS_ModulePermission>(strSql, param);
        }

        /// <summary>
        /// 获取 角色，用户 模块菜单权限
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        public IList GetModulePermission(string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  UM.UserId ,
                                    UM.MenuId ,
                                    SM.ParentId ,
                                    SM.FullName ,
                                    SM.Description ,
                                    SM.Img ,
                                    SM.NavigateUrl ,
                                    SM.FormName ,
                                    SM.Target ,
                                    SM.IsUnfold
                            FROM    AMS_SysMenu SM
                                    INNER JOIN ( SELECT UR.UserId AS UserId ,
                                                        RM.MenuId AS MenuId
                                                 FROM   AMS_RoleMenu RM
                                                        INNER JOIN AMS_UserRole UR ON RM.RoleId = UR.RoleId
                                                 UNION
                                                 SELECT UserId ,
                                                        MenuId
                                                 FROM   AMS_UserMenu
                                                 UNION
                                                 SELECT U.UserId ,
                                                        RM.MenuId
                                                 FROM   AMS_User U
                                                        LEFT JOIN AMS_RoleMenu RM ON U.RoleId = RM.RoleId
                                                 WHERE  U.UserId = @UserId
                                               ) UM ON SM.MenuId = UM.MenuId
                            WHERE   UM.UserId = @UserId
                            ORDER BY SM.SortCode");
            SqlParam[] param = {
                                         new SqlParam("@UserId",UserId)
                                        
                               };
            return DbHelper.GetDataListBySQL<AMS_ModulePermission>(strSql, param);
        }

        /// <summary>
        /// 获取 角色，用户 按钮权限
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        public IList GetButtonPermission(string UserId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  BID.UserId ,
                                    BID.MenuId ,
                                    BID.ButtonId ,
                                    B.FullName ,
                                    B.Img ,
                                    B.Event ,
                                    B.Control_ID ,
                                    B.Category ,
                                    B.Split ,
                                    B.Description,
                                    MB.SortCode
                            FROM    AMS_Button B
                                    INNER JOIN ( SELECT UR.UserId AS UserId ,
                                                        RMB.MenuId AS MenuId ,
                                                        RMB.ButtonId AS ButtonId
                                                 FROM   AMS_UserRole UR
                                                        INNER JOIN AMS_RoleMenuButton RMB ON UR.RoleId = RMB.RoleId
                                                 GROUP BY RMB.MenuId ,
                                                        RMB.ButtonId ,
                                                        UR.UserId
                                                 UNION
                                                 SELECT UM.UserId AS UserId ,
                                                        UM.MenuId AS MenuId ,
                                                        UMB.ButtonId AS ButtonId
                                                 FROM   AMS_UserMenu UM
                                                        INNER JOIN AMS_UserMenuButton UMB ON UM.MenuId = UMB.MenuId
                                                 GROUP BY UM.UserId ,
                                                        UM.MenuId ,
                                                        UMB.ButtonId
                                                 UNION
                                                 SELECT U.UserId ,
                                                        RMB.MenuId ,
                                                        RMB.ButtonId
                                                 FROM   AMS_User u
                                                        INNER JOIN AMS_RoleMenuButton RMB ON u.RoleId = RMB.RoleId
                                                 WHERE  U.UserId = @UserId
                                               ) BID ON B.ButtonId = BID.ButtonId
                                    INNER JOIN AMS_SysMenuButton MB ON BID.MenuId = MB.MenuId
                                                                            AND BID.ButtonId = MB.ButtonId
                            WHERE   BID.UserId = @UserId
                            GROUP BY BID.UserId ,
                                    BID.MenuId ,
                                    BID.ButtonId ,
                                    B.FullName ,
                                    B.Img ,
                                    B.Event ,
                                    B.Control_ID ,
                                    B.Category ,
                                    B.Split ,
                                    B.Description ,
                                    MB.SortCode
                            ORDER BY MB.SORTCODE");
            SqlParam[] param = {
                                         new SqlParam("@UserId",UserId)};
            return DbHelper.GetDataListBySQL<AMS_ButtonPermission>(strSql, param);
        }

        /// <summary>
        /// 获取 所有 模块菜单权限
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        public IList GetModulePermissionByLang(string lan, string strWhere)
        {
            string lan_field = "";
            if (lan == "en-US")
            {
                lan_field = "FullName_EN";
            }
            else
            {
                lan_field = "FullName";
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  MenuId ,
                                    ParentId ,
                                    " + lan_field + @" as FullName ,
                                    Description ,
                                    Img ,
                                    NavigateUrl ,
                                    FormName ,
                                    Target ,
                                    IsUnfold
                            FROM    AMS_SysMenu SM where 1=1");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(@" and " + strWhere);
            }
            strSql.Append(@" ORDER BY SortCode");
            return DbHelper.GetDataListBySQL<AMS_ModulePermission>(strSql);
        }

        public IList GetSubModulePermissionByLang(string lan, string parentId)
        {
            string lan_field = "";
            if (lan == "en-US")
            {
                lan_field = "FullName_EN";
            }
            else
            {
                lan_field = "FullName";
            }

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  MenuId  id,
                                    ParentId parentnodes,
                                    '/Themes/Images/Icon16/'||Img img,
                                    " + lan_field + @" text ,
                                    " + lan_field + @" value ,
                                    navigateurl Location,
                                   'false' hasChildren
                            FROM    AMS_SysMenu SM where 1=1");
            if (!string.IsNullOrEmpty(parentId))
            {
                strSql.Append(@"  AND SM.parentid='" + parentId + "'");
            }
            strSql.Append(@" ORDER BY SortCode");
            List<TreeView> list = (List<TreeView>)DbHelper.GetDataListBySQL<TreeView>(strSql);
            var list2 = list.FindAll(m => m.parentnodes == parentId);
            foreach (TreeView m in list2)
            {
                AddChildNode(list, m);
            }
            return list2;
        }
        /// <summary>
        /// 获取 角色数据权限
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        public Hashtable GetDataPermission(string UserId)
        {
            Hashtable ht = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  DP.ResourceId ,
                                    DP.ObjectId
                            FROM    AMS_DataPermission DP
                                    LEFT JOIN AMS_UserRole UR ON UR.RoleId = DP.RoleId
                            WHERE   UR.UserId = @UserId
                            UNION ALL
                            SELECT  DP.ResourceId ,
                                    DP.ObjectId
                            FROM    AMS_DataPermission DP
                                    INNER JOIN AMS_User U ON DP.RoleId = U.RoleId
                            WHERE   U.UserId = @UserId");
            SqlParam[] param = {
                                         new SqlParam("@UserId",UserId)};
            DataTable dt = DbHelper.GetDataTableBySQL(strSql, param);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ht[dt.Rows[i]["ResourceId"]] != null)
                {
                    ht[dt.Rows[i]["ResourceId"]] = ht[dt.Rows[i]["ResourceId"]] + "," + dt.Rows[i]["ObjectId"].ToString();
                }
                else
                {
                    ht[dt.Rows[i]["ResourceId"]] = dt.Rows[i]["ObjectId"];
                }
            }
            return ht;
        }


        /// <summary>
        /// 获取 所有 模块菜单权限
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        public IList GetModulePermission()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  MenuId ,
                                    ParentId ,
                                    FullName ,
                                    Description ,
                                    Img ,
                                    NavigateUrl ,
                                    FormName ,
                                    Target ,
                                    IsUnfold
                            FROM    AMS_SysMenu
                            ORDER BY SortCode");
            return DbHelper.GetDataListBySQL<AMS_ModulePermission>(strSql);
        }

        /// <summary>
        /// 根据用户登录名，取消业务菜单(由于新版圈闭信息管理系统权限模块的需要，而重新组织数据结构) by yhb 2014-10-16
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IList GetWebBusinessModulePermission(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select b.* from WebMenuConfig B inner join WebUserRight C on B.MenuId=C.MID inner join Trap_User A on A.UID=C.UID or A.UGroup=C.UID where A.UName = @UName and c.inherited=1");
            SqlParam[] param = { new SqlParam("@UName", userName) };
            return DbHelper.GetDataListBySQL<AMS_ModulePermission>(strSql, param);
        }

        /// <summary>
        /// 获取 所有 按钮权限
        /// </summary>
        /// <returns></returns>
        public IList GetButtonPermission()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  MB.MenuId ,
                                    B.ButtonId ,
                                    B.FullName ,
                                    B.Img ,
                                    B.Event ,
                                    B.Control_ID ,
                                    B.Category ,
                                    B.Split ,
                                    B.Description
                            FROM    AMS_SysMenuButton MB
                                    LEFT JOIN AMS_Button B ON B.ButtonId = MB.ButtonId
                                    ORDER BY MB.SORTCODE, MB.CreateDate");
            return DbHelper.GetDataListBySQL<AMS_ButtonPermission>(strSql);
        }

        /// <summary>
        /// 获取 所有数据权限
        /// </summary>
        /// <returns></returns>
        public Hashtable GetDataPermission()
        {
            Hashtable ht = new Hashtable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  DP.ResourceId ,
                                    DP.ObjectId
                            FROM    AMS_DataPermission DP
                                    LEFT JOIN AMS_UserRole UR ON UR.RoleId = DP.RoleId
                            UNION ALL
                            SELECT  DP.ResourceId ,
                                    DP.ObjectId
                            FROM    AMS_DataPermission DP
                                    INNER JOIN AMS_User U ON DP.RoleId = U.RoleId");
            DataTable dt = DbHelper.GetDataTableBySQL(strSql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (ht[dt.Rows[i]["ResourceId"]] != null)
                {
                    ht[dt.Rows[i]["ResourceId"]] = ht[dt.Rows[i]["ResourceId"]] + "," + dt.Rows[i]["ObjectId"].ToString();
                }
                else
                {
                    ht[dt.Rows[i]["ResourceId"]] = dt.Rows[i]["ObjectId"];
                }
            }
            return ht;
        }

        public static void AddChildNode(List<TreeView> list, TreeView m)
        {
            var data = list.FindAll(t => t.parentnodes == m.id);
            if (data != null && data.Count > 0)
            {
                m.hasChildren = true;
                m.ChildNodes = data;
            }
            foreach (var tree in data)
            {
                AddChildNode(list, tree);
            }
        }
    }
}

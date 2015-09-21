using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 用户帐户角色关系
    /// </summary>
    public class AMS_UserRoleBLL : ServiceBase
    {
        //private readonly AMS_UserRoleDAL dal = new AMS_UserRoleDAL();

        /// <summary>
        /// 加载未添加成员
        /// </summary>
        /// <param name="RoleId">角色主键</param>
        /// <returns></returns>
        public IList GetListNotMember(string RoleId)
        {
            //return dal.GetListNotMember(RoleId);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT * FROM AMS_User WHERE 1=1 ");
            strSql.Append(" AND UserId NOT IN(SELECT UserId FROM AMS_UserRole WHERE RoleId = @RoleId)");
            strSql.Append(" ORDER BY SortCode");
            SqlParam[] param = {
                                         new SqlParam("@RoleId",RoleId)};
            return DbHelper.GetDataListBySQL<AMS_User>(strSql, param);
        }
        /// <summary>
        /// 加载角色里面成员
        /// </summary>
        /// <param name="Parm_Key_Value">搜索条件键值</param>
        /// <returns></returns>
        public IList GetListMember(Hashtable Parm_Key_Value)
        {
            StringBuilder SqlWhere = new StringBuilder();
            List<SqlParam> ListParam = new List<SqlParam>();
            //编号
            if (Parm_Key_Value["Code"] != null && !string.IsNullOrEmpty(Parm_Key_Value["Code"].ToString()))
            {
                SqlWhere.Append(" AND Code like @Code");
                ListParam.Add(new SqlParam("@Code", '%' + Parm_Key_Value["Code"].ToString() + '%'));
            }
            //账户
            if (Parm_Key_Value["Account"] != null && !string.IsNullOrEmpty(Parm_Key_Value["Code"].ToString()))
            {
                SqlWhere.Append(" AND Account like @Account");
                ListParam.Add(new SqlParam("@Account", '%' + Parm_Key_Value["Account"].ToString() + '%'));
            }
            //名称
            if (Parm_Key_Value["RealName"] != null && !string.IsNullOrEmpty(Parm_Key_Value["RealName"].ToString()))
            {
                SqlWhere.Append(" AND (RealName like @RealName");
                SqlWhere.Append(" OR Spell like @Spell)");
                ListParam.Add(new SqlParam("@RealName", '%' + Parm_Key_Value["RealName"].ToString() + '%'));
                ListParam.Add(new SqlParam("@Spell", '%' + Parm_Key_Value["RealName"].ToString() + '%'));
            }
            //手机
            if (Parm_Key_Value["Mobile"] != null && !string.IsNullOrEmpty(Parm_Key_Value["Mobile"].ToString()))
            {
                SqlWhere.Append(" AND Mobile like @Mobile");
                ListParam.Add(new SqlParam("@Mobile", '%' + Parm_Key_Value["Mobile"].ToString() + '%'));
            }
            SqlWhere.Append(" AND UserId IN(SELECT UserId FROM AMS_UserRole WHERE RoleId = @RoleId)");
            ListParam.Add(new SqlParam("@RoleId", Parm_Key_Value["RoleId"].ToString()));
            return GetListMember(SqlWhere, ListParam.ToArray());
        }

        /// <summary>
        /// 加载角色里面成员
        /// </summary>
        /// <param name="RoleId">角色主键</param>
        /// <returns></returns>
        public IList GetListMember(StringBuilder where, SqlParam[] param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    U.UserId ,
                                                U.Code ,
                                                U.Account,
                                                U.RealName ,
                                                U.Gender ,
                                                U.Mobile ,
                                                I.ItemName AS DutyId ,
                                                IA.ItemName AS TitleId ,
                                                U.Enabled ,
					                            U.SortCode,
												ORG.FullName AS DepartmentId,
                                                U.Spell,
                                                U.Description
                                        FROM      AMS_User U
												LEFT JOIN AMS_Organization ORG ON ORG.OrganizationId = U.DepartmentId
                                                LEFT JOIN AMS_ItemDetails I ON U.DutyId = I.ItemCode
                                                                                AND I.ItemsId = '137a2d97-d1d9-4752-9c5e-239097e2ed68'
                                                LEFT JOIN AMS_ItemDetails IA ON U.TitleId = IA.ItemCode
                                                                                    AND IA.ItemsId = '2acba9e8-5fa7-4b6f-8ebd-56e753dd059a'
                                    ) A WHERE 1=1");
            strSql.Append(where);
            strSql.Append(" ORDER BY DepartmentId,SortCode");
            return DbHelper.GetDataListBySQL<AMS_User>(strSql, param);
        }

        /// <summary>
        /// 批量添加角色成员 
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="RoleId">角色主键</param>
        /// <param name="CreateUserId">操作用户主键</param>
        /// <param name="CreateUserName">操作用户</param>
        /// <returns></returns>
        public bool BatchAddMember(string[] KeyValue, string RoleId, string CreateUserId, string CreateUserName)
        {
            //return dal.BatchAddMember(KeyValue, RoleId, CreateUserId, CreateUserName) >= 0 ? true : false;
            StringBuilder[] sqls = new StringBuilder[KeyValue.Length];
            object[] objs = new object[KeyValue.Length];
            int index = 0;
            foreach (string item in KeyValue)
            {
                if (item.Length > 0)
                {
                    AMS_UserRole entity = new AMS_UserRole();
                    entity.UserRoleId = CommonHelper.GetGuid;
                    entity.RoleId = RoleId;
                    entity.UserId = item;
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
        /// <summary>
        /// 删除角色成员
        /// </summary>
        /// <param name="RoleId">角色主键</param>
        /// <param name="KeyValue">要删除值</param>
        /// <returns></returns>
        public bool BatchDeleteMember(string RoleId, string[] KeyValue)
        {
            //return dal.BatchDeleteMember(RoleId, KeyValue) >= 0 ? true : false;
            StringBuilder[] sqls = new StringBuilder[KeyValue.Length];
            object[] objs = new object[KeyValue.Length];
            int index = 0;
            foreach (string item in KeyValue)
            {
                if (item.Length > 0)
                {
                    Hashtable ht = new Hashtable();
                    ht["RoleId"] = RoleId;
                    ht["UserId"] = item;
                    sqls[index] = SqlParamHelper.DeleteSql("AMS_UserRole", ht);
                    objs[index] = SqlParamHelper.GetParameter(ht);
                    index++;
                }
            }
            int IsOK = DbHelper.BatchExecuteBySql(sqls, objs);
            return IsOK >= 0 ? true : false;
        }

        /// <summary>
        /// 用户分配角色列表，
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        public DataTable GetUserRoleList(string UserId)
        {
            //return dal.GetUserRoleList(UserId);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  
                            R.RoleId ,
                            R.FullName ,
                            R.Description,
                            R.OrganizationId,
                            UR.RoleId AS IsExist
                            FROM    AMS_Roles R
                            LEFT JOIN AMS_UserRole UR ON R.RoleId = UR.RoleId AND 1=1");
            strSql.Append(" AND UserId =@UserId");
            strSql.Append(" ORDER BY R.SortCode");
            SqlParam[] param = {
                                         new SqlParam("@UserId",UserId)};
            return DbHelper.GetDataTableBySQL(strSql, param);
        }
        /// <summary>
        /// 用户批量添加角色
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="UserId">模用户主键</param>
        /// <param name="CreateUserId">操作用户主键</param>
        /// <param name="CreateUserName">操作用户</param>
        /// <returns></returns>
        public bool BatchAddUserRole(string[] KeyValue, string UserId, string CreateUserId, string CreateUserName)
        {
            //return dal.BatchAddUserRole(KeyValue, UserId, CreateUserId, CreateUserName) >= 0 ? true : false;
            StringBuilder[] sqls = new StringBuilder[KeyValue.Length + 1];
            object[] objs = new object[KeyValue.Length + 1];
            sqls[0] = SqlParamHelper.DeleteSql("AMS_UserRole", "UserId");
            objs[0] = new SqlParam[] { new SqlParam("@UserId", UserId) };
            int index = 1;
            foreach (string item in KeyValue)
            {
                if (item.Length > 0)
                {
                    AMS_UserRole entity = new AMS_UserRole();
                    entity.UserRoleId = CommonHelper.GetGuid;
                    entity.UserId = UserId;
                    entity.RoleId = item;
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
        /// <summary>
        /// 根据用户查询 拥有角色列表
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public IList GetUserRoleListByUserId(string UserId)
        {
            //return dal.GetUserRoleListByUserId(UserId);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  R.RoleId ,
                                    R.Code ,
                                    R.FullName ,
                                    I.ItemName AS Category ,
                                    R.Description
                            FROM    AMS_UserRole UR
                                    LEFT JOIN AMS_Roles R ON R.RoleId = UR.RoleId
                                    LEFT JOIN AMS_ItemDetails I ON R.Category = I.ItemCode
                                                                    AND I.ItemsId = '5fed1313-7355-4cc4-a7ec-73211a361fa6'
                            WHERE   1 = 1");
            strSql.Append(" AND UserId = @UserId");
            SqlParam[] param = {
                                         new SqlParam("@UserId",UserId)};
            return DbHelper.GetDataListBySQL<AMS_Roles>(strSql, param);

        }
    }
}
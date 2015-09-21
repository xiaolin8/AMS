using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AMS.Entity;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 用户、帐户
    /// </summary>
    public class BASE_USERBLL : ServiceBase
    {
        #region Method
        /// <summary>
        /// 获取最大编号
        /// </summary>
        /// <returns></returns>
        public object GetMaxCode()
        {
            return DbUtils.GetMax("BASE_USER", "SortCode");
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <returns></returns>
        public int GetRecordCount(string KeyValue)
        {
            return DbUtils.RecordCount("BASE_USER", "UserId", KeyValue);
        }

        /// <summary>
        /// 获取记录总数（带条件）
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public int GetRecordCount(StringBuilder where, SqlParam[] param)
        {
            return DbUtils.RecordCount("BASE_USER", where, param);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Insert(BASE_USER entity)
        {
            //return dal.Insert(entity) >= 0 ? true : false;

            entity.sortcode = CommonHelper.GetInt(this.GetMaxCode());
            int IsOk = DbUtils.Insert(entity);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.AddTaskLog<BASE_USER>(entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;

        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public bool Update(BASE_USER entity)
        {
            //return dal.Update(entity) >= 0 ? true : false;
            #region 获取旧值
            var oldEntity = this.GetEntity(AMS_SysLogBLL.Instance.GetKeyFieldValue<BASE_USER>(entity).ToString());
            #endregion
            int IsOk = DbUtils.Update(entity, "UserId");
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.UpdateTaskLog<BASE_USER>(oldEntity, entity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
            }
            #endregion
            return IsOk >= 0 ? true : false;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="entity">实体类</param>
        /// <returns></returns>
        public int UpdateNotLog(BASE_USER entity)
        {
            return DbUtils.Update(entity, "UserId");
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
            int IsOk = DbUtils.Delete("BASE_USER", "UserId", KeyValue);
            #region 写日操作日志
            if (IsOk > 0)
            {
                AMS_SysLogBLL.Instance.DeleteTaskLog<BASE_USER>(oldEntity, RequestSession.GetSessionUser().UserId, RequestSession.GetSessionUser().UserName);
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
            int IsOk = DbUtils.BatchDelete("BASE_USER", "UserId", KeyValue);
            return IsOk >= 0 ? true : false;

        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public BASE_USER GetEntity(string KeyValue)
        {
            //return dal.GetEntity(KeyValue);
            return DbUtils.GetModelById<BASE_USER>("UserId", KeyValue);

        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <returns></returns>
        public IList GetList()
        {
            return GetListWhere(null, null);
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
            strSql.Append("SELECT * FROM BASE_USER WHERE 1=1");
            strSql.Append(where);
            strSql.Append(DataPermission.Instance.User);
            return DbHelper.GetDataListBySQL<BASE_USER>(strSql, param);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="Parm_Key_Value">搜索条件键值</param>
        /// <param name="orderField">排序字段</param>
        /// <param name="orderType">排序类型</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        public IList GetPageList(Hashtable Parm_Key_Value, string orderField, string orderType, int pageIndex, int pageSize, ref int count)
        {
            StringBuilder SqlWhere = new StringBuilder();
            List<SqlParam> ListParam = new List<SqlParam>();
            //显示有效、无效
            if (Parm_Key_Value["Enabled"] != null && !string.IsNullOrEmpty(Parm_Key_Value["Enabled"].ToString()))
            {
                SqlWhere.Append(" AND Enabled = @Enabled");
                ListParam.Add(new SqlParam("@Enabled", Parm_Key_Value["Enabled"].ToString()));
            }
            //编号
            if (Parm_Key_Value["Code"] != null && !string.IsNullOrEmpty(Parm_Key_Value["Code"].ToString()))
            {
                SqlWhere.Append(" AND Code like @Code");
                ListParam.Add(new SqlParam("@Code", '%' + Parm_Key_Value["Code"].ToString() + '%'));
            }
            //账户
            if (Parm_Key_Value["Account"] != null && !string.IsNullOrEmpty(Parm_Key_Value["Account"].ToString()))
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
            return GetPageListWhere(SqlWhere, ListParam.ToArray(), orderField, orderType, pageIndex, pageSize, ref  count);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT  U.UserId ,
                                    U.Code ,
                                    U.Account ,
                                    U.RealName ,
                                    U.Gender ,
                                    U.Email ,
                                    U.Mobile ,
                                    U.OICQ ,
                                    U.Enabled ,
                                    U.SortCode ,
                                    ORGA.FullName AS CompanyId ,
                                    ORG.FullName AS DepartmentId ,
                                    U.Spell ,
                                    U.LogOnCount ,
                                    U.LastVisit ,
                                    U.Description
                            FROM    BASE_USER U
                                    LEFT JOIN AMS_Organization ORG ON ORG.OrganizationId = U.DepartmentId
                                    LEFT JOIN AMS_Organization ORGA ON ORGA.OrganizationId = U.CompanyId");
            strSql.Append(" WHERE 1=1 " + DataPermission.Instance.User + ") A WHERE 1=1");
            strSql.Append(where);
            return DbHelper.GetPageList<BASE_USER>(strSql.ToString(), param, "CompanyId," + CommonHelper.ToOrderField("SortCode", orderField), orderType, pageIndex, pageSize, ref count);
        }
        #endregion
        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="Account">登录账户</param>
        /// <returns></returns>
        public BASE_USER GetEntityByAccount(string Account)
        {
            return DbUtils.GetModelById<BASE_USER>("Account", Account);
        }
       
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <param name="Account">账户</param>
        /// <param name="Password">密码</param>
        /// <param name="msg">消息</param>
        /// <returns></returns>
        public BASE_USER UserLogin(string Account, string Password, out string msg)
        {
            BASE_USER entity = GetEntityByAccount(Account);
            if (entity.account != null && entity.account != "")
            {
                string dbPassword = Md5Helper.MD5(DESEncrypt.Encrypt(Password, entity.secretkey), 32);
                if (dbPassword == entity.password)
                {
                    DateTime PreviousVisit = CommonHelper.GetDateTime(entity.lastvisit);
                    DateTime LastVisit = DateTime.Now;
                    int LogOnCount = CommonHelper.GetInt(entity.logoncount) + 1;
                    entity.previousvisit = PreviousVisit;
                    entity.lastvisit = LastVisit;
                    entity.logoncount = LogOnCount;
                    UpdateNotLog(entity);
                    msg = "succeed";
                    return entity;
                }
                else
                {
                    msg = "error";
                    return entity;
                }
            }
            msg = "-1";
            return null;
        }
        /// <summary>
        /// 根据机构查询用户列表
        /// </summary>
        /// <param name="Category">分类</param>
        /// <param name="OrganizationId">机构主键</param>
        /// <returns></returns>
        public IList GetDataTableByOrganizationId(string Category, string OrganizationId)
        {
            //return dal.GetDataTableByOrganizationId(Category, OrganizationId);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT UserId,Code,RealName,Gender FROM BASE_USER WHERE 1=1");
            List<SqlParam> ListParam = new List<SqlParam>();
            //机构查询
            if (!string.IsNullOrEmpty(Category))
            {
                if (Category == "公司")
                {
                    strSql.Append(" AND CompanyId = @OrganizationId");
                }
                else if (Category == "部门")
                {
                    strSql.Append(" AND DepartmentId = @OrganizationId");
                }
                else if (Category == "工作组")
                {
                    strSql.Append(" AND WorkgroupId = @OrganizationId");
                }
                ListParam.Add(new SqlParam("@OrganizationId", OrganizationId));
            }
            strSql.Append(DataPermission.Instance.User);
            strSql.Append(" ORDER BY CompanyId,DepartmentId,WorkgroupId,SortCode DESC");
            return DbHelper.GetDataListBySQL<BASE_USER>(strSql, ListParam.ToArray());

        }

        ///// <summary>
        ///// 根据机构查询用户列表
        ///// </summary>
        ///// <param name="Category">分类</param>
        ///// <param name="OrganizationId">机构主键</param>
        ///// <returns></returns>
        //public IList GetDataTableByOrganizationId(string Category, string OrganizationId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("SELECT UserId,Code,RealName,Gender FROM BASE_USER WHERE 1=1");
        //    List<SqlParam> ListParam = new List<SqlParam>();
        //    //机构查询
        //    if (!string.IsNullOrEmpty(Category))
        //    {
        //        if (Category == "公司")
        //        {
        //            strSql.Append(" AND CompanyId = @OrganizationId");
        //        }
        //        else if (Category == "部门")
        //        {
        //            strSql.Append(" AND DepartmentId = @OrganizationId");
        //        }
        //        else if (Category == "工作组")
        //        {
        //            strSql.Append(" AND WorkgroupId = @OrganizationId");
        //        }
        //        ListParam.Add(new SqlParam("@OrganizationId", OrganizationId));
        //    }
        //    strSql.Append(DataPermission.Instance.User);
        //    strSql.Append(" ORDER BY CompanyId,DepartmentId,WorkgroupId,SortCode DESC");
        //    return DbHelper.GetDataListBySQL<BASE_USER>(strSql, ListParam.ToArray());
        //}

        /// <summary>
        /// 得到一个对象IList
        /// </summary>
        /// <param name="KeyValue">主键</param>
        /// <returns></returns>
        public IList GetIListById(string KeyValue)
        {
            //return dal.GetIListById(KeyValue);

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    U.UserId ,
                                                U.Code ,
                                                U.Account ,
                                                U.RealName ,
                                                U.Gender ,
                                                U.Email ,
                                                U.Mobile ,
                                                U.OICQ ,
                                                U.Birthday,
                                                U.Telephone,
                                                I.ItemName AS DutyId ,
                                                IA.ItemName AS TitleId ,
                                                U.Enabled ,
                                                U.SortCode ,
                                                ORG_C.FullName AS CompanyId,
                                                ORG_D.FullName AS DepartmentId ,
                                                ORG_W.FullName AS WorkgroupId ,
                                                U.Spell ,
                                                U.LogOnCount ,
                                                U.LastVisit ,
                                                U.Description,
                                                R.FullName AS RoleId
                                      FROM      BASE_USER U
                                                LEFT JOIN AMS_ItemDetails I ON U.DutyId = I.ItemCode
                                                                                AND I.ItemsId = '137a2d97-d1d9-4752-9c5e-239097e2ed68'
                                                LEFT JOIN AMS_ItemDetails IA ON U.TitleId = IA.ItemCode
                                                                                 AND IA.ItemsId = '2acba9e8-5fa7-4b6f-8ebd-56e753dd059a'
                                                LEFT JOIN AMS_Roles R ON R.RoleId = U.RoleId
                                                LEFT JOIN AMS_Organization ORG_C ON ORG_C.OrganizationId = U.CompanyId
                                                LEFT JOIN AMS_Organization ORG_D ON ORG_D.OrganizationId = U.DepartmentId
                                                 LEFT JOIN AMS_Organization ORG_W ON ORG_W.OrganizationId = U.WorkgroupId
                                    ) A
                            WHERE   1 = 1");
            strSql.Append(" AND UserId = @UserId");
            SqlParam[] param = {
                                         new SqlParam("@UserId",KeyValue)};
            return DbHelper.GetDataListBySQL<BASE_USER>(strSql, param);

        }
        /// <summary>
        /// 自动补全(显示20行)
        /// </summary>
        /// <param name="Search">条件:编号，名称</param>
        /// <returns></returns>
        public IList AutoComplete(string Search)
        {
            StringBuilder strSql = new StringBuilder();
            if (!string.IsNullOrEmpty(Search))
            {
                strSql.Append("AND ROWNUM<=10 AND (RealName like @RealName OR Spell like @Spell");
                strSql.Append(" OR Code like @Code)");
                SqlParam[] para = {
                                        new SqlParam("@RealName", '%' +Search.Trim() + '%'),
                                        new SqlParam("@Spell", '%' +Search.Trim() + '%'),
                                        new SqlParam("@Code", '%' + Search.Trim() + '%')};
                return AutoComplete(strSql, para);
            }
            else
            {
                return AutoComplete(strSql, null);
            }
        }

        /// <summary>
        /// 自动补全(显示20行)
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="param">参数化</param>
        /// <returns></returns>
        public IList AutoComplete(StringBuilder where, SqlParam[] param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  UserId,RealName,Code FROM BASE_USER WHERE 1=1");
            strSql.Append(where);
            strSql.Append(DataPermission.Instance.User);
            strSql.Append(" ORDER BY CreateDate");
            return DbHelper.GetDataListBySQL<BASE_USER>(strSql, param);
        }
    }
}
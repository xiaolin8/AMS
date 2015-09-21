﻿using System.Collections;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 数据权限存储公共类
    /// </summary>
    public class DataPermission
    {
        public static DataPermission Instance
        {
            get
            {
                return new DataPermission();
            }
        }
        public DataPermission()
        {
            string UserId = RequestSession.GetSessionUser().UserId;
            Hashtable ht = (Hashtable)StorePermission.Instance.GetDataPermission(UserId);
            if (ht["Organization"] != null)
            {
                this.Organization = " AND OrganizationId IN(" + ht["Organization"].ToString() + ")";
                this.User = " AND DepartmentId IN(" + ht["Organization"].ToString() + ") AND WorkgroupId IN(" + ht["Organization"].ToString() + ")";
                this.Employee = " AND DepartmentId IN(" + ht["Organization"].ToString() + ") AND WorkgroupId IN(" + ht["Organization"].ToString() + ")";
            }
        }
        /// <summary>
        /// 组织机构
        /// </summary>
        public string Organization { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// 员工
        /// </summary>
        public string Employee { get; set; }
    }
}

using System.Collections;
using DotNet.Utilities;

namespace AMS.BLL
{
    /// <summary>
    /// 使用服务器缓存 - 存储权限
    /// </summary>
    public class StorePermission
    {
        //private readonly AMS_PermissionDAL dal = new AMS_PermissionDAL();

        private readonly AMS_PermissionBLL PermissionBLL = new AMS_PermissionBLL();
        

        private static StorePermission item;
        public static StorePermission Instance
        {
            get
            {
                if (item == null)
                {
                    item = new StorePermission();
                }
                return item;
            }
        }

        /// <summary>
        /// 将【模块权限】保存在服务器缓存中，提高性能。这样就不用每次去数据库读
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public void SetModulePermission(string UserId, IList list)
        {
            CacheHelper.Insert("Module" + UserId, list);
        }
        public void SetSubModulePermission(string UserId, string parentId, IList list)
        {
            CacheHelper.Insert("SubModule" + UserId + parentId, list);
        }
        /// <summary>
        /// 将Moddule权限数据放入缓存 by yhb 2014-10-16
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="list"></param>
        public void SetWebBusinessModulePermission(string userName, IList list)
        {
            CacheHelper.Insert("Module" + userName, list);
        }
        /// <summary>
        /// 将【操作按钮权限】保存在服务器缓存中，提高性能。这样就不用每次去数据库读
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public void SetButtonPermission(string UserId, IList list)
        {
            CacheHelper.Insert("Button" + UserId, list);
        }
        /// <summary>
        /// 将【数据权限】保存在服务器缓存中，提高性能。这样就不用每次去数据库读
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <param name="ht"></param>
        /// <returns></returns>
        public void SetDataPermission(string UserId, Hashtable ht)
        {
            CacheHelper.Insert("Data" + UserId, ht);
        }

        /// <summary>
        /// 获取【模块权限】在服务器缓存中，提高性能。这样就不用每次去数据库读
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public object GetModulePermission(string UserId)
        {
            string strKey = "Module" + UserId;
            if (!CacheHelper.IsExist(strKey))
            {
                if (UserId == ConfigHelper.GetValue("CurrentUserName")) //判断超级管理员
                {
                    this.SetModulePermission(UserId, AMS_PermissionBLL.Instance.GetModulePermission());
                }
                else
                {
                    this.SetModulePermission(UserId, AMS_PermissionBLL.Instance.GetModulePermission(UserId));
                }
            }
            return CacheHelper.GetCache(strKey);
        }


        /// <summary>
        /// 获取【模块权限】在服务器缓存中，提高性能。这样就不用每次去数据库读
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public object GetModulePermission(string UserId, string Lan, string strWhere)
        {
            string strKey = "Module" + UserId;
            if (!CacheHelper.IsExist(strKey))
            {
                if (UserId == ConfigHelper.GetValue("CurrentUserName")) //判断超级管理员
                {
                    this.SetModulePermission(UserId, AMS_PermissionBLL.Instance.GetModulePermissionByLang(Lan, strWhere));
                }
                else
                {
                    this.SetModulePermission(UserId, AMS_PermissionBLL.Instance.GetModulePermission(UserId, Lan, strWhere));
                }
            }
            return CacheHelper.GetCache(strKey);
        }
        public object GetSubModulePermission(string UserId, string Lan, string parentId)
        {
            string strKey = "SubModule" + UserId + parentId;
            if (!CacheHelper.IsExist(strKey))
            {
                if (UserId == ConfigHelper.GetValue("CurrentUserName")) //判断超级管理员
                {
                    this.SetSubModulePermission(UserId, parentId, AMS_PermissionBLL.Instance.GetSubModulePermissionByLang(Lan, parentId));
                }
                else
                {
                    this.SetSubModulePermission(UserId, parentId, AMS_PermissionBLL.Instance.GetSubModulePermission(UserId, Lan, parentId));
                }
            }
            return CacheHelper.GetCache(strKey);
        }
        /// <summary>
        /// 根据用户登录名，取消业务菜单 by yhb 2014-10-16
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public object GetWebBusinessModulePermission(string userName)
        {
            string strKey = "Module" + userName;
            if (!CacheHelper.IsExist(strKey))
            {
                this.SetWebBusinessModulePermission(userName, AMS_PermissionBLL.Instance.GetWebBusinessModulePermission(userName));
            }
            return CacheHelper.GetCache(strKey);
        }
        /// <summary>
        /// 获取【操作按钮权限】在服务器缓存中，提高性能。这样就不用每次去数据库读
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public object GetButtonPermission(string UserId)
        {
            string strKey = "Button" + UserId;
            if (!CacheHelper.IsExist(strKey))
            {
                if (UserId == ConfigHelper.GetValue("CurrentUserName")) //判断超级管理员
                {
                    this.SetButtonPermission(UserId, AMS_PermissionBLL.Instance.GetButtonPermission());
                }
                else
                {
                    this.SetButtonPermission(UserId, AMS_PermissionBLL.Instance.GetButtonPermission(UserId));
                }
            }
            return CacheHelper.GetCache(strKey);
        }
        /// <summary>
        /// 获取【数据权限】在服务器缓存中，提高性能。这样就不用每次去数据库读
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public object GetDataPermission(string UserId)
        {
            string strKey = "Data" + UserId;
            if (!CacheHelper.IsExist(strKey))
            {
                if (UserId == ConfigHelper.GetValue("CurrentUserName")) //判断超级管理员
                {
                    this.SetDataPermission(UserId, AMS_PermissionBLL.Instance.GetDataPermission());
                }
                else
                {
                    this.SetDataPermission(UserId, AMS_PermissionBLL.Instance.GetDataPermission(UserId));
                }
            }
            return CacheHelper.GetCache(strKey);
        }
    }
}

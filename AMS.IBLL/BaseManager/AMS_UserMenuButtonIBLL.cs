using System.Data;

namespace AMS.IBLL
{
    /// <summary>
    /// 用户菜单按钮关系 - 接口
    /// </summary>
    public interface AMS_UserMenuButtonIBLL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="UserId">用户主键</param>
        /// <returns></returns>
        DataTable GetList(string UserId);
        /// <summary>
        /// 分配用户按钮权限
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="UserId">用户主键</param>
        /// <param name="CreateUserId">操作用户主键</param>
        /// <param name="CreateUserName">操作用户</param>
        /// <returns></returns>
        bool AddButtonPermission(string[] KeyValue, string UserId, string CreateUserId, string CreateUserName);
    }
}
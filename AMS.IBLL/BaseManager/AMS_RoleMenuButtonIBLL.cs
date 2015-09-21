using System.Data;

namespace AMS.IBLL
{
    /// <summary>
    /// 系统角色菜单按钮关系 - 接口
    /// </summary>
    public interface AMS_RoleMenuButtonIBLL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="RoleId">角色主键</param>
        /// <returns></returns>
        DataTable GetList(string RoleId);
        /// <summary>
        /// 分配角色按钮权限
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="RoleId">角色主键</param>
        /// <param name="CreateUserId">操作用户主键</param>
        /// <param name="CreateUserName">操作用户</param>
        /// <returns></returns>
        bool AddButtonPermission(string[] KeyValue, string RoleId, string CreateUserId, string CreateUserName);
    }
}
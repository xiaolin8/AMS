using System.Data;

namespace AMS.IBLL
{
    /// <summary>
    /// 菜单导航操作按钮关系表 - 接口
    /// </summary>
    public interface AMS_SysMenuButtonIBLL
    {
        /// <summary>
        /// 获得数据列表(带条件)
        /// </summary>
        /// <param name="MenuId">模块菜单主键</param>
        /// <returns></returns>
        DataTable GetListWhere(string MenuId);

        /// <summary>
        /// 设批量添加，菜单导航操作按钮关系
        /// </summary>
        /// <param name="KeyValue">主键值</param>
        /// <param name="MenuId">模块菜单主键</param>
        /// <param name="CreateUserId">操作用户主键</param>
        /// <param name="CreateUserName">操作用户</param>
        /// <returns></returns>
        bool BatchAddMenuButton(string[] KeyValue, string MenuId, string CreateUserId, string CreateUserName);
    }
}